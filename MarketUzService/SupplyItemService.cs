using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Data.Common;

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
            try
            {
                var supplyItems = _context.SupplyItems.ToList();

                var supplyItemDtos = _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItems);

                return supplyItemDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between SupplyItems and SupplyItemDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while fetching supplyitems.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching supplyitems.", ex.Message);
                throw;
            }
        }

        public SupplyItemDto? GetSupplyItemById(int id)
        {
            try
            {
                var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);

                var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItem);

                return supplyItemDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between SupplyItem and SupplyItemDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching supplyItem with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching supplyItem with id: {id}.", ex.Message);
                throw;
            }
        }

        public SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate)
        {
            try
            {
                var supplyItemEntity = _mapper.Map<SupplyItem>(supplyItemToCreate);

                _context.SupplyItems.Add(supplyItemEntity);
                _context.SaveChanges();

                var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItemEntity);

                return supplyItemDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between SupplyItem and SupplyItemDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new supplyItem.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new supplyItem.", ex.Message);
                throw;
            }
        }

        public void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate)
        {
            try
            {
                var supplyItemEntity = _mapper.Map<SupplyItem>(supplyItemToUpdate);

                _context.SupplyItems.Update(supplyItemEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between SupplyItem and SupplyItemDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating supplyItem.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating supplyItem.", ex.Message);
                throw;
            }
        }

        public void DeleteSupplyItem(int id)
        {
            try
            {
                var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);
                if (supplyItem is not null)
                {
                    _context.SupplyItems.Remove(supplyItem);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting supplyItem with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting supplyItem with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
