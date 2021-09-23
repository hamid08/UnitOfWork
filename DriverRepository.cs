using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RohaniShop.Common;
using RohaniShop.Data.Contracts;
using RohaniShop.Entities.identity;
using RohaniShop.ViewModels.Customer;
using RohaniShop.ViewModels.Driver;

namespace RohaniShop.Data.Repositories
{
    public class DriverRepository:IDriverRepository
    {
        private readonly RohaniShopDBContext _context;

        public DriverRepository(RohaniShopDBContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));

        }

        public async Task<List<DriversViewModel>> GetPaginateDriversAsync(int offset, int limit, string orderBy, string searchText)
        {
            var getDateTimesForSearch = searchText.GetDateTimeForSearch();
            var drivers = await _context.Drivers.Include(c => c.FactorDrivers).Include(c => c.User)
                .Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText) ||
                            t.Address.Contains(searchText) || t.PhoneNumber.Contains(searchText) ||
                            (t.RegisterDateTime >= getDateTimesForSearch.First() &&
                             t.RegisterDateTime <= getDateTimesForSearch.Last()))
                .OrderBy(orderBy)
                .Skip(offset).Take(limit)
                .Select(driver => new DriversViewModel
                {
                     DriverId = driver.DriverId,
                    PhoneNumber = driver.PhoneNumber,
                    FirstName = driver.FirstName,
                    LastName = driver.LastName,
                    IsActive = driver.IsActive,
                    Image = driver.Image,
                    Address = driver.Address,
                    RegistrantUser = driver.User.FirstName + " " + driver.User.LastName,
                    PersianBirthDate = driver.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
                    PersianRegisterDateTime = driver.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"),
                    GenderName = driver.Gender == GenderType.Male ? "مرد" : "زن",
                    FactorCount = driver.FactorDrivers.Count()
                }).AsNoTracking().ToListAsync();

            foreach (var item in drivers)
                item.Row = ++offset;

            return drivers;
        }

        public async Task<List<DriversViewModel>> FinAllDriversAsync()
        {
            var drivers = await _context.Drivers
                .Select(driver => new DriversViewModel
                {
                    DriverId = driver.DriverId,
                    PhoneNumber = driver.PhoneNumber,
                    FirstName = driver.FirstName,
                    LastName = driver.LastName,
                    IsActive = driver.IsActive,
                    Image = driver.Image,
                    Address = driver.Address,
                    RegistrantUser = driver.User.FirstName + " " + driver.User.LastName,
                    PersianBirthDate = driver.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
                    PersianRegisterDateTime = driver.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"),
                    GenderName = driver.Gender == GenderType.Male ? "مرد" : "زن",
                    FactorCount = driver.FactorDrivers.Count(),
                    DriverFullName = driver.FirstName+" "+driver.LastName
                }).AsNoTracking().ToListAsync();

         

            return drivers;
        }
    }
}
