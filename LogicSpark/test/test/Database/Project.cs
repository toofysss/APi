using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    [Table("projects")]
    public class Project
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public string ProjectFile { get; set; }
        public ICollection<ProjectImg> ProjectImg { get; set; }
        public Team Team { get; set; }
    }
}
