# HARITA SISTEMI DOKUMANI

## Genel Bakis

Oyun iki ayri harita katmanindan olusur:
1. **Dunya Haritasi** - Tum oyuncularin bulundugu buyuk harita
2. **Sehir Haritasi** - Oyuncunun kendi ussu/sehri

---

# BOLUM 1: DUNYA HARITASI

## 1.1 Grid Yapisi

```
Yapi: HEXAGONAL (Altigen)
Boyut: 2000 x 2000 hex tile
Toplam Tile: 4.000.000 hex

Koordinat Sistemi: Axial (q, r)
   _____
  /     \
 /  q,r  \
 \       /
  \_____/
```

### Hexagonal Koordinat Sistemi

```
Axial Koordinatlar (q, r):

        (-1,-1) (0,-1) (1,-1)
            \    |    /
       (-1,0) - (0,0) - (1,0)
            /    |    \
        (-1,1)  (0,1)  (1,1)

Komsuluk (6 yon):
- Kuzey-Dogu: (q+1, r-1)
- Dogu:       (q+1, r)
- Guney-Dogu: (q, r+1)
- Guney-Bati: (q-1, r+1)
- Bati:       (q-1, r)
- Kuzey-Bati: (q, r-1)
```

### Mesafe Hesaplama

```
Hex Mesafe Formulu:
distance = (abs(q1-q2) + abs(q1+r1-q2-r2) + abs(r1-r2)) / 2
```

---

## 1.2 Bolge Sistemi (Zones)

Harita 4 bolgeden olusur, merkeze dogru zorlasir:

```
+--------------------------------------------------+
|                                                  |
|    BOLGE 4 (Dis Cevre) - Baslangic Bolgesi       |
|    +----------------------------------------+    |
|    |                                        |    |
|    |    BOLGE 3 - Orta Seviye               |    |
|    |    +------------------------------+    |    |
|    |    |                              |    |    |
|    |    |    BOLGE 2 - Ileri Seviye    |    |    |
|    |    |    +--------------------+    |    |    |
|    |    |    |                    |    |    |    |
|    |    |    |   BOLGE 1          |    |    |    |
|    |    |    |   (MERKEZ)         |    |    |    |
|    |    |    |   Krallik Kalesi   |    |    |    |
|    |    |    |                    |    |    |    |
|    |    |    +--------------------+    |    |    |
|    |    |                              |    |    |
|    |    +------------------------------+    |    |
|    |                                        |    |
|    +----------------------------------------+    |
|                                                  |
+--------------------------------------------------+
```

| Bolge | Yaricap (hex) | Ozellik | Kaynak Seviyesi |
|-------|---------------|---------|-----------------|
| Bolge 4 | 800-1000 | Baslangic, guvenli | Level 1-2 |
| Bolge 3 | 500-800 | Orta zorluk | Level 2-3 |
| Bolge 2 | 200-500 | Zor, PvP yogun | Level 3-4 |
| Bolge 1 | 0-200 | Merkez, en degerli | Level 4-5 |

### Bolge Gecisleri (Pass/Kaplar)

Bolgeler arasi gecis icin kapilar vardir:

```
Her bolge arasinda 4-8 adet gecit noktasi
Gecitler ittifaklar tarafindan kontrol edilebilir
Kontrol eden ittifak: Gecis ucreti alabilir veya kapatabilir
```

---

## 1.3 Harita Elemanlari

### 1.3.1 Oyuncu Sehri

```
Gorunum: Kale ikonu + Bayrak (ulus rengi)
Boyut: 1 hex
Bilgi Gosterimi:
  - Oyuncu adi
  - Guc seviyesi
  - Ittifak etiketi
  - Kalkan durumu (varsa)

Ozellikler:
  - Tasima: Teleport ile
  - Saldiri: Diger oyuncular saldirabiliir
  - Koruma: Kalkan aktifken saldirilmaz
```

### 1.3.2 Ittifak Yapilari

