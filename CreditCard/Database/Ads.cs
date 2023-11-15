using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCard.Database
{
    [Table("T_Ad")]
    public class Ads
    {
        public int Id { get; set; }
        public byte? Img { get; set; }
        public string Dscrp { get; set; }
    }
}
