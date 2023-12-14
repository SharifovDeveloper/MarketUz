using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.SaleItem;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public SaleItemService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public SaleItemDto CreatSaleItem(SaleItemForCreateDto saleItem)
        {
            try
            {
                var saleItemEntity = _mapper.Map<SaleItem>(saleItem);
                var createdEntity = _repository.SaleItem.Create(saleItemEntity);

                _repository.SaveChanges();

                return _mapper.Map<SaleItemDto>(saleItemEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new saleItem ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new saleItem ", ex);
                throw;
            }
        }

        public void DeleteSaleItem(int id)
        {
            try
            {
                _repository.SaleItem.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting saleItem with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting saleItem with id : {id}", ex);
                throw;
            }
        }

        public SaleItemDto GetSaleItem(int id)
        {
            try
            {
                var sale = _repository.SaleItem.FindById(id);
                var saleDto = _mapper.Map<SaleItemDto>(sale);

                return saleDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching saleItem with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching saleItem with id : {id}", ex);
                throw;
            }
        }

        public IEnumerable<SaleItemDto> GetSaleItems()
        {
            try
            {
                var sales = _repository.SaleItem.FindAll();
                var saleDto = _mapper.Map<IEnumerable<SaleItemDto>>(sales);

                return saleDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching saleItem ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching saleItem ", ex);
                throw;
            };
        }

        public void UpdateSaleItem(SaleItemForUpdateDto saleItem)
        {
            try
            {
                var saleEntity = _mapper.Map<SaleItem>(saleItem);
                _repository.SaleItem.Update(saleEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error updating saleItem with id : {saleItem.Id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating saleItem with id : {saleItem.Id}", ex);
                throw;
            }
        }
    }
}
