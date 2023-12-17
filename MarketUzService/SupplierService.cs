using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supplier;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

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
            var suppliers = _context.Suppliers.ToList();

            var supplierDtos = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);

            return supplierDtos;

        }

        public SupplierDto? GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
            if (supplier is null)
            {
                throw new EntityNotFoundException($"Supplier with id: {id} not found");
            }
            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            return supplierDto;

        }

        public SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate)
        {

            var supplierEntity = _mapper.Map<Supplier>(supplierToCreate);

            _context.Suppliers.Add(supplierEntity);
            _context.SaveChanges();

            var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);

            return supplierDto;

        }

        public void UpdateSupplier(SupplierForUpdateDto supplierToUpdate)
        {

            var supplierEntity = _mapper.Map<Supplier>(supplierToUpdate);

            _context.Suppliers.Update(supplierEntity);
            _context.SaveChanges();

        }

        public void DeleteSupplier(int id)
        {

            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
            if (supplier is not null)
            {
                _context.Suppliers.Remove(supplier);
            }

        }
    }
}
