using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

[Route("api/github")]
[ApiController]
public class GitHubController : ControllerBase
{
    private readonly GitHubApiService _gitHubApiService;

    public GitHubController(GitHubApiService gitHubApiService)
    {
        _gitHubApiService = gitHubApiService;
    }

    [HttpGet("portfolio")]
    public async Task<IActionResult> GetPortfolio()
    {
        var repos = await _gitHubApiService.GetPortfolioAsync();
        return Ok(repos);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchRepositories([FromQuery] string query, [FromQuery] string language, [FromQuery] string user)
    {
        var repos = await _gitHubApiService.SearchRepositoriesAsync(query, language, user);
        return Ok(repos);
    }
}
