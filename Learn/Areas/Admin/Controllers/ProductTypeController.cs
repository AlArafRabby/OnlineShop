using Learn.Data;
using Learn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Learn.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin/[controller]")]
    //[Authorize(Roles = "Admin")]

    public class ProductTypeController : Controller
    {
        private readonly OnlineShopJoyContext _context;

        public ProductTypeController(OnlineShopJoyContext context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_context.ProductTypes.ToList());
        }

        //AddOrEdit Get Method
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //AddOrEdit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Learn.Models.ProductType employeeData)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Add(employeeData);
                await _context.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(employeeData);


            //bool IsEmployeeExist = false;

            //Learn.Models.ProductType employee = await _context.ProductTypes.FindAsync(Id);

            //if (employee != null)
            //{
            //    IsEmployeeExist = true;
            //}
            //else
            //{
            //    employee = new Learn.Models.ProductType();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        employee.Id = employeeData.Id;
            //        employee.ProductType1 = employeeData.ProductType1;
            //        //employee.Address = employeeData.Address;
            //        //employee.Salary = employeeData.Salary;
            //        //employee.JoiningDate = employeeData.JoiningDate;

            //        if (IsEmployeeExist)
            //        {
            //            _context.Update(employee);
            //        }
            //        else
            //        {
            //            _context.Add(employee);
            //        }
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        throw;
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(employeeData);
        }


        // Employee Details
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var employee = await _context.ProductTypes.FirstOrDefaultAsync(m => m.Id == Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employees/Delete/1
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var employee = await _context.ProductTypes.FirstOrDefaultAsync(m => m.Id == Id);

            return View(employee);
        }

        // POST: Employees/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var employee = await _context.ProductTypes.FindAsync(Id);
            _context.ProductTypes.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