#### Ittifak Merkezi (HQ)
```
Boyut: 3x3 hex (merkez + cevre)
Kurulum: Ittifak lideri kurar
Ozellikler:
  - Ittifak uyelerinin toplanma noktasi
  - Bolge genisletme baslangici
  - Yok edilirse ittifak bonuslari kaybolur

HP: 1.000.000
Savunma Bonusu: +50%
```

#### Ittifak Kulesi
```
Boyut: 1 hex
Kurulum: R4+ uye kurabilir
Maliyet: Ittifak kaynaklarindan
Ozellikler:
  - Bolge genisletir (yaricap: 5 hex)
  - Kulelerin birbirine bagli olmasi gerekir
  - Savunma gorevi gorur

HP: 100.000
Seviyeler: 1-5 (yukseltme ile daha genis alan)
```

#### Ittifak Bayragi
```
Boyut: 1 hex
Amac: Kaynak noktalarini talep etme
Ozellikler:
  - Kaynak noktasinin yanina dikilir
  - Ittifak uyelerine bonus verir
  - 24 saat sonra aktif olur
```

### 1.3.3 Kaynak Noktalari

| Kaynak | Ikon | Seviyeler | Kapasite | Yenilenme |
|--------|------|-----------|----------|-----------|
| Ciftlik (Yiyecek) | Buğday | 1-5 | 10K-100K | 6 saat |
| Kereste Ormani | Ağaç | 1-5 | 10K-100K | 6 saat |
| Tas Ocagi | Kaya | 1-5 | 8K-80K | 8 saat |
| Demir Madeni | Demir | 1-5 | 5K-50K | 12 saat |
| Altin Madeni | Altin | 1-5 | 2K-20K | 24 saat |
| Gem Madeni | Elmas | 3-5 | 100-1000 | 48 saat |

```
Kaynak Toplama:
- Ordu gonderilir
- Toplama hizi = Asker sayisi x Tasima kapasitesi
- Birden fazla oyuncu ayni kaynakta toplayabilir
- Kaynak bitince yok olur, baska yerde yenisi olusur
```

### 1.3.4 NPC Yapilari

#### Barbar Kampi
```
Seviyeler: 1-30
Odul: Kaynak + XP + Item
Tek oyuncu saldirir
Yenilenme: Yok edilince 1 saat sonra baska yerde olusur

Guc Tablosu:
Seviye 1-5:   1.000 - 5.000 guc
Seviye 6-10:  5.000 - 20.000 guc
Seviye 11-20: 20.000 - 100.000 guc
Seviye 21-30: 100.000 - 500.000 guc
```

#### Barbar Kalesi
```
Seviyeler: 1-10
Rally gerektirir (ittifak ortak saldiri)
Min oyuncu: 2-8 (seviyeye gore)
Odul: Nadir item + cok kaynak + ozel birim

HP: 500.000 - 5.000.000
```

#### Canavar (Boss)
```
Haritada gezen ozel NPC'ler
Belirli saatlerde ortaya cikar
Rally gerektirir
Oldurenler arasinda odul paylasimi
```

### 1.3.5 Stratejik Yapilar

#### Kutsal Mekan (Holy Site)
```
Haritada 8-12 adet sabit konum
Kontrol: Ittifak ele gecirir ve korur
Buff: Tum ittifak uyeleri bonus alir

Buff Turleri:
- Savas Tapinagi: +5% saldiri
- Kaynak Tapinagi: +10% kaynak uretimi
- Hiz Tapinagi: +5% ordu hizi
- Savunma Tapinagi: +5% savunma
```

#### Krallik Kalesi (Lost Temple)
```
Konum: Harita merkezi (0, 0)
Kontrol: En guclu ittifak
Ele geciren ittifak lideri = Kral
Kral Yetkileri:
  - Oyunculara unvan verme
  - Sunucu capiinda buff
  - Ozel etkinlik baslata
```

### 1.3.6 Arazi Turleri

