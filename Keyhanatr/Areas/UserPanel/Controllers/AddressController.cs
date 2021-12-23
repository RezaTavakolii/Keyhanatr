using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class AddressController : Controller
    {
        private KeyhanatrContext _Context;

        public AddressController(KeyhanatrContext context)
        {
            _Context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _Context.Addresses.ToListAsync());
        }
        private bool AddressExists(int id)
        {
            return _Context.Addresses.Any(a => a.AddressID == id);
        }

        #region Create
        public ActionResult Create(int id)
        {
            ViewBag.userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(new Address()
            {
                UserId = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(address);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == null)
            {
                return NotFound();
            }
            var address = await _Context.Addresses.FirstOrDefaultAsync(a => a.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressID,UserId,Name,Family,Company,Ostan,Shahr,Adress,CodePosti,Mobile")] Address address)
        {
            if (id != address.AddressID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(address);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var address = await _Context.Addresses.FirstOrDefaultAsync(a => a.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _Context.Addresses.FindAsync(id);
            _Context.Addresses.Remove(address);
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var address = await _Context.Addresses.FirstOrDefaultAsync(a => a.AddressID == id);

            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }
    }
}
