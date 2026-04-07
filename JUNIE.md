# JUNIE.md

This file provides guidance to Junie (JetBrains AI Agent) when working with code in this repository.

## Project Overview

Death Zone (DZ-Game) is a space shooter game built with MonoGame Framework 3.8.1.303 targeting .NET 6.0. The game features a title screen, player-controlled spaceship, alien enemies, multiple levels with waves, collision detection, and visual/audio effects.

## Build & Run Commands

```bash
# Build the project
cd DZ-Game
dotnet build

# Run the game
dotnet run

# Clean build artifacts
dotnet clean

# Restore dependencies and tools (automatically runs before build)
dotnet restore
```

**Important**: The project uses MonoGame Content Pipeline (MGCB) which requires `dotnet tool restore` to be run before building content assets. This is configured to run automatically via a build target.

## Architecture

### Game State Management

The game uses a state machine pattern controlled by `GameState` enum (`TitleScreen`, `Playing`). The main game class `DzGame.cs` switches between states:
- **TitleScreen**: Shows title, displays scrolling stars, waits for fire input
- **Playing**: Full game loop with player control, aliens, collision detection

### Object Hierarchy

All game objects follow an inheritance hierarchy:

```
IMovingObject (interface)
  └── BaseMovingObject (abstract)
       └── CollisionObject (abstract) - adds collision detection
            ├── Alien (abstract) - base for all enemy types
            │    └── Alien1 (concrete)
            ├── Player
            └── Bullet
       └── Star - background scrolling stars
       └── PixelShatter - explosion particle effect
```

### Movement System

Movement is handled through the `IMovingObject` interface:
- **MoveAuto()**: Called every frame for automatic movement (stars scrolling, alien AI)
- **MoveLeft/Right/Up/Down()**: Player-controlled or AI-controlled directional movement
- **Note**: In `BaseMovingObject`, Left/Right movement is inverted (Left adds to X, Right subtracts) due to the game's coordinate system where player input is relative to stars scrolling in opposite direction.

### Level Management

`GameLevel` class manages:
- Level configuration (number of aliens, waves, alien types)
- Alien population and spawning
- Wave system - when all aliens are destroyed and waves remain, `ResetAliens()` repopulates the level

### Content Pipeline (MonoGame MGCB)

Assets are managed through `Content/Content.mgcb`:
- **Textures**: Use `TextureImporter`/`TextureProcessor` with ColorKey transparency (magenta: 255,0,255,255)
- **Audio**: Use `WavImporter`/`SoundEffectProcessor`
- **Fonts**: Avoid using `FontDescriptionProcessor` - it requires freetype6 library which causes build issues on macOS. The project uses a custom pixel-based text renderer instead.

## Key Files

- `DZ-Game/DzGame.cs`: Main game loop, state management, rendering, collision detection
- `DZ-Game/GameObjects/GameLevel.cs`: Level configuration, alien population, wave management
- `DZ-Game/GameObjects/TitleScreen.cs`: Custom pixel font renderer for title screen
- `DZ-Game/Content/Content.mgcb`: MonoGame content pipeline configuration

## Development Guidelines

1. **Code Style**: Follow existing C# 10.0 and .NET 6.0 patterns in the project.
2. **Branch Strategy**: Use `main` for stable code and `feature/*` for new features.
3. **Collision Detection**: Be aware that it uses `System.Drawing.Rectangle.IntersectsWith()`.
4. **Active Flag**: Use the `Active` boolean for object lifecycle management.
5. **Testing**: Add or update reproduction tests for bugs. For features, add tests proportional to complexity.
