using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    public class ProjectImg
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public int ProjectId { get; set; }
        public int Projectid { get; set; }
        public int Projectimg { get; set; }

        public Project Project { get; set; }
    }
}
