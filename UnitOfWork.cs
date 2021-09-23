using AutoMapper;
using Microsoft.Extensions.Configuration;
using RohaniShop.Data.Contracts;
using RohaniShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RohaniShop.Data;

namespace RohaniShop.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public RohaniShopDBContext _Context { get; }
        private IMapper _mapper;
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;
        private IDriverRepository _driverRepository;
        private IFactorRepository _factorRepository;
        private IFactorDriverRepository _factorDriverRepository;
        private readonly IConfiguration _configuration;

        public UnitOfWork(RohaniShopDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _Context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class
        {
            IBaseRepository<TEntity> repository = new BaseRepository<TEntity,RohaniShopDBContext>(_Context);
            return repository;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_Context);

                return _customerRepository;
            }
        }
        public IFactorDriverRepository FactorDriverRepository
        {
            get
            {
                if (_factorDriverRepository == null)
                    _factorDriverRepository = new FactorDriverRepository(_Context);

                return _factorDriverRepository;
            }
        }
        public IDriverRepository DriverRepository
        {
            get
            {
                if (_driverRepository == null)
                    _driverRepository = new DriverRepository(_Context);

                return _driverRepository;
            }
        } 
        public IFactorRepository FactorRepository
        {
            get
            {
                if (_factorRepository == null)
                    _factorRepository = new FactorRepository(_Context);

                return _factorRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_Context);

                return _productRepository;
            }
        }

        public async Task Commit()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
