﻿using System;
using System.Threading.Tasks;
using JiraHelpBot2.Bots;
using JiraHelpBot2.JiraClient;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JiraHelpBot2.Tests
{
    public class JiraHelpBotTests
    {
        [Fact]
        public async Task OnTurnAsync_SimpleMentionWithOneTicketIdThatExistsInJira_ExpectCardWithTicketDetails()
        {
            //Arrange
            var reviewBot = MakeJiraHelpBot();
            var messageTurnContext = MSTeamsTurnContext.CreateUserToBotMessage("@JiraHelp SKYE-1234");

            //Act
            await reviewBot.OnTurnAsync(messageTurnContext);

            //Assert
            var activity = messageTurnContext.Responses.Peek();
            Assert.Single(activity.Attachments);
            Assert.IsType<ThumbnailCard>(activity.Attachments[0].Content);
            var thumbnailCard = (ThumbnailCard)activity.Attachments[0].Content;
            Assert.Equal("SKYE-1234: Cool feature summary", thumbnailCard.Subtitle);
            Assert.Equal(
                // language=html
                @"<strong>Type:</strong> Backlog item &nbsp;<strong>Status:</strong> Review &nbsp;<strong>Priority:</strong> 2 = Major</br>
<strong>Assignee:</strong> John &nbsp; <strong>Fix versions:</strong> v1.1.0 v2.0.0 </br><a href=""https://acme.atlassian.net/browse/SKYE-1234"">Open</a>",
                thumbnailCard.Text);
        }

        [Fact]
        public async Task OnTurnAsync_SimpleMentionWithOneTicketIdButJiraServiceThrows_ExpectCardWithMessageOfErrorThrown()
        {
            //Arrange
            var reviewBot = MakeJiraHelpBotWithFailingJiraIssueService();
            var messageTurnContext = MSTeamsTurnContext.CreateUserToBotMessage("@JiraHelp SKYE-1234");

            //Act
            await reviewBot.OnTurnAsync(messageTurnContext);

            //Assert
            var activity = messageTurnContext.Responses.Peek();
            Assert.Null(activity.Text);
            Assert.Single(activity.Attachments);
            Assert.IsType<ThumbnailCard>(activity.Attachments[0].Content);
            var thumbnailCard = (ThumbnailCard)activity.Attachments[0].Content;
            Assert.Equal("SKYE-1234", thumbnailCard.Subtitle);
            Assert.Equal("Issue not found.", thumbnailCard.Text);
        }

        private static ILogger<JiraHelpBot> MockLogger()
        {
            return new LoggerFactory().CreateLogger<JiraHelpBot>();
        }

        private static IConfiguration MockConfiguration()
        {
            // TODO see if it's possible to use concrete config classes instead of mocks
            var configMock = new Mock<IConfiguration>();
            var jiraProjectIdsConfigSectionMock = new Mock<IConfigurationSection>();
            jiraProjectIdsConfigSectionMock.Setup(section => section.Value).Returns("SKYE");
            configMock.Setup(config => config.GetSection("JiraHelpJiraProjectIds")).Returns(jiraProjectIdsConfigSectionMock.Object);
            var jiraEndpointConfigSectionMock = new Mock<IConfigurationSection>();
            jiraEndpointConfigSectionMock.Setup(section => section.Value).Returns("https://acme.atlassian.net");
            configMock.Setup(config => config.GetSection("JiraHelpJiraEndpoint")).Returns(jiraEndpointConfigSectionMock.Object);

            return configMock.Object;
        }

        private static JiraHelpBot MakeJiraHelpBot()
        {
            var jiraClientMock = new Mock<IJiraClient>();
            var issue = new Issue
            {
                id = "123465",
                key = "SKYE-1234",
                fields = new Fields
                {
                    summary = "Cool feature summary",
                    issuetype = new Issuetype { name = "Backlog item" },
                    status = new Status { id = "2", name = "Review" },
                    priority = new Priority { name = "2 = Major" },
                    assignee = new Assignee { displayName = "John" },
                    fixVersions = new[] { new Fixversion { name = "v1.1.0" }, new Fixversion { name = "v2.0.0" } }
                }
            };
            jiraClientMock.Setup(c => c.GetTicket(It.Is<string>(s => s == "SKYE-1234"))).ReturnsAsync(() => issue);

            return new JiraHelpBot(MockConfiguration(), MockLogger(), jiraClientMock.Object);
        }

        private static JiraHelpBot MakeJiraHelpBotWithFailingJiraIssueService()
        {
            var jiraClientMock = new Mock<IJiraClient>();
            jiraClientMock.Setup(c => c.GetTicket(It.IsAny<string>())).Throws(() => new Exception("Issue not found."));

            return new JiraHelpBot(MockConfiguration(), MockLogger(), jiraClientMock.Object);
        }
    }
}