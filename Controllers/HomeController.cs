using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialPortal.Models;
using SocialPortal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using SocialPortal.Models.ManageViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using SocialPortal.Model;
using System.Threading;
using Microsoft.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SocialPortal.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ApplicationDbContext _db, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, IHostingEnvironment appEnvironment)
        {
            db = _db;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
            _contextAccessor = contextAccessor;

        }

        public IActionResult Index()
        {
            MainViewModel mvm = new MainViewModel();
            mvm.utwory = db.Utwory.Where(x => x.verified == true).Take(4).ToList();
            mvm.top10 = db.Utwory.OrderByDescending(x => x.Popularity).Where(x => x.verified == true).Take(10).ToList();

            List<Autor> autorzy = db.Autorzy.Where(x => x.verified == true).ToList();
            List<Zespol> zespoly = db.Zespoly.Where(x => x.verified == true).ToList();
            List<artysta> artysci = new List<artysta>();
            artysta art = new artysta();

            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                artysci.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                artysci.Add(art);
                art = new artysta();
            }

            mvm.wykonawcy = artysci.Take(6).ToList();

            mvm.wydarzenia = db.Eventy.Where(x => x.verified == true).Take(6).ToList();
            mvm.relacje = db.Relacje.Where(x => x.verified == true).Take(6).ToList();
            mvm.albumy = db.Albumy.Where(x => x.verified == true).Take(6).ToList();
            slider sl = new slider();
            using (StreamReader readtext = new StreamReader(Path.Combine(_appEnvironment.WebRootPath, "images") + "/SliderConf.txt"))
            {
                sl.title1 = readtext.ReadLine();
                sl.title2 = readtext.ReadLine();
                sl.title3 = readtext.ReadLine();
                sl.desc1 = readtext.ReadLine();
                sl.desc2 = readtext.ReadLine();
                sl.desc3 = readtext.ReadLine();
                sl.link1 = readtext.ReadLine();
                sl.link2 = readtext.ReadLine();
                sl.link3 = readtext.ReadLine();
            }

            mvm.slide = sl;

            return View(mvm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Podziel()
        {
            return View("~/Views/Home/Podziel.cshtml");
        }




        public IActionResult Wydarzenia()
        {
            WydarzenieViewModel wvm = new WydarzenieViewModel();
            wvm.nadchodząceWydarzenia = db.Eventy.Where(x => x.Date > DateTime.Now).Take(10).ToList();
            wvm.poprzednieWydarzenia = db.Eventy.Where(x => x.Date < DateTime.Now).Take(10).ToList();
            wvm.relacjeZWydarzen = db.Relacje.Take(10).ToList();
            wvm.Wydarzenia = db.Eventy.ToList();

            List<String> wykonawcy = new List<String>();
            wvm.Date = DateTime.Now.AddYears(-5);
            List<Autor> autorzy = db.Autorzy.ToList();
            List<Zespol> zespoly = db.Zespoly.ToList();
            foreach (var wyko in autorzy)
            {
                wykonawcy.Add(wyko.Name);
            }
            foreach (var wyko in zespoly)
            {
                wykonawcy.Add(wyko.Name);
            }
            wvm.wykonawcy = wykonawcy;

            List<String> miejsca = new List<String>();
            List<Event> eventy = db.Eventy.ToList();
            foreach (var ev in eventy)
            {
                miejsca.Add(ev.Place);
            }
            wvm.miejsce = miejsca;

            return View("~/Views/Home/Wydarzenia.cshtml", wvm);
        }

        public IActionResult WydarzeniaWiecej()
        {
            WydarzenieViewModel wvm = new WydarzenieViewModel();
            wvm.Wydarzenia = db.Eventy.ToList();

            List<String> wykonawcy = new List<String>();
            wvm.Date = DateTime.Now.AddYears(-5);
            List<Autor> autorzy = db.Autorzy.ToList();
            List<Zespol> zespoly = db.Zespoly.ToList();
            foreach (var wyko in autorzy)
            {
                wykonawcy.Add(wyko.Name);
            }
            foreach (var wyko in zespoly)
            {
                wykonawcy.Add(wyko.Name);
            }
            wvm.wykonawcy = wykonawcy;

            List<String> miejsca = new List<String>();
            List<Event> eventy = db.Eventy.ToList();
            foreach (var ev in eventy)
            {
                miejsca.Add(ev.Place);
            }
            wvm.miejsce = miejsca;

            return View("~/Views/Home/WydarzeniaWiecej.cshtml", wvm);
        }





        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Recenzent")]
        public IActionResult Admin()
        {
            AdminViewModel avm = new AdminViewModel();
            avm.albumy = db.Albumy.ToList();
            avm.autorzy = db.Autorzy.ToList();
            avm.eventy = db.Eventy.ToList();
            avm.relacje = db.Relacje.ToList();
            avm.teksty = db.Teksty.ToList();
            avm.utwory = db.Utwory.ToList();
            avm.zespoly = db.Zespoly.ToList();
            return View("~/Views/Home/Admin.cshtml", avm);
        }

        private string ProcessImageSlider(string croppedImage, string counter)
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

                    var uploads = Path.Combine(_appEnvironment.WebRootPath, "images");
                    var fileName = counter + ".jpg";
                    //filePath = filePath + fileName + ";";

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

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Recenzent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZdjeciaSlider(IFormCollection collection)
        {
            //string PhotosToDatabaseLink = String.Empty;
            //if (ModelState.IsValid)
            //{
            //    var files = HttpContext.Request.Form.Files;
            //    foreach (var Image in files)
            //    {
            //        if (Image != null && Image.Length > 0)
            //        {
            //            var file = Image;
            //            var uploads = Path.Combine(_appEnvironment.WebRootPath, "images");
            //            if (file.Length > 0)
            //            {
            //                var fileName = file.FileName ;
            //                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            //                {
            //                    await file.CopyToAsync(fileStream);
            //                }
            //            }
            //        }
            //    }


            //}

            try
            {
                string PhotosToDatabaseLink = String.Empty;
                if (ModelState.IsValid)
                {

                    int counter = 0;
                    foreach (var cs in collection)
                    {
                        if (cs.Key.Contains("avatarCropped")) counter++;
                    }
                    for (int i = 1; i <= counter; i++)
                    {
                        string avatar = "avatarCropped" + i.ToString();
                        string coun = "baner" + i.ToString();
                        PhotosToDatabaseLink = PhotosToDatabaseLink + ProcessImageSlider(collection[avatar], coun);
                    }

                }
            }
            catch { }


            return RedirectToAction(nameof(Profil));
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Recenzent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SliderTeksty(IFormCollection collection)
        {
            using (StreamWriter writetext = new StreamWriter(Path.Combine(_appEnvironment.WebRootPath, "images") + "/SliderConf.txt"))
            {
                writetext.WriteLine(collection["title1"]);
                writetext.WriteLine(collection["title2"]);
                writetext.WriteLine(collection["title3"]);
                writetext.WriteLine(collection["desc1"]);
                writetext.WriteLine(collection["desc2"]);
                writetext.WriteLine(collection["desc3"]);
                writetext.WriteLine(collection["link1"]);
                writetext.WriteLine(collection["link2"]);
                writetext.WriteLine(collection["link3"]);
            }

            return RedirectToAction(nameof(Profil));
        }


        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Recenzent")]
        public IActionResult AdminNotVerified()
        {
            AdminViewModel avm = new AdminViewModel();
            avm.albumy = db.Albumy.Where(x => x.verified == false).ToList();
            avm.autorzy = db.Autorzy.Where(x => x.verified == false).ToList();
            avm.eventy = db.Eventy.Where(x => x.verified == false).ToList();
            avm.relacje = db.Relacje.Where(x => x.verified == false).ToList();
            avm.teksty = db.Teksty.Where(x => x.verified == false).ToList();
            avm.utwory = db.Utwory.Where(x => x.verified == false).ToList();
            avm.zespoly = db.Zespoly.Where(x => x.verified == false).ToList();
            return View("~/Views/Home/AdminNotVerified.cshtml", avm);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Recenzent")]
        public async Task<IActionResult> AdminRoles()
        {
            List<ApplicationUser> users = db.Users.ToList();

            return View("~/Views/Home/AdminRoles.cshtml", users);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Recenzent")]
        public ActionResult EditUser(string id)
        {
            ApplicationUser user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return View("~/Views/Home/EditUser.cshtml", user);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRoles(IFormCollection collection)
        {
            string norma = Convert.ToString(collection["NormalizedEmail"]).Split(",")[1];
            var user = db.Users.SingleOrDefault(x => x.NormalizedEmail == norma);

            if (!await _userManager.IsInRoleAsync(user, Convert.ToString(collection["NormalizedEmail"]))) ;
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                { _userManager.RemoveFromRoleAsync(user, "Admin"); }
                if (await _userManager.IsInRoleAsync(user, "Recenzent"))
                { _userManager.RemoveFromRoleAsync(user, "Recenzent"); }
                if (await _userManager.IsInRoleAsync(user, "User"))
                { _userManager.RemoveFromRoleAsync(user, "User"); }

                await _userManager.AddToRoleAsync(user, Convert.ToString(collection["NormalizedEmail"]).Split(",")[0]);
            }

            user.UserName = collection["UserName"];
            user.Email = collection["Email"];

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction(nameof(Profil));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profil(IFormCollection collection)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            ProfilViewModel pvm = new ProfilViewModel();
            pvm.user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            pvm.albumy = db.Albumy.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.relacje = db.Relacje.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.utwory = db.Utwory.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.wydarzenia = db.Eventy.Where(x => x.Liker.Contains(idk)).ToList();

            List<Autor> autorzy = db.Autorzy.Where(x => x.Fan.Contains(idk)).ToList();
            List<Zespol> zespoly = db.Zespoly.Where(x => x.Fan.Contains(idk)).ToList();
            List<artysta> artysci = new List<artysta>();
            artysta art = new artysta();

            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                artysci.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                artysci.Add(art);
                art = new artysta();
            }

            pvm.wykonawcy = artysci.ToList();

            return View("~/Views/Home/Profil.cshtml", pvm);
        }

        public async Task<IActionResult> Profil()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            ProfilViewModel pvm = new ProfilViewModel();
            pvm.user = db.Users.Where(x => x.Id == idk).FirstOrDefault();



            pvm.albumy = db.Albumy.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.relacje = db.Relacje.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.utwory = db.Utwory.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.wydarzenia = db.Eventy.Where(x => x.Liker.Contains(idk)).ToList();
            pvm.teksty = db.Teksty.Where(x => x.Liker.Contains(idk)).ToList();


            List<Autor> autorzy = db.Autorzy.Where(x => x.Fan.Contains(idk)).ToList();
            List<Zespol> zespoly = db.Zespoly.Where(x => x.Fan.Contains(idk)).ToList();
            List<artysta> artysci = new List<artysta>();
            artysta art = new artysta();

            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                artysci.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                artysci.Add(art);
                art = new artysta();
            }

            pvm.wykonawcy = artysci.ToList();


            pvm.albumyDodane = db.Albumy.Where(x => x.Adder.Contains(idk)).ToList();
            pvm.relacjeDodane = db.Relacje.Where(x => x.Adder.Contains(idk)).ToList();
            pvm.utworyDodane = db.Utwory.Where(x => x.Adder.Contains(idk)).ToList();
            pvm.wydarzeniaDodane = db.Eventy.Where(x => x.Adder.Contains(idk)).ToList();
            pvm.tekstyDodane = db.Teksty.Where(x => x.Adder.Contains(idk)).ToList();

            autorzy = db.Autorzy.Where(x => x.Adder.Contains(idk)).ToList();
            zespoly = db.Zespoly.Where(x => x.Adder.Contains(idk)).ToList();
            artysci = new List<artysta>();
            art = new artysta();

            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                artysci.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                artysci.Add(art);
                art = new artysta();
            }

            pvm.wykonawcyDodane = artysci;

            return View("~/Views/Home/Profil.cshtml", pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProfilName(IFormCollection collection)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);
            ApplicationUser appUser = db.Users.Where(x => x.Id == idk).FirstOrDefault();
            appUser.UserName = Convert.ToString(collection["au.UserName"]);
            db.Entry(appUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Profil));
            return View("~/Views/Home/Profil.cshtml");


        }

        public IActionResult Projekt()
        {
            return View();
        }
        public IActionResult Dziala()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult Regulamin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Kontakt()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Kontakt(IFormCollection form)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("", "");

            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("");

            smtpClient.Host = "";
            smtpClient.Port = 666;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = "Mail z portalu Łączy Nas Muzyka od " + form["email"];
            message.IsBodyHtml = true;
            message.Body = form["body"];
            message.To.Add("");
            message.To.Add("");

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message   ex.InnerException);
            }

            return View();
        }
        public IActionResult Wesprzyj()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> Ustawienia()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            UstawieniaViewModel uvm = new UstawieniaViewModel();
            uvm.au = db.Users.Where(x => x.Id == idk).FirstOrDefault();
            uvm.passViewModel = new ChangePasswordViewModel { StatusMessage = StatusMessage };

            return View(uvm);
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
                    filePath = filePath + fileName;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UstawieniaZdjecieProfilowe(IFormCollection collection)
        {
            string PhotosToDatabaseLink = String.Empty;
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        //There is an error here
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                PhotosToDatabaseLink = PhotosToDatabaseLink + fileName + ";";
                            }

                        }
                    }
                }
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

                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var idk = _userManager.GetUserId(User);

                ApplicationUser au = new ApplicationUser();
                au = db.Users.Where(x => x.Id == idk).FirstOrDefault();

                au.Photo = PhotosToDatabaseLink; db.Entry(au).State = EntityState.Modified; db.SaveChanges();

            }

            return RedirectToAction("Ustawienia", "Home", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UstawieniaZdjecieWtle(IFormCollection collection)
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

                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var idk = _userManager.GetUserId(User);

                ApplicationUser au = new ApplicationUser();
                au = db.Users.Where(x => x.Id == idk).FirstOrDefault();

                au.PhotoWtle = PhotosToDatabaseLink; db.Entry(au).State = EntityState.Modified; db.SaveChanges();

            }

            return RedirectToAction("Ustawienia", "Home", new { area = "Admin" });
        }

        public IActionResult Uzytkownicy()
        {
            List<ApplicationUser> users = db.Users.ToList();
            return View(users);
        }

        public IActionResult DetailsUser(string id)
        {


            ProfilViewModel pvm = new ProfilViewModel();
            pvm.user = db.Users.Where(x => x.Id == id).FirstOrDefault();

            pvm.albumy = db.Albumy.Where(x => x.Liker.Contains(id)).ToList();
            pvm.relacje = db.Relacje.Where(x => x.Liker.Contains(id)).ToList();
            pvm.utwory = db.Utwory.Where(x => x.Liker.Contains(id)).ToList();
            pvm.wydarzenia = db.Eventy.Where(x => x.Liker.Contains(id)).ToList();
            pvm.teksty = db.Teksty.Where(x => x.Liker.Contains(id)).ToList();


            List<Autor> autorzy = db.Autorzy.Where(x => x.Fan.Contains(id)).ToList();
            List<Zespol> zespoly = db.Zespoly.Where(x => x.Fan.Contains(id)).ToList();
            List<artysta> artysci = new List<artysta>();
            artysta art = new artysta();

            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                artysci.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                artysci.Add(art);
                art = new artysta();
            }

            pvm.wykonawcy = artysci.ToList();

            pvm.albumyDodane = db.Albumy.Where(x => x.Adder.Contains(id)).ToList();
            pvm.relacjeDodane = db.Relacje.Where(x => x.Adder.Contains(id)).ToList();
            pvm.utworyDodane = db.Utwory.Where(x => x.Adder.Contains(id)).ToList();
            pvm.wydarzeniaDodane = db.Eventy.Where(x => x.Adder.Contains(id)).ToList();
            pvm.tekstyDodane = db.Teksty.Where(x => x.Adder.Contains(id)).ToList();

            autorzy = db.Autorzy.Where(x => x.Adder.Contains(id)).ToList();
            zespoly = db.Zespoly.Where(x => x.Adder.Contains(id)).ToList();
            artysci = new List<artysta>();
            art = new artysta();

            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                artysci.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                artysci.Add(art);
                art = new artysta();
            }

            pvm.wykonawcyDodane = artysci;

            return View(pvm);

        }

        public IActionResult DodajZnajomego(string id)
        {
            ApplicationUser znajomy = db.Users.Where(x => x.Id == id.ToString()).FirstOrDefault();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            ApplicationUser user = new ApplicationUser();
            user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            if (user.Znajomi != null && user.Znajomi.Contains(id)) return RedirectToAction(nameof(Profil));

            user.Znajomi = user.Znajomi + znajomy.Id + ";";
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction(nameof(Profil));
            return View();
        }

        public IActionResult Znajomi(string id)
        {
            //ApplicationUser znajomy = db.Users.Where(x => x.Id == id.ToString()).FirstOrDefault();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            ApplicationUser user = new ApplicationUser();
            user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            ApplicationUser znajomy = new ApplicationUser();
            List<ApplicationUser> znajomii = new List<ApplicationUser>();
            if (user.Znajomi != null && user.Znajomi != string.Empty)
            {
                string[] IDznajomych = user.Znajomi.Split(';');
                foreach (var idik in IDznajomych)
                {
                    znajomy = db.Users.Where(x => x.Id == idik).FirstOrDefault();
                    if (znajomy != null)
                        znajomii.Add(znajomy);
                    znajomy = new ApplicationUser();
                }
            }
            return View(znajomii);
        }

        public IActionResult UtworyUser()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            //ApplicationUser user = new ApplicationUser();
            //user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            List<Utwor> utwory = db.Utwory.Where(x => x.Adder.Contains(idk)).ToList();
            return View(utwory);
        }

        public IActionResult WykonawcyUser()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            List<Autor> autorzy = db.Autorzy.Where(x => x.Adder.Contains(idk)).ToList();
            List<Zespol> zespoly = db.Zespoly.Where(x => x.Adder.Contains(idk)).ToList();

            List<artysta> dodaniPrzezUsera = new List<artysta>();
            artysta art = new artysta();
            foreach (var autor in autorzy)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Autor";

                dodaniPrzezUsera.Add(art);
                art = new artysta();
            }


            foreach (var autor in zespoly)
            {
                art.ID = autor.ID;
                art.Name = autor.Name;
                art.Photo = autor.Photos;
                art.Popularity = autor.Popularity;
                art.AddDate = autor.AddDate;
                art.Controller = "Zespol";

                dodaniPrzezUsera.Add(art);
                art = new artysta();
            }
            return View(dodaniPrzezUsera);
        }

        public IActionResult ChangeEmail(IFormCollection collection)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);
            var user = db.ApplicationUser.Where(x => x.Id == idk).FirstOrDefault();

            user.Email = collection["au.Email"];
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction(nameof(Profil));
        }

        public IActionResult UsunKonto()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);
            var user = db.ApplicationUser.Where(x => x.Id == idk).FirstOrDefault();

            if (user != null)
            {
                db.ApplicationUser.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult WydarzeniaUser()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            WydarzenieViewModel wvm = new WydarzenieViewModel();

            wvm.relacjeZWydarzen = db.Relacje.Where(x => x.Adder.Contains(idk)).ToList();
            wvm.Wydarzenia = db.Eventy.Where(x => x.Adder.Contains(idk)).ToList();

            List<String> wykonawcy = new List<String>();
            wvm.Date = DateTime.Now.AddYears(-5);
            List<Autor> autorzy = db.Autorzy.ToList();
            List<Zespol> zespoly = db.Zespoly.ToList();
            foreach (var wyko in autorzy)
            {
                wykonawcy.Add(wyko.Name);
            }
            foreach (var wyko in zespoly)
            {
                wykonawcy.Add(wyko.Name);
            }
            wvm.wykonawcy = wykonawcy;

            List<String> miejsca = new List<String>();
            List<Event> eventy = db.Eventy.ToList();
            foreach (var ev in eventy)
            {
                miejsca.Add(ev.Place);
            }
            wvm.miejsce = miejsca;
            return View(wvm);
        }

        public IActionResult TekstyUser()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            //ApplicationUser user = new ApplicationUser();
            //user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            List<Tekst> teksty = db.Teksty.Where(x => x.Adder.Contains(idk)).ToList();
            return View(teksty);
        }

        public IActionResult AlbumyUser()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);

            //ApplicationUser user = new ApplicationUser();
            //user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            List<Album> albumy = db.Albumy.Where(x => x.Adder.Contains(idk)).ToList();
            return View(albumy);
        }

        public string Notifications()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var idk = _userManager.GetUserId(User);
            ApplicationUser user = db.Users.Where(x => x.Id == idk).FirstOrDefault();

            string ret = string.Empty;
            if (Startup.What == "podziel")
            {
                if (Startup.Who != String.Empty)
                {
                    if (user.Znajomi.Contains(Startup.Who))
                    {
                        ApplicationUser au = db.Users.Where(x => x.Id == Startup.Who).FirstOrDefault();

                        if (Startup.Type == "utwor")
                        {
                            Utwor ut = db.Utwory.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String YouTubeFilmID = ut.URL.Replace("https://", "").Replace("http://", "").Replace("www.youtube.com/watch?v=", "").Replace("youtu.be/", "");
                            String YoutubeSRC = String.Format("https://www.youtube.com/embed/{0}", @YouTubeFilmID);


                            string frame = "<iframe src = '" + YoutubeSRC + "' frameborder = '0' allow = 'autoplay; encrypted - media' allowfullscreen style = 'width: 100%;' ></iframe>";
                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał utwór " + "<a href='/Utwor/Details/" + ut.ID + "'>" + ut.Name + "</a>" + " do portalu <br>" + frame + "<br>";
                        }

                        if (Startup.Type == "autor")
                        {
                            Autor aut = db.Autorzy.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = aut.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wykonawcę " + "<a href='/Autor/Details/" + aut.ID + "'>" + aut.Name + "</a>" + " do portalu <br>" + image + "<br>";
                        }

                        if (Startup.Type == "zespol")
                        {
                            Zespol zes = db.Zespoly.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = zes.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wykonawcę " + "<a href='/Zespol/Details/" + zes.ID + "'>" + zes.Name + "</a>" + " do portalu <br>" + image + "<br>";
                        }

                        if (Startup.Type == "event")
                        {
                            Event ev = db.Eventy.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = ev.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wydarzenie " + "<a href='/Event/Details/" + ev.ID + "'>" + ev.Name + "</a>" + " do portalu <br>" + image + "<br>";
                        }

                        if (Startup.Type == "relacja")
                        {
                            Relacja rel = db.Relacje.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = rel.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wykonawcę " + "<a href='/Relacja/Details/" + rel.ID + "'>" + rel.Name + "</a>" + " do portalu <br>" + image + "<br>";
                        }

                        if (Startup.Type == "tekst")
                        {
                            Tekst tek = db.Teksty.Where(x => x.ID == Startup.ID).FirstOrDefault();


                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał tekst " + "<a href='/Tekst/Details/" + tek.ID + "'>" + tek.Name + "</a>" + " do portalu <br>";
                        }
                        if (Startup.Type == "album")
                        {
                            Album alb = db.Albumy.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = alb.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał album " + "<a href='/Album/Details/" + alb.ID + "'>" + alb.Name + "</a>" + " do portalu <br>" + image + "<br>";
                        }

                        Startup.ID = 0;
                        Startup.Who = String.Empty;
                        Startup.What = String.Empty;
                        return ret;
                    }
                }
            }

            if (Startup.What == "ulubione")
            {
                if (Startup.Who != String.Empty)
                {
                    if (user.Znajomi.Contains(Startup.Who))
                    {
                        ApplicationUser au = db.Users.Where(x => x.Id == Startup.Who).FirstOrDefault();

                        if (Startup.Type == "utwor")
                        {
                            Utwor ut = db.Utwory.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String YouTubeFilmID = ut.URL.Replace("https://", "").Replace("http://", "").Replace("www.youtube.com/watch?v=", "").Replace("youtu.be/", "");
                            String YoutubeSRC = String.Format("https://www.youtube.com/embed/{0}", @YouTubeFilmID);


                            string frame = "<iframe src = '" + YoutubeSRC + "' frameborder = '0' allow = 'autoplay; encrypted - media' allowfullscreen style = 'width: 100%;' ></iframe>";
                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał utwór " + "<a href='/Utwor/Details/" + ut.ID + "'>" + ut.Name + "</a>" + " do ulubionych <br>" + frame + "<br>";
                        }

                        if (Startup.Type == "autor")
                        {
                            Autor aut = db.Autorzy.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = aut.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wykonawcę " + "<a href='/Autor/Details/" + aut.ID + "'>" + aut.Name + "</a>" + " do ulubionych <br>" + image + "<br>";
                        }

                        if (Startup.Type == "zespol")
                        {
                            Zespol zes = db.Zespoly.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = zes.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wykonawcę " + "<a href='/Zespol/Details/" + zes.ID + "'>" + zes.Name + "</a>" + " do ulubionych <br>" + image + "<br>";
                        }

                        if (Startup.Type == "event")
                        {
                            Event ev = db.Eventy.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = ev.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wydarzenie " + "<a href='/Event/Details/" + ev.ID + "'>" + ev.Name + "</a>" + " do ulubionych <br>" + image + "<br>";
                        }

                        if (Startup.Type == "relacja")
                        {
                            Relacja rel = db.Relacje.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = rel.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał wykonawcę " + "<a href='/Relacja/Details/" + rel.ID + "'>" + rel.Name + "</a>" + " do ulubionych <br>" + image + "<br>";
                        }

                        if (Startup.Type == "tekst")
                        {
                            Tekst tek = db.Teksty.Where(x => x.ID == Startup.ID).FirstOrDefault();


                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał tekst " + "<a href='/Tekst/Details/" + tek.ID + "'>" + tek.Name + "</a>" + " do ulubionych <br>";
                        }
                        if (Startup.Type == "album")
                        {
                            Album alb = db.Albumy.Where(x => x.ID == Startup.ID).FirstOrDefault();

                            String imgSrc = alb.Photos.Split(';')[0];
                            string image = "<img src = '/uploads/img/" + imgSrc + "' class='img-responsive' />";

                            ret = "<a href='/Home/DetailsUser/" + au.Id + "'>" + au.UserName + "</a>" + " dodał album " + "<a href='/Album/Details/" + alb.ID + "'>" + alb.Name + "</a>" + " do ulubionych <br>" + image + "<br>";
                        }

                        Startup.ID = 0;
                        Startup.Who = String.Empty;
                        Startup.What = String.Empty;
                        return ret;
                    }
                }
            }

            return String.Empty;
        }



        public ActionResult Message(string message)

        {
            //if (message == null) return null;
            var result = String.Empty;
            var sb = new StringBuilder();
            //message = "Witaj Mateusz";
            var cos = JsonConvert.SerializeObject(Notifications());
            sb.AppendFormat("data: {0}\n\n", cos);

            string co = sb.ToString();

            return Content(co, "text /event-stream");

        }

    }

    //public class Noti :Controller
    //{
    //    UserManager<ApplicationUser> _userManager;
    //    ApplicationDbContext _db;
    //    IHttpContextAccessor _contextAccessor;
    //    public Noti(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IHttpContextAccessor contextAccessor)
    //    {
    //        _userManager = userManager;
    //        _db = db;
    //        _contextAccessor = contextAccessor;
    //    }

    //    public ActionResult Message(string message)

    //    {
    //        if (message == null) return null;
    //        var result = string.Empty;
    //        var sb = new StringBuilder();
    //        var cos = JsonConvert.SerializeObject(message);
    //        sb.AppendFormat("data: {0}\n\n", cos);

    //        string co = sb.ToString();

    //        return Content(co, "text /event-stream");

    //    }
    //    public JsonResult Notify(string message)
    //    {
    //        System.Security.Claims.ClaimsPrincipal currentUser = this.User;
    //        var idk = _userManager.GetUserId(User);
    //        ApplicationUser user = _db.Users.Where(x => x.Id == idk).FirstOrDefault();

    //        string ret = string.Empty;
    //        if (TempData["Adder"] != null)
    //        {
    //            string ajdi = TempData["Adder"].ToString();
    //            ApplicationUser adder = _db.Users.Where(x => x.Id == ajdi).FirstOrDefault();

    //            if (adder != null && user.Znajomi.Contains(adder.Id))
    //            {
    //                ret = "Uzytkownik " + adder.UserName + "wlasnie dodał " + TempData["Notification"].ToString() + " do portalu";
    //            }
    //        }

    //        string adderUlubione = _contextAccessor.HttpContext.Session.GetString("AdderUlubione");
    //        string Ulubione = _contextAccessor.HttpContext.Session.GetString("Ulubione");
    //        QueryValueService qvs = new QueryValueService(_contextAccessor);
    //        string cos = qvs.GetValue();
    //        if (adderUlubione != null)
    //        {
    //            string ajdi = adderUlubione;
    //            ApplicationUser adder = _db.Users.Where(x => x.Id == ajdi).FirstOrDefault();

    //            if (adder != null && user.Znajomi.Contains(adder.Id))
    //            {
    //                ret = "Uzytkownik " + adder.UserName + "wlasnie dodał " + Ulubione + " do ulubionych";
    //            }
    //        }
    //        //NotificationHub nh = new NotificationHub();
    //        //nh.SendMessage("us","Index action invoked.");

    //        //Sender(n);

    //        Message("Witaj Mateusz");
    //        return Json(ret);
    //    }
    //}



}



