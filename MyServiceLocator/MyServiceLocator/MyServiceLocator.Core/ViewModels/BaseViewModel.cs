using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace MyServiceLocator.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public IMvxCommand CloseThisVMCommand
        {
            get { return new MvxCommand(CloseThis); }
        }

        private void CloseThis()
        {
            Close(this);
        }
    }
}
