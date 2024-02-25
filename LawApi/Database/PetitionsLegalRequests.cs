using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("petitionslegalrequests")]
    public class PetitionsLegalRequests
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        public string Details { get; set; }
    }
}
