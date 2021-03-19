#region using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlassian.Jira;
using Atlassian.Jira.Remote;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

#endregion

namespace JiraHelpBot.Tests
{
    [TestFixture]
    public class ReviewBotTests
    {
        [Test]
        public async Task OnTurnAsync_SimpleMentionWithOneTicketIdThatExistsInJira_ExpectCardWithTicketDetails()
        {
            //Arrange
            var reviewBot = MakeJiraHelpBot();
            var messageTurnContext = MSTeamsTurnContext.CreateUserToBotMessage("@JiraHelp SKYE-1234");

            //Act
            await reviewBot.OnTurnAsync(messageTurnContext);

            //Assert
            var activity = messageTurnContext.Responses.Peek();
            Assert.That(activity.Text, Is.Empty);
            Assert.That(activity.Attachments, Has.Count.EqualTo(1));
            Assert.That(activity.Attachments[0].Content, Is.TypeOf<ThumbnailCard>());
            var thumbnailCard = activity.Attachments[0].Content as ThumbnailCard;
            Assert.That(thumbnailCard.Subtitle, Is.EqualTo("SKYE-1234: Cool feature summary"));
            Assert.That(thumbnailCard.Text, Is.EqualTo(@"<strong>Type:</strong> Backlog item &nbsp;<strong>Status:</strong> Review &nbsp;<strong>Priority:</strong> 2 = Major</br>
<strong>Assignee:</strong> John &nbsp; <strong>Fix versions:</strong> v1.0.0 </br><a href=""https://acme.atlassian.net/browse/SKYE-1234"">Open</a>"));
        }

        [Test]
        public async Task OnTurnAsync_SimpleMentionWithOneTicketIdButJiraServiceThrows_ExpectCardWithMessageOfErrorThrown()
        {
            //Arrange
            var reviewBot = MakeJiraHelpBotWithFailingJiraIssueService();
            var messageTurnContext = MSTeamsTurnContext.CreateUserToBotMessage("@JiraHelp SKYE-1234");

            //Act
            await reviewBot.OnTurnAsync(messageTurnContext);

            //Assert
            var activity = messageTurnContext.Responses.Peek();
            Assert.That(activity.Text, Is.Empty);
            Assert.That(activity.Attachments, Has.Count.EqualTo(1));
            Assert.That(activity.Attachments[0].Content, Is.TypeOf<ThumbnailCard>());
            var thumbnailCard = activity.Attachments[0].Content as ThumbnailCard;
            Assert.That(thumbnailCard.Subtitle, Is.EqualTo("SKYE-1234"));
            Assert.That(thumbnailCard.Text, Is.EqualTo(@"Issue not found."));
        }

        private static ILoggerFactory MockLogger()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            loggerFactoryMock.Setup(s => s.CreateLogger(It.IsAny<string>())).Returns(new Mock<ILogger>().Object);

            return loggerFactoryMock.Object;
        }

        private static IConfiguration MockConfiguration()
        {
            var configMock = new Mock<IConfiguration>();
            var configSectionMock = new Mock<IConfigurationSection>();
            configSectionMock.Setup(section => section.Value).Returns("SKYE");
            configMock.Setup(config => config.GetSection("JiraHelpJiraProjectIds")).Returns(configSectionMock.Object);

            return configMock.Object;
        }

        private static JiraHelpBot MakeJiraHelpBot()
        {
            var jiraRestClient = Jira.CreateRestClient("https://acme.atlassian.net/", "username", "password");
            var remoteIssue = new RemoteIssue
            {
                key = "SKYE-1234",
                type = new RemoteIssueType { name = "Backlog item" },
                status = new RemoteStatus { id = "2", name = "Review" },
                priority = new RemotePriority { name = "2 = Major" },
                assignee = "John",
                fixVersions = new[] { new RemoteVersion {id = "v1.0.0", name = "v1.0.0"}},
                summary = "Cool feature summary"
            };
            var issue = new Issue(jiraRestClient, remoteIssue);
            var resultStub = new PagedQueryResultStub<Issue>(new List<Issue>{issue}, 0, 20, 1);
            var issueServiceMock = new Mock<IIssueService>();
            issueServiceMock.Setup(s => s.GetIssuesFromJqlAsync(It.IsAny<IssueSearchOptions>(), CancellationToken.None)).ReturnsAsync(() => resultStub);

            return new JiraHelpBot(MockLogger(), issueServiceMock.Object, MockConfiguration());
        }

        private static JiraHelpBot MakeJiraHelpBotWithFailingJiraIssueService()
        {
            var issueServiceMock = new Mock<IIssueService>();
            issueServiceMock.Setup(s => s.GetIssuesFromJqlAsync(It.IsAny<IssueSearchOptions>(), CancellationToken.None)).Throws(new Exception("Issue not found."));

            return new JiraHelpBot(MockLogger(), issueServiceMock.Object, MockConfiguration());
        }

        private class PagedQueryResultStub<T> : IPagedQueryResult<T>
        {
            private readonly IEnumerable<T> _enumerable;

            public PagedQueryResultStub(IEnumerable<T> enumerable,
                                        int startAt,
                                        int itemsPerPage,
                                        int totalItems)
            {
                _enumerable = enumerable;
                StartAt = startAt;
                ItemsPerPage = itemsPerPage;
                TotalItems = totalItems;
            }

            public int StartAt { get; }

            public int ItemsPerPage { get; }

            public int TotalItems { get; }

            public IEnumerator<T> GetEnumerator() => _enumerable.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _enumerable.GetEnumerator();

        }
    }
}