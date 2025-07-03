using AutoMapper;
using Server.Exceptions;
using Server.Models.DTOs;
using Server.Models.DTOs.HouseDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;
using System.Threading.Tasks;

namespace Server.Repo.repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly IGenericRepository<House> _repository;
        private readonly IMapper _mapper;

        public HouseRepository(IGenericRepository<House> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddAsync(CreateHouseDTO houseDTO)
        {
            var house = _mapper.Map<House>(houseDTO);
            int result = await _repository.AddAsync(house);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "House added successfully" : "Failed to add house"
            };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _repository.DeleteAsync(id);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "House deleted successfully" : "Failed to delete house"
            };
        }

        public async Task<IEnumerable<GetHouseDTO>> GetAllAsync()
        {
            var houses = await _repository.GetAllAsync();
            if (houses == null || !houses.Any())
            {
                return Enumerable.Empty<GetHouseDTO>();
            }
            // Map the list of House entities to a list of GetHouseDTOs
            var houseDTOs = _mapper.Map<IEnumerable<GetHouseDTO>>(houses);

            return houseDTOs;
        }

        public async Task<GetHouseDTO> GetByIdAsync(Guid id)
        {

            var house = await _repository.GetByIdAsync(id);
            if (house == null)
            {
                throw new NotFoundException("House not found");
            }
            // Map the House entity to a GetHouseDTO
            var houseDTO = _mapper.Map<GetHouseDTO>(house);

            return houseDTO;

        }

        public async Task<IQueryable<House>> Query()
        {
            var houses = _repository.Query();
            return houses;  
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateHouseDTO houseDTO)
        {
            var house = _mapper.Map<House>(houseDTO);
            int result = await _repository.UpdateAsync(house);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "House updated successfully" : "Failed to update house"
            };
        }

    
    }
}
