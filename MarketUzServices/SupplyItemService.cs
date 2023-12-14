using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SupplyItemService : ISupplyItemService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public SupplyItemService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto createDto)
        {
            try
            {
                var supplyItemEntity = _mapper.Map<SupplyItem>(createDto);
                var createdEntity = _repository.SupplyItem.Create(supplyItemEntity);

                _repository.SaveChanges();

                return _mapper.Map<SupplyItemDto>(supplyItemEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new supplyItem ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new supplyItem ", ex);
                throw;
            }
        }

        public void DeleteSupplyItem(int id)
        {
            try
            {
                _repository.SupplyItem.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting supplyItem with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting supplyItem with id : {id}", ex);
                throw;
            }
        }

        public SupplyItemDto GetSupplyItem(int id)
        {
            try
            {
                var supplyItem = _repository.SupplyItem.FindById(id);
                var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItem);

                return supplyItemDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching supplyItem with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching supplyItem with id : {id}", ex);
                throw;
            }
        }

        public IEnumerable<SupplyItemDto> GetSupplyItems()
        {
            try
            {
                var supplyItem = _repository.SupplyItem.FindAll();
                var supplyItemDto = _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItem);

                return supplyItemDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching supplyItem ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching supplyItem ", ex);
                throw;
            }
        }

        public void UpdateSupplyItem(SupplyItemForUpdateDto updateDto)
        {
            try
            {
                var supplyItemEntity = _mapper.Map<SupplyItem>(updateDto);
                _repository.SupplyItem.Update(supplyItemEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error updating new supplyItem ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating new supplyItem ", ex);
                throw;
            }
        }
    }
}
