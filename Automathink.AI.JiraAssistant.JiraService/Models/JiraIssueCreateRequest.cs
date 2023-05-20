namespace Automathink.AI.JiraAssistant.JiraService.Models;

public class JiraIssueCreateRequest
{
    public string Project { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string IssueType { get; set; }
    public string Priority { get; set; }
    public string Reporter { get; set; }
    public string Assignee { get; set; }
    public string Labels { get; set; }
    public string EpicLink { get; set; }
    public string Sprint { get; set; }
    public string Comment { get; set; }
    public string Parent { get; set; }
    public string Subtasks { get; set; }
    public string LinkedIssues { get; set; }
}
