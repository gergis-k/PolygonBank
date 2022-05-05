namespace PolygonBank.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public Customer Sender { get; set; } = Customer.Empty;
        public Customer Receiver { get; set; } = Customer.Empty;
        public DateTime Date { get; set; }
        public Transaction Transaction { get; set; } = Transaction.Empty;

        public static Transfer Empty => new Transfer();
    }
}
