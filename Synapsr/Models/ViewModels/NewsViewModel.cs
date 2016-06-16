using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Synapsr.Models.ViewModels
{
    public class NewsViewModel
    {
        [Required,Display(Name ="Titlu")]
        public string title { get; set; }
        [Required,Display(Name ="Noutate")]
        public string body { get; set; }
        [Display(Name ="Informeaza studentii")]
        public bool inform { get; set; }
    }
}