using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supply;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Data.Common;

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
            try
            {
                var supplies = _context.Supplies.ToList();

                var supplyDtos = _mapper.Map<IEnumerable<SupplyDto>>(supplies);

                return supplyDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supply and SupplyDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while fetching supply.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching supply.", ex.Message);
                throw;
            }
        }

        public SupplyDto? GetSupplyById(int id)
        {
            try
            {
                var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);

                var supplyDto = _mapper.Map<SupplyDto>(supply);

                return supplyDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supply and SupplyDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching supply with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching supply with id: {id}.", ex.Message);
                throw;
            }
        }

        public SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate)
        {
            try
            {
                var supplyEntity = _mapper.Map<Supply>(supplyToCreate);

                _context.Supplies.Add(supplyEntity);
                _context.SaveChanges();

                var supplyDto = _mapper.Map<SupplyDto>(supplyEntity);

                return supplyDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supply and SupplyDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new supply.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new supply.", ex.Message);
                throw;
            }
        }

        public void UpdateSupply(SupplyForUpdateDto supplyToUpdate)
        {
            try
            {
                var supplyEntity = _mapper.Map<Supply>(supplyToUpdate);

                _context.Supplies.Update(supplyEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Supply and SupplyDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating supply.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating supply.", ex.Message);
                throw;
            }
        }

        public void DeleteSupply(int id)
        {
            try
            {
                var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);
                if (supply is not null)
                {
                    _context.Supplies.Remove(supply);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting supply with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting supply with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
