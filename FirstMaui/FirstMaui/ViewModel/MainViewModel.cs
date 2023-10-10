using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMaui.ViewModel
{
    public partial class MainViewModel :ObservableObject
    {
        IConnectivity _connectivity;

        [ObservableProperty]
        string color;

        [ObservableProperty]
        ObservableCollection<string> colorItems;

        public MainViewModel(IConnectivity connectivity)
        {
            colorItems = new ObservableCollection<string>();
            _connectivity = connectivity;
        }
        [RelayCommand]
         async Task AddColor()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Hata", "İnternet yok", "Tamam");
            return;
            }
            ColorItems.Add(Color); 
            Color = string.Empty;
        }
        [RelayCommand]
        void RemoveColor(string colorName)
        {
            ColorItems.Remove(colorName);
            Color = string.Empty;

        }
    }
}
