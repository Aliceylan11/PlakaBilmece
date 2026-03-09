using Microsoft.Maui.Controls;
using System; 
namespace PlakaBilmece;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnIlTahminClicked(object sender, EventArgs e)
    {
        // 2. Sayfaya "Il" modunda geçiyoruz
        await Navigation.PushAsync(new GamePage("Il"));
    }

    private async void OnIlceTahminClicked(object sender, EventArgs e)
    {
        // 2. Sayfaya "Ilce" modunda geçiyoruz
        await Navigation.PushAsync(new GamePage("Ilce"));
    }
}