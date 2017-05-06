using System;
using System.Collections.Generic;

namespace ServiceLocator.Entities
{
    public class Human
    {
        public string Name { get; set; }
        public string LastName { get; set; }
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
    }

    public class Client : Human
    {
    }


    public class Record
    {
        public bool IsBusy { get; set; }
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public Master Master { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string Service { get; set; }
        public decimal Money { get; set; }
    }
}