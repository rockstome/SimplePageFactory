using Atlassian.Jira;
using System.Threading.Tasks;
using System.Linq;

namespace Utils
{
    public class JiraManager
    {
        private Jira jira;

        public JiraManager(string url, string username, string password)
        {
            jira = Jira.CreateRestClient(url, username, password);
        }

        private async Task<IPagedQueryResult<Issue>> GetIssuesFromJqlAsync(string jqlQuery)
        {
            return await jira.Issues.GetIssuesFromJqlAsync(jqlQuery);
        }

        public async void DeleteComments(string jqlQuery)
        {
            var issues = await GetIssuesFromJqlAsync(jqlQuery);

            issues.Select(async i =>
                {
                    var comments = await i.GetCommentsAsync();
                    comments.Select(c => i.DeleteCommentAsync(c));
                    await i.SaveChangesAsync();
                });
        }

        public async void SetStatusInProgres(string jqlQuery)
        {
            var issues = await GetIssuesFromJqlAsync(jqlQuery);

            issues.First().
        }
    }
}
