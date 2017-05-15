using ServiceLocator.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Record GetRecord(Guid id);
        string GetType(int id);
        Client GetClient(int id);
        void DellRecord(Guid idRecord);
        void UpdateRecordMasters(Record record);
        void UpdateRecordClients(Record record);
        ObservableCollection<Record> GetRecords(DateTime date, List<int> ids);
        List<int> GetMastersByFriends(List<int> ids);
    }
}
