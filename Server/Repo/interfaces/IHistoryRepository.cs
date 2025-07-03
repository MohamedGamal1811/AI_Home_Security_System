using Server.Models.DTOs;
using Server.Models.DTOs.HistoryDTO;

namespace Server.Repo.interfaces
{
    public interface IHistoriesRepository
    {
        Task<List<GetHistoryDto>> GetAllAsync();
        //Task<GetHistoryDto> GetByIdAsync(Guid id);
        //Task<ServiceResponse> AddAsync(CreateHistoryDto entity);
        //Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
