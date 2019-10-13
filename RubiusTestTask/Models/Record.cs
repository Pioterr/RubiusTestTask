using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RubiusTestTask.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Project { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }
        
    }
}