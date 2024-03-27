using AutoMapper;
using MarketUz.Domain.DTOs.Supply;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using MarketUz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiyorMarket.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;

        public SupplyService(IMapper mapper, MarketUzDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetBaseResponse<SupplyDto> GetSupplies(
            SupplyResourceParameters supplyResourceParameters)
        {
            var query = GetQuerySupplyResParameters(supplyResourceParameters);

            var supplies = query.ToPaginatedList(supplyResourceParameters.PageSize, supplyResourceParameters.PageNumber);

            foreach (var supply in supplies)
            {
                supply.Supplier = _context.Suppliers.FirstOrDefault(x => x.Id == supply.SupplierId);
            }

            var supplyDtos = _mapper.Map<IEnumerable<SupplyDto>>(supplies);

            var paginatedResult = new PaginatedList<SupplyDto>(supplyDtos.ToList(), supplies.TotalCount, supplies.CurrentPage, supplies.PageSize);

            return paginatedResult.ToResponse();
        }

        public IEnumerable<SupplyDto> GetAllSupplies()
        {
            var supplies = _context.Supplies.ToList();

            return _mapper.Map<IEnumerable<SupplyDto>>(supplies) ?? Enumerable.Empty<SupplyDto>();
        }

        public SupplyDto? GetSupplyById(int id)
        {
            var supply = _context.Supplies
                .Include(s => s.Supplier)
                .FirstOrDefault(x => x.Id == id);

            var supplyDto = _mapper.Map<SupplyDto>(supply);

            return supplyDto;
        }

        public SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate)
        {
            var supplyEntity = _mapper.Map<Supply>(supplyToCreate);

            foreach (var supplyItem in supplyEntity.SupplyItems)
            {
                var item = _context.Products.FirstOrDefault(x => x.Id == supplyItem.ProductId);

                if (item is null)
                {
                    throw new EntityNotFoundException($"Product with id: {supplyItem.ProductId} does not exist");
                }

                item.QuantityInStock += supplyItem.Quantity;
            }

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

        private IQueryable<Supply> GetQuerySupplyResParameters(
            SupplyResourceParameters supplyResourceParameters)
        {
            var query = _context.Supplies
                    .Include(x => x.SupplyItems)
                    .IgnoreAutoIncludes()
                    .AsNoTracking()
                    .AsQueryable();

            if (supplyResourceParameters.SupplyDate is not null)
            {
                query = query.Where(x => x.SupplyDate.Date
                >= supplyResourceParameters.SupplyDate.Value.Date
                && x.SupplyDate.Date <= DateTime.Now.Date);
            }

            if (supplyResourceParameters.SupplierId is not null)
            {
                query = query.Where(x => x.SupplierId == supplyResourceParameters.SupplierId);
            }

            if (!string.IsNullOrEmpty(supplyResourceParameters.OrderBy))
            {
                query = supplyResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "id" => query.OrderBy(x => x.Id),
                    "iddesc" => query.OrderByDescending(x => x.Id),
                    "supplydate" => query.OrderBy(x => x.SupplyDate),
                    "supplydatedesc" => query.OrderByDescending(x => x.SupplyDate),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            return query;
        }
    }
}
