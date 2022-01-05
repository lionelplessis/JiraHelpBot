using System.Threading.Tasks;

namespace JiraHelpBot2.JiraClient
{
    public interface IJiraClient
    {
        Task<Issue> GetTicket(string searchedTicketKey);
    }
}
