using System;

namespace Challenge.Models
{
    public class Operation
    {
        public long Id { get; set; }
        public string Client { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
    }
}
