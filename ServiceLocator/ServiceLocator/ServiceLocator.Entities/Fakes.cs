using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocator.Entities
{
    public static class Fakes
    {
        public static ObservableCollection<Master> Masters = new ObservableCollection<Master>
        {
            new Master
            {
                Id = 58767238,
                Name = "1",
                IsRegistration = false,
                Categories = new List<string> {ServicesCategory.Business},
                AboutMe = "Открываю ИП, ООО и т.п.",
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(17),
                LastName = "2"
            }
        };

        public static ObservableCollection<Record> Records = new ObservableCollection<Record>
        {
            new Record
            {
                Id = Guid.NewGuid(),
                IdClient = Masters.FirstOrDefault().Id,
                IdMaster = 58767238,
                Duration = TimeSpan.FromHours(1),
                IsBusy = true,
                Service = "Спилить ногти на зубах!",
                Money = 10000,
                Time = DateTime.Now
            },
            new Record
            {
                Id = Guid.NewGuid(),
                IdClient = Masters.FirstOrDefault().Id,
                IdMaster = 5226616,
                Duration = TimeSpan.FromHours(1),
                IsBusy = true,
                Service = "Спилить ногти",
                Money = 1500,
                Time = DateTime.Now
    },
            new Record
            {
                Id = Guid.NewGuid(),

                IdClient = 58767238,
                IdMaster = Masters.FirstOrDefault().Id,
                Duration = TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(25)),
                IsBusy = false,
                Service = "Выдрать брови",
                Money = 5000,
                Time = DateTime.Now
            },
       
        new Record
        {
            Id = Guid.NewGuid(),
            IdClient = Masters.FirstOrDefault().Id,
            IdMaster = 58767238,
            Duration = TimeSpan.FromHours(1),
            IsBusy = false,
            Service = "Спилить ногти на зубах!",
            Money = 10000,
            Time = DateTime.Now
        },
        new Record
        {
            Id = Guid.NewGuid(),
            IdClient = Masters.FirstOrDefault().Id,
            IdMaster = 5226616,
            Duration = TimeSpan.FromHours(1),
            IsBusy = true,
            Service = "Спилить ногти",
            Money = 1500,
            Time = DateTime.Now
        },
        new Record
        {
            Id = Guid.NewGuid(),

            IdClient = Masters.FirstOrDefault().Id,
            IdMaster = 5226616,
            Duration = TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(25)),
            IsBusy = false,
            Service = "Выдрать брови",
            Money = 5000,
            Time = DateTime.Now
        }
    };

    }
}
