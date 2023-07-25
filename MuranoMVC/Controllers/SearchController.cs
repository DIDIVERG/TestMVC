using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication10.DataAccessLayer;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DTOs;
using WebApplication10.Services.Interfaces;
using WebApplication10.Views.Search.ViewModels;

namespace WebApplication10.Controllers;

public class SearchController : Controller
{
    private readonly IYandexService _yandexService;
    private readonly IGoogleService _googleService;
    private readonly IBingService _bingService;
    private readonly IQueryService _queryService;
    private readonly IHashService _hashService;
    private readonly IMapper _mapper;
    public SearchController(IYandexService yandexService, IGoogleService googleService, 
        IBingService bingService, IQueryService queryService, 
        IHashService hashService, IMapper mapper)
    {
        _yandexService = yandexService;
        _googleService = googleService;
        _bingService = bingService;
        _queryService = queryService;
        _hashService = hashService;
        _mapper = mapper;
    }
    
    public IActionResult Index()
    {
        var viewModel = new SearchViewModel
        {
            Words = new List<string>(),
            SearchResults = null // Set the SearchResults to null or any initial value as needed.
        };
        return View(viewModel);
    }
    
    [HttpPost("SendWords")]
    public async Task<IActionResult> SendWords([FromBody] List<string> keywords)
    {
        var hash = _hashService.GetHash(keywords);
        string inputString = string.Join(" ", keywords);
        IEnumerable<SearchResult> searchResults = null;
        var query = await _queryService.GetQueryByHashAsync(hash);
        if (query != null)
        {
            searchResults = query.Results;
        }
        else
        {
            var yandexTask = _yandexService.Search(inputString);
            var googleTask = _googleService.Search(inputString);
            var bingTask = _bingService.Search(inputString);
            switch (await Task.WhenAny(yandexTask, googleTask, bingTask))
            {
                case var completedTask when completedTask == yandexTask:
                    searchResults = await yandexTask;
                    break;
                case var completedTask when completedTask == googleTask:
                    searchResults = await googleTask;
                    break;
                case var completedTask when completedTask == bingTask:
                    searchResults = await bingTask;
                    break;
            }
            await _queryService.InsertQueryAsync(new List<Query> 
            {
                new Query { QueryKeywordHash = hash, Results = searchResults }
            });
        }
        ViewBag.SearchResults = _mapper.Map<IEnumerable<SearchResultDto>>(searchResults);
        var viewModel = new SearchViewModel
        {
            Words = new List<string>(),
            SearchResults = _mapper.Map<IEnumerable<SearchResultDto>>(searchResults)
        };
        
        return Ok(_mapper.Map<IEnumerable<SearchResultDto>>(searchResults));
    }
}