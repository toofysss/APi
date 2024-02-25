using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("DecisionsFederalSupremeCourt")]
    public class DecisionsFederalSupremeCourt
    {
        public int ID { get; set; }
        public string Count { get; set; }
        public string Date { get; set; }
        public string Files { get; set; }
    }
}
