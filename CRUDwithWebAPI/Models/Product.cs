using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CRUDwithWebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
