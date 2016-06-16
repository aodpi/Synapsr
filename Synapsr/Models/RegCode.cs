using System.ComponentModel.DataAnnotations.Schema;

namespace Synapsr.Models
{
    [Table("reg_codes")]
    public class RegCode
    {
        public int id { get; set; }
        public string code { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}