using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using JiraHelpBot2.JiraClient;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JiraHelpBot2.Bots;

public class JiraHelpBot : ActivityHandler
{
    private const string JiraCardBodyTemplate = """
                                                <strong>Type:</strong> {0} &nbsp;<strong>Status:</strong> {1} &nbsp;<strong>Priority:</strong> {2}</br>
                                                <strong>Assignee:</strong> {3} &nbsp; <strong>Fix versions:</strong> {4} </br><a href="{5}">Open</a>
                                                """;
    private readonly string _jiraAddress;
    private readonly IJiraClient _jiraClient;

    private readonly ILogger<JiraHelpBot> _logger;
    private readonly Regex _regex;

    public JiraHelpBot(IConfiguration configuration, ILogger<JiraHelpBot> logger, IJiraClient jiraClient)
    {
        _logger = logger;
        _jiraClient = jiraClient;

        _jiraAddress = configuration.GetSection("JiraHelpJiraEndpoint")?.Value;

        var jiraProjectIds = configuration.GetSection("JiraHelpJiraProjectIds")?.Value.Split(',').Select(id => id.Trim());
        _regex = new Regex($@"(?<ticket>({string.Join("|", jiraProjectIds)})-\d+)", RegexOptions.Compiled);
    }

    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Handling message: {Message}", turnContext.Activity.Text);

        var ticketIds = ExtractTicketIds(turnContext.Activity.Text).Distinct().ToList();
        if (!ticketIds.Any())
            return;

        var searches = ticketIds
                       .Select(
                           ticketId => new
                           {
                               Ticket = ticketId,
                               Task = Task.Run(
                                   async () =>
                                   {
                                       try
                                       {
                                           var ticket = await _jiraClient.GetTicket(ticketId);

                                           return GetIssueResult.Success(ticket);
                                       }
                                       catch (Exception e)
                                       {
                                           _logger.LogInformation(e, $"Getting Jira ticket '{ticketId}' failed.");
                                           return GetIssueResult.Failure(e.Message);
                                       }
                                   },
                                   cancellationToken)
                           }).ToArray();

        await Task.WhenAll(searches.Select(t => t.Task));

        var thumbnailCards = searches.Select(
            search =>
            {
                var getIssueResult = search.Task.Result;

                if (!getIssueResult.IsSuccessful)
                    return new ThumbnailCard(subtitle: search.Ticket, text: getIssueResult.ErrorMessage).ToAttachment();

                var issue = getIssueResult.Issue;
                var issueNumber = issue.key;
                var issueUrl = _jiraAddress + "/browse/" + issueNumber;

                var thumbnailCard = new ThumbnailCard(
                    subtitle: issueNumber + ": " + issue.fields.summary,
                    text: string.Format(
                        JiraCardBodyTemplate,
                        issue.fields.issuetype?.name,
                        issue.fields.status?.name,
                        issue.fields.priority?.name,
                        issue.fields.assignee?.displayName,
                        string.Join(" ", issue.fields.fixVersions?.Select(fv => fv.name) ?? Enumerable.Empty<string>()),
                        issueUrl));

                return thumbnailCard.ToAttachment();
            });

        await turnContext.SendActivityAsync(MessageFactory.Attachment(thumbnailCards), cancellationToken);
    }

    private IEnumerable<string> ExtractTicketIds(string message)
    {
        var matchCollection = _regex.Matches(message);
        if (matchCollection.Count == 0) yield break;

        foreach (Match m in matchCollection)
            yield return m.Groups["ticket"].Value;
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

        public static GetIssueResult Success(Issue issue) => new(issue);

        public static GetIssueResult Failure(string errorMessage) => new(errorMessage);
    }
}