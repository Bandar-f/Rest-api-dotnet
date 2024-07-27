using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockDto
    {

        [Required]
        [MaxLength(10,ErrorMessage ="Symbol cannot be over 10 characters")]
        
        public string Symbol { get; set; }=string.Empty;

        [Required]
        [MaxLength(12,ErrorMessage ="Companyname can not be longer then 12 characters")]
        public String CompanyName { get; set; }=string.Empty;
        
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage ="Industry can not be longer then 12 characters")]
        public string Industry { get; set; } = string.Empty;

        [Range(1,50000000000)]
        public long marketCap {get; set;}
    }
}