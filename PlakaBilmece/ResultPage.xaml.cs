using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace PlakaBilmece;

public partial class ResultPage : ContentPage
{
    // Constructor'a "int puan" parametresi eklendi
    public ResultPage(List<string> bilinenler, List<string> bilemediklerim, int puan)
    {
        InitializeComponent();

        lblDogruSayisi.Text = bilinenler.Count.ToString();
        lblKacanSayisi.Text = bilemediklerim.Count.ToString();
        lblFinalPuan.Text = puan.ToString(); // Puaný ekrana yazdýr

        cvBilinenler.ItemsSource = bilinenler;
        cvBilemediklerim.ItemsSource = bilemediklerim;
    }

    private async void OnTekrarOynaClicked(object sender, EventArgs e)
    {
        // Ana menüye deðil, direkt oyunun içine geri döndürür. (Biraz çetrefilli olduðu için þimdilik ana menüye yolluyoruz)
        await Navigation.PopToRootAsync();
    }

    private async void OnAnaMenuClicked(object sender, EventArgs e)
    {
        // En baþtaki Ana Menü (MainPage) sayfasýna geri döner
        await Navigation.PopToRootAsync();
    }
}