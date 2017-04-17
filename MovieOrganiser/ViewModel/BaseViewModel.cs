// File created by Bartosz Nowak on 20/07/2014 00:17

using GalaSoft.MvvmLight;

namespace MovieOrganiser.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool isBusy;
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.Set(ref this.isBusy, value);
        }
    }
}