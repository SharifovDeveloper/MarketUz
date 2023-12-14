using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supply;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public SupplyService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public SupplyDto CreateSupply(SupplyForCreateDto supply)
        {
            try
            {
                var supplyEntity = _mapper.Map<Supply>(supply);
                var createdEntity = _repository.Supply.Create(supplyEntity);

                _repository.SaveChanges();

                return _mapper.Map<SupplyDto>(supplyEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new supply ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new supply ", ex);
                throw;
            }
        }

        public void DeleteSupply(int id)
        {
            try
            {
                _repository.Supply.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting supply with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting supply with id : {id}", ex);
                throw;
            }
        }

        public IEnumerable<SupplyDto> GetSupplies()
        {
            try
            {
                var supply = _repository.Supply.FindAll();
                var supplyDto = _mapper.Map<IEnumerable<SupplyDto>>(supply);

                return supplyDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching supply ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching supply ", ex);
                throw;
            }
        }

        public SupplyDto GetSupplyById(int id)
        {
            try
            {
                var supply = _repository.Supply.FindById(id);
                var supplyDto = _mapper.Map<SupplyDto>(supply);

                return supplyDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching supply with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching supply with id : {id}", ex);
                throw;
            }
        }

        public void UpdateSupply(SupplyForUpdateDto supply)
        {
            try
            {
                var supplyEntity = _mapper.Map<Supply>(supply);
                _repository.Supply.Update(supplyEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error updating new supply ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating new supply ", ex);
                throw;
            }
        }
    }
}
