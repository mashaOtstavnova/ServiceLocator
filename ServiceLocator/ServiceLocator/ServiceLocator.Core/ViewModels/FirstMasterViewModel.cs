﻿using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using ServiceLocator.Entities;
using ServiceLocator.Core.IServices;

namespace ServiceLocator.Core.ViewModels
{
    public class FirstMasterViewModel : BaseViewModel
    {
        private List<string> _selectedCategories;
        public List<SheduleDay> Shedule;
        

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

        public MvxCommand SaveInfoCommand
        {
            get { return new MvxCommand(SaveInfoCommandClick); }
        }
        private void SaveInfoCommandClick()
        {
            ShowViewModel<HomeMasterViewModel>();
            //_dataLoader.SetTypeUser();
        }
    }

    
}