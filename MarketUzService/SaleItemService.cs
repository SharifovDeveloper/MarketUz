using AutoMapper;
using MarketUz.Domain.DTOs.SaleItem;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using MarketUz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiyorMarket.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;

        public SaleItemService(IMapper mapper, MarketUzDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetBaseResponse<SaleItemDto> GetSaleItems(SaleItemResourceParameters saleItemResourceParameters)
        {
            var query = _context.SaleItems.AsQueryable();

            if (saleItemResourceParameters.ProductId is not null)
            {
                query = query.Where(x => x.ProductId == saleItemResourceParameters.ProductId);
            }

            if (saleItemResourceParameters.SaleId is not null)
            {
                query = query.Where(x => x.SaleId == saleItemResourceParameters.SaleId);
            }

            if (saleItemResourceParameters.UnitPrice is not null)
            {
                query = query.Where(x => x.UnitPrice == saleItemResourceParameters.UnitPrice);
            }

            if (saleItemResourceParameters.UnitPriceLessThan is not null)
            {
                query = query.Where(x => x.UnitPrice < saleItemResourceParameters.UnitPriceLessThan);
            }

            if (saleItemResourceParameters.UnitPriceGreaterThan is not null)
            {
                query = query.Where(x => x.UnitPrice > saleItemResourceParameters.UnitPriceGreaterThan);

            }

            if (!string.IsNullOrEmpty(saleItemResourceParameters.OrderBy))
            {
                query = saleItemResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "id" => query.OrderBy(x => x.Id),
                    "iddesc" => query.OrderByDescending(x => x.Id),
                    "unitprice" => query.OrderBy(x => x.UnitPrice),
                    "unitpricedesc" => query.OrderByDescending(x => x.UnitPrice),
                    "saleid" => query.OrderBy(x => x.SaleId),
                    "saleiddesc" => query.OrderByDescending(x => x.SaleId),
                    "productid" => query.OrderBy(x => x.ProductId),
                    "productiddesc" => query.OrderByDescending(x => x.ProductId),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var saleItems = query.ToPaginatedList(saleItemResourceParameters.PageSize, saleItemResourceParameters.PageNumber);

            var saleItemDtos = _mapper.Map<List<SaleItemDto>>(saleItems);

            var paginatedResult = new PaginatedList<SaleItemDto>(saleItemDtos, saleItems.TotalCount, saleItems.CurrentPage, saleItems.PageSize);

            return paginatedResult.ToResponse();
        }

        public IEnumerable<SaleItemDto> GetAllSaleItems()
        {
            var saleItems = _context.SaleItems.ToList();

            return _mapper.Map<IEnumerable<SaleItemDto>>(saleItems) ?? Enumerable.Empty<SaleItemDto>();
        }

        public IEnumerable<SaleItemDto> GetSalesSaleItems(int salesId)
        {
            var salesSaleItems = _context.SaleItems
                .Include(x => x.Product)
                .IgnoreAutoIncludes()
                .Where(x => x.SaleId == salesId).
                ToList();

            return _mapper.Map<IEnumerable<SaleItemDto>>(salesSaleItems) ?? Enumerable.Empty<SaleItemDto>();
        }

        public SaleItemDto? GetSaleItemById(int id)
        {
            var saleItem = _context.SaleItems.FirstOrDefault(x => x.Id == id);

            var saleItemDto = _mapper.Map<SaleItemDto>(saleItem);

            return saleItemDto;
        }

        public SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate)
        {
            var saleItemEntity = _mapper.Map<SaleItem>(saleItemToCreate);

            _context.SaleItems.Add(saleItemEntity);
            _context.SaveChanges();

            var saleItemDto = _mapper.Map<SaleItemDto>(saleItemEntity);

            return saleItemDto;
        }

        public void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate)
        {
            var saleItemEntity = _mapper.Map<SaleItem>(saleItemToUpdate);

            _context.SaleItems.Update(saleItemEntity);
            _context.SaveChanges();
        }

        public void DeleteSaleItem(int id)
        {
            var saleItem = _context.SaleItems.FirstOrDefault(x => x.Id == id);
            if (saleItem is not null)
            {
                _context.SaleItems.Remove(saleItem);
            }
            _context.SaveChanges();
        }
    }
}
