using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserInfoesController : Controller
    {
        private KeyhanatrContext _Context;

        public UserInfoesController(KeyhanatrContext context)
        {
            _Context = context;
        }

       

        public async Task<IActionResult> Index()
        {
            return View(await _Context.UserInfos.ToListAsync());
        }
        public bool IsNewUserInfo(int userId)
        {
            return _Context.UserInfos.Any(u => u.UserId == userId);
        }

        #region Create
        public ActionResult Create(int id)
        {
            ViewBag.userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(new UserInfo()
            {
                UserId = id
            }) ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserInfoID,UserId,Name,Family,ImageName,Email,PhoneDaftar")] UserInfo userInfo, IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                userInfo.ImageName = "default.jpg";
                if (imgUp != null)
                {
                    userInfo.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserInfoImages",
                        userInfo.ImageName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imgUp.CopyTo(stream);
                    }
                }
                _Context.Add(userInfo);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
        #endregion
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserInfoID,UserId,Name,Family,ImageName,Email,PhoneDaftar")] UserInfo userinfo, IFormFile imgUp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!IsNewUserInfo(userinfo.UserId))
        //        {
        //            userinfo.ImageName = "default.jpg";
        //            if (imgUp != null)
        //            {
        //                userinfo.ImageName = Guid.NewGuid().ToString().Replace("-", "")
        //                                            + Path.GetExtension(imgUp.FileName);
        //                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserInfoImages",
        //                    userinfo.ImageName);

        //                using (var stream = new FileStream(savePath, FileMode.Create))
        //                {
        //                    imgUp.CopyTo(stream);
        //                }
        //            }
        //            _Context.Add(userinfo);
        //        }
        //        else
        //        {
        //            return RedirectToAction(nameof(Edit), new { id = userinfo.UserId.ToString() });
        //        }
        //        await _Context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userinfo);
        //}


        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == null)
            {
                return NotFound();
            }
            var userinfo = await _Context.UserInfos.FirstOrDefaultAsync(a => a.UserInfoID == id);
            if (userinfo == null)
            {
                return NotFound();
            }
            return View(userinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserInfoID,UserId,Name,Family,ImageName,Email,PhoneDaftar")] UserInfo userinfo, IFormFile imgUp)
        {
            if (id != userinfo.UserInfoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgUp != null)
                    {
                        if (userinfo.ImageName != "default.jpg")
                        {
                            string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserInfoImages", userinfo.ImageName);
                            if (System.IO.File.Exists(oldImagePath))
                                System.IO.File.Delete(oldImagePath);
                        }
                        userinfo.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserInfoImages",
                            userinfo.ImageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            imgUp.CopyTo(stream);
                        }
                    }
                    _Context.Update(userinfo);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsNewUserInfo(userinfo.UserInfoID))
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
            return View(userinfo);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userinfo = await _Context.UserInfos.FirstOrDefaultAsync(a => a.UserInfoID == id);
            if (userinfo == null)
            {
                return NotFound();
            }
            return View(userinfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userinfo = await _Context.UserInfos.FindAsync(id);
            _Context.UserInfos.Remove(userinfo);
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
            var userinfo = await _Context.UserInfos.FirstOrDefaultAsync(a => a.UserInfoID == id);

            if (userinfo == null)
            {
                return NotFound();
            }
            return View(userinfo);
        }
    }
}


//if (ModelState.IsValid)
//{
//    userinfo.ImageName = "default.jpg";
//    if (imgUp != null)
//    {
//        userinfo.ImageName = Guid.NewGuid().ToString().Replace("-", "")
//                                    + Path.GetExtension(imgUp.FileName);
//        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserInfoImages",
//            userinfo.ImageName);

//        using (var stream = new FileStream(savePath, FileMode.Create))
//        {
//            imgUp.CopyTo(stream);
//        }
//    }
//    _Context.Add(userinfo);
//    await _Context.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}
//return View(userinfo);