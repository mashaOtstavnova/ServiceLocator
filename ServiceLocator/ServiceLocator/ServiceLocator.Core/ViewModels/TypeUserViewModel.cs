using ServiceLocator.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;

namespace ServiceLocator.Core.ViewModels
{
    public class TypeUserViewModel : BaseViewModel
    {
        private IDataLoaderService _dataLoader;
        public TypeUserViewModel()
        {
            _dataLoader = Mvx.Resolve<IDataLoaderService>();
        }
        public MvxCommand SetTypeMasterClickCommand
        {
            get { return new MvxCommand(SetTypeMasterClick); }
        }
        private void SetTypeMasterClick()
        {
            ShowViewModel<FirstMasterViewModel>();
            //_dataLoader.SetTypeUser();
        }
        public MvxCommand SetTypeClientClickCommand
        {
            get { return new MvxCommand(SetTypeClientClick); }
        }
        private void SetTypeClientClick()
        {
            ShowViewModel<HomeClientViewModel>();
            //_dataLoader.SetTypeUser();
        }
    }
}
