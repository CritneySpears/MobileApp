using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WorkHorse.Models
{
    public class State
    {
        [PrimaryKey]
        public string ClockState { get; set; }
    }
}
