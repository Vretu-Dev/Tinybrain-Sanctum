# SCP1853 Buff for EXILED
![downloads](https://img.shields.io/github/downloads/Vretu-Dev/SCP1853Buff/total)
## Features:
- If you have the SCP 1853 effect (Trades your hp into stamina when it's over)
- It is the same solution as in [LifeForMoreStamina](https://github.com/Vretu-Dev/LifeForMoreStamina)
### Minimum Exiled Version: 8.9.8

```yaml
SCP1853Buff:
  is_enabled: true
  debug: false
  # How much stamina does the player receive when it runs out?
  stamina_added: 0.0599999987
  # Stamina level at which it starts to regenerate.
  stamina_threshold: 0.0250000004
  # How much health is deducted?
  hp_removed: 0.5
  # Effect intensity.
  movement_boost_intensity: 10
  # Kills the player when their HP reaches zero.
  kill_on_zero_hp: true
  # Apply Bleeding Effect when HP is depleting (Visual Only)
  visual_effect: true
  # Recommended to set to true; this ignores receiving effects from other plugins or items.
  ignore_another_movement_boost: true
```
