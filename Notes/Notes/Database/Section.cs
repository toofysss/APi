using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notes.Database
{
    [Table("T_Section")]
    public class Section
    {
        public int id { get; set; }
        public string Sections { get; set; }
        [JsonIgnore]
        public List<DataNotes> DataNotes { get; set; }
    }

}
