using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("iraqigazette")]
    public class IraqiGazette
    {
        public int ID { get; set; }
        public string Count { get; set; }
        public string Files { get; set; }
    }
}
