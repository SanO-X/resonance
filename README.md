# 🎮 Resonance

**Resonance** is a 2D Top-Down Action/RPG Adventure developed in **Unity** using **C#** and **Universal Render Pipeline (URP)**.

The main feature of the project is a **dynamic World State System** that changes the visual atmosphere of the game world based on the player's behavior.

The world in Resonance is not just a static background. It reacts to the player's pace, aggression and inactivity, creating a more responsive gameplay experience.

---

## 📝 Project Description

Resonance is a single-player 2D game with a top-down perspective and dark fantasy pixel-art style.

The project was created as a diploma / portfolio prototype focused on gameplay systems, combat mechanics and dynamic world adaptation.

The core idea is to show how player actions can directly affect the state of the game world. Depending on how the player behaves, the world smoothly changes its color palette and atmosphere through post-processing effects.

---

## 🎭 World State System

The game world has three main states:

### 🟢 Harmony

Harmony is activated when the player uses a balanced playstyle: movement, careful attacks and normal combat rhythm.

In this state, the world looks softer, brighter and more stable.

### 🔴 Aggression

Aggression appears when the player attacks frequently and quickly defeats enemies.

Using **Global Volume** and **Color Adjustments**, the world gradually shifts into anxious red tones, making the atmosphere more intense and dangerous.

### ⚫ Void

Void appears when the player stays inactive for too long or plays too passively.

In this state, the world loses saturation, becomes pale, grey and visually lifeless.

---

## 🛠 Tech Stack

* **Unity 2022+**
* **C#**
* **Universal Render Pipeline**
* **Unity Tilemap**
* **Global Volume**
* **Color Adjustments**
* **Rigidbody2D**
* **Physics2D.OverlapCircleAll**
* **Animator Controller**
* **UI Slider**

---

## ⚙️ Implemented Mechanics

* Top-down player movement
* Melee combat system
* Attack area detection
* Player health system
* Enemy health system
* Enemy damage and death logic
* Basic enemy AI
* Enemy chase behavior
* Enemy attack radius
* Attack cooldown system
* Death screen
* Scene restart after player death
* Dynamic world state switching
* Smooth visual transitions between world states

---

## 🧠 Project Architecture

### `WorldManager.cs`

The central controller of the World State System.

It is implemented as a Singleton and is responsible for tracking player behavior, attack activity and inactivity time. Based on these values, it switches the world between Harmony, Aggression and Void states.

### `PlayerMovement.cs`

Handles player movement using `Rigidbody2D`.

Movement logic is processed in `FixedUpdate`, while animation parameters are updated based on the player's movement direction and speed.

### `PlayerCombat.cs`

Controls the melee combat system.

The script uses `Physics2D.OverlapCircleAll` to detect enemies inside the attack radius and apply damage to them.

### `PlayerHealth.cs`

Controls the player's health, damage receiving and death logic.

When the player's health reaches zero, the death screen is activated.

### `Enemy.cs`

Stores enemy health and handles damage receiving.

When enemy health reaches zero, the enemy object is destroyed.

### `EnemyAI.cs`

Controls enemy behavior.

Enemies can detect the player inside a chase radius, move toward the player and attack when the player enters the attack radius. The attack system also uses cooldown logic.

### `HealthBar.cs`

Updates the player's health bar through the UI system.

---

## 📂 Repository Structure

The project contains several game scenes inside:

```text
Assets/Scenes/
```

Scene structure:

```text
0 - Starting scene
1 - Combat location
2 - Combat location
3 - Main combat demo scene
4 - Final boss scene
```

The current version is a **vertical slice prototype**.

The main gameplay systems, combat logic, enemy AI and World State System are implemented and demonstrated in the main combat scene.

---

## 🚀 How to Run

1. Clone the repository:

```bash
git clone https://github.com/SanO-X/resonance.git
```

2. Open the project through **Unity Hub**.

3. Use a Unity version with **URP support**.

4. Open the scenes folder:

```text
Assets/Scenes/
```

5. Start scene `0` or the main combat demo scene.

---

## 🎮 Controls

* **WASD / Arrow Keys** — movement
* **Left Mouse Button** — sword attack

---

## 📌 Project Status

The project is currently a working diploma / portfolio prototype.

Implemented core systems:

* player movement;
* combat;
* enemy AI;
* health system;
* death screen;
* scene restart;
* dynamic world state adaptation.

Further development may include:

* additional enemy types;
* improved boss mechanics;
* more world reactions;
* UI polishing;
* sound and music transitions;
* additional levels.

---

## 👤 Author

**Sanham Murshudov**
Junior C# / Unity Developer

GitHub: [SanO-X](https://github.com/SanO-X)