| Arazi | Hareket | Ozellik | Renk Kodu |
|-------|---------|---------|-----------|
| Duz Ova | 1x (normal) | Standart | Acik yesil |
| Orman | 0.7x (yavas) | Gizlenme +20% | Koyu yesil |
| Tepe | 0.5x (yavas) | Savunma +10% | Kahverengi |
| Dag | GECILMEZ | - | Gri |
| Su/Deniz | GECILMEZ | - | Mavi |
| Col | 0.8x | Yiyecek tuketimi +20% | Sari |
| Kar | 0.6x | Yiyecek tuketimi +30% | Beyaz |
| Bataklik | 0.4x | Hastalik riski | Koyu yesil |

---

## 1.4 Sis ve Kesif Sistemi

```
Baslangic: Haritanin %95'i sisli (fog of war)
Kesif Yontemlari:
1. Ordu gonderme - gitigi yolu acar
2. Gozlem kulesi - yakinini acar
3. Kesif itemi - 10x10 hex acar

Acilan Alan Kaliciligi:
- Kendi sehir cevren: Kalici acik
- Ittifak bolgesi: Kalici acik
- Diger: 24 saat sonra tekrar kapanir
```

---

## 1.5 Teleport Sistemi

### Teleport Turleri

| Tur | Kullanim | Maliyet | Kisitlama |
|-----|----------|---------|-----------|
| Rastgele Teleport | Rastgele bos hex'e | 1.000 Altin | Yok |
| Bolge Teleport | Ayni bolge icinde secilen yere | 5.000 Altin | Bos hex olmali |
| Ittifak Teleport | Ittifak bolgesi icine | Ucretsiz | Ittifak uyesi olmali |
| Ileri Teleport | Herhangi bir bos hex | 50 Gem | Kalkan aktif olmamali |
| Baslangic Teleport | Yeni oyuncuya 2 adet | Ucretsiz | Ilk 7 gun |

### Teleport Kurallari

```
Temel Kurallar:
1. Hedef hex bos olmali (bina, dag, su olmamali)
2. Savas sirasinda teleport yapilamaz
3. Ordu disaridayken teleport yapilamaz (tum ordular evde olmali)
4. Teleport sonrasi 5 dakika koruma
5. Kalkan aktifken bazi teleportlar calismaz

Ozel Durumlar:
- Ittifak bolgesi doluya: Bos hex bulana kadar otomatik kaydirilir
- Bolge degisikligi: Bolge siniri gecilirse onay istenir
```

### Teleport Akisi

```
[Teleport Istegi]
       |
       v
[Koşul Kontrolü]----[HATA: Ordu disarida]
       |
       v
[Hedef Hex Kontrolü]----[HATA: Dolu/Gecersiz]
       |
       v
[Maliyet Kontrolü]----[HATA: Yetersiz kaynak]
       |
       v
[Teleport Gerceklestir]
       |
       v
[5 dk Koruma Baslat]
       |
       v
[Harita Guncelle]
```

---

## 1.6 Hareket ve Yol Bulma

### Ordu Hareketi

```
Temel Hiz: 1 hex / dakika (birime gore degisir)

Hiz Modifikatorleri:
- Arazi etkisi (yukarida tablo)
- Kahraman bonusu
- Arastirma bonusu
- Ittifak bonusu
- Item bonusu

Maksimum Menzil:
- Saldiri: Sehirden 200 hex
- Kaynak: Sehirden 150 hex
- Kesif: Sinir yok
```

### A* Yol Bulma Algoritmasi

```
Hexagonal A* icin:
1. Baslangic ve hedef hex belirlenir
2. Komsuluk: 6 yon kontrol edilir
3. Maliyet: Arazi tipi x mesafe
4. Engeller: Dag, su, dolu hex'ler atlanir
5. En kisa + en az maliyetli yol bulunur

Optimizasyon:
- Chunk sistemi (100x100 hex bloklari)
- Onceden hesaplanmis ana yollar
- Hierarchical pathfinding
```

---

## 1.7 Gunduz/Gece Dongusu

