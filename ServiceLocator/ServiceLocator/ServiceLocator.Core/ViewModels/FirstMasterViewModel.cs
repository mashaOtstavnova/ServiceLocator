using System.Collections.Generic;

namespace ServiceLocator.Core.ViewModels
{
    public class FirstMasterViewModel : BaseViewModel
    {
        private List<string> _selectedCategories;

        public FirstMasterViewModel()
        {
            SelectedCategories = new List<string>();
        }

        public List<string> SelectedCategories
        {
            get => _selectedCategories;
            set
            {
                _selectedCategories = new List<string>(value);
                RaisePropertyChanged(() => SelectedCategories);
                RaisePropertyChanged(() => SelectedCategoriesString);
            }
        }

        public string SelectedCategoriesString
        {
            get
            {
                return SelectedCategories.Count > 0
                    ? string.Join("; ", SelectedCategories)
                    : "Ваши услуги";
            }
        }
    }
}