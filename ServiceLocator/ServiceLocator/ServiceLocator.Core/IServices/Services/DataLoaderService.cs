using ServiceLocator.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLocator.Core.ViewModels;

namespace ServiceLocator.Core.IServices.Services
{
    public class DataLoaderService : IDataLoaderService
    {
        public void GetTypeUser()
        {
            //throw new NotImplementedException();
        }

        public void SetTypeUser()
        {
            //throw new NotImplementedException();
        }
        public void DellRecord(Guid idRecord)
        {
            //throw new NotImplementedException();
        }
        public void UpdateRecordMasters(Record record)
        {
            //throw new NotImplementedException();
        }
        public void UpdateRecordClients(Record record)
        {
            //throw new NotImplementedException();
        }
        public void AddNewRecord(Record record)
        {
            throw new NotImplementedException();
        }

        public List<string> GetIsBusiRecordsMaster(int idMaster, DateTime date)
        {
            var list = new List<string>();
            list.Add("16:40");
            list.Add("12:40");
            return list;
        }

        public Master GetMaster(int id)
        {
            //if(получение мастера из базы)
            //если нет то подгрузка из вк
            Master master = new Master();
            var cat = new List<string>();
            cat.Add("It");
            var ser = new List<string>();
            ser.Add(ServicesCategory.IT);
            master.Categories = ser;
            master.AboutMe = "Мобильная разработка";
            master.Subcategory = "Добрый день. Хочу предложить Вам услуги в сфере создания сайтов. \r\nМои преимущества: \r\n• Фиксированная цена. \r\n• Отличное качество работ. \r\n• Малые сроки исполнения (1-3 дня)\r\n• Работа на результат. \r\n• Никаких \"заумных\" слов и обмана. \r\nВ стоимость войдет: \r\n• Адаптивная верстка под мобильные устройства\r\n• Домен в зоне. ru или. рф\r\n• В стоимость включена разработка до 5-ти страниц\r\n• Форма быстрой связи с клиентом\r\n• Выгрузка сайта на хостинг (оплата хостинга в стоимость не включена) \r\n• Предустановленная административная панель управления сайтом (возможность самостоятельной замены картинок и текста на сайте)\r\nДополнительные услуги: \r\n• Разработка логотипа в векторном формате\r\n• Регистрация в поисковых системах\r\n• \"Красивости\" на сайте (параллакс эффекты, ajax формы, анимация и т. д. ) \r\nПортфолио вышлю по запросу. \r\nКонсультация бесплатно.";
            master.IsRegistration = true;
            return master;
        }

        public Record GetRecord(int id)
        {
            var record = new Record();
            record.IdMaster = 5337911;
            record.IdClient = 58767238;
            record.Duration = new TimeSpan(0,0,12,0);
            record.Money = 100;
            record.Service = "Уменьшение вонючести";
            record.Time = DateTime.Now;
            return record;
        }

        public string GetType(int id)
        {
            return "Client";
        }
        public Client GetClient(int id)
        {
            //if(получение мастера из базы)
            //если нет то подгрузка из вк
            Client client = new Client();
            client.IsRegistration = true;
            return client;
        }
        /// <summary>
        /// Возвращаем записи по дате для мастеров из списка ids
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ObservableCollection<Record> GetRecords(DateTime date, List<int> ids)
        {
            try
            {
                var t = Fakes.Records;
                return new ObservableCollection<Record>(
                    t.Where(r => r.Time.Date.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy") && ids.Exists(i => i == r.IdMaster)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Получение мастеров из числа друзей
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<int> GetMastersByFriends(List<int> ids)
        {
            return new List<int>
            {
                58767238,
                5226616

            };
        }

    }
}
