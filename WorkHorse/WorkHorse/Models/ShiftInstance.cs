using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHorse.Models
{
    public class ShiftInstance
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
