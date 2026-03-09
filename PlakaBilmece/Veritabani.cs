using System;
using System.Collections.Generic;

namespace PlakaBilmece;

// Soru Nesnemiz (Hem iller hem ilçeler için ortak kullanılacak)
public class Soru
{
    public string Ad { get; set; }
    public string Plaka { get; set; }
}

public static class Veritabani
{
    // --- 1. İLLER LİSTESİ ---
    // İndeks mantığıyla çalıştığı için 0. sıraya boşluk/başlık koyduk.
    private static readonly string[] illerDizisi = {
        "İL ADLARI","ADANA","ADIYAMAN","AFYON","AĞRI","AMASYA","ANKARA",
        "ANTALYA","ARTVİN","AYDIN","BALIKESİR","BİLECİK","BİNGÖL","BİTLİS","BOLU","BURDUR",
        "BURSA","ÇANAKKALE","ÇANKIRI","ÇORUM","DENİZLİ","DİYARBAKIR","EDİRNE",
        "ELAZIĞ","ERZİNCAN","ERZURUM","ESKİŞEHİR","GAZİANTEP","GİRESUN","GÜMÜŞHANE",
        "HAKKÂRİ","HATAY","ISPARTA","MERSİN","İSTANBUL","İZMİR","KARS","KASTAMONU",
        "KAYSERİ","KIRKLARELİ","KIRŞEHİR","KOCAELİ","KONYA","KÜTAHYA","MALATYA",
        "MANİSA","KAHRAMANMARAŞ","MARDİN","MUĞLA","MUŞ","NEVŞEHİR","NİĞDE",
        "ORDU","RİZE","SAKARYA","SAMSUN","SİİRT","SİNOP","SİVAS","TEKİRDAG",
        "TOKAT","TRABZON","TUNCELİ","ŞANLIURFA","UŞAK","VAN","YOZGAT","ZONGULDAK",
        "AKSARAY","BAYBURT","KARAMAN","KIRIKKALE","BATMAN","ŞIRNAK","BARTIN",
        "ARDAHAN","IĞDIR","YALOVA","KARABÜK","KİLİS","OSMANİYE","DÜZCE"
    };

