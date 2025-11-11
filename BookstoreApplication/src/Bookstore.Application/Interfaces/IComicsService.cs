using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.Common;

namespace Bookstore.Application.Interfaces
{
    public interface IComicsService
    {
        Task<List<ComicVolumeResponseDto>> GetAllVolumesAsync(string volumeName);
        Task<PaginatedList<ComicIssueSummaryResponseDto>> GetPagedIssuesByVolumeAsync(int volumeId, int page);
        Task AddComicIssueAsync(int issueId, int volumeId, ComicIssueCreateDto issueDto);
    }
}