```
Saat Dongusu:
- Gunduz: 06:00 - 18:00 (sunucu saati)
- Gece: 18:00 - 06:00

Gece Etkileri:
- Kesif mesafesi %50 azalir
- Surpriz saldiri bonusu +10%
- Bazi canavarlar sadece gece cikar
- Gece gorseli (karanlik filtre)
```

---

# BOLUM 2: SEHIR HARITASI

## 2.1 Sehir Grid Yapisi

```
Yapi: HEXAGONAL (Dunya ile uyumlu)
Boyut: 50 x 50 hex (baslangic)
Genisletilebilir: 100 x 100 hex (max)

+------------------------------------------+
|              KALE (Merkez)               |
|                  ⬡                       |
|            ⬡   ⬡   ⬡                     |
|          ⬡   ⬡   ⬡   ⬡                   |
|        ⬡   ⬡   ⬡   ⬡   ⬡    BINALAR      |
|          ⬡   ⬡   ⬡   ⬡                   |
|            ⬡   ⬡   ⬡                     |
|                                          |
|    ================SUR================   |
|                                          |
|              DIS ALAN                    |
|        (Ciftlik, Maden, Orman)           |
+------------------------------------------+
```

## 2.2 Sehir Bolgeleri

| Bolge | Icerik | Ozellik |
|-------|--------|---------|
| Ic Kale | Ana bina, yonetim binalari | En korunakli |
| Sur Ici | Askeri binalar, depolar | Surla korunur |
| Sur Disi | Kaynak binalari | Saldirilabilir |

## 2.3 Bina Yerlestirme Kurallari

```
1. Her bina belirli hex sayisi kaplar
2. Binalar arasinda minimum 1 hex bosluk
3. Bazi binalar sadece belirli bolgeye kurulur
4. Ana bina (Kale) hareket ettirilemez
5. Yol sistemi: Binalar arasi baglantilar
```

---

# BOLUM 3: VERITABANI SEMALARI (On Tasarim)

## Harita Verileri

```sql
-- Hex Tile Tablosu
CREATE TABLE WorldMapTiles (
    TileId BIGINT PRIMARY KEY,
    Q INT NOT NULL,                    -- Axial koordinat Q
    R INT NOT NULL,                    -- Axial koordinat R
    ZoneId TINYINT NOT NULL,           -- Bolge (1-4)
    TerrainType TINYINT NOT NULL,      -- Arazi tipi
    OccupantType TINYINT NULL,         -- Ne var (bos, sehir, kaynak, vs)
    OccupantId BIGINT NULL,            -- Varsa ID'si
    IsExplored BIT DEFAULT 0,          -- Kesfedildi mi
    LastUpdate DATETIME2,

    INDEX IX_Coords (Q, R),
    INDEX IX_Zone (ZoneId),
    INDEX IX_Occupant (OccupantType, OccupantId)
);

-- Oyuncu Sehir Konumu
CREATE TABLE PlayerCities (
    CityId BIGINT PRIMARY KEY,
    PlayerId BIGINT NOT NULL,
    TileId BIGINT NOT NULL,            -- WorldMapTiles FK
    Q INT NOT NULL,
    R INT NOT NULL,
    ZoneId TINYINT NOT NULL,
    ShieldExpiry DATETIME2 NULL,       -- Kalkan suresi
    LastTeleport DATETIME2,

    FOREIGN KEY (TileId) REFERENCES WorldMapTiles(TileId)
);

-- Kaynak Noktalari
CREATE TABLE ResourceNodes (
    NodeId BIGINT PRIMARY KEY,
    TileId BIGINT NOT NULL,
    ResourceType TINYINT NOT NULL,     -- 1:Food, 2:Wood, 3:Stone, 4:Iron, 5:Gold
    Level TINYINT NOT NULL,            -- 1-5
    CurrentAmount INT NOT NULL,
    MaxAmount INT NOT NULL,
    RespawnTime DATETIME2,

    FOREIGN KEY (TileId) REFERENCES WorldMapTiles(TileId)
);

-- Ittifak Bolgesi
CREATE TABLE AllianceTerritories (
    TerritoryId BIGINT PRIMARY KEY,
    AllianceId BIGINT NOT NULL,
    TileId BIGINT NOT NULL,
    StructureType TINYINT NOT NULL,    -- 1:HQ, 2:Tower, 3:Flag
    Level TINYINT DEFAULT 1,
    HP INT NOT NULL,
    MaxHP INT NOT NULL,

    FOREIGN KEY (TileId) REFERENCES WorldMapTiles(TileId)
);
```

