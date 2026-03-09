# PlakaBilmece Mobile

PlakaBilmece, Türkiye'nin 81 ilini ve bu illerin plaka kodlarını eğlenceli ve interaktif bir şekilde öğretmeyi amaçlayan, hız ve hafıza odaklı bir mobil bulmaca oyunudur. .NET MAUI teknolojisi ile modern mobil ihtiyaçlar göz önünde bulundurularak geliştirilmiştir.

## 🎯 Oyun Deneyimi
Kullanıcıya rastgele sunulan şehir isimlerinin plaka kodlarını doğru tahmin etmesi beklenir. Oyun, hem genel bir zaman baskısı hem de her soru için özel bir süre kısıtlaması ile heyecanı yüksek tutar.

## 🚀 Öne Çıkan Özellikler

* **Dinamik Süre Sistemi:** 2 dakikalık toplam oyun süresinin yanı sıra, her soru için görsel bir `ProgressBar` ile takip edilen 10 saniyelik ek süre.
* **Akıllı Puanlama:** Doğru tahminler için +5 puan eklenirken, hatalı tahminlerde -2 puan cezası ile stratejik düşünme teşvik edilir.
* **Haptik Geri Bildirim (Vibration):** Yanlış cevap verildiğinde cihazın titremesi ile kullanıcıya anlık fiziksel geri bildirim sağlanır.
* **UX Optimizasyonu:** * **Klavye Uyumu:** Plaka girişi için otomatik olarak sayısal klavye açılır.
    * **ScrollView Yapısı:** Klavye açıldığında butonların ve içeriğin ekranda kaybolmasını engelleyen, özellikle uzun ekranlı cihazlar (örn. Xiaomi/Redmi serisi) için optimize edilmiş arayüz.
* **Görsel Tasarım:** Koyu tema (Dark Mode) uyumlu modern arayüz ve özel tasarım uygulama ikonu.

## 🛠️ Teknik Altyapı

* **Framework:** .NET 9.0 MAUI
* **Programlama Dili:** C#
* **Platformlar:** Android (API 24+), iOS
* **Derleme Yapısı:** Tüm işlemci mimarilerini (ARM64, X86 vb.) destekleyen evrensel APK paketleme sistemi.

## 🎮 Nasıl Oynanır?

1.  Uygulamayı başlatın ve Başlat  butonuna tıklayın.
2.  Ekranda beliren şehrin plaka kodunu aşağıdaki kutucuğa girin.
3.  "CEVAPLA" butonuna basarak puanınızı artırın.
4.  Süre bitmeden en yüksek skora ulaşmaya çalışın!

## ⚙️ Geliştirici İçin Kurulum

1.  Depoyu klonlayın.
2.  Visual Studio 2022 (v17.12 veya üstü) ile projeyi açın.
3.  `.NET 9` SDK'sının yüklü olduğundan emin olun.
4.  Derleme modunu `Release` yaparak kendi APK'nızı oluşturun.

---
