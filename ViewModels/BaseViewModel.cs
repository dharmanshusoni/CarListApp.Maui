using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarListApp.Maui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title; //Title of page

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        bool isLoading; //Is page busy

        public bool IsNotLoading => !isLoading;
    }

    //public class BaseViewModel : INotifyPropertyChanged
    //{
    //    string _title; //Title of page
    //    bool _isBusy; //Is page busy

    //    public bool IsBusy
    //    {
    //        get => _isBusy;
    //        set
    //        {
    //            if (_isBusy == value)
    //            {
    //                return;
    //            }
    //            _isBusy = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public string Title
    //    {
    //        get => _title;
    //        set
    //        {
    //            if (_title == value)
    //            {
    //                return;
    //            }
    //            _title = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public event PropertyChangedEventHandler? PropertyChanged;

    //    public void OnPropertyChanged([CallerMemberName] string name = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    //    }
    //}
}
