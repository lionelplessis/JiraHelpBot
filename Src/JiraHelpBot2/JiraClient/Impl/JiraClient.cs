using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JiraHelpBot2.JiraClient.Impl;

public class JiraClient : IJiraClient
{
    private readonly HttpClient _httpClient;
    private readonly string _jiraAddress;
    private readonly string _jiraApiToken;
    private readonly string _jiraUserName;
    private readonly ILogger<JiraClient> _logger;

    public JiraClient(ILogger<JiraClient> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();

        _jiraUserName = configuration.GetSection("JiraHelpJiraAccountId")?.Value;
        _jiraApiToken = configuration.GetSection("JiraHelpJiraAccountPassword")?.Value;
        _jiraAddress = configuration.GetSection("JiraHelpJiraEndpoint")?.Value;
    }

    public async Task<Issue> GetTicket(string searchedTicketKey)
    {
        var query = @$"key = {searchedTicketKey}";

        var tickets = await GetTicketsByQuery(query);

        // Ticket not found or multiple matches are not tolerated
        if (tickets is { Count: 1 }) return tickets.Single();

        return null;
    }

    private async Task<List<Issue>> GetTicketsByQuery(string query)
    {
        using var request = new HttpRequestMessage(
            new HttpMethod("GET"),
            $"{_jiraAddress}/rest/api/2/search?jql={query}");

        var base64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_jiraUserName}:{_jiraApiToken}"));
        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64Authorization}");

        _logger.LogTrace("JiraClient getting tickets by JQL: '{Query}'", query);

        var response = await _httpClient.SendAsync(request);

        _logger.LogTrace("JiraClient received tickets response with status code: '{StatusCode}'", response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var rawIssuesResponse = JsonConvert.DeserializeObject<Rootobject>(content);

        _logger.LogTrace("JiraClient deserialized {TicketsCount} tickets.", rawIssuesResponse?.issues.Length ?? 0);

        return (rawIssuesResponse?.issues ?? Array.Empty<Issue>()).ToList();
    }
}