# Neon Dash Trail

---

## Vision

**Neon Dash Trail** is a small 2D runner developed with Godot and C#. Instead of a traditional endless mode, it features 
handcrafted levels. The goal of the project is to learn and demonstrate the fundamentals of game development with Godot.

The game combines fast, precise controls with a neon aesthetic and offers a series of pre-designed levels, each featuring 
its own challenges and design ideas.

This project also serves as a devlog to make the development process transparent and provide insights into creating a 
small but complete game.

### Naming Conventions

- Entity
  -A larger, cohesive scene that actively exists within the game (e.g. Runner).
- Manager
  - Manages game states (e.g. MenuManager).
- [Connectors (Explanation)](docs/pattern/ConnectorPattern.md)
  - A system for easily connecting signals across multiple layers of components  

---

## Devlog

Welcome to the development diary of Neon Dash Trail! Here, I regularly document progress, challenges, and exciting insights.

### Version 0.5 - Wall run (2025-10-06)

- Implemented WallRunWalls and wall running functionality
  - Wall run is possible in all 4 directions

### Version 0.4 – The Dash (2025-07-07)

- Implemented a state machine for easier extension of runner states
- Runner can now dash on key press

### Version 0.3 – First Level Structures (2025-07-06)

- Checkpoints to which the runner can be moved
- Checkpoints are automatically activated when the runner passes by
- The runner can be reset to a checkpoint by pressing a key or upon colliding with an obstacle
- When reaching the goal, the main menu is displayed
- Entering the JumpingPad triggers a higher jump

![Version 03 animation](docs/gifs/version_03.gif)

### Version 0.2 – Menus (2025-07-03)

- Main and pause menu implemented
- Players can pause the game during gameplay
- Communication via connectors between distant nodes

### Version 0.1 – Start (2025-07-02)

- Project initialized with Godot 4.4
- First player character and movement implemented
- Simple level created with basic mechanics (running, jumping, obstacles)

![Version 01 animation](docs/gifs/version_01.gif)

---

Thank you for reading – Jonas 👋🏻

## Assets & Quellen

### Sound effects

👉🏻 [Detailed listing](docs/sources/audio_sources.md)

- Various sound effects from [Kenney.nl](https://kenney.nl/) (Public Domain, CC0)
