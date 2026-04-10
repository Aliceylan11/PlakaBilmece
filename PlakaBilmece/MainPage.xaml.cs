using Microsoft.Maui.Controls;
using System;

namespace PlakaBilmece;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Sayfa her açıldığında rekoru kontrol et
    protected override void OnAppearing()
    {
        base.OnAppearing();

        int rekor = Preferences.Default.Get("Highscore", 0);
        string isim = Preferences.Default.Get("HighscorePlayer", "");

        if (rekor > 0)
        {
            lblHighscore.Text = $"🏆 REKOR: {isim.ToUpper()} - {rekor} PUAN";
            lblHighscore.IsVisible = true;
        }
    }

    private async void OnIlTahminClicked(object sender, EventArgs e)
    {
        string oyuncuAdi = GetPlayerName();
        // GamePage(mod, isim) formatında gönderiyoruz
        await Navigation.PushAsync(new GamePage("Il", oyuncuAdi));
    }

    private async void OnIlceTahminClicked(object sender, EventArgs e)
    {
        string oyuncuAdi = GetPlayerName();
        // İlçe modunda da ismi gönderiyoruz
        await Navigation.PushAsync(new GamePage("Ilce", oyuncuAdi));
    }

    // İsim kontrolü için yardımcı metod
    private string GetPlayerName()
    {
        return string.IsNullOrWhiteSpace(PlayerNameEntry.Text) ? "Yarışmacı" : PlayerNameEntry.Text;
    }
}