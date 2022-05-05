using System.ComponentModel.DataAnnotations;

namespace PolygonBank.DTO
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public string Sender { get; set; } = string.Empty;
        public string Reciever { get; set; } = string.Empty;

        public string Amount { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Serial { get; set; } = string.Empty;
    }
}
