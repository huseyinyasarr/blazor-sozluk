using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly IHttpClientFactory httpClientFactory;

    private const string ClientName = "WebApiClient";

    public EntryService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }


    public async Task<List<GetEntriesViewModel>> GetEntires()
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?isEnties=false&count=30");

        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var result = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");

        return result;
    }


    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var res = await client.PostAsJsonAsync("/api/Entry/CreateEntry", command);

        if (!res.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var res = await client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);

        if (!res.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        var result = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");

        return result;
    }
}

