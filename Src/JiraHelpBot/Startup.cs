#region using

using System;
using System.Linq;
using Atlassian.Jira;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace JiraHelpBot
{
    /// <summary>
    ///   The Startup class configures services and the request pipeline.
    /// </summary>
    public class Startup
    {
        private ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json", true, true)
                          .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        ///   Gets the configuration that represents a set of key/value application configuration properties.
        /// </summary>
        /// <value>
        ///   The <see cref="IConfiguration" /> that represents a set of key/value application configuration properties.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///   This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">
        ///   The <see cref="IServiceCollection" /> specifies the contract for a collection of service
        ///   descriptors.
        /// </param>
        /// <seealso cref="IStatePropertyAccessor{T}" />
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection" />
        /// <seealso
        ///   cref="https://docs.microsoft.com/en-us/azure/bot-service/bot-service-manage-channels?view=azure-bot-service-4.0" />
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);

            // Jira service
            var jiraUserName = Configuration.GetSection("JiraHelpJiraAccountId")?.Value;
            var jiraApiToken = Configuration.GetSection("jiraHelpJiraAccountPassword")?.Value;
            var jiraAddress = Configuration.GetSection("JiraHelpJiraEndpoint")?.Value;
            services.AddTransient(provider => Jira.CreateRestClient(jiraAddress, jiraUserName, jiraApiToken, new JiraRestClientSettings()).Issues);

            // Bot service
            var botChannelsRegistrationAppId = Configuration.GetSection("JiraHelpBotChannelsRegistrationAppId")?.Value;
            var botChannelsRegistrationAppPassword = Configuration.GetSection("JiraHelpBotChannelsRegistrationAppPassword")?.Value;
            services.AddBot<JiraHelpBot>(
                options =>
                {
                    options.CredentialProvider = new SimpleCredentialProvider(botChannelsRegistrationAppId, botChannelsRegistrationAppPassword);

                    // Creates a logger for the application to use.
                    ILogger logger = _loggerFactory.CreateLogger<JiraHelpBot>();

                    // Catches any errors that occur during a conversation turn and logs them.
                    options.OnTurnError = async (context, exception) =>
                    {
                        logger.LogError($"Exception caught : {exception}");
                        await context.SendActivityAsync($"Oops, it looks like something went wrong. Error: {exception.Message}");
                    };
                });
            services.AddSingleton<JiraHelpBot>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;

            app.UseDefaultFiles()
               .UseStaticFiles()
               .UseBotFramework();
        }
    }
}