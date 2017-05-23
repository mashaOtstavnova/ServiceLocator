using System;
using System.Collections.Generic;

namespace ServiceLocator.Entities
{
    public class Human
    {
        public int Id { get; set; }
        public bool IsRegistration { get; set; }
    }

    public class Type
    {
        public int Id { get; set; }
        public string TypeUser { get; set; }
    }
    public class Master : Human
    {
        public List<string> Categories { get; set; }
        public string AboutMe { get; set; }
        public string Subcategory { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class Client : Human
    {
    }

    public class SheduleDay
    {
        public string Day { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
    }
    public class Record
    {
        public bool IsBusy { get; set; }
        public Guid Id { get; set; }
        public int IdClient { get; set; }
        public int IdMaster { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string Service { get; set; }
        public decimal Money { get; set; }
    }
}