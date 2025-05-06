![downloads](https://img.shields.io/github/downloads/Vretu-Dev/CaveiraPistol/total)
## What is it?
The Caveira's Pistol is a modified Com-18, inspired by the weapon used by Caveira in Rainbow Six Siege.

### Parameters
- **Base Damage:** 40 (Configurable).
- **Attachments:** Equipped with a suppressor only, just like Caveira's weapon in Rainbow Six Siege.
- **Rampage Mode:** A special ability triggered under specific conditions.
- **Spawn Location:** Configurable (default locations: HID Chamber and 079 Chamber).

### Rampage Mode
**Activated when the player takes damage, Rampage Mode grants the following abilities for 10 seconds (Configurable):**

- **Window Time:** The player has 30 seconds after take damage to activate mode (Configurable)
- **Notification:** The player receives a detection sound triggered by SCP-079.
- **Movement Boost:** The player's speed is increased.
- **Silent Walk:** Footsteps become inaudible.
- **Vitality:** Immunity to SCP-207's damage.
- **Goggle Effect:** Wallhack vision is enabled.
- **Damage Boost:** The pistol's damage output is doubled.

### Config
```yaml
Caveira:
# Whether the plugin is enabled.
  is_enabled: true
  debug: false
  # Notifies when rampage is activated.
  hint: true
  hint_duration: 4
  damage: 40
  rampage_damage_multiplier: 2
  rampage_duration: 10
  rampage_window_activation: 30
  # Spawn locations with their respective chances. Format: Location: Chance
  spawn_locations:
    Inside079Secondary: 100
    InsideHidChamber: 80
  # Should rampage be active when you affected by:
  scp207: false
  scp1853: false
  antiscp207: false
  # Translations:
  rampage_activated: 'Rampage mode active!'
  rampage_fail_use: 'Rampage cannot be used.'
  window_time_active: 'Press key to activate Rampage!'
  window_time_expired: 'The activation time expired.'
```