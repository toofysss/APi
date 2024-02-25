using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawApi.Database
{
    [Table("iraqbank")]
    public class IraqBank
    {
        public int ID { get; set; }
        public string Dscrp { get; set; }
        [JsonIgnore]
        public List<IraqBankDetails> IraqBankDetails { get; set; }
    }

    [Table("iraqbank_details")]
    public class IraqBankDetails
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Dscrp { get; set; }
        public string Files { get; set; }
        public string Img { get; set; }
        public int IraqBankID { get; set; }
    }

}
