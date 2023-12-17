using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supply;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<SupplyService> _logger;

        public SupplyService(IMapper mapper, MarketUzDbContext context, ILogger<SupplyService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<SupplyDto> GetSupplies()
        {

            var supplies = _context.Supplies.ToList();

            var supplyDtos = _mapper.Map<IEnumerable<SupplyDto>>(supplies);

            return supplyDtos;

        }

        public SupplyDto? GetSupplyById(int id)
        {

            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);
            if (supply is null)
            {
                throw new EntityNotFoundException($"Supply with id: {id} not found");
            }
            var supplyDto = _mapper.Map<SupplyDto>(supply);

            return supplyDto;
        }

        public SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate)
        {

            var supplyEntity = _mapper.Map<Supply>(supplyToCreate);

            _context.Supplies.Add(supplyEntity);
            _context.SaveChanges();

            var supplyDto = _mapper.Map<SupplyDto>(supplyEntity);

            return supplyDto;

        }

        public void UpdateSupply(SupplyForUpdateDto supplyToUpdate)
        {

            var supplyEntity = _mapper.Map<Supply>(supplyToUpdate);

            _context.Supplies.Update(supplyEntity);
            _context.SaveChanges();

        }

        public void DeleteSupply(int id)
        {

            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);
            if (supply is not null)
            {
                _context.Supplies.Remove(supply);
            }
            _context.SaveChanges();
        }
    }
}
