using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialPortal.Data;
using SocialPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SocialPortal.Controllers
{
    public class EventController : Controller
    {
        ApplicationDbContext db;
        private readonly IHostingEnvironment _appEnvironment;
        private UserManager<ApplicationUser> _userManager;

        public EventController(ApplicationDbContext _db, IHostingEnvironment appEnvironment, UserManager<ApplicationUser> userManager)
        {
            db = _db;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        // GET: Event
        public ActionResult Index()
        {
            var Eventy = db.Eventy.Where(x => x.verified == true).ToArray();
            return View("~/Views/Event/ListEvent.cshtml",Eventy);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            Event ev = db.Eventy.Where(x => x.ID == id).FirstOrDefault();

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);
            if (ev != null)
            {
                if (ev.Adder == idk)
                {
                    ev.ConfirmAdder = true;
                }
                else
                {
                    ev.ConfirmAdder = false;
                }

                ev.Adder = db.Users.Where(x => x.Id == ev.Adder).FirstOrDefault().UserName;
                return View(ev);
            }
            return View();
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View("~/Views/Event/CreateEvent.cshtml");
        }

        private string ProcessImage(string croppedImage)

        {

            string filePath = String.Empty;

            try

            {

                int i = 0;


                var files = HttpContext.Request.Form.Files;
                //foreach (var Image in files)
                //{
                string base64 = croppedImage;
                byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
                var Image = bytes;
                var file = Image;
                if (Image != null && Image.Length > 0)
                {

                    var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    filePath = filePath + fileName + ";";

                    using (FileStream stream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))

                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                i++;
                //}

            }

            catch (Exception ex)

            {

                string st = ex.Message;

            }

            return filePath;

        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                string PhotosToDatabaseLink = String.Empty;
                if (ModelState.IsValid)
                {
                    //var files = HttpContext.Request.Form.Files;
                    //foreach (var Image in files)
                    //{
                    //    if (Image != null && Image.Length > 0)
                    //    {
                    //        var file = Image;
                    //        //There is an error here
                    //        var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
                    //        if (file.Length > 0)
                    //        {
                    //            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    //            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    //            {
                    //                await file.CopyToAsync(fileStream);
                    //                PhotosToDatabaseLink = PhotosToDatabaseLink + fileName + ";";
                    //            }

                    //        }
                    //    }
                    //}
                    int counter = 0;
                    foreach (var cs in collection)
                    {
                        if (cs.Key.Contains("avatarCropped")) counter++;
                    }
                    for (int i = 1; i <= counter; i++)
                    {
                        string avatar = "avatarCropped" + i.ToString();
                        PhotosToDatabaseLink = PhotosToDatabaseLink + ProcessImage(collection[avatar]);
                    }

                }

                Event even = new Event();
                even.Name = Convert.ToString(collection["wydarzenie.Name"]);

                var test = db.Eventy.Where(x => x.Name == even.Name).FirstOrDefault();
                if (test != null) return RedirectToAction("Index", "Home");

                even.Place = Convert.ToString(collection["wydarzenie.Place"]);
                even.Artist = Convert.ToString(collection["wydarzenie.Artist"]);
                even.Date = Convert.ToDateTime(collection["wydarzenie.Date"]);
                even.URL = Convert.ToString(collection["wydarzenie.URL"]);
                even.Photos = PhotosToDatabaseLink;
                even.Youtube = Convert.ToString(collection["wydarzenie.Youtube"]);
                even.Hour = Convert.ToDateTime(collection["wydarzenie.Hour"]);
                even.Price = Convert.ToInt32(collection["wydarzenie.Price"]);
                even.Description = Convert.ToString(collection["wydarzenie.Description"]);


                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var id = _userManager.GetUserId(User);
                if (id == null) return RedirectToAction("Index", "Home");
                even.Adder = id;

                db.Eventy.Add(even);
                db.SaveChanges();

               return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Event/Edit/5
       
        public ActionResult Edit(int id)
        {
            Event even =  db.Eventy.Where(x => x.ID == id).FirstOrDefault();

            return View("~/Views/Event/EditEvent.cshtml", even);
        }

        // POST: Event/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Event even = db.Eventy.Where(x => x.ID == id).FirstOrDefault();
                if (even!=null)
                {
                    System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                    var idk = _userManager.GetUserId(User);
                    if (!currentUser.IsInRole("Admin") && even.Adder != idk) RedirectToAction("Index", "Home");

                    even.Name = Convert.ToString(collection["Name"]);
                    even.Place = Convert.ToString(collection["Place"]);
                    even.Date = Convert.ToDateTime(collection["Date"]);
                    even.URL = Convert.ToString(collection["URL"]);
                    even.Artist = Convert.ToString(collection["Artist"]);
                   
                    even.Youtube = Convert.ToString(collection["Youtube"]);
                    even.Hour = Convert.ToDateTime(collection["Hour"]);
                    even.Price = Convert.ToInt32(collection["Price"]);
                    even.Description = Convert.ToString(collection["Description"]);


                    string PhotosToDatabaseLink = String.Empty;
                    int counter = 0;
                    foreach (var cs in collection)
                    {
                        if (cs.Key.Contains("avatarCropped")) counter++;
                    }
                    for (int i = 1; i <= counter; i++)
                    {
                        string avatar = "avatarCropped" + i.ToString();
                        PhotosToDatabaseLink = PhotosToDatabaseLink + ProcessImage(collection[avatar]);
                    }

                    if (PhotosToDatabaseLink == String.Empty)
                    {
                        even.Photos = collection["Photos"];
                    }
                    else
                    {
                        even.Photos = PhotosToDatabaseLink;
                    }

                    var temp = collection["verified"];
                    
                    if (currentUser.IsInRole("Admin"))
                    {
                        if (temp.Count == 2)
                        {
                            even.verified = true;
                        }
                        else
                        {
                            even.verified = false;
                        }
                    }
                    else
                    {
                        even.verified = false;
                    }

                    db.Entry(even).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (even.verified == true)
                {
                    Startup.Who = even.Adder;
                    Startup.ID = even.ID;
                    Startup.What = "podziel";
                    Startup.Type = "event";
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Event/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Event even = db.Eventy.Where(x => x.ID == id).FirstOrDefault();

            return View("~/Views/Event/DeleteEvent.cshtml", even);
        }

        // POST: Event/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Event even = db.Eventy.Where(x => x.ID == id).FirstOrDefault();
                if (even != null)
                {
                    db.Eventy.Remove(even);
                    db.SaveChanges();
                }

               return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult Favorite(string a, int id)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            Event ev = db.Eventy.Where(x => x.ID == id).FirstOrDefault();

            if (idk == null) return Json("Zaloguj się");
            if (ev.Liker != null && ev.Liker.Contains(idk)) return Json("Wydarzenie było wcześniej dodane do ulubionych");

            ev.Popularity = ev.Popularity + 1;

            ev.Liker = ev.Liker + idk + ";";

            db.SaveChanges();

            Startup.Who = idk;
            Startup.ID = ev.ID;
            Startup.What = "ulubione";
            Startup.Type = "event";

            return Json("Wydarzenie dodane do ulubionych");
        }

    }
}