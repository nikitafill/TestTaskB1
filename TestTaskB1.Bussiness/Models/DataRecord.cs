using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskB1.Domain.Models
{
    public class DataRecord
    {
        [Key]
        public int Id { get; set; } 
        public DateTime RecordDate { get; set; } 
        [MaxLength(10)]
        public string LatinCharacters { get; set; }
        [MaxLength(10)]
        public string CyrillicCharacters { get; set; }
        public int EvenInteger { get; set; } 
        public decimal DecimalNumber { get; set; } 
    }
}
