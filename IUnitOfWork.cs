using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RohaniShop.Data.Contracts
{
    public interface IUnitOfWork
    {
        IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;
        ICustomerRepository CustomerRepository { get; }
        IDriverRepository DriverRepository { get; }
        IProductRepository ProductRepository { get; }
        IFactorRepository FactorRepository { get; }
        IFactorDriverRepository FactorDriverRepository { get; }
        RohaniShopDBContext _Context { get; }
        Task Commit();
    }
}
