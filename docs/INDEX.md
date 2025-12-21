# ORTACAG STRATEJI OYUNU - ANA DOKUMAN INDEKSI

## Proje Ozeti
| Bilgi | Deger |
|-------|-------|
| **Proje Adi** | [Belirlenecek] |
| **Tur** | Mobil MMO Strateji |
| **Gorus** | 3D Low-Poly Hexagonal |
| **Platform** | Android, iOS, Windows |
| **Tema** | Ortacag - Coklu Medeniyet (8 Ulus) |

## Teknoloji Stack
| Katman | Teknoloji |
|--------|-----------|
| Oyun Client | Unity 3D (C#) |
| Backend API | ASP.NET Core WebAPI |
| Veritabani | Microsoft SQL Server |
| Gercek Zamanli | SignalR |
| Admin Panel | Angular |

## Asset Paketi
| Paket | Link |
|-------|------|
| **KayKit Medieval Hexagon** | https://kaylousberg.itch.io/kaykit-medieval-hexagon |
| Alternatif: EXTRA tier | $9.99 (150+ ekstra asset) |
| Alternatif: POLYGON Fantasy | $349.99 (profesyonel kalite) |

---

# DOKUMANTASYON HARITASI

## 1. OYUN TASARIMI (01-game-design/)

| # | Klasor | Icerik | Durum |
|---|--------|--------|-------|
| 1.0 | `ASSETS.md` | Asset paketi kararlari | [x] TAMAM |
| 1.1 | `nations/` | Uluslar, avantajlar, ozel birimler | [x] TAMAM |
| 1.2 | `map/` | Harita sistemi, tile yapisi, bolgeler | [x] TAMAM |
| 1.3 | `buildings/` | Bina turleri, yukseltme sistemi | [ ] Bekliyor |
| 1.4 | `units/` | Asker birimleri, istatistikler | [ ] Bekliyor |
| 1.5 | `resources/` | Kaynak turleri, ekonomi dengesi | [ ] Bekliyor |
| 1.6 | `combat/` | Savas mekanikleri, PvP/PvE | [ ] Bekliyor |
| 1.7 | `progression/` | Seviye, XP, oduller | [ ] Bekliyor |

## 2. TEKNIK MIMARI (02-technical/)

| # | Klasor | Icerik | Durum |
|---|--------|--------|-------|
| 2.1 | `unity-client/` | Unity proje yapisi, scene'ler | [ ] Bekliyor |
| 2.2 | `backend-api/` | API endpoint'leri, servisler | [ ] Bekliyor |
| 2.3 | `database/` | MSSQL tablo semalari | [ ] Bekliyor |
| 2.4 | `realtime/` | SignalR hub'lari | [ ] Bekliyor |
| 2.5 | `admin-panel/` | Angular yonetim paneli | [ ] Bekliyor |

## 3. ALGORITMALAR (03-algorithms/)

| # | Klasor | Icerik | Durum |
|---|--------|--------|-------|
| 3.1 | `pathfinding/` | A* yol bulma algoritmasi | [ ] Bekliyor |
| 3.2 | `combat-calculation/` | Savas sonuc hesaplama | [ ] Bekliyor |
| 3.3 | `matchmaking/` | Oyuncu eslestirme | [ ] Bekliyor |
| 3.4 | `resource-generation/` | Kaynak uretim formulleri | [ ] Bekliyor |
| 3.5 | `ai/` | NPC ve bot davranislari | [ ] Bekliyor |

---

# HIZLI REFERANS

Konu ararken kullan:

| Aranan | Konum |
|--------|-------|
| Sehir/Us kurma | `buildings/` |
| Asker egitme | `units/` |
| Haritada hareket | `map/` + `pathfinding/` |
| Savas sistemi | `combat/` + `combat-calculation/` |
| Kaynak toplama | `resources/` |
| Ulke bonuslari | `nations/` |
| Veritabani | `database/` |
| API'ler | `backend-api/` |

---

# KARAR LOGU

| Tarih | Karar | Neden |
|-------|-------|-------|
| 2024-XX-XX | 2D secildi | Ucretsiz asset kullanimi, hizli gelistirme |
| 2024-XX-XX | MSSQL secildi | Mevcut kurulum |
| 2024-XX-XX | Unity + ASP.NET | Gelistirici C# biliyor |

---

*Versiyon: 0.1.0*
