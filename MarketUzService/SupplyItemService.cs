using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class SupplyItemService : ISupplyItemService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<SupplyItemService> _logger;

        public SupplyItemService(IMapper mapper, MarketUzDbContext context, ILogger<SupplyItemService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<SupplyItemDto> GetSupplyItems()
        {

            var supplyItems = _context.SupplyItems.ToList();

            var supplyItemDtos = _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItems);

            return supplyItemDtos;
        }

        public SupplyItemDto? GetSupplyItemById(int id)
        {

            var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);
            if (supplyItem is null)
            {
                throw new EntityNotFoundException($"SupplyItem with id: {id} not found");
            }
            var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItem);

            return supplyItemDto;

        }

        public SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate)
        {

            var supplyItemEntity = _mapper.Map<SupplyItem>(supplyItemToCreate);

            _context.SupplyItems.Add(supplyItemEntity);
            _context.SaveChanges();

            var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItemEntity);

            return supplyItemDto;
        }



        public void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate)
        {

            var supplyItemEntity = _mapper.Map<SupplyItem>(supplyItemToUpdate);

            _context.SupplyItems.Update(supplyItemEntity);
            _context.SaveChanges();

        }

        public void DeleteSupplyItem(int id)
        {
            var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);
            if (supplyItem is not null)
            {
                _context.SupplyItems.Remove(supplyItem);
            }
            _context.SaveChanges();

        }
    }
}
