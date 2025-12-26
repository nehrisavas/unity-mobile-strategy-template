# Unity Mobile Strategy Game Template

A production-ready Unity 6 template for mobile strategy games like Rise of Kingdoms, Mafia City, or Clash of Clans.

## Features

### Hex Map System
- **2000x2000 hex grid** with procedural terrain generation
- **Chunked tile loading** for massive maps without memory issues
- **On-demand tile data generation** - tiles only created when needed
- **Multiple terrain types**: Grass, Water, Forest, Mountain, Desert, Snow, etc.

### Camera System
- **Professional drag-to-pan** using world plane raycasting
- **Smooth orthographic zoom** with pinch-to-zoom on mobile
- **Keyboard support** (WASD/Arrow keys)
- **Map boundary clamping**

### MiniMap
- **Circular minimap** with real-time camera tracking
- **Click-to-navigate** - tap anywhere on minimap to move camera
- **Viewport indicator** showing current view area
- **Configurable size and zoom levels**

### Mobile Optimization
- **Chunk-based rendering** with configurable load radius
- **Quality settings** optimized for mobile (shadows, LOD, AA)
- **Legacy + New Input System** support for reliable touch handling
- **Android build scripts** included

### Architecture
- **GameConfig** - Centralized configuration system
- **WorldSettings** ScriptableObject for easy tweaking
- **Modular components** - easy to extend

## Project Structure

```
src/
├── client/EmpireWars/          # Unity project
│   ├── Assets/
│   │   ├── Scripts/
│   │   │   ├── Core/           # GameConfig, WorldSettings, Bootstrap
│   │   │   ├── Camera/         # MapCameraController
│   │   │   ├── WorldMap/       # ChunkedTileLoader, TileData
│   │   │   ├── UI/             # MiniMapController
│   │   │   └── Data/           # TerrainType, KingdomMapGenerator
│   │   ├── Editor/             # BuildScript for Android
│   │   ├── Resources/          # Database, Settings
│   │   └── Scenes/             # WorldMap scene
│   └── Packages/               # Unity packages
├── backend/                    # Backend placeholder
├── admin/                      # Admin panel placeholder
└── docs/                       # Design documents
```

## Getting Started

1. Clone this repository
2. Open `src/client/EmpireWars` in Unity 6.x
3. Open `Scenes/WorldMap` scene
4. Press Play

## Requirements

- Unity 6000.3.x LTS (Unity 6)
- Input System package
- Universal Render Pipeline (URP)

## Mobile Build

### Android
```bash
# Using Unity batch mode
Unity.exe -batchmode -projectPath "path/to/EmpireWars" -executeMethod BuildScript.BuildAndroid -quit

# Or use the included script
./batch-build-android.bat
```

## Configuration

Edit `GameConfig.cs` constants or create a `WorldSettings` ScriptableObject in `Resources/` folder:

```csharp
// Map size
DEFAULT_MAP_WIDTH = 2000
DEFAULT_MAP_HEIGHT = 2000

// Camera zoom limits
DEFAULT_MIN_ZOOM = 10f
DEFAULT_MAX_ZOOM = 70f

// Chunk settings (mobile optimized)
MOBILE_CHUNK_SIZE = 16
MOBILE_LOAD_RADIUS = 1
```

## License

MIT License - Use freely for your projects.
