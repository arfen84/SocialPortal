using SocialPortal.Models.ManageViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SocialPortal.Models
{

    public class slider
    {
        public string str { get; set; }

        public string desc1 { get; set; }
        public string title1 { get; set; }
        public string link1 { get; set; }
        public string desc2 { get; set; }
        public string title2 { get; set; }
        public string link2 { get; set; }
        public string desc3 { get; set; }
        public string title3 { get; set; }
        public string link3 { get; set; }
    }

    public class podzielViewModel
    {
        public Utwor utwor { get; set; }
        public Autor autor { get; set; }
        public Zespol zespol { get; set; }
        public Event wydarzenie { get; set; }
        public Relacja relacja { get; set; }
        public Tekst tekst { get; set; }
        public Album album { get; set; }
    }

    public class AlbumViewModel
    {
        public List<Album> ostatnioDodane { get; set; }
        public List<Album> najpopuparniejsze { get; set; }
        public List<Album> albumy { get; set; }
        public List<String> wykonawcy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }

    public class TekstViewModel
    {
        public List<Tekst> ostatnioDodane { get; set; }
        public List<Tekst> najpopularniejsze { get; set; }
        public List<Tekst> teksty { get; set; }
        public List<String> wykonawcy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }

    public class wydarzenie
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public int Popularity { get; set; }
        public DateTime AddDate { get; set; }
        public string Controller { get; set; }
        public int ID { get; set; }
    }

    public class WydarzenieViewModel
    {
        public List<Event> nadchodząceWydarzenia { get; set; }
        public List<Event> poprzednieWydarzenia { get; set; }
        public List<Relacja> relacjeZWydarzen { get; set; }
        public List<String> wykonawcy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public List<String> miejsce { get; set; }
        public artysta art { get; set; }
        public List<Event> Wydarzenia { get; set; }
    }

    public class artysta
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public int Popularity { get; set; }
        public DateTime AddDate { get; set; }
        public string Controller { get; set; }
        public int ID { get; set; }
    }

    public class WykonawcyViewModel
    {
        public List<artysta> ostatnioDodani { get; set; }
        public List<artysta> najpopularniejsi { get; set; }
        public List<artysta> artysci { get; set; }
        public List<String> wykonawcy { get; set; }

    }

    public class UtworyViewModel
    {
        public List<Utwor> ostatnioDodane { get; set; }
        public List<Utwor> najpopularniejsze { get; set; }
        public List<Utwor> TOP { get; set; }
        public List<Utwor> ostatnioOgladane { get; set; }
        public List<Utwor> utwory { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public List<String> wykonawcy { get; set; }
        public artysta art { get; set; }
    }

    /// <summary>
    /// wyslany do widoku details
    /// </summary>
    public class ZespolViewModel
    {
        public Zespol zespol { get; set; }
        public List<Event> wydarzenia { get; set; }
        public List<Utwor> utwory { get; set; }
        public List<Album> albumy { get; set; }
        public List<ApplicationUser> fani { get; set; }
    }


    /// <summary>
    /// wyslany do widoku details
    /// </summary>
    public class AutorViewModel
    {
        public Autor autor { get; set; }
        public List<Event> wydarzenia { get; set; }
        public List<Utwor> utwory { get; set; }
        public List<Album> albumy { get; set; }
        public List<ApplicationUser> fani { get; set; }
    }

    public class TekstViewModelDetail
    {
        public Tekst tekst { get; set; }
        public Utwor utwor { get; set; }
        public artysta art { get; set; }
    }

    public class AlbumViewModelDetail
    {
        public Album album { get; set; }
        public List<Utwor> utwory { get; set; }
        public artysta art { get; set; }
    }

    public class UtworViewModelDetails
    {
        public Utwor utwor { get; set; }
        public artysta art { get; set; }
        public List<Utwor> ostatnioOgladane { get; set; }
    }

    public class AdminViewModel
    {
        public List<Album> albumy { get; set; }
        public List<Event> eventy { get; set; }
        public List<Relacja> relacje { get; set; }
        public List<Tekst> teksty { get; set; }
        public List<Utwor> utwory { get; set; }
        public List<Autor> autorzy { get; set; }
        public List<Zespol> zespoly { get; set; }
    }

    public class MainViewModel
    {
        public List<Utwor> utwory { get; set; }
        public List<Utwor> top10 { get; set; }
        public List<artysta> wykonawcy { get; set; }
        public List<Event> wydarzenia { get; set; }
        public List<Relacja> relacje { get; set; }
        public List<Album> albumy { get; set; }
        public slider slide { get; set; }
    }

    public class ProfilViewModel
    {
        public ApplicationUser user { get; set; }
        public List<Utwor> utwory { get; set; }
        public List<artysta> wykonawcy { get; set; }
        public List<Event> wydarzenia { get; set; }
        public List<Relacja> relacje { get; set; }
        public List<Tekst> teksty { get; set; }
        public List<Album> albumy { get; set; }

        public List<Utwor> utworyDodane { get; set; }
        public List<artysta> wykonawcyDodane { get; set; }
        public List<Event> wydarzeniaDodane { get; set; }
        public List<Relacja> relacjeDodane { get; set; }
        public List<Tekst> tekstyDodane { get; set; }
        public List<Album> albumyDodane { get; set; }

        public string Nazwa { get; set; }
    }


    public class UstawieniaViewModel
    {
        public ChangePasswordViewModel passViewModel { get; set; }
        public ApplicationUser au { get; set; }
    }

    public class QueryValueService
    {
        private readonly IHttpContextAccessor _accessor;

        public QueryValueService(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public string GetValue()
        {
            return _accessor.HttpContext.Request.Query["Adder"];
        }
    }
}
