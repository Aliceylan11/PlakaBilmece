using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices; // TİTREŞİM (Vibration) İÇİN BU ŞART!
using System;
using System.Collections.Generic;

namespace PlakaBilmece;

public partial class GamePage : ContentPage
{
    List<Soru> soruHavuzu = new List<Soru>();
    List<string> bilinenler = new List<string>();
    List<string> bilemediklerim = new List<string>();

    Soru aktifSoru;
    string aktifMod;
    string _oyuncuAdi;
    Random rnd = new Random();

    IDispatcherTimer genelTimer;
    IDispatcherTimer soruTimer;

    int genelSureSaniye = 120;
    int soruSureSaniye = 100;
    int toplamPuan = 0;
    int comboCount = 0;

    public GamePage(string mod, string oyuncuAdi)
    {
        InitializeComponent(); // BU SATIR XAML İSİMLERİNİ BAĞLAR!
        aktifMod = mod;
        _oyuncuAdi = oyuncuAdi;

        SoruHavuzunuHazirla();
        ZamanlayicilariHazirla();
        YeniSoruGetir();
    }

    private void SoruHavuzunuHazirla()
    {
        if (aktifMod == "Il")
        {
            lblSoruBaslik.Text = "İlin Plakası Nedir?";
            soruHavuzu = Veritabani.TumIlleriGetir();
        }
        else if (aktifMod == "Ilce")
        {
            lblSoruBaslik.Text = "İlçenin Plakası Nedir?";
            soruHavuzu = Veritabani.TumIlceleriGetir();
        }
    }

    private void ZamanlayicilariHazirla()
    {
        genelTimer = Dispatcher.CreateTimer();
        genelTimer.Interval = TimeSpan.FromSeconds(1);
        genelTimer.Tick += (s, e) =>
        {
            genelSureSaniye--;
            lblGenelSure.Text = TimeSpan.FromSeconds(genelSureSaniye).ToString(@"mm\:ss");
            if (genelSureSaniye <= 0) OyunuBitir();
        };
        genelTimer.Start();

        soruTimer = Dispatcher.CreateTimer();
        soruTimer.Interval = TimeSpan.FromMilliseconds(100);
        soruTimer.Tick += (s, e) =>
        {
            soruSureSaniye--;
            pbSoruSuresi.Progress = soruSureSaniye / 100.0;

            if (pbSoruSuresi.Progress < 0.3) pbSoruSuresi.ProgressColor = Colors.Red;
            else if (pbSoruSuresi.Progress < 0.6) pbSoruSuresi.ProgressColor = Colors.Orange;

            if (soruSureSaniye <= 0) HataliCevapIslemi();
        };
    }

    private void YeniSoruGetir()
    {
        if (soruHavuzu.Count == 0)
        {
            OyunuBitir();
            return;
        }

        int index = rnd.Next(soruHavuzu.Count);
        aktifSoru = soruHavuzu[index];
        lblSoru.Text = aktifSoru.Ad;
        soruHavuzu.RemoveAt(index);

        txtCevap.Text = "";
        pbSoruSuresi.Progress = 1.0;
        pbSoruSuresi.ProgressColor = Colors.LimeGreen;

        soruSureSaniye = 100;
        soruTimer.Start();
        txtCevap.Focus();
    }

    private void OnCevaplaClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCevap.Text)) return;

        if (txtCevap.Text == aktifSoru.Plaka)
            DogruCevapIslemi();
        else
            HataliCevapIslemi();
    }

    private void DogruCevapIslemi()
    {
        comboCount++;
        int kazanilanPuan = 5;

        if (comboCount >= 5)
        {
            kazanilanPuan = 7;
            lblCombo.Text = $"COMBO X{comboCount } 🔥";
            lblCombo.IsVisible = true;
        }

        toplamPuan += kazanilanPuan;
        lblPuan.Text = toplamPuan.ToString();

        soruTimer.Stop();
        bilinenler.Add($"{aktifSoru.Ad} ({aktifSoru.Plaka})");
        YeniSoruGetir();
    }

    private void HataliCevapIslemi()
    {
        comboCount = 0;
        lblCombo.IsVisible = false;

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

        await Navigation.PushAsync(new ResultPage(bilinenler, bilemediklerim, toplamPuan, _oyuncuAdi));
        Navigation.RemovePage(this);
    }
}