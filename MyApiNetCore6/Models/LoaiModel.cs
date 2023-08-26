using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Models
{
    public class LoaiModel
    {
        [Required]
        [MaxLength(50)]
        public string TenLoai { get; set; }
        
    }
}
