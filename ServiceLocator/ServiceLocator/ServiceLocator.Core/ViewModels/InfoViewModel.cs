using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocator.Core.ViewModels
{
    public class InfoViewModel : BaseViewModel
    {
        private string _appDiscription;

        public string AppDiscription
        {
            get { return _appDiscription; }
            set
            {
                _appDiscription = value;
                RaisePropertyChanged(() => AppDiscription);
            }
        }
    }
}
