# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

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

Game starts in `TitleScreen` state. Pressing fire (Space bar or GamePad A button) transitions to `Playing` state and initializes level 1.

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

**Key design pattern**: Objects that need collision detection inherit from `CollisionObject`, which provides a `CollisionRectangle` property using `System.Drawing.Rectangle`. All moving objects implement `IMovingObject` with methods: `MoveAuto()`, `MoveLeft()`, `MoveRight()`, `MoveUp()`, `MoveDown()`.

### Movement System

Movement is handled through the `IMovingObject` interface:
- **MoveAuto()**: Called every frame for automatic movement (stars scrolling, alien AI)
- **MoveLeft/Right/Up/Down()**: Player-controlled or AI-controlled directional movement
- **Note**: In `BaseMovingObject`, Left/Right movement is inverted (Left adds to X, Right subtracts) due to the game's coordinate system where player input is relative to stars scrolling in opposite direction

### Level Management

`GameLevel` class manages:
- Level configuration (number of aliens, waves, alien types)
- Alien population and spawning
- Wave system - when all aliens are destroyed and waves remain, `ResetAliens()` repopulates the level
- Active alien counter - decrements on alien death via `RemoveAlien()`

Levels are configured in `GetGameLevel()` switch statement in `DzGame.cs`. Each level defines alien images array and creates a `GameLevel` instance.

### Content Pipeline (MonoGame MGCB)

Assets are managed through `Content/Content.mgcb`:
- **Textures**: Use `TextureImporter`/`TextureProcessor` with ColorKey transparency (magenta: 255,0,255,255)
- **Audio**: Use `WavImporter`/`SoundEffectProcessor`
- **Fonts**: Avoid using `FontDescriptionProcessor` - it requires freetype6 library which causes build issues on macOS

**Font rendering workaround**: The title screen uses a custom pixel-based text renderer (`TitleScreen.cs`) that draws characters using rectangles and a pixel texture, avoiding SpriteFont/freetype6 dependency.

### Input Handling

Game supports dual input:
- **Keyboard**: Arrow keys for movement, Space for fire, Escape to exit
- **Gamepad**: Left thumbstick for movement, A button for fire, Back button to exit

Both input methods are checked simultaneously in `Update()` using `Keyboard.GetState()` and `GamePad.GetState(PlayerIndex.One)`.

## Key Files

- `DzGame.cs`: Main game loop, state management, rendering, collision detection
- `GameLevel.cs`: Level configuration, alien population, wave management
- `TitleScreen.cs`: Custom pixel font renderer for title screen
- `Content/Content.mgcb`: MonoGame content pipeline configuration
- `Enums/GameState.cs`: Game state enum (TitleScreen, Playing)
- `Enums/MovingObjectType.cs`: Object type enum for collision filtering

## Common Gotchas

1. **Coordinate System**: Left/Right movement appears inverted in `BaseMovingObject` because player controls move the world, not the player ship (stars scroll in opposite direction)

2. **MonoGame Content Build**: Changes to `.bmp`, `.wav`, or other content files require rebuilding the content pipeline. MonoGame automatically rebuilds changed assets.

3. **Collision Detection**: Uses `System.Drawing.Rectangle.IntersectsWith()` not `Microsoft.Xna.Framework.Rectangle`. Collision rectangles must be updated when objects move (currently only set in constructor - may need updates for moving objects).

4. **Active Flag**: Objects use `Active` boolean for lifecycle management. Inactive objects are removed via `_movingObjects.RemoveAll(listItem => !listItem.Active)` in Update loop.

5. **Fire Rate Limiting**: `_validBullet` counter prevents rapid-fire by requiring counter > 9 before allowing new bullet. Increments every frame, resets to 0 on fire.

## Branch Strategy

- `main`: Stable code
- `feature/*`: New features (e.g., current branch `feature/title-screen-test`)
