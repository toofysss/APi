using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    public class Team
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Title { get; set; }
        public string Job { get; set; }
        public string Img { get; set; }

        public string Contact { get; set; }
        public ICollection<Project> Projects { get; set; }
        public int ProjectTeam { get; set; }
        public Project Project { get; set; }
    }
}
