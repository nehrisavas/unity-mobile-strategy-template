# Empire Wars - Unity Kurulum Rehberi

## Hizli Baslangic

### Adim 1: Unity'yi Acin
1. Unity Hub'dan "EmpireWars" projesini acin
2. Proje yuklendiginde Console panelinde hata olmamali

### Adim 2: Sahneyi Kurun (Otomatik)
1. Ust menuden: **EmpireWars > Setup World Map Scene**
2. Acilan pencerede "Sahneyi Kur" butonuna basin
3. Sahneyi kaydedin (Ctrl+S)

### Adim 3: Play Moduna Girin
1. Unity'de Play butonuna basin
2. Harita otomatik olarak olusturulacak
3. Kamera kontrolleri:
   - **WASD/Ok tuslari**: Harekete et
   - **Mouse tekerlek**: Zoom
   - **Sag tik surukleme**: Pan
   - **Sol tik**: Tile sec

---

## KayKit Asset'lerini Baglama

KayKit Medieval Hexagon Pack asset'lerini haritada kullanmak icin:

### 1. Arazi Prefab'lari Olusturun

Her arazi tipi icin prefab olusturun:

| Arazi Tipi | KayKit Model |
|------------|--------------|
| Grass | `hexagon_grass.fbx` |
| Forest | `hexagon_forest.fbx` |
| Mountain | `hexagon_mountain.fbx` |
| Water | `hexagon_water.fbx` |
| Hill | `hexagon_dirt.fbx` |
| Desert | `hexagon_sand.fbx` |
| Snow | `hexagon_snow.fbx` |
| Swamp | `hexagon_swamp.fbx` (veya water + mud) |

### 2. Prefab Olusturma Adimlari

1. `Assets/KayKit_Medieval_Hexagon/Models` klasorunu acin
2. Bir FBX dosyasini (ornegin `hexagon_grass.fbx`) Scene'e surukleyin
3. Model uzerine sag tiklayin > **Prefab > Create Prefab Variant**
4. Prefab'i `Assets/Prefabs/Terrain/` klasorune kaydedin
5. Prefab adini duzenleyin (ornegin: `Grass_Tile`)

### 3. WorldMapManager'a Atama

1. Hierarchy'de **WorldMapManager** objesini secin
2. Inspector'da "Arazi Modelleri" bolumunu bulun
3. Olusturduguzun prefab'lari ilgili alanlara surukleyin:
   - `grassTilePrefab` -> Grass_Tile
   - `waterTilePrefab` -> Water_Tile
   - `mountainPrefab` -> Mountain_Tile
   - vb.

---

## Script Yapisi

```
Assets/Scripts/
├── Core/
│   ├── HexCoordinates.cs    # Hex koordinat sistemi
│   └── HexMetrics.cs        # Hex sabitleri
├── Data/
│   └── TerrainType.cs       # Arazi tipleri
├── Map/
│   ├── WorldMapManager.cs   # Ana harita yoneticisi
│   ├── HexCell.cs           # Tek hex hucresi
│   ├── HexChunk.cs          # Chunk sistemi
│   ├── FogOfWarManager.cs   # Sis sistemi
│   ├── HexPathfinder.cs     # A* yol bulma
│   ├── PathVisualizer.cs    # Yol gorsellestirme
│   ├── HexSelectionManager.cs # Secim sistemi
│   └── ResourceNode.cs      # Kaynak noktalari
├── Camera/
│   └── MapCameraController.cs # Kamera kontrolu
├── UI/
│   ├── TileInfoPanel.cs     # Tile bilgi paneli
│   └── MiniMapController.cs # Mini harita
└── Editor/
    └── WorldMapSetup.cs     # Otomatik kurulum
```

---

## Onemli Notlar

### Performans
- Harita 2000x2000 hex icin tasarlandi
- Chunk sistemi ile sadece gorunen alan yuklenir
- Test icin 100x100 boyutunda baslayin

### Test Icin Kucuk Harita
`HexMetrics.cs` dosyasinda:
```csharp
public const int MapWidth = 100;  // 2000 yerine
public const int MapHeight = 100; // 2000 yerine
```

### Hatalar
Console'da hata gorurseniz:
1. TextMeshPro import edilmis mi kontrol edin
2. Tum scriptler derleniyor mu kontrol edin (Console temiz olmali)
3. Sahne kurulumunu yeniden calistirin

---

## Sonraki Adimlar

1. [ ] KayKit prefab'larini bagla
2. [ ] Kamera ayarlarini optimize et
3. [ ] UI elemanlarini duzenle
4. [ ] Backend baglantisi icin hazirlik
