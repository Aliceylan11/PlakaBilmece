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
    string _oyuncuAdi; // A.b Adımı: İsmi burada tutuyoruz
    Random rnd = new Random();

    // Zamanlayıcılar
    IDispatcherTimer genelTimer;
    IDispatcherTimer soruTimer;

    int genelSureSaniye = 120;
    int soruSureSaniye = 100;
    int toplamPuan = 0;
    int comboCount = 0; // B Adımı: Seri (Combo) sayacı

    // Constructor: Hem modu hem de oyuncu adını alıyor
    public GamePage(string mod, string oyuncuAdi)
    {
        InitializeComponent();
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

        // B ADIMI: Doğru mu Yanlış mı kontrolü burada birleşti
        if (txtCevap.Text == aktifSoru.Plaka)
        {
            DogruCevapIslemi();
        }
        else
        {
            HataliCevapIslemi();
        }
    }

    private void DogruCevapIslemi()
    {
        comboCount++;
        int kazanilanPuan = 5;

        if (comboCount >= 3)
        {
            kazanilanPuan = 10;
            // COMBO ETİKETİNİ GÖSTER VE GÜNCELLE
            lblCombo.Text = $"COMBO X{comboCount - 1} 🔥";
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
        lblCombo.IsVisible = false; // COMBO BOZULDU, YAZIYI GİZLE

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

        // A.b ADIMI: Oyuncu adını ve puanını ResultPage'e yolluyoruz
        await Navigation.PushAsync(new ResultPage(bilinenler, bilemediklerim, toplamPuan, _oyuncuAdi));
        Navigation.RemovePage(this);
    }
}