﻿using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using Inflow.Core.SaleItem;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISaleItemService
    {
        IEnumerable<SaleItemDto> GetAllSaleItems();
        IEnumerable<SaleItemDto> GetSalesSaleItems(int salesId);
        GetBaseResponse<SaleItemDto> GetSaleItems(SaleItemResourceParameters saleItemResourceParameters);
        SaleItemDto? GetSaleItemById(int id);
        SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate);
        void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate);
        void DeleteSaleItem(int id);
    }
}