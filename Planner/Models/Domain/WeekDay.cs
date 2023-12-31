﻿using Planner.Service;
using System.Reflection.Metadata.Ecma335;

namespace Planner.Models.Domain
{
    public class WeekDay
    {
        public Guid Id { get; set; }
        public DateTime ArrivalDay {get;set;}
        public DateTime Day { get; set; }
        public int ActivityDay { get; set; }
        public double QuantityMbK { get; set; }
        public double RemainderMBK { get; set; }
        public int RegisteredPatients { get; set; }
        public int RemainingPatients { get; set; }
        public int NumberOfPatients { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
