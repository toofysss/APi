using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    [Table("projectimg")]
    public class ProjectImg
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public int ProjectId { get; set; }

    }
}
