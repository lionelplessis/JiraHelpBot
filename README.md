# JiraHelp bot

## What is the bot for?

The main goal of this bot is to display cards with Jira ticket information in Microsoft Teams whenever a Jira ticket id is written in Team's channel and the bot is mentionned.

![Card example](https://raw.githubusercontent.com/lionelplessis/JiraHelpBot/master/Docs/JiraHelp_card-example.png)

## Credits

All credits to [Gianluigi Conti](https://github.com/glconti) for the original idea and [implementation](https://github.com/glconti/jira-bot-teams) which I just adapted to a more recent version of the Bot framework and to [Martin Škuta](https://github.com/martinskuta) for his excellent [ReviewBot](https://github.com/martinskuta/ReviewBot) which as used as example of bot on framework v4.

## Installation

### MS Teams

At the moment the bot is supported only in **MS Teams**. You can either build and deploy the bot yourself or you can start using it for free by using the steps below. Consider the free version as a way to try it, it might not run forever.

1. Download the MS Teams install bundle: [ReviewBotMsTeamsInstallBundle.zip](https://raw.githubusercontent.com/martinskuta/ReviewBot/master/Bundle/ReviewBotMsTeamsInstallBundle.zip "MS Teams installation bundle")
2. Open MS Teams and on the left locate team where you want the bot to be available and click the three dots menu and select **Manage team**
3. On the manage teams page select **Apps** tab.
4. At the bottom you should see a link '**Upload a custom app**'. Click on it and select the bundle you downloaded in first step.
5. Now you can start using the bot by mentioning it in a channel (**@Review help**). Remember that the bot works per channel, so all the review data are stored per channel too, so don't be surprised if it doesn't remember things across channels.

## Techno used

* [.NET Core 2.2](https://github.com/dotnet/core)
* [Bot framework V4](https://dev.botframework.com/)
* [ASP.NET Core 2.2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)
* [NUnit testing framework v3](https://nunit.org/)
* [Moq4 mocking framework](https://github.com/Moq/moq4/wiki/Quickstart)

* The bot is hosted in [Azure App Service](https://azure.microsoft.com/en-us/services/app-service/)
