using System;

namespace Challenge.Models
{
    public class History
    {
        public long Id { get; set; }
        public Client Client { get; set; }
        public DateTime Data { get; set; }
        public string Type { get; set; }
    }
}
