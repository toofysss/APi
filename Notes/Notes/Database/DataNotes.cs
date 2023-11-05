using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notes.Database
{
    [Table("T_Data")]
    public class DataNotes
    {
        public int id { get; set; }
        public int SectionID { get; set; }
        public string Dscrp { get; set; }
        public string bg { get; set; }
    }
}
