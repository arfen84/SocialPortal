using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialPortal.Models
{
    public class Wydarzenie
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Miejsce")]
        public string Place { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data ")]
        public DateTime Date { get; set; }
        [Display(Name = "Link do strony internetowej lub profilu w mediach społecznościowych")]
        public string URL { get; set; }
        [Display(Name = "Zdjęcia")]
        public string Photos { get; set; }
        [Display(Name = "Artysta")]
        public string Artist { get; set; }
        [Display(Name = "Youtube URL")]
        public string Youtube { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string Adder { get; set; }       //kto dodał
        public bool ConfirmAdder { get; set; }  //potwierdź, że user może edytować wpis
        public bool verified { get; set; }      //zweryfikowany przez Admina
        public int Popularity { get; set; }     //popularnosc
        public string Liker { get; set; }       //kto polubił
    }

    public class Event : Wydarzenie
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
        [Display(Name = "Godzina rozpoczecia")]
        public DateTime Hour { get; set; }
        [Display(Name = "Cena biletu")]
        public int Price { get; set; }
       
    }

    public class Relacja : Wydarzenie
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Relacja z wydarzenia/koncertu")]
        public string relation { get; set; }
    }
}
