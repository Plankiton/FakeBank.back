namespace Challenge.Models
{
    public class Client
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
    }

    public class GenericRequest
    {
        public long ClientId { get; set; }
        public double Value { get; set; }
        public string Password { get; set; }
    }

    public class TradeRequest
    {
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public double Value { get; set; }
        public string Password { get; set; }
    }
}
