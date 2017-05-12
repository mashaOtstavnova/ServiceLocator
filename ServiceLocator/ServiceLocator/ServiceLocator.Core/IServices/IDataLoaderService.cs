using ServiceLocator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLocator.Core.ViewModels;

namespace ServiceLocator.Core.IServices
{
    public  interface IDataLoaderService
    {
        void GetTypeUser();
        void SetTypeUser();
        void AddNewRecord(Record record);
        List<string> GetIsBusiRecordsMaster(int idMaster, DateTime date);
        Master GetMaster(int id);
        Record GetRecord(int id);
        string GetType(int id);
        Client GetClient(int id);
    }
}
