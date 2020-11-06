using System;

namespace Challenge.Models
{
    public class History
    {
        public long Id { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
