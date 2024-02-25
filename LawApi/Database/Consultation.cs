using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("consultation")]
    public class Consultation
    {
        public int? ID { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Dscrp { get; set; }
        public int? UserID { get; set; }
        public int? Status { get; set; }
        public string Files { get; set; }

    }
}
