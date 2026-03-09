using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace PlakaBilmece;

public partial class GamePage : ContentPage
{
    // Veriler
    List<Soru> soruHavuzu = new List<Soru>();
    List<string> bilinenler = new List<string>();
    List<string> bilemediklerim = new List<string>();

    Soru aktifSoru;
    string aktifMod;
    Random rnd = new Random();

    // Zamanlayýcýlar
    IDispatcherTimer genelTimer;
    IDispatcherTimer soruTimer;

    int genelSureSaniye = 120; // 2 Dakika
    int soruSureSaniye = 100;  // 10 saniye
    int toplamPuan = 0;        // YENÝ EKLENEN PUAN DEÐÝÞKENÝ
    public GamePage(string mod)
    {
        InitializeComponent();
        aktifMod = mod;

        SoruHavuzunuHazirla();
        ZamanlayicilariHazirla();
        YeniSoruGetir();
    }

    private void SoruHavuzunuHazirla()
    {
        // VERÝTABANINDAN VERÝLERÝ ÇEKÝYORUZ!
        if (aktifMod == "Il")
        {
            lblSoruBaslik.Text = "Ýlin Plakasý Nedir?";
            soruHavuzu = Veritabani.TumIlleriGetir();
        }
        else if (aktifMod == "Ilce")
        {
            lblSoruBaslik.Text = "Ýlçenin Plakasý Nedir?";
            soruHavuzu = Veritabani.TumIlceleriGetir();
        }
    }

    private void ZamanlayicilariHazirla()
    {
        // 1. Genel Süre Sayacý (1 saniyede bir düþer)
        genelTimer = Dispatcher.CreateTimer();
        genelTimer.Interval = TimeSpan.FromSeconds(1);
        genelTimer.Tick += (s, e) =>
        {
            genelSureSaniye--;
            lblGenelSure.Text = TimeSpan.FromSeconds(genelSureSaniye).ToString(@"mm\:ss");

            if (genelSureSaniye <= 0)
            {
                OyunuBitir();
            }
        };
        genelTimer.Start();

        // 2. Soru Süresi Çubuðu (Çok hýzlý düþer, animasyon hissi verir)
        soruTimer = Dispatcher.CreateTimer();
        soruTimer.Interval = TimeSpan.FromMilliseconds(100);
        soruTimer.Tick += (s, e) =>
        {
            soruSureSaniye--;
            pbSoruSuresi.Progress = soruSureSaniye / 100.0;

            // Çubuk Rengi
            if (pbSoruSuresi.Progress < 0.3) pbSoruSuresi.ProgressColor = Colors.Red;
            else if (pbSoruSuresi.Progress < 0.6) pbSoruSuresi.ProgressColor = Colors.Orange;

            // 10 Saniye Dolduysa
            if (soruSureSaniye <= 0)
            {
                HataliCevapIslemi();
            }
        };
    }

    private void YeniSoruGetir()
    {
        // Havuzda soru bittiyse (Oyuncu 2 dakika dolmadan hepsini bildiyse)
        if (soruHavuzu.Count == 0)
        {
            OyunuBitir();
            return;
        }

        // Rastgele soru çek
        int index = rnd.Next(soruHavuzu.Count);
        aktifSoru = soruHavuzu[index];
        lblSoru.Text = aktifSoru.Ad;

        // Sorulaný listeden at
        soruHavuzu.RemoveAt(index);

        // Arayüzü Sýfýrla
        txtCevap.Text = "";
        pbSoruSuresi.Progress = 1.0;
        pbSoruSuresi.ProgressColor = Colors.LimeGreen;

        // Soru sayacýný baþtan baþlat
        soruSureSaniye = 100;
        soruTimer.Start();

        txtCevap.Focus(); // Klavyeyi hazýr tut
    }

    private void OnCevaplaClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCevap.Text)) return;

        if (txtCevap.Text == aktifSoru.Plaka)
        {
            // DOÐRU BÝLDÝ (+5 Puan)
            toplamPuan += 5;
            lblPuan.Text = toplamPuan.ToString();

            soruTimer.Stop();
            bilinenler.Add($"{aktifSoru.Ad} ({aktifSoru.Plaka})");
            YeniSoruGetir();
        }
        else
        {
            // YANLIÞ BÝLDÝ (-2 Puan)
            HataliCevapIslemi();
        }
    }

    private void HataliCevapIslemi()
    {
        // YANLIÞ VEYA SÜRE BÝTTÝ (-2 Puan)
        toplamPuan -= 2;
        lblPuan.Text = toplamPuan.ToString();

        soruTimer.Stop();
        bilemediklerim.Add($"{aktifSoru.Ad} ({aktifSoru.Plaka})");

        try { Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(400)); } catch { }

        YeniSoruGetir();
    }
    private async void OyunuBitir()
    {
        genelTimer.Stop();
        soruTimer.Stop();

        // PUANI DA SONUÇ EKRANINA YOLLUYORUZ
        await Navigation.PushAsync(new ResultPage(bilinenler, bilemediklerim, toplamPuan));
        Navigation.RemovePage(this);
    }
}