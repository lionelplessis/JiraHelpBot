# JiraHelp bot

## What is the bot for?

The main goal of this bot is to display cards with Jira ticket information in Microsoft Teams whenever a Jira ticket id is written in Team's channel and the bot is mentionned.


![Card example](https://github.com/lionelplessis/JiraHelpBot/blob/master/Docs/JiraHelp_card-example.png)

## Credits

All credits to [Gianluigi Conti](https://github.com/glconti) for the original idea and [implementation](https://github.com/glconti/jira-bot-teams) which I just adapted to a more recent version of the Bot framework and to [Martin Škuta](https://github.com/martinskuta) for his excellent [ReviewBot](https://github.com/martinskuta/ReviewBot) which I used as example of bot on framework v4.

## Installation

### MS Teams

At the moment the bot is supported only in **MS Teams**.
## Techno used

* [.NET 8.0](https://github.com/dotnet/core)
* [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
* [Bot framework v4.21](https://github.com/Microsoft/botbuilder-dotnet)
* [xUnit testing framework](https://github.com/xunit/xunit)
* [Moq mocking framework](https://github.com/devlooped/moq)

* The bot is hosted on [Azure App Service](https://azure.microsoft.com/en-us/services/app-service/)
