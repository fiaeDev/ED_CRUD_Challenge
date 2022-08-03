using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Location { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual DeviceType Type { get; set; }        
        public int HealthId { get; set; }
        [ForeignKey("HealthId")]       
        public virtual DeviceHealth Health { get; set; }       
        public DateTime LastUsed { get; set; }
        public decimal Price { get; set; }
        public string ColorHexValue { get; set; }
    }
}
