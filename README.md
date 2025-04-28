# **Match-2 / Match Blast Puzzle Core**

Welcome to the repository for a modular, performant, and highly maintainable Match-2 Puzzle Core System, built using Unity and C#.

### **This project is designed with a strong emphasis on:**

* Clean Architecture
* SOLID Principles
* Modularity and Reusability
* Animation-Driven Feedback Loops
* Efficient Memory and Performance Management (Mobile Ready)

It demonstrates an engineering mindset towards building scalable casual games while keeping the codebase clean, understandable, and easily extendable for new features.

### **Core Features**

* Dynamic Grid System:
  A generic, reusable grid management structure (GridSystem<T>) enabling fast item placement, movement, and removal.

* Block Matching System:
  Blocks are dynamically matched via a **DFS (Depth-First Search)** algorithm to find connected groups of the same type.

* Gravity and Refill Logic:
  After a match and removal, blocks fall smoothly to fill gaps and new blocks are spawned dynamically with tweens.

* Icon Management:
  Block visuals change dynamically based on match group size (e.g., small, medium, large groups) to provide visual feedback.

* Optimized Object Pooling:
  All blocks are handled with pooling (ObjectPool<T>) to minimize GC pressure and maximize runtime performance on mobile.

* Fully Decoupled Design:
  Each manager (GridManager, GameManager, MatchController, etc.) respects single responsibility, making the system highly maintainable.

* ScriptableObject Driven Configurations:
  Matchable block types and icons are managed through MatchableBlockIconSO for easy extension and content updates.

### **System Architecture Overview**

* Managers:
  Orchestrate the high-level gameplay systems (e.g., GameManager, LevelManager, GridManager).

* Logic Layer:
  Contains reusable logic (e.g., GravityController, BlockMatcher, MatchIconController) decoupled from scene-specific behaviors.

* Blocks:
  Represents visual and interactive units on the board (e.g., Block, MatchableBlock) supporting tween-based animation and user input.

* Cores:
  Provides shared core systems like grid management and pooling mechanics.