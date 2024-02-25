using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("laws")]
    public class Law
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        [JsonIgnore]
        public List<LawDetails> LawDetails { get; set; }
    }
    [Table("lawdetails")]
    public class LawDetails
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        public int LawID { get; set; }

    }
}
