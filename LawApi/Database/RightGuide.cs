using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("rightsguide")]
    public class RightGuide
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        [JsonIgnore]
        public List<RightDetails> RightDetails { get; set; }
    }


    [Table("rightsdetails")]
    public class RightDetails
    {
        public int ID { get; set; }
        public string Qusetion { get; set; }
        public string Answer { get; set; }
        public string Laws { get; set; }
        public int RightGuideID { get; set; }
    }
}
