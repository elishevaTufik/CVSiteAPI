using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
public class GitHubOptions
{
    public string Token { get; set; }
    public string Username { get; set; }
}

public class GitHubApiService
{
    private readonly GitHubClient _client;
    private readonly IMemoryCache _cache;
    private readonly string _username;

    public GitHubApiService(IOptions<GitHubOptions> options, IMemoryCache cache)
    {
        _username = options.Value.Username;
        _cache = cache;

        _client = new GitHubClient(new ProductHeaderValue("CVSite"))
        {
            Credentials = new Credentials(options.Value.Token)
        };
    }
    public async Task<IReadOnlyList<Repository>> GetPortfolioAsync()
    {
        if (_cache.TryGetValue("PortfolioCache", out IReadOnlyList<Repository> cachedRepos))
        {
            var latestCommit = await _client.Repository.Commit.Get(_username, cachedRepos[0].Name, "HEAD");

            var cachedTime = _cache.Get<DateTime>("LastUpdatedTime");

            if (latestCommit.Commit.Author.Date.UtcDateTime > cachedTime)
            {
                _cache.Remove("PortfolioCache");
            }
            else
            {
                return cachedRepos;
            }
        }

        var repositories = await _client.Repository.GetAllForUser(_username);

        _cache.Set("LastUpdatedTime", DateTime.UtcNow);
        _cache.Set("PortfolioCache", repositories, TimeSpan.FromMinutes(10));

        return repositories;
    }


    public async Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string query, string language = null, string user = null)
    {
        //var searchRequest = new SearchRepositoriesRequest(query)
        //{
        //    Language = language != null ? new RepositoryLanguage?(Enum.Parse<RepositoryLanguage>(language, true)) : null,
        //    User = user
        //};

        //var searchResults = await _client.Search.SearchRepo(searchRequest);
        //return searchResults.Items;
        var queryParts = new List<string>();
       
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("At least one search parameter must be provided.");
        var req = new SearchRepositoriesRequest(query);
        var res = await _client.Search.SearchRepo(req);
        return res.Items.Take(5).ToList();
    }
}
