using DiyorMarket.Domain.DTOs.SupllyItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISupplyItemService
    {
        IEnumerable<SupplyItemDto> GetSupplyItems();
        SupplyItemDto GetSupplyItem(int id);
        SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto createDto);
        void UpdateSupplyItem(SupplyItemForUpdateDto updateDto);
        void DeleteSupplyItem(int id);
    }
}
