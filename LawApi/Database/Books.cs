using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("books")]
    public class Books
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        public string Files { get; set; }
    }
}
