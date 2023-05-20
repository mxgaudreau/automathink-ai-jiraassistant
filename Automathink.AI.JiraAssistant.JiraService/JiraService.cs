using Atlassian.Jira;

using Automathink.AI.JiraAssistant.JiraService.Models;

namespace Automathink.AI.JiraAssistant.JiraService
{
    public class JiraService
    {
        private readonly string _jiraUrl;
        private readonly string _username;
        private readonly string _password;

        public JiraService(string jiraUrl, string username, string password)
        {
            _jiraUrl = jiraUrl ?? throw new ArgumentNullException(nameof(jiraUrl));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public async Task<JiraIssueCreateResponse> CreateIssueAsync(JiraIssueCreateRequest request)
        {
            var jira = Jira.CreateRestClient(_jiraUrl, _username, _password);

            var issue = new Issue(jira, request.Project)
            {
                Type = request.IssueType,
                Summary = request.Summary,
                Description = request.Description,
                Reporter = request.Reporter,
                Assignee = request.Assignee,
                Priority = request.Priority
            };

            // Add the custom fields
            issue.CustomFields.Add("Labels", request.Labels);
            issue.CustomFields.Add("Epic Link", request.EpicLink);
            issue.CustomFields.Add("Sprint", request.Sprint);
            issue.CustomFields.Add("Comment", request.Comment);
            issue.CustomFields.Add("Parent", request.Parent);
            issue.CustomFields.Add("Subtasks", request.Subtasks);
            issue.CustomFields.Add("Linked Issues", request.LinkedIssues);

            var createdIssueKey = await jira.Issues.CreateIssueAsync(issue);

            // Fetch the created issue again to retrieve its details
            var createdIssue = await jira.Issues.GetIssueAsync(createdIssueKey);

            // Extract issue ID from issue key
            var issueId = createdIssue.Key.ToString().Split('-')[1];

            return new JiraIssueCreateResponse
            {
                Id = issueId,
                Key = createdIssue.Key.Value,
                Self = $"{this._jiraUrl}/browse/{createdIssue.Key}"
            };
        }

    }
}
