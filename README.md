# 🐍 Snake Game - Unity

A classic Snake game with modern twists, implemented in Unity with C#. Guide your snake through the grid, eat food to grow, and avoid crashing into yourself!

![Unity](https://img.shields.io/badge/Unity-2020.3+-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-9.0-blue?logo=c-sharp)
![License](https://img.shields.io/badge/license-MIT-green)

## 🎮 Features

### Core Gameplay
- **Classic Snake Mechanics**: Move around the grid, eat food, and grow your snake
- **Smooth Controls**: Responsive arrow key controls with frame-perfect input handling
- **Collision Detection**: Game over when the snake collides with itself
- **Score Tracking**: Real-time score display with persistent high score system

### Special Food Types
The game features three different types of food, each with unique effects:

| Food Type | Effect | Score |
|-----------|--------|-------|
| 🍎 **Apple** | Grows snake by 1 segment | +1 |
| 🍌 **Banana** | Grows snake by 2 segments | +2 |
| 🍉 **Watermelon** | Grows snake by 1 segment & doubles game speed | +1 |

### Visual Features
- **Directional Eating Animations**: The snake displays different animations based on movement direction (up, down, left, right)
- **Dynamic Sprites**: Animated sprite system for enhanced visual feedback
- **Clean UI**: Score and high score display using TextMesh Pro

### Game Modes
- **Main Menu**: Navigate to game, tutorial, or quit
- **Tutorial Scene**: Learn how to play before starting
- **Game Scene**: The main gameplay experience

## 🎯 How to Play

### Controls
- **Arrow Keys**: Control snake direction
  - `↑` Move Up
  - `↓` Move Down
  - `←` Move Left
  - `→` Move Right

### Rules
1. The snake starts with 4 segments and moves continuously
2. Eat food to grow your snake and increase your score
3. The snake cannot turn 180° directly (e.g., can't go from left to right in one move)
4. Game over occurs when the snake collides with itself
5. High scores are saved between game sessions

### Pro Tips
- Plan your path ahead to avoid trapping yourself
- Be careful after eating watermelons - the increased speed can be tricky!
- Eating bananas gives you double growth, so use them strategically

## 🚀 Installation & Setup

### Prerequisites
- **Unity**: Version 2020.3 or higher
- **Operating System**: Windows, macOS, or Linux

### Running from Source

1. **Clone the repository**
   ```bash
   git clone https://github.com/pavlosgit/snake-game-unity.git
   cd snake-game-unity
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Open" and select the project folder
   - Wait for Unity to import all assets

3. **Play the game**
   - Open `Assets/Scenes/Main menu` scene
   - Click the Play button in Unity Editor
   - Or go to File → Build and Run to create an executable

## 📁 Project Structure

```
snake-game-unity/
├── Assets/
│   ├── Scenes/           # Game scenes (Main menu, Tutorial, Game)
│   ├── Characters/       # Character sprites and assets
│   ├── Resources/        # Game resources
│   ├── snake.cs          # Main snake controller
│   ├── Food.cs           # Food spawning and types
│   ├── ScoreHandler.cs   # Score management and persistence
│   ├── TimeManager.cs    # Game speed controller
│   ├── SceneHandle.cs    # Scene navigation
│   └── Item.cs           # Base item class
├── Game/                 # Built executables
├── ProjectSettings/      # Unity project configuration
└── Packages/             # Unity package dependencies
```

## 🛠️ Technologies Used

- **Game Engine**: Unity
- **Programming Language**: C#
- **UI**: TextMesh Pro
- **Audio**: Unity Audio System
- **Data Persistence**: Unity PlayerPrefs

## 📝 Code Structure

### Main Scripts

- **`Snake.cs`**: Handles snake movement, rendering, collision detection, and growth mechanics
- **`Food.cs`**: Manages food spawning, types, and collision with the snake
- **`ScoreHandler.cs`**: Tracks current score and manages high score persistence using PlayerPrefs
- **`TimeManager.cs`**: Controls game speed (base speed and watermelon speed boost)
- **`SceneHandle.cs`**: Manages scene transitions between menu, tutorial, and game
- **`TutorialController.cs`**: Controls the tutorial scene

## 🎓 Acknowledgments

This project was created as a learning exercise to explore Unity game development and C# programming.

---
👥 Authors

- **Pavlos Papadopoulos**
- **Lawend Mardini**
- **Filip Eriksson**

## 
**Enjoy the game!** 🎮🐍
