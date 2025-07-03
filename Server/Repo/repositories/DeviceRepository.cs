using AutoMapper;
using Server.Exceptions;
using Server.Models.DTOs;
using Server.Models.DTOs.DeviceDTo;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IGenericRepository<Device> _repository;
        private readonly IMapper _mapper;

        public DeviceRepository(IGenericRepository<Device> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddAsync(CreateDeviceDTO deviceDTO)
        {
            var device = _mapper.Map<Device>(deviceDTO);
            int result = await _repository.AddAsync(device);
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

        public async Task<IEnumerable<GetDeviceDTO>> GetAllAsync()
        {
            var devices = await _repository.GetAllAsync();
            if (devices == null || !devices.Any())
            {
                return Enumerable.Empty<GetDeviceDTO>();
            }
            // Map the list of House entities to a list of GetHouseDTOs
            var device = _mapper.Map<IEnumerable<GetDeviceDTO>>(devices);

            return device;
        }

        public async Task<GetDeviceDTO> GetByIdAsync(Guid id)
        {

            var device = await _repository.GetByIdAsync(id);
            if (device == null)
            {
                throw new NotFoundException("House not found");
            }
            // Map the House entity to a GetHouseDTO
            var deviceDTO = _mapper.Map<GetDeviceDTO>(device);

            return deviceDTO;

        }

        public async Task<ServiceResponse> UpdateAsync(UpdateDeviceDTO deviceDTO)
        {
            var device = _mapper.Map<Device>(deviceDTO);
            int result = await _repository.UpdateAsync(device);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "House updated successfully" : "Failed to update house"
            };
        }

    }
}
