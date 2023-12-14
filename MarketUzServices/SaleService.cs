using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public SaleService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public SaleDto CreateSale(SaleForCreateDto sale)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(sale);
                var createdEntity = _repository.Sale.Create(saleEntity);

                _repository.SaveChanges();

                return _mapper.Map<SaleDto>(saleEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new sale ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new sale ", ex);
                throw;
            }
        }

        public void DeleteSale(int id)
        {
            try
            {
                _repository.Sale.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting sale with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting sale with id : {id}", ex);
                throw;
            }
        }

        public SaleDto GetSale(int id)
        {
            try
            {
                var sale = _repository.Sale.FindById(id);
                var saleDto = _mapper.Map<SaleDto>(sale);

                return saleDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching sale with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching sale with id : {id}", ex);
                throw;
            }
        }

        public IEnumerable<SaleDto> GetSales()
        {
            try
            {
                var sales = _repository.Sale.FindAll();
                var saleDto = _mapper.Map<IEnumerable<SaleDto>>(sales);

                return saleDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching sale ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching sale ", ex);
                throw;
            };
        }

        public void UpdateSale(SaleForUpdateDto sale)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(sale);
                _repository.Sale.Update(saleEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error updating sale with id : {sale.Id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating sale with id : {sale.Id}", ex);
                throw;
            }
        }
    }
}
