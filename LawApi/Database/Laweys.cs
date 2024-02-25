using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("laweys")]
    public class Laweys
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Depart { get; set; }
        public int CityID { get;set;}
        public string City { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }
        public string Dscrp { get; set; }
        public int Rating { get; set; }
        public string Img { get; set; }
        public string Imgcardlaw { get; set; }
        public int Status { get; set; }

    }
}
