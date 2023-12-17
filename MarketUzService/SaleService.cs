using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace DiyorMarket.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<SaleService> _logger;

        public SaleService(IMapper mapper, MarketUzDbContext context, ILogger<SaleService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<SaleDto> GetSales()
        {
            try
            {
                var sales = _context.Sales.ToList();

                var saleDtos = _mapper.Map<IEnumerable<SaleDto>>(sales);

                return saleDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while fetching sales.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching sales.", ex.Message);
                throw;
            }
        }

        public SaleDto? GetSaleById(int id)
        {
            try
            {
                var sale = _context.Sales.FirstOrDefault(x => x.Id == id);

                var saleDto = _mapper.Map<SaleDto>(sale);

                return saleDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching sale with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching sale with id: {id}.", ex.Message);
                throw;
            }
        }

        public SaleDto CreateSale(SaleForCreateDto saleToCreate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToCreate);

                _context.Sales.Add(saleEntity);
                _context.SaveChanges();

                var saleDto = _mapper.Map<SaleDto>(saleEntity);

                return saleDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new sale.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new sale.", ex.Message);
                throw;
            }
        }

        public void UpdateSale(SaleForUpdateDto saleToUpdate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToUpdate);

                _context.Sales.Update(saleEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating sale.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating sale.", ex.Message);
                throw;
            }
        }

        public void DeleteSale(int id)
        {
            try
            {
                var sale = _context.Sales.FirstOrDefault(x => x.Id == id);
                if (sale is not null)
                {
                    _context.Sales.Remove(sale);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting sale with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting sale with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
