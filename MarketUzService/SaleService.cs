using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

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
            var sales = _context.Sales.ToList();

            var saleDtos = _mapper.Map<IEnumerable<SaleDto>>(sales);

            return saleDtos;

        }

        public SaleDto? GetSaleById(int id)
        {

            var sale = _context.Sales.FirstOrDefault(x => x.Id == id);
            if (sale is null)
            {
                throw new EntityNotFoundException($"Sale with id: {id} not found");
            }
            var saleDto = _mapper.Map<SaleDto>(sale);

            return saleDto;

        }

        public SaleDto CreateSale(SaleForCreateDto saleToCreate)
        {

            var saleEntity = _mapper.Map<Sale>(saleToCreate);

            _context.Sales.Add(saleEntity);
            _context.SaveChanges();

            var saleDto = _mapper.Map<SaleDto>(saleEntity);

            return saleDto;

        }

        public void UpdateSale(SaleForUpdateDto saleToUpdate)
        {

            var saleEntity = _mapper.Map<Sale>(saleToUpdate);

            _context.Sales.Update(saleEntity);
            _context.SaveChanges();

        }

        public void DeleteSale(int id)
        {
            var sale = _context.Sales.FirstOrDefault(x => x.Id == id);
            if (sale is not null)
            {
                _context.Sales.Remove(sale);
            }
        }
    }
}
