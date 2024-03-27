using AutoMapper;
using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using MarketUz.Infrastructure.Persistence;

namespace DiyorMarket.Services
{
    public class SupplyItemService : ISupplyItemService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;

        public SupplyItemService(IMapper mapper, MarketUzDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetBaseResponse<SupplyItemDto> GetSupplyItems(SupplyItemResourceParameters supplyItemResourceParameters)
        {
            var query = _context.SupplyItems.AsQueryable();

            if (supplyItemResourceParameters.ProductId is not null)
            {
                query = query.Where(x => x.ProductId == supplyItemResourceParameters.ProductId);
            }

            if (supplyItemResourceParameters.SupplyId is not null)
            {
                query = query.Where(x => x.SupplyId == supplyItemResourceParameters.SupplyId);
            }

            if (supplyItemResourceParameters.UnitPrice is not null)
            {
                query = query.Where(x => x.UnitPrice == supplyItemResourceParameters.UnitPrice);
            }

            if (supplyItemResourceParameters.UnitPriceLessThan is not null)
            {
                query = query.Where(x => x.UnitPrice < supplyItemResourceParameters.UnitPriceLessThan);
            }

            if (supplyItemResourceParameters.UnitPriceGreaterThan is not null)
            {
                query = query.Where(x => x.UnitPrice > supplyItemResourceParameters.UnitPriceGreaterThan);

            }

            if (!string.IsNullOrEmpty(supplyItemResourceParameters.OrderBy))
            {
                query = supplyItemResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "id" => query.OrderBy(x => x.Id),
                    "iddesc" => query.OrderByDescending(x => x.Id),
                    "unitprice" => query.OrderBy(x => x.UnitPrice),
                    "unitpricedesc" => query.OrderByDescending(x => x.UnitPrice),
                    "saleid" => query.OrderBy(x => x.SupplyId),
                    "saleiddesc" => query.OrderByDescending(x => x.SupplyId),
                    "productid" => query.OrderBy(x => x.ProductId),
                    "productiddesc" => query.OrderByDescending(x => x.ProductId),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var supplyItems = query.ToPaginatedList(supplyItemResourceParameters.PageSize, supplyItemResourceParameters.PageNumber);

            var supplyItemDtos = _mapper.Map<List<SupplyItemDto>>(supplyItems);

            var paginatedResult = new PaginatedList<SupplyItemDto>(supplyItemDtos, supplyItems.TotalCount, supplyItems.CurrentPage, supplyItems.PageSize);

            return paginatedResult.ToResponse();
        }

        public IEnumerable<SupplyItemDto> GetAllSupplyItems()
        {
            var supplyItems = _context.SupplyItems.ToList();

            return _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItems) ?? Enumerable.Empty<SupplyItemDto>();
        }

        public SupplyItemDto? GetSupplyItemById(int id)
        {
            var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);

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
