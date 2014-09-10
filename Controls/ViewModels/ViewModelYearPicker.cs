using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace YorgiControls.ViewModels
{
    public class ViewModelYearPicker : ViewModelBase
    {
        private int minYear = 1900;
        private int maxYear = 2085;
        private int page = 1;
        private int pageCount;
        private int elementsCount = 12;
        private string header;
        private IEnumerable<string> dateItems;
        private bool isYearSelectionMode;

        private ICommand leftArrowCommand;
        private ICommand rightArrowCommand;
        private ICommand itemSelectedCommand;
        private ICommand headerClickCommand;

        public ViewModelYearPicker()
        {
            this.Header = "Select decade...";

            CalculateDecadePeriods();

            DateItems = DecadeItems;
        }

        #region Properties

        #region Commands
        public ICommand LeftArrowCommand
        {
            get
            {
                return leftArrowCommand ??
                       (this.leftArrowCommand = new RelayCommand(() =>
                       {
                           if (this.page > 1) this.Page--;
                       },
                       () => page > 1));
            }
        }
        public ICommand RightArrowCommand
        { //TODO Fix CanExecute condition
            get
            {
                return rightArrowCommand ?? (this.rightArrowCommand = new RelayCommand(() =>
                {
                    if (this.page < this.pageCount) this.Page++;

                }, () => page == 1));
            }
        }
        public ICommand ItemSelectedCommand
        {
            get { return itemSelectedCommand ?? (this.itemSelectedCommand = new RelayCommand<string>(s =>
            {
                if (isYearSelectionMode)
                {
                    Console.Beep();
                }
                else
                {
                    this.Header = "Select year...";
                    ProduceYearItems(s);
                }
                
            })); }
        }
        public ICommand HeaderClickCommand
        {
            get
            {
                return headerClickCommand ?? (this.headerClickCommand = new RelayCommand(() =>
                {
                    if (isYearSelectionMode)
                    {
                        isYearSelectionMode = false;
                        this.Header = "Select decade...";
                        DateItems = DecadeItems;
                    }
                }));
            }
        }

        #endregion

        public int MinYear
        {
            get { return minYear; }
            set { minYear = value; }
        }
        public int MaxYear
        {
            get { return maxYear; }
            set { maxYear = value; }
        }
        public int ElementsCount
        {
            get { return elementsCount; }
            set { elementsCount = value; }
        }

        public string Header
        {
            get { return this.header; }
            set
            {
                if (value == this.header) return;
                this.header = value;
                RaisePropertyChanged("Header");
            }
        }

        public int Page
        {
            get { return this.page; }
            set
            {
                if (this.page == value) return;
                this.page = value;
                RaisePropertyChanged("DateItems");
            }
        }

        public IEnumerable<string> DateItems
        {
            get
            {
                return dateItems.Skip(12 * (page - 1));
            }
            set
            {
                this.dateItems = value;
                RaisePropertyChanged("DateItems");
            }
        }

        private IList<string> DecadeItems { get; set; }

        #endregion

        #region Methods

        private void CalculateDecadePeriods()
        {
            var period = DateTime.Now.Year - MinYear;
            this.pageCount = (period + elementsCount - 1) / elementsCount;

            DecadeItems = new List<string>();

            for (var year = minYear; year < maxYear; year += 10)
            {
                var endYear = year + 9;
                DecadeItems.Add(string.Format("{0}-\n{1}", year, endYear > maxYear ? maxYear : endYear));
            }
        }

        private void ProduceYearItems(string param)
        {
            var years = param.Split('-').Select(int.Parse).ToArray();
            var yearsItems = new List<string>();
            for (var year = years[0] - 1; year <= years[1] + 1; year++)
            {
                yearsItems.Add(year.ToString(CultureInfo.InvariantCulture));
            }
            DateItems = yearsItems;
            isYearSelectionMode = true;
        }

        #endregion

    }
}
