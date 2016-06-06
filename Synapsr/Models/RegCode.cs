
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Synapsr.Models
{
    [Table("reg_codes")]
    public class RegCode
    {
        public int id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
    }
}