using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Domain.Entities.Common;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Entities.UserEntities;
using Bookstore.Domain.ExternalEntities.ComicEntities;
using Bookstore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Bookstore.Application.Services
{
    public class ComicsService : IComicsService
    {
        private readonly IExternalComicsService _externalComicsService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IComicNoSqlService _comicNoSqlService;

        public ComicsService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IExternalComicsService externalComicsService, IComicNoSqlService comicNoSqlService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _externalComicsService = externalComicsService;
            _comicNoSqlService = comicNoSqlService;
        }

        public async Task<List<ComicVolumeResponseDto>> GetAllVolumesAsync(string volumeName)
        {
            if (String.IsNullOrEmpty(volumeName))
            {
                volumeName = "The Amazing Spider-Man";
            }

            string url = $"{_configuration["ExternalProvider:ComicVine:BaseUrl"]}" + "/volumes?api_key=" +
                $"{_configuration["ExternalProvider:ComicVine:ApiKey"]}" +
                $"&format=json&filter=name:{Uri.EscapeDataString(volumeName)}&field_list=id,name,start_year,image";


            var response =  await _externalComicsService.Get(url);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var tempList = JsonSerializer.Deserialize<List<JsonElement>>(response.Results, options)
               ?? new List<JsonElement>();

            var volumes = tempList.Select(v => new ComicVolumeResponseDto
            {
                Id = v.GetProperty("id").ValueKind == JsonValueKind.Number
                ? v.GetProperty("id").GetInt32()
                : int.Parse(v.GetProperty("id").GetString()),
                Name = v.GetProperty("name").GetString(),
                StartYear = v.TryGetProperty("start_year", out var startYearProp) && startYearProp.ValueKind != JsonValueKind.Null
                ? startYearProp.ValueKind == JsonValueKind.Number
                ? startYearProp.GetInt32()
                : int.TryParse(startYearProp.GetString(), out var parsed) ? parsed : 0
                : 0,
                Image = v.GetProperty("image").GetProperty("original_url").GetRawText()
            }).ToList();

            return volumes;

            //return JsonSerializer.Deserialize<List<ComicVolumeResponseDto>>(response.Results, options) ?? new List<ComicVolumeResponseDto>();
        }

        public async Task<PaginatedList<ComicIssueSummaryResponseDto>> GetPagedIssuesByVolumeAsync(int volumeId, int page)
        {
            string volumeUrl = $"{_configuration["ExternalProvider:ComicVine:BaseUrl"]}" + "/volume/" +
                $"4050-{volumeId}?api_key=" +
                $"{_configuration["ExternalProvider:ComicVine:ApiKey"]}" +
                $"&format=json&field_list=id";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var responseVolume = await _externalComicsService.Get(volumeUrl);
            var volumeResponse = JsonSerializer.Deserialize<ComicVolumeResponseDto>(responseVolume.Results, options);
            if (volumeResponse == null)
            {
                throw new NotFoundException($"Volume with id {volumeId} not found");
            }

            int pageSize = 6;
            if (page < 1)
            {
                page = 1;
            }
            page -= 1;

            string url = $"{_configuration["ExternalProvider:ComicVine:BaseUrl"]}" + "/issues?api_key=" +
                $"{_configuration["ExternalProvider:ComicVine:ApiKey"]}" +
                $"&format=json&filter=volume:{volumeId}&field_list=id,name,cover_date,issue_number,image" +
                $"&limit={pageSize}&offset={page * pageSize}";

            var responseIssues = await _externalComicsService.Get(url, true);

            var tempList = JsonSerializer.Deserialize<List<JsonElement>>(responseIssues.Results, options)
               ?? new List<JsonElement>();

            var issues = tempList.Select(v => new ComicIssueSummaryResponseDto
            {
                Id = v.GetProperty("id").ValueKind == JsonValueKind.Number
                ? v.GetProperty("id").GetInt32()
                : int.Parse(v.GetProperty("id").GetString()),
                Name = v.GetProperty("name").GetString(),
                IssueNumber = v.GetProperty("issue_number").GetRawText(),
                CoverDate = v.GetProperty("cover_date").ValueKind == JsonValueKind.Null
                ? new DateTime(1000, 1, 1, 0, 0, 0) : v.GetProperty("cover_date").GetDateTime(),
                Image = v.GetProperty("image").GetProperty("original_url").GetRawText()
            }).ToList();
            int? totalCount = responseIssues.TotalResults;
            return new PaginatedList<ComicIssueSummaryResponseDto>(issues, totalCount ?? issues.Count, page, pageSize);
        }

        public async Task AddComicIssueAsync(int issueId, int volumeId, ComicIssueCreateDto issueDto)
        {
            string url = $"{_configuration["ExternalProvider:ComicVine:BaseUrl"]}" + $"/issue/4000-{issueId}?api_key=" +
                $"{_configuration["ExternalProvider:ComicVine:ApiKey"]}" +
                $"&format=json&field_list=id,name,cover_date,issue_number,image";

            var response = await _externalComicsService.Get(url);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var tempList = JsonSerializer.Deserialize<JsonElement>(response.Results, options);

            var externalIssue = new ComicIssueExt
            {
                Id = tempList.GetProperty("id").ValueKind == JsonValueKind.Number
                ? tempList.GetProperty("id").GetInt32()
                : int.Parse(tempList.GetProperty("id").GetString()),
                Name = tempList.GetProperty("name").GetString(),
                IssueNumber = tempList.GetProperty("issue_number").GetRawText(),
                CoverDate = tempList.GetProperty("cover_date").GetDateTime(),
                Image = tempList.GetProperty("image").GetProperty("original_url").GetRawText()
            };

            if (externalIssue == null)
            {
                throw new NotFoundException($"Comic issue with id {issueId} not found in external provider");
            }
            var comicIssue = _mapper.Map<ComicIssue>(externalIssue);
            comicIssue = _mapper.Map(issueDto, comicIssue);

            try
            {
                await _comicNoSqlService.AddIssueAsync(comicIssue);
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while adding the comic issue to the repository.", ex);
            }
            return;
        }
    }
}
