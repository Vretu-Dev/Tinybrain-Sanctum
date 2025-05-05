# Tinybrain-Sanctum. The SCP: Secret Laboratory Plugins Collection

This repository contains a collection of custom plugins developed for **SCP: Secret Laboratory (Exiled framework)**. These plugins aim to enhance gameplay, assist in event creation, and modify server mechanics in creative ways.

## Plugins Overview

### 🔁 AutoSR
Automatically restarts the server after a configurable amount of time with a broadcast message to all players.

### 🩸 Bleeding
Players will gradually lose health if:
- Attacked by SCP-096 or SCP-939.
- Their HP drops below 20.

### 🚪 CBDoor
Changes the door-closing mechanics to behave more similarly to the classic *SCP: Containment Breach* game.

### 🔑 ClassDChamberKeycard
Class-D cell doors require an **Armory Level Keycard** (e.g., Guard card) to open. Adds more control over Class-D movement.

### 🧹 ClearItems
Periodically clears specified items from the ground after a set amount of time to reduce clutter and improve performance.

### 💥 DisableWarhead
Permanently disables the Alpha Warhead, useful for custom events or scenarios where detonation is not desired.

### 🃏 FakeO5Card
A joke card that looks like a real keycard. When used, it turns the player into a spectator. Ideal for fun or troll events.

### 🎒 InventoryReset
When a player escapes, their inventory is reset, removing all previously held items upon respawning as another class.

### 🔇 MuteCassie
Disables CASSIE voice announcements while keeping subtitles visible for immersive but silent gameplay.

### 👓 No Goggles
Prevents SCP-1344 (Wallhack Goggles) from spawning naturally in the game world.

### 🌀 SCP-1162
Allows players to exchange items in SCP-173’s chamber (original plugin by `xRoier`, recompiled for the latest Exiled version).

### 🧢 ScrambleHat (SCP-268)
A special item that, when equipped:
- Allows the wearer to look at SCP-096 without triggering its rage.
- Prevents the wearer from damaging SCP-096.

---
## Installation

1. Download the desired plugin `.dll` file(s) from Releases.
2. Place them into your server's `Plugins` folder.
3. Restart your server.
4. Configure settings in the generated config files as needed.
---
## License

Feel free to use and modify these plugins for personal or server use. Attribution is appreciated.

