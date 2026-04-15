# ⌨️ Typing Game

A CLI typing speed practice game built with C# 

## 📋 Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [How to Play](#how-to-play)
- [Game Mechanics](#game-mechanics)
- [Leaderboard System](#leaderboard-system)
- [Project Structure](#project-structure)
- [Usage](#usage)

## ✨ Features

- **Live Feedback**: Real-time character display with color coding (green for correct, red for incorrect)
- **Customizable Rounds**: Choose how many sentences you want to type (you can edit the sentences.txt to add more or to delete some)
- **Detailed Statistics**:
  - Words Per Minute (WPM)
  - Accuracy percentage
  - Total mistakes
  - Time taken

- **Leaderboard System**: 
  - Permanently saves your scores as JSON
  - View top 10 scores ranked by WPM
  - See how well you perforomed: number of sentences, how long it took you in total for all sentences, how many mistakes happened, accuracy, and the Date of playtime

- **Main Menu**: Easy navigation between playing and viewing scores

## 🛠️ Requirements

- **.NET 10** or higher
- **C# 14.0** or compatible version
- Windows/Linux/macOS (cross-platform)

## 📥 Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/shahzebmughel/TypingGame.git
   cd TypingGame
   ```

2. **Build the project**:
   ```bash
   dotnet build
   ```

3. **Run the game**:
   ```bash
   dotnet run
   ```

## 🎮 How to Play

1. **Start the application** - You'll see the main menu
2. **Select "Play Game"** - Option 1
3. **Choose number of sentences** - Enter how many sentences you want to type (1-50 Default Sentences inside the Text file)
4. **Type each sentence** - Match the displayed sentence character-by-character:
   - Correct characters appear in **green**
   - Incorrect characters appear in **red**
   - Use **Backspace** to correct mistakes
5. **Complete each round** - Press any key to move to the next sentence
6. **View your final score** - See your WPM, accuracy, and mistakes
7. **Score is automatically saved** to the leaderboard

### Main Menu Options:
- **1**: Play Game
- **2**: View Leaderboard (top 10 scores)
- **3**: Exit

## 🎯 Game Mechanics

### Scoring System

**Words Per Minute (WPM)**:
```
WPM = (totalCharacters / 5.0) / totalMinutes;
```
Standard typing convention where 5 characters = 1 word.

**Accuracy**:
```
Accuracy = Math.Max(0, ((double)(totalCharacters - totalMistakesMade) / totalCharacters) * 100);
```

**Timing**:

Calculates total time taken for all sentences in minutes:

  ```
  totalMinutes = totalTimePassed / 60.0;
  ```
- Measures how well you performed each round and overall time taken for all sentences


## 📊 Leaderboard System

### Features

- **Persistent Storage**: Scores are saved in `leaderboard.json`
- **Ranked by WPM**: Top scores are sorted by Words Per Minute (highest first)
- **Contextual Information**: Each entry shows:
  - Rank position
  - WPM achieved
  - Accuracy percentage
  - Number of sentences completed
  - Mistakes made
  - Date and time of the game

### Leaderboard Display

```
Rank | WPM | Accuracy | Sentences | Mistakes | Date
1    | 65.3| 98.5%    | 5         | 2        | 2024-01-15 14:30
2    | 58.2| 96.0%    | 5         | 3        | 2024-01-15 14:25
```

## 📁 Project Structure

```
TypingGame/
├── Program.cs              # Main game logic and menu system
├── LeaderboardManager.cs   # Handles score persistence (JSON)
├── ScoreEntry.cs           # Data model for individual scores
├── sentences.txt           # Database of typing sentences
├── leaderboard.json        # Persistent leaderboard (auto-generated)
├── TypingGame.csproj       # Project configuration
└── README.md               # This file
```

### File Descriptions

| File | Purpose |
|------|---------|
| `Program.cs` | Core game logic, input handling, main menu, and round management |
| `LeaderboardManager.cs` | JSON serialization, leaderboard display, score persistence |
| `ScoreEntry.cs` | Data model containing score statistics (WPM, accuracy, etc.) |
| `sentences.txt` | Plain text file with one sentence per line for typing practice |
| `leaderboard.json` | Auto-generated JSON file storing all historical scores |

## 🚀 Usage

### Basic Workflow

1. **First Run**: Game creates `leaderboard.json` automatically on first score save
2. **Playing**: Follow menu prompts to select number of rounds
3. **Scoring**: Automatically saved after each game session
4. **Viewing**: Access leaderboard anytime from main menu

### Adding Custom Sentences

Edit `sentences.txt` and add one sentence per line:

```
Your first custom sentence goes here.
Add as many sentences as you'd like.
Each one should be on a new line.
```

The game will automatically load them on next run.


### Potential Enhancements
- Difficulty levels (Easy/Medium/Hard)
- Timed mode gameplay
- GUI version
---
