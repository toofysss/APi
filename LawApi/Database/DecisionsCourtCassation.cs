using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("decisionscourtcassation")]
    public class DecisionsCourtCassation
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        [JsonIgnore]
        public List<DecisionsCourtCassationDetails> DecisionsCourtCassationDetails { get; set; }
    }

    [Table("decisionscourtcassationdetails")]
    public class DecisionsCourtCassationDetails
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Dscrp { get; set; }
        public string Files { get; set; }
        public string Img { get; set; }
        public int DecisionsCourtCassationID { get; set; }
    }
}
