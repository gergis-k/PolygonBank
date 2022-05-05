using System.ComponentModel.DataAnnotations;

namespace PolygonBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }

        [MaxLength(128)]
        public string Serial { get; set; } = string.Empty;

        public static Transaction Empty => new Transaction();
    }
}
