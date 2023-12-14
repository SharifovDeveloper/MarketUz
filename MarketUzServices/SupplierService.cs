using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supplier;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public SupplierService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            try
            {
                var supplier = _repository.Supplier.FindAll();
                var supplierDto = _mapper.Map<IEnumerable<SupplierDto>>(supplier);

                return supplierDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching supplier ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching supplier ", ex);
                throw;
            }
        }

        public SupplierDto CreateSupplier(SupplierForCreateDto supplier)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplier);
                var createdEntity = _repository.Supplier.Create(supplierEntity);

                _repository.SaveChanges();

                return _mapper.Map<SupplierDto>(supplierEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new supplier ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new supplier ", ex);
                throw;
            }
        }

        public void DeleteSupplier(int id)
        {
            try
            {
                _repository.Supplier.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting supplier with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting supplier with id : {id}", ex);
                throw;
            }
        }

        public SupplierDto GetSupplierById(int id)
        {
            try
            {
                var supplier = _repository.Supplier.FindById(id);
                var supplierDto = _mapper.Map<SupplierDto>(supplier);

                return supplierDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching supplier with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching supplier with id : {id}", ex);
                throw;
            }
        }

        public void UpdateSupplier(SupplierForUpdateDto supplier)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplier);
                _repository.Supplier.Update(supplierEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error updating supplier with id : {supplier.Id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating supplier with id : {supplier.Id}", ex);
                throw;
            }
        }
    }
}
