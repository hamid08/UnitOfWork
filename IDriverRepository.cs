using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RohaniShop.ViewModels.Driver;

namespace RohaniShop.Data.Contracts
{
   public interface IDriverRepository
    {
        Task<List<DriversViewModel>> GetPaginateDriversAsync(int offset, int limit, string orderBy,
            string searchText);

        Task<List<DriversViewModel>> FinAllDriversAsync();
    }
}
