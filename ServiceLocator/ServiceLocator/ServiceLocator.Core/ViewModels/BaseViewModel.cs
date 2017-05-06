using MvvmCross.Core.ViewModels;

namespace ServiceLocator.Core.ViewModels
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