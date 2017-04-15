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
        public MvxCommand SetTypeUserClickCommand
        {
            get { return new MvxCommand(SetTypeUserClick); }
        }
        private void SetTypeUserClick()
        {
            ShowViewModel<FirstMasterViewModel>();
            //_dataLoader.SetTypeUser();
        }
    }
}
