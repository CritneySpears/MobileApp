using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WorkHorse.Models
{
    public class ClockInstance
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string ClockString { get; set; }
    }
}