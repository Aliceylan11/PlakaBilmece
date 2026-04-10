using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace PlakaBilmece;

public partial class ResultPage : ContentPage
{
    string _oyuncuAdi;
    int _toplamPuan;

    public ResultPage(List<string> bilinenler, List<string> bilemediklerim, int puan, string oyuncuAdi)
    {
        InitializeComponent();

        _oyuncuAdi = oyuncuAdi;
        _toplamPuan = puan;

        // Ekrana Temel Bilgileri Yazdır
        lblOyuncuMesaj.Text = $"TEBRİKLER {oyuncuAdi.ToUpper()}!";
        lblDogruSayisi.Text = bilinenler.Count.ToString();
        lblKacanSayisi.Text = bilemediklerim.Count.ToString();
        lblFinalPuan.Text = puan.ToString();

        cvBilinenler.ItemsSource = bilinenler;
        cvBilemediklerim.ItemsSource = bilemediklerim;

        // C ADIMI: Rekor Kontrolü ve Kaydetme
        RekorKontroluYap();
    }

    private void RekorKontroluYap()
    {
        // Telefon hafızasından mevcut rekoru çek
        int mevcutRekor = Preferences.Default.Get("Highscore", 0);

        if (_toplamPuan > mevcutRekor)
        {
            // Yeni Rekor Kırıldı!
            Preferences.Default.Set("Highscore", _toplamPuan);
            Preferences.Default.Set("HighscorePlayer", _oyuncuAdi);

            // Görsel geri bildirim ver
            lblRekorMesaj.IsVisible = true;
        }
    }

    private async void OnTekrarOynaClicked(object sender, EventArgs e)
    {
        // Direkt ana sayfaya dön (Yeni oyuna başlama mantığı root üzerinden kurulur)
        await Navigation.PopToRootAsync();
    }

    private async void OnAnaMenuClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}