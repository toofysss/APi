using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{

    [Table("decisionscommand")]
    public class DecisionsCommand
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
    }
}
