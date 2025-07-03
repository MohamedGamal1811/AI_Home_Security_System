using AutoMapper;
using Server.Exceptions;
using Server.Models.DTOs;
using Server.Models.DTOs.HistoryDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class HistoryRepository : IHistoriesRepository
    {
        private readonly IGenericRepository<History> _historyRepo;
        private readonly IMapper _mapper;
        public HistoryRepository(IGenericRepository<History> repository, IMapper mapper)
        {
            _historyRepo = repository;
            _mapper = mapper;
        }

        #region
        //public async Task<ServiceResponse> AddAsync(CreateHistoryDto historyDto)
        //{
        //    var history = _mapper.Map<History>(historyDto);
        //    int result = await _historyRepo.AddAsync(history);
        //    return new ServiceResponse
        //    {
        //        Success = result > 0,
        //        Message = result > 0 ? "History added successfully" : "Failed to add history"
        //    };
        //}

        //public async Task<ServiceResponse> DeleteAsync(Guid id)
        //{
        //    int result = await _historyRepo.DeleteAsync(id);
        //    return new ServiceResponse
        //    {
        //        Success = result > 0,
        //        Message = result > 0 ? "House deleted successfully" : "Failed to delete house"
        //    };
        //}
        #endregion
        public async Task<List<GetHistoryDto>> GetAllAsync()
        {
            var histories = await _historyRepo.GetAllAsync();
            if (histories == null || !histories.Any())
            {
                return [];
            }
            // Map the list of House entities to a list of GetHouseDTOs
            var historiesDTOs = _mapper.Map<List<GetHistoryDto>>(histories);

            return historiesDTOs;
        }

        
    }
}