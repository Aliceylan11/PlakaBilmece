# 🚗 PlakaBilmece Mobile - Türkiye'yi Plakalarla Keşfet!

[![Framework](https://img.shields.io/badge/.NET-9.0_MAUI-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/apps/maui)
[![Platform](https://img.shields.io/badge/Platform-Android_|_iOS-brightgreen)](https://dotnet.microsoft.com/en-us/apps/maui)
[![Version](https://img.shields.io/badge/Version-v1.0.0--Stable-blue)](https://github.com/aliceylan11/plaka_bilmece)

**PlakaBilmece**, Türkiye'nin 81 ilini ve plaka kodlarını eğlenceli, rekabetçi ve interaktif bir şekilde öğretmeyi amaçlayan modern bir mobil bulmaca oyunudur. .NET 9.0 MAUI ile geliştirilen uygulama, yüksek performans ve kullanıcı odaklı bir deneyim sunar.

---

## 🎯 Oyun Deneyimi ve Mekanikler

Uygulama, sadece bir test değil; hafızayı zorlayan ve anlık karar verme becerilerini geliştiren bir sistem üzerine kuruludur:

* **Zaman Baskısı:** 2 dakikalık (`120 saniye`) toplam süre ile yarışırken, her soru için dinamik bir `ProgressBar` ile takip edilen `10 saniyelik` mikro süreler heyecanı canlı tutar.
* **Akıllı Combo Sistemi (🔥):** Üst üste **5 doğru** tahmin yapıldığında aktifleşen Combo modu, oyuncuyu ödüllendirir. Combo serisi boyunca puan kazanımı **+5'ten +7'ye** çıkar.
* **Haptik Geri Bildirim:** Yanlış cevaplarda veya süre bitiminde tetiklenen **Vibration (Titreşim)** motoru, kullanıcıya fiziksel bir geri bildirim sağlayarak deneyimi zenginleştirir.
* **Detaylı Analiz:** Oyun sonunda oyuncunun hangi illeri bildiği, hangilerinde takıldığı liste halinde sunularak öğrenme süreci desteklenir.

## 🚀 Öne Çıkan Teknik Özellikler

* **UX Optimizasyonu:** `ScrollView` yapısı sayesinde klavye açıldığında tasarımın bozulması engellenmiştir. Özellikle Xiaomi, Redmi ve benzeri uzun ekranlı cihazlar için tam uyumludur.
* **Dinamik Veri Yönetimi:** İller ve ilçeler için optimize edilmiş veri havuzu (`Veritabani.cs`) ile anlık soru üretimi.
* **Cross-Platform:** Tek bir C# kod tabanı ile hem Android hem de iOS için yerel performans.
* **Dark Mode Support:** Gece kullanımına uygun, göz yormayan modern koyu tema tasarımı.

## 🛠️ Teknik Altyapı

* **Framework:** .NET 9.0 (MAUI)
* **Dil:** C#
* **Mimari:** MVVM Pattern (Model-View-ViewModel) esintileriyle güçlendirilmiş yapı.
* **Paketleme:** `android-arm64` ve `x86_64` mimarilerini kapsayan evrensel APK çıktısı.

---


## ⚙️ Kurulum ve Derleme

Projeyi yerel ortamınızda çalıştırmak için:

1.  Bu depoyu klonlayın: `git clone https://github.com/aliceylan11/plaka_bilmece.git`
2.  **Visual Studio 2022 (v17.12+)** ile `.sln` dosyasını açın.
3.  **Workloads:** ".NET Multi-platform App UI development" yükünün kurulu olduğundan emin olun.
4.  **Derleme:**
    * Windows üzerinden denemek için hedefi `Windows Machine` seçin.
    * APK almak için terminale şu komutu girin:
    ```powershell
    dotnet publish -f net9.0-android -c Release -p:AndroidPackageFormat=apk -p:RuntimeIdentifier=android-arm64 -p:AndroidKeyStore=false
    ```

## 📈 v2 Planları (Roadmap)

- [ ] Online Skor Tablosu (Leaderboard).
- [ ] Türkiye Haritası üzerinde görsel tahmin modu.
- [ ] Ses efektleri ve sesli soru okuma özelliği.
- [ ] Farklı zorluk seviyeleri (Bölgelere göre ayırma).

---

## 👨‍💻 Geliştirici

**Ali Ceylan**
* Gümüşhane Üniversitesi - Bilgisayar Programcılığı
* Kodlama ve Dijital Çözümler Kulübü Başkanı
* [LinkedIn](https://https://www.linkedin.com/in/aliceylan11/) | [GitHub](https://github.com/Aliceylan11)

---
**v1.0.0 Tamamlandı - 2026**
