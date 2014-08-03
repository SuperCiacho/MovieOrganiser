using GalaSoft.MvvmLight;
using MovieOrganiser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.ViewModel
{
    public class ViewModelView : ViewModelBase
    {
        protected static FilmWebApi FilmWebApi;
        private bool isBusy;

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        static ViewModelView()
        {
            FilmWebApi = new FilmWebApi(new ApiHelper()); 
        }
    }
}
