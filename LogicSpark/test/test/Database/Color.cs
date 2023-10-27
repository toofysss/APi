using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace test
{
    [Table("color")]
    public class Color
    {
        [Key]
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Title { get; set; }

        [ForeignKey("Websiteinfo")]
        public int WebsiteinfoID { get; set; }

        [JsonIgnore]
        public Websiteinfo WebsiteSetting { get; set; }
    }
}