    // --- 2. İLÇELER LİSTESİ ---
    // İlk 2 hane plaka, sonrası ilçe adı.
    private static readonly string[] ilcelerDizisi = { "01ADANA","01ALADAĞ","01CEYHAN ","01FEKE","01İMAMOĞLU",
                                    "01KARAİSALI","01KARATAŞ","01KOZAN","01POZANTI","01SAİMBEYLİ","01SEYHAN ","01TUFANBEYLİ","01YUMURTALIK","01YÜREĞİR","02ADIYAMAN",
                                    "02BESNİ","02ÇELİKHAN","02GERGER","02GÖLBAŞI","02KAHTA","02SAMSAT","02SİNCİT","02TUT","03AFYONKARAHİSAR","03BAYAT",
                                    "03BAŞMAKÇI","03BOLVADİN","03ÇAY","03ÇOBANLAR","03DAZKIRI","03DİNAR","03EMİRDAĞ","03EVCİLER","03HOCALAR","03İNCEHİSAR",
                                    "03KIZILÖREN","03SANDIKLI","03SİNCANLI","03SULTANDAĞI","03İHSANİYE","03ŞUHUT","04AĞRI","04DİYADİN","04DOĞUBEYAZIT","04ELEŞKİRT",
                                    "04HAMUR","04PATNOS","04TATLIÇAY","04TUTAK","05AMASYA","05GÜMÜŞHACIKÖY","05GÖYNÜCEK","05HAMAMÖZÜ","05MERZİFON","05SULUOVA",
                                    "05TAŞOVA","06ANKARA","06AKYURT","06ALTINDAĞ","06AYAŞ","06BALA","06BEYPAZARI","06ÇAMLIDERE","06ÇANKAYA","06ÇUBUK",
                                    "06ELMADAĞ","06ETİMESGUT","06EVREN","06GÜDÜL","06GÖLBAŞI","06HAYMANA","06KALECİK","06KAZAN","06KEÇİÖREN ","06KIZILCAHAMAM",
                                    "06MAMAK","06NALLIHAN","06POLATLI","06SİNCAN","06YENİMAHALLE","06ŞEREFLİKOÇHİSAR","07ANTALYA","07KEPEZ ","07KONYAALTI","07MURATPAŞA",
                                    "07AKSEKİ","07ALANYA","07ELMALI","07FİNİKE","07GAZİPAŞA","07GÜNDOĞMUŞ","07İBRALI","07KALE","07KAŞ","07KEMER",
                                    "07KORKUTELİ","07KUMLUCA","07MANAVGAT","07MANAVGAT","08ARTVİN","08ARDANUÇ","08ARHAVİ","08BOKÇA","08HOPA","08MURGUL",
                                    "08YUSUFELİ","08ŞAVŞAT","09AYDIN","09BOZDOĞAN","09BUHARKENT","09ÇİNE","09DİDİM","09GERMENCİK","09İNCİRLİOVA","09KARACASU",
                                    "09KUYUCAK","09KÖŞK","09NAZİLLİ","09SULTANHİSAR","09SÖKE","09YENİPAZAR","09KUŞADASI","09KOÇARLI","09KARPUZLU","10BALIKESİR",
                                    "10AYVALIK","10BALYA","10BANDIRMA","10BİGADİÇ","10BURHANİYE","10DURSUNBEY","10EDREMİT","10ERDEK","10GÖMEÇ","10GÖNEN",
                                    "10HAVRAN","10İVRİNDİ","10KEPSUT","10MANYAS","10MARMARA","10SAVAŞTEPE","10SUSURLUK","10SINDIRGI","11BİLECİK","11BÖZÜYÜK",
                                    "11GÖLPAZARI","11İNHİSAR","11OSMANELİ","11PAZARYERİ","11SÖĞÜT","11YENİPAZAR","12BİNGÖL","12ADAKLI","12GENÇ","12KARLIOVA",
                                    "12KIĞI","12SOLHAN","12YAYLADERE","12YEDİSU","13BİTLİS","13ADİLCEVAZ","13AHLAT","13GÜROYMAK","13HİZAN","13MUTKİ",
                                    "13TATVAN","14BOLU","14DÖRTDİVAN","14GEREDE","14GÖYNÜK","14KIBRISCIK","14MENGEN","14MUDURNU","14SEBEN","14YENİÇAĞA",
                                    "15BURDUR","15AĞLASUN","15ALTINYAYLA","15BUCAK","15ÇAVDIR","15ÇELTİKLİ","15GÖLHİSAR","15KARAMANLI","15KEMER","15TEFENNİ",
                                    "15YEŞİLOVA","16BURSA","16BÜYÜKORHAN","16GEMLİK","16GÜRSU","16HARMANCIK","16İNEGÖL","16İZNİK","16KARACABEY","16KELEŞ",
                                    "16KESTEL","16MUDANYA","16MUSTAFAKEMALPAŞA","16NİLÜFER","16ORHANELİ","16ORHANGAZİ","16OSMANGAZİ","16YENİŞEHİR","16YILDIRIM ","17ÇANAKKALE",
                                    "17AYVACIK","17BAYRAMİÇ","17BİGA","17BOZCAADA","17ÇAN","17ECEABAT","17EZİNE","17GELİBOLU","17GÖKÇEADA","17LAPSEKİ",
                                    "17YENİCE","18ÇANKIRI","18ATKARACALAR","18BAYRAMÖREN","18ÇERKEŞ","18ELDİVAN","18ILGAZ","18KORGUN","18KURŞUNLU","18KIZILIRMAK",
                                    "18ORTA","18YAPRAKLI","18ŞABANÖZÜ","19ÇORUM","19ALACA","19BAYAT","19BOĞAZKALE","19DOGURGA","19KARGI","19LAÇİN",
                                    "19MECİTÖZÜ","19OĞUZLAR","19ORTAKÖY","19OSMANCIK","19SUNGURLU","19UĞURLUDAĞ","19İSKİLİP","20DENİZLİ","20ACIPAYAM","20AKKÖY",
                                    "20BABADAĞ","20BAKİLİ","20BAKLAN","20BEYAĞAÇ","20BOZKURT","20BULDAN","20ÇAL","20ÇAMELİ","20ÇARDAK","20ÇİVRİL",
                                    "20GÜNEY","20HONAZ","20KALE","20SARAYKÖY","20SERİNHİSAR","20TAVAS","21DİYARBAKIR","21BİSMİL","21ÇERMİK","21ÇINAR",
                                    "21ÇÜNGÜŞ","21DİCLE","21EĞİL","21ERGANİ","21HANİ","21HAZRO","21KOCAKÖY","21KULP","21LİCE","21SİLVAN",
                                    "22EDİRNE","22ENEZ","22HAVSA","22İPSALA","22KEŞAN","22LALAPAŞA","22MERİÇ","22SÜLOĞLU","22UZUNKÖPRÜ","23ELAZIĞ",
                                    "23AĞIN","23ALACAKAYA","23ARICAK","23BAŞKİL","23HARPUT","23KARAKOÇAN","23KEBAN","23KOVANCILAR","23KEBAN","23KOVANCILAR",
                                    "23MADEN","23PALU","23SİVRİCE","24ERZİNCAN","24ÇAYIRLI","24İLİÇ","24KEMAH","24KEMALİYE","24OTLUKBELİ","24REFAHİYE",
                                    "24TERCAN","24ÜZÜMLÜ","25ERZURUM","25AŞKALE","25ÇAT","25HORASAN","25HINIS","25ILICA","25İSPİR","25KARAÇOBAN",
                                    "25KARAYAZI","25KÖPRÜKÖY","25NARMAN","25OLTU","25OLUR","25PASİNLER","25PAZARYOLU","25TEKMAN","25TORTUM","25UZUNDERE",
                                    "25ŞENKAYA","26ESKİŞEHİR","26ALPU","26BEYLİKOVA","26ÇİFTELER","26GÜNYÜZÜ","26HAN","26İNÖNÜ","26MAHMUDİYE","26MİHALGAZİ",
                                    "26MİHALÇCIK","26SARICAKAYA","26SEYİTGAZİ","26SİVRİHİSAR","27GAZİANTEP","27ARABAN","27ISLAHİYE","27KARGAMIŞ","27NİZİP","27NURDAĞ",
                                    "27OĞUZELİ","27YAVUZELİ","27ŞAHİNBEY","27ŞEHİTKAMİL","28GİRESUN","28ALUCRA","28BULANCAK","28ÇAMOLUK","28ÇANAKÇI","28DERELİ",
                                    "28DOĞANKENT","28ESPİYE","28EYNESİL","28GÜCE","28GÖRELE","28KEŞAP","28PİRAZİZ","28TİREBOLU","28YAĞLIDERE","28ŞEBİNKARAHİSAR",
                                    "29GÜMÜŞHANE","29KELKİT","29KÜRTÜN","29KÖSE","29TORUL","29ŞİRAN","30HAKKARİ","30BAĞIŞLI","30GEÇİTLİ","30ÇUKURCA",
                                    "30YÜKSEKOVA","30ŞEMDİNLİ","31HATAY","31ALTINÖZÜ","31BELEN","31DÖRTYOL","31ERZİN","31HASSA","31İSKENDERUN","31KUMLU",
                                    "31KIRIKHAN","31REYHANLI","31SAMANDAĞ","31YAYLADAĞI","32ISPARTA","32AKSU","32ATABEY","32EĞİRDİR","32GELENDOST","32GÖNEN",
                                    "32KEÇİBORLU","32SENİRKENT","32SÜTÇÜLER","32ULUBORLU","32YALVAÇ","32YENİŞARBADEMLİ","32ŞARKİKARAAĞAÇ","33MERSİN","33ANAMUR","33AYDINCIK",
                                    "33BOZYAZI","33ÇAMLIYAYLA","33ERDEMLİ","33GÜLNAR","33MUT","33SİLİFKE","33TARSUS","34İSTANBUL","34ADALAR","34AVCILAR",
                                    "34BAĞCILAR","34BAHÇELİEVLER","34BAKIRKÖY","34BAYRAMPAŞA","34BEYKOZ","34BEYOĞLU","34BEŞİKTAŞ","34BÜYÜKÇEKMECE","34ÇATALCA","34EMİNÖNÜ",
                                    "34ESENLER","34EYÜP","34FATİH","34GAZİOSMANPAŞA","34GÜNGÖREN","34KADIKÖY","34KAĞITHANE","34KARTAL ","34KÜÇÜKÇEKMECE","34MALTEPE",
                                    "34PENDİK","34SARIYER","34SİLİVRİ","34SULTANBEYLİ","34TUZLA","34ÜMRANİYE","34ÜSKÜDAR","34ZEYTİNBURNU","34ŞİLE","35İZMİR",
                                    "35ALİAĞA","35BALÇOVA","35BAYINDIR","35BERGAMA","35BEYDAĞ","35BORNOVA","35BUCA","35ÇEŞME","35ÇİĞLİ","35DİKİLİ",
                                    "35FOÇA","35GAZİEMİR","35GÜZELBAHÇE","35KARABURUN","35KARŞIYAKA","35KEMALPAŞA","35KİRAZ","35KONAK","35KINIK","35MENDERES",
                                    "35MENEMEN","35NARLIDERE","35SEFERHİSAR","35SELÇUK","35TİRE ","35TORBALI","35URLA","35ÖDEMİŞ","36KARS","36AKYAKA",
                                    "36ARPAÇAY","36DİGOR","36KAĞIZMAN","36SARIKAMIŞ","36SELİM","36SUSUZ","37KASTAMONU","37ABANA","37AĞLI","37ARAÇ",
                                    "37AZDAVAY","37BOZKURT","37ÇATALZEYTİN","37CİDE","37DADAY","37DEVREKANİ","37DOĞANYURT","37HANÖNÜ","37İHSANGAZİ","37İNEBOLU",
                                    "37KÜRE","37PINARBAŞI","37SEYDİLER","37TAŞKÖPRÜ","37TOSYA","37ŞENPAZAR","38KAYSERİ","38AKIŞLA","38BÜNYAN","38DEVELİ",
                                    "38FELAHİYE","38HACILAR","38İNCESU","38KOCASİNAN","38MELİKGAZİ","38PINARBAŞI","38SARIOĞLAN","38SARIZ","38TALAS","38TOMARZA",
                                    "38YAHYALI","38YEŞİLHİSAR","38ÖZVATAN","39KIRKLARELİ","39BABAESKİ","39DEMİRKÖY","39KOFÇAZ","39LÜLEBURGAZ","39PEHLİVANKÖY","39PINARHİSAR",
                                    "39VİZE","40KIRŞEHİR","40AKÇAKENT","40AKPINAR","40BOZTEPE","40ÇİÇEKDAĞI","40KAMAN","40MUCUR","41İZMİT","41GEBZE",
                                    "41GÖLCÜK","41KANDIRA","41KARAMÜRSEL","41KÖRFEZ","42KONYA","42AHIRLI","42AKÖREN","42AKŞEHİR","42ALTINEKİN","42BEYŞEHİR",
                                    "42BOZKIR","42ÇELTİK","42CİHANBEYLİ","42ÇUMRU","42DERBENT","42DEREBUCAK","42DOĞANHİSAR","42EMİRGAZİ","42EREĞLİ","42GÜNEYSINIR",
                                    "42HADIM","42HALKAPINAR","42HÜYÜK","42ILGIN","42KADINHANI","42KARAPINAR","42KARATAY","42KULU","42MERAM","42SARAYÖNÜ",
                                    "42SELÇUKLU","42SEYDİŞEHİR","42TAŞKENT","42TUZLUKÇU","42ALIHÜYÜK","42YUNAK","43KÜTAHYA","43ALTINTAŞ","43ASLANAPA","43ÇAVDARHİSAR",
                                    "43DOMANİÇ","43DUMLUPINAR","43EMET","43GEDİZ","43HİSARCIK","43PAZARLAR","43SİMAV","43TAVŞANLI","43ŞAPHANE","44MALATYA",
                                    "44AKÇADAĞ","44ARAPKİR","44ARGUVAN","44BATTALGAZİ","44DARENDE","44DOĞANYOL","44DOĞANŞEHİR","44HEKİMHAN","44KALE","44KULUNCUK",
                                    "44PÖTÜRGE","44YAZIHAN","44YEŞİLYURT","45MANİSA","45AHMETLİ","45AKHİSAR","45ALAŞEHİR","45DEMİRCİ","45GÖLMARMARA","45GÖRDES",
                                    "45KULA","45KIRKAĞAÇ","45KÖPRÜBAŞI","45SALİHLİ","45SARUHANLI","45SARIGÖL","45SELENDİ","45SOMA","45TURGUTLU","46KAHRAMANMARAŞ",
                                    "46AFŞİN","46ANDIRAN","46ÇAĞLAYANCERİT","46EKİNÖZÜ","46ELBİSTAN","46GÖKSUN","46NURHAK","46PAZARCIK","46TÜRKOĞLU","47MARDİN",
                                    "47DARGEÇİT","47DERİK","47KIZILTEPE","47MAZIDAĞI","47MİDYAT","47NUSAYBİN","47SAVUR","47YEŞİLLİ","47ÖMERLİ","48MUĞLA",
                                    "48BODRUM","48DALAMAN","48DATÇA","48FETHİYE","48KAVAKLIDERE","48KÖYCEĞİZ","48MARMARİS","48MİLAS","48ORTACA","48ULA",
                                    "48YATAĞAN","49MUŞ","49BULANIK","49HASKÖY","49KORKUT","49MALZGİRT","49VARTO","50NEVŞEHİR","50ACIGÖL","50AVANOS",
                                    "50DERİNKUYU","50GÜLŞEHİR","50HACIBEKTAŞ","50KOZAKLI","50ÜRGÜP","51NİĞDE","51ALTUNHİSAR","51BOR","51ÇAMARDI","51ÇİFTLİK",
                                    "51ULUKIŞLA","52ORDU","52AKKUŞ","52AYBASTI","52ÇAMAŞ","52ÇATALPINAR","52ÇAYBAŞI","52FATSA","52GÜKYALI","52GÜRGENTEPE",
                                    "52GÖLKÖY","52İKİZCE","52KABADÜZ","52KABATAŞ","52KORGAN","52KUMRU","52MESUDİYE","52PERŞEMBE","52ULUBEY","52ÜNYE",
                                    "53FINDIKLI","53GÜNEYSU","53HEMŞİN","53İKİZDERE","53İYİDERE","53KALKANDERE","53PAZAR","53RİZE","53ARDEŞEN","53ÇAMLIHEMŞİN",
                                    "53ÇAYELİ","53DEREPAZARI","54ADAPAZARI","54AKYAZI","54FERİZLİ","54GEYVE","54HENDEK","54KARAPÜRÇEK","54KARASU","54KAYNARCA",
                                    "54KOCAALİ","54PAMUKOVA","54SAPANCA","54SÖĞÜTLÜ","54TARAKLI","55ÇARŞAMBA","55HAVZA","55KAVAK","55LADİK","55ONDOKUZMAYIS",
                                    "55SALIPAZARI","55TEKKEKÖY","55TERME","55VEZİRKÖPRÜ","55YAKAKENT","55SAMSUN","55ALAÇAM ","55ASARCIK","55AYVACIK","55BAFRA",
                                    "56PERVARİ","56ŞİRVAN","57SİNOP","57AYANCIK","57BOYABAT","56SİİRT","56AYDINLAR","56BAYKAN","56ERUH","56KURTALAN",
                                    "57DİKMEN","57DURAĞAN","57ERFELEK","57GERZE","57SARAYDÜZÜ","57TÜRKELİ","58SİVAS","58AKINCILAR","58ALTINYAYLA","58DİVRİĞİ",
                                    "58DOĞANHİSAR","58GEMEREK","58GÜLOVA","58GÜRÜN","58HAFİK","58İMRANLI","58KANGAL","58KOYULHİSAR","58SUŞEHRİ","58ULAŞ",
                                    "58YILDIZELİ","58ZARA","58ŞARKIŞLA","59TEKİRDAĞ","59ÇERKEZKÖY","59ÇORLU","59HAYRABOLU","59MALKARA","59MARMARAEREĞLİSİ","59MURATLI",
                                    "59SARAY","59ŞARKÖY","60TOKAT","60ALMUŞ","60ARTOVA","60BAŞÇİFTLİK","60ERBAA","60NİKSAR","60PAZAR","60REŞADİYE",
                                    "60SULUSARAY","60TURHAL","60YEŞİLYURT","60ZİLE","61TRABZON","61AKÇAABAT","61ARAKLI","61ASRİN","61BEŞİKDÜZÜ","61ÇARŞIBAŞI",
                                    "61ÇAYKARA","61DERNEKPAZARI","61DÜZKÖY","61HAYRAT","61KÖPRÜBAŞI","61MAÇKA","61OF","61SÜRMENE","61TONYA","61VAKFIKEBİR",
                                    "61YOMRA","61ŞALPAZARI","62TUNCELİ","62ÇEMİŞGEZEK","62HOZAT","62MAZGİRT","62NAZIMİYE","62OVACIK","62PERTEK","62PÜLÜMÜR",
                                    "63HALFETİ","63HARRAN","63HİLVAN","63SİVEREK","63SURUÇ","63ŞANLIURFA","63AKÇAKALE","63BİRECİK","63BOZOVA","63CEYLANPINAR",
                                    "63VİRANŞEHİR","64UŞAK","64BANAZ","64EŞME","64KARAHALLI","64SİVASLI","64ULUBEY","65VAN","65BAHÇESARAY","65BAŞKALE",
                                    "65ÇALDIRAN","65ÇATAK","65EDREMİT","65ERCİŞ","65GEVAŞ","65GÜRPINAR","65MURADİYE","65ÖZALP","65SARAY","66YOZGAT",
                                    "66AKDAĞMADENİ","66AYDINCIK","66BOĞAZLIYAN","66ÇANDIR","66ÇAYIRALAN","66ÇEKEREK","66KADIŞEHRİ","66SARAYKENT","66SARIKAYA","66SORGUN",
                                    "66YENİFAKILI","66YERKÖY","66ŞEFAATLİ","67ZONGULDAK","67ALAPLI","67ÇAYCUMA","67DEVREK","67EREĞLİ","67GÖKÇEBEY","68AKSARAY",
                                    "68AĞAÇÖREN","68ESKİL","68GÜLAĞAÇ","68GÜZELYURT","68ORTAKÖY","68SARIYAHŞİ","69BAYBURT","69AYDINTEPE","69DEMİRÖZÜ","70KARAMAN",
                                    "70AYRANCI","70BAŞYAYLA","70ERMENEK","70KAZIMKARABEKİR","70SARIVELİLER","71KARAKEÇİLİ","71KESKİN","71SULAKYURT","71YAHŞİHAN","72BATMAN",
                                    "71KIRIKKALE","71BAHŞILI","71BALIŞEYH","71ÇELEBİ","71DELİCE","72BEŞİRİ","72GERCÜŞ","72HASANKEYF","72KOZLUK","72SASON",
                                    "73ŞIRNAK","73BEYTÜŞŞEBAP","73CİZRE","73GÜÇLÜKONAK","73SİLOPİ","73ULUDERE","73İDİL","74BARTIN","74AMASRA","74KURUCAŞİLE",
                                    "74ULUS","75ARDAHAN","75ÇILDIR","75DAMAL","75GÖLE","75HANAK","75POSOF","76IĞDIR","76ARALIK","76KARAKOYUNLU",
                                    "76TUZLUCA","77YALOVA","77ALTINOVA","77ARMUTLU","77ÇİFTLİKKÖY","77ÇINARCIK","77TERMAL","78KARABÜK","78EFLANİ","78ESKİPAZAR",
                                    "78OVACIK","78SAFRANBOLU","78YENİCE","79KİLİS","79ELBEYLİ","79MUSABEYLİ","79POLATELİ","80OSMANİYE","80BAHÇE","80DÜZİÇİ",
                                    "80HASANBEYLİ","80KADİRLİ","80SUMBAŞ","80TOPRAKKALE","81DÜZCE","81AKÇAKOCA","81ÇİLİMLİ","81CUMAYERİ","81GÜMÜŞOVA","81GÖLYAKA","81KAYNAŞLI","81YIĞILCA",
    };


    // --- METOT 1: İLLERİ GETİR ---
    public static List<Soru> TumIlleriGetir()
    {
        List<Soru> liste = new List<Soru>();
        for (int i = 1; i <= 81; i++)
        {
            // i.ToString("D2") -> Sayı tek haneliyse başına 0 koyar. 1 -> "01", 10 -> "10"
            liste.Add(new Soru { Ad = illerDizisi[i], Plaka = i.ToString("D2") });
        }
        return liste;
    }


    // --- METOT 2: İLÇELERİ PARÇALA VE GETİR ---
    public static List<Soru> TumIlceleriGetir()
    {
        List<Soru> liste = new List<Soru>();

        foreach (string veri in ilcelerDizisi)
        {
            // Plaka artık string olduğu için Convert yapmamıza gerek kalmadı!
            // İlk 2 karakteri doğrudan alıyoruz. Örn: "01ADANA" -> "01"
            string plaka = veri.Substring(0, 2);

            // 2. indeksten sonrasını kelime olarak al (Örn: "ADANA")
            string ilceAdi = veri.Substring(2);

            liste.Add(new Soru { Ad = ilceAdi, Plaka = plaka });
        }

        return liste;
    }


}