---

# BOLUM 4: ALGORITMA OZETLERI

## 4.1 Hex Mesafe Algoritmasi
```csharp
public int HexDistance(int q1, int r1, int q2, int r2)
{
    return (Math.Abs(q1 - q2)
          + Math.Abs(q1 + r1 - q2 - r2)
          + Math.Abs(r1 - r2)) / 2;
}
```

## 4.2 Komsuluk Algoritmasi
```csharp
public List<(int q, int r)> GetNeighbors(int q, int r)
{
    return new List<(int, int)>
    {
        (q + 1, r - 1),  // Kuzey-Dogu
        (q + 1, r),      // Dogu
        (q, r + 1),      // Guney-Dogu
        (q - 1, r + 1),  // Guney-Bati
        (q - 1, r),      // Bati
        (q, r - 1)       // Kuzey-Bati
    };
}
```

## 4.3 Bolge Belirleme
```csharp
public int GetZone(int q, int r)
{
    int distance = HexDistance(q, r, 0, 0); // Merkeze uzaklik

    if (distance <= 200) return 1;      // Merkez
    if (distance <= 500) return 2;      // Ileri
    if (distance <= 800) return 3;      // Orta
    return 4;                            // Dis
}
```

---

# BOLUM 5: GORUNTULER VE UI

## Dunya Haritasi Ekrani

```
+--------------------------------------------------+
|  [<] Geri    DUNYA HARITASI    [?] Yardim        |
+--------------------------------------------------+
|                                                  |
|     Zoom: [-] ====O==== [+]                      |
|                                                  |
|  +--------------------------------------------+  |
|  |                                            |  |
|  |        ⬡ ⬡ ⬡ ⬡ ⬡ ⬡ ⬡                      |  |
|  |       ⬡ ⬡ 🏰 ⬡ ⬡ ⬡ ⬡                      |  |
|  |        ⬡ ⬡ ⬡ ⬡ 🌲 ⬡ ⬡      [Mini Map]     |  |
|  |       ⬡ ⬡ ⬡ ⬡ ⬡ ⬡ ⬡       +--------+      |  |
|  |        ⬡ ⬡ ⛏️ ⬡ ⬡ ⬡ ⬡      |  . x   |      |  |
|  |       ⬡ ⬡ ⬡ ⬡ ⬡ ⬡ ⬡       |    .   |      |  |
|  |        ⬡ ⬡ ⬡ ⬡ ⬡ ⬡ ⬡      +--------+      |  |
|  |                                            |  |
|  +--------------------------------------------+  |
|                                                  |
|  [🏠 Sehir] [🔍 Ara] [📍 Teleport] [⚔️ Saldiri]  |
+--------------------------------------------------+
```

## Hex Tiklaninca Popup

```
+---------------------------+
|  KERESTE ORMANI Lv.3      |
|---------------------------|
|  Kaynak: 45,000 / 50,000  |
|  Konum: (234, -156)       |
|  Bolge: 3                 |
|---------------------------|
|  [Topla]  [Konum Kaydet]  |
+---------------------------+
```

---

# BOLUM 6: SONRAKI ADIMLAR

Bu dokuman tamamlaninca:
1. [ ] Kaynak sistemi detay dokumani
2. [ ] Bina sistemi detay dokumani
3. [ ] Unity prototip - harita gorunumu
4. [ ] Backend API - harita servisleri
5. [ ] Veritabani tablolari olusturma

---

*Dokuman Versiyonu: 1.0*
*Olusturma Tarihi: 2024*
*Son Guncelleme: -*
