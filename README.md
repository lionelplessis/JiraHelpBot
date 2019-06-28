# JiraHelp bot

## What is the bot for?

The main goal of this bot is to display cards with Jira ticket information in Microsoft Teams whenever a Jira ticket id is written in Team's channel and the bot is mentionned.


![Card example](https://github.com/lionelplessis/JiraHelpBot/blob/master/Docs/JiraHelp_card-example.png)

## Credits

All credits to [Gianluigi Conti](https://github.com/glconti) for the original idea and [implementation](https://github.com/glconti/jira-bot-teams) which I just adapted to a more recent version of the Bot framework and to [Martin Škuta](https://github.com/martinskuta) for his excellent [ReviewBot](https://github.com/martinskuta/ReviewBot) which as used as example of bot on framework v4.

## Installation

### MS Teams

At the moment the bot is supported only in **MS Teams**.
## Techno used

* [.NET Core 2.2](https://github.com/dotnet/core)
* [ASP.NET Core 2.2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2)
* [Bot framework V4](https://dev.botframework.com/)
* [Atlassian.Sdk](https://bitbucket.org/farmas/atlassian.net-sdk)
* [NUnit testing framework v3](https://nunit.org/)
* [Moq4 mocking framework](https://github.com/Moq/moq4/wiki/Quickstart)

* The bot is hosted in [Azure App Service](https://azure.microsoft.com/en-us/services/app-service/)
