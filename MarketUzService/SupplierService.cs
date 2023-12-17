using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supplier;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace DiyorMarket.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(IMapper mapper, MarketUzDbContext context, ILogger<SupplierService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            try
            {
                var suppliers = _context.Suppliers.ToList();

                var supplierDtos = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);

                return supplierDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supplier and SupplierDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while fetching suppliers.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching suppliers.", ex.Message);
                throw;
            }
        }

        public SupplierDto? GetSupplierById(int id)
        {
            try
            {
                var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);

                var supplierDto = _mapper.Map<SupplierDto>(supplier);

                return supplierDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supplier and SupplierDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching supplier with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching supplier with id: {id}.", ex.Message);
                throw;
            }
        }

        public SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplierToCreate);

                _context.Suppliers.Add(supplierEntity);
                _context.SaveChanges();

                var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);

                return supplierDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supplier and SupplierDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new supplier.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new supplier.", ex.Message);
                throw;
            }
        }

        public void UpdateSupplier(SupplierForUpdateDto supplierToUpdate)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplierToUpdate);

                _context.Suppliers.Update(supplierEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supplier and SupplierDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating supplier.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating supplier.", ex.Message);
                throw;
            }
        }

        public void DeleteSupplier(int id)
        {
            try
            {
                var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
                if (supplier is not null)
                {
                    _context.Suppliers.Remove(supplier);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting supplier with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting supplier with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
