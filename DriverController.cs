using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RohaniShop.Common;
using RohaniShop.Data.Contracts;
using RohaniShop.Entities;
using RohaniShop.Services.Contracts;
using RohaniShop.ViewModels.Customer;
using RohaniShop.ViewModels.Driver;
using RohaniShop.ViewModels.DynamicAccess;

namespace RohaniShop.Areas.Admin.Controllers
{
    [DisplayName("مدیریت راننده ها")]
    public class DriverController : BaseController
    {
        private readonly IUnitOfWork _uw;
        private readonly IMapper _mapper;
        private readonly IApplicationUserManager _userManager;
        private const string DriverNotFound = "راننده یافت نشد.";

        public DriverController(IUnitOfWork uw, IMapper mapper,IApplicationUserManager userManager)
        {
            _uw = uw;
            _uw.CheckArgumentIsNull(nameof(_uw));

            _mapper = mapper;
            _mapper.CheckArgumentIsNull(nameof(_mapper));

            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));
        }
        [HttpGet, DisplayName("مشاهده")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDrivers(string search, string order, int offset, int limit, string sort)
        {
            List<DriversViewModel> allDrivers;

            int total = _uw._Context.Customers.Count();

            if (string.IsNullOrWhiteSpace(search))
                search = "";

            if (limit == 0)
                limit = total;

            if (sort == "نام")
            {
                if (order == "asc")
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "FirstName", search);
                else
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "FirstName desc", search);
            }

            else if (sort == "نام خانوادگی")
            {
                if (order == "asc")
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "LastName", search);
                else
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "LastName desc", search);
            }

            else if (sort == "شماره تماس")
            {
                if (order == "asc")
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "PhoneNumber", search);
                else
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "PhoneNumber desc", search);
            }

            else if (sort == "تاریخ عضویت")
            {
                if (order == "asc")
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "RegisterDateTime", search);
                else
                    allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "RegisterDateTime desc", search);
            }

            else
                allDrivers = await _uw.DriverRepository.GetPaginateDriversAsync(offset, limit, "RegisterDateTime desc", search);

            if (search != "")
                total = allDrivers.Count();

            return Json(new { total = total, rows = allDrivers });
        }

        [HttpGet, DisplayName("درج و ویرایش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RenderDriver(int? driverId)
        {
            var driver = new DriversViewModel();

            if (driverId != null)
            {
                driver = _mapper.Map<DriversViewModel>(await _uw.BaseRepository<Driver>().FindByIdAsync(driverId));
                driver.PersianBirthDate = driver.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd");
            }

            return PartialView("_RenderDriver", driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(DriversViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                if (viewModel.DriverId != null)
                {
                    var driver = await _uw.BaseRepository<Driver>().FindByIdAsync(viewModel.DriverId);
                    if (driver != null)
                    {
                        driver.FirstName = viewModel.FirstName;
                        driver.LastName = viewModel.LastName;
                        driver.Gender = viewModel.Gender;
                        driver.PhoneNumber = viewModel.PhoneNumber;
                        driver.Address = viewModel.Address;
                        driver.DriverId = (int)viewModel.DriverId;
                        _uw.BaseRepository<Driver>().Update(driver);
                        //_uw.BaseRepository<Driver>().Update(_mapper.Map(viewModel, driver));
                        TempData["notification"] = EditSuccess;
                    }
                    else
                        ModelState.AddModelError(string.Empty, DriverNotFound);

                }

                else
                {
                    

                    viewModel.UserId =  _userManager.GetUserAsync(User).Result.Id;
                    await _uw.BaseRepository<Driver>().CreateAsync(_mapper.Map<Driver>(viewModel));

                    TempData["notification"] = InsertSuccess;

                }


                await _uw.Commit();

            }

            return PartialView("_RenderDriver", viewModel);
        }


        [HttpGet, DisplayName("حذف")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(int? driverId)
        {
            if (driverId == 0)
                ModelState.AddModelError(string.Empty, DriverNotFound);
            else
            {
                var driver = await _uw.BaseRepository<Driver>().FindByIdAsync(driverId);
                if (driver == null)
                    ModelState.AddModelError(string.Empty, DriverNotFound);
                else
                    return PartialView("_DeleteConfirmation", driver);
            }
            return PartialView("_DeleteConfirmation");
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Driver model)
        {
            var driver = await _uw.BaseRepository<Driver>().FindByIdAsync(model.DriverId);
            if (driver == null)
                ModelState.AddModelError(string.Empty, DriverNotFound);
            else
            {
                _uw.BaseRepository<Driver>().Delete(driver);
                await _uw.Commit();

                TempData["notification"] = DeleteSuccess;
                return PartialView("_DeleteConfirmation", driver);

            }

            return PartialView("_DeleteConfirmation");
        }

        [HttpPost, ActionName("DeleteGroup"), DisplayName("حذف گروهی")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> DeleteGroupConfirmed(int[] btSelectItem)
        {
            if (!btSelectItem.Any())
                ModelState.AddModelError(string.Empty, "هیچ راننده ای برای حذف انتخاب نشده است.");
            else
            {
                foreach (var item in btSelectItem)
                {
                    var driver = await _uw.BaseRepository<Driver>().FindByIdAsync(item);
                    _uw.BaseRepository<Driver>().Delete(driver);

                }
                TempData["notification"] = "حذف گروهی اطلاعات با موفقیت انجام شد.";
                await _uw.Commit();
            }

            return PartialView("_DeleteGroup");
        }
    }
}
