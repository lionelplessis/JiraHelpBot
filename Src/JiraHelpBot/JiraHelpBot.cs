#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Atlassian.Jira;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace JiraHelpBot
{
    public class JiraHelpBot : IBot
    {
        private const string JiraCardTitle = @"<strong>Type:</strong> {0} &nbsp;<strong>Status:</strong> {1} &nbsp;<strong>Priority:</strong> {2}</br>
<strong>Assignee:</strong> {3} &nbsp; <strong>Fix versions:</strong> {4} </br><a href=""{5}"">Open</a>";

        private readonly IIssueService _jiraIssueService;
        private readonly ILogger<JiraHelpBot> _logger;
        private readonly Regex _regex;

        public JiraHelpBot(ILoggerFactory loggerFactory, IIssueService jiraIssueService, IConfiguration config)
        {
            _jiraIssueService = jiraIssueService;
            _logger = loggerFactory.CreateLogger<JiraHelpBot>();
            _logger.LogTrace("JiraHelp bot started.");

            var jiraProjectIds = config.GetSection("JiraHelpJiraProjectIds")?.Value.Split(',').Select(id => id.Trim());
            _regex = new Regex($@"(?<ticket>({string.Join("|", jiraProjectIds)})-\d+)", RegexOptions.Compiled);
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                await HandleMessage(turnContext);
                return;
            }

            _logger.LogTrace($"Activity not handled: {turnContext.Activity.Type}");
        }

        private IEnumerable<string> ExtractTicketIds(string message)
        {
            var matchCollection = _regex.Matches(message);
            if (matchCollection.Count == 0) yield break;

            foreach (Match o in matchCollection)
                yield return o.Groups["ticket"].Value;
        }

        private async Task HandleMessage(ITurnContext turnContext)
        {
            _logger.LogTrace($"Handling message: {turnContext.Activity.Text}");

            var ticketIds = ExtractTicketIds(turnContext.Activity.Text).Distinct().ToList();
            if (!ticketIds.Any())
                return;

            var searches = ticketIds
                           .Select(
                               t => new
                               {
                                   Ticket = t,
                                   Task = Task.Run(
                                       async () =>
                                       {
                                           try
                                           {
                                               var results = await _jiraIssueService.GetIssuesFromJqlAsync(
                                                   new IssueSearchOptions($"key = {t}")
                                                   {
                                                       AdditionalFields = new List<string>() { "assignee", "status", "issuetype", "summary", "priority", "fixVersion" }, FetchBasicFields = false
                                                   });

                                               //var issue = await _jiraIssueService.GetIssueAsync(t);
                                               return GetIssueResult.Success(results.First());
                                           }
                                           catch (Exception e)
                                           {
                                               _logger.LogInformation(e, $"Getting Jira ticket '{t}' failed.");
                                               return GetIssueResult.Failure(e.Message);
                                           }
                                       })
                               }).ToArray();

            await Task.WhenAll(searches.Select(t => t.Task));

            var thumbnailCards = searches.Select(
                search =>
                {
                    var getIssueResult = search.Task.Result;

                    if (!getIssueResult.IsSuccessful)
                        return new ThumbnailCard(subtitle: search.Ticket, text: getIssueResult.ErrorMessage).ToAttachment();

                    var issue = getIssueResult.Issue;
                    var issueNumber = issue.Key.ToString();
                    var issueUrl = issue.Jira.Url + "browse/" + issueNumber;

                    var thumbnailCard = new ThumbnailCard(
                        subtitle: issueNumber + ": " + issue.Summary,
                        text: string.Format(
                            JiraCardTitle,
                            issue.Type.Name,
                            issue.Status.Name,
                            issue.Priority.Name,
                            issue.Assignee,
                            string.Join(" ", issue.FixVersions),
                            issueUrl));

                    return thumbnailCard.ToAttachment();
                });

            var reply = turnContext.Activity.CreateReply();

            reply.AttachmentLayout = AttachmentLayoutTypes.List;
            reply.Attachments = thumbnailCards.ToList();

            //reply.ChannelData = JObject.FromObject(new TeamsChannelData(notification: new NotificationInfo(false)));

            _logger.LogTrace($"Sending response attachment(s) count: {reply.Attachments.Count}");
            await turnContext.SendActivityAsync(reply);
        }

        private class GetIssueResult
        {
            private GetIssueResult(Issue issue)
            {
                IsSuccessful = true;
                Issue = issue;
            }

            private GetIssueResult(string errorMessage)
            {
                IsSuccessful = false;
                ErrorMessage = errorMessage;
            }

            public bool IsSuccessful { get; }

            public string ErrorMessage { get; }

            public Issue Issue { get; }

            public static GetIssueResult Success(Issue issue) => new GetIssueResult(issue);

            public static GetIssueResult Failure(string errorMessage) => new GetIssueResult(errorMessage);
        }
    }
}