## NewUpdateFixes
I fixed some of the terrible changes Northwood made in the 13.5 update.

- Cola has pre 13.5 movement speeds
- Cola has pre 13.5 health drain
- SCP-500 can cure the Traumatized effect caused by SCP-106 - (suggested by Follow The Owl – not related to 13.5, but included as an extra feature)
- Jailbird flash effect has pre 13.5.1, lasting 4 seconds

Each of these features is configurable, allowing them to be enabled or disabled as desired.

### Updated Values:
![image](https://github.com/Half-0001/NewUpdateFixes/assets/108597230/f5eda295-91b1-460c-a419-d48f8e6286ce)
### Default Config
```yaml
NUF:
# Whether or not this plugin is enabled.
  is_enabled: true
  # Whether or not debug messages should be shown in the console.
  debug: false
  # Revert SCP-207 speeds back to its pre 13.5 speeds.
  old_cola_speed: true
  # Revert SCP-207 health drain to its pre 13.5 values.
  old_cola_health_drain: true
  # If SCP-500 cures the traumatised effect caused by SCP-106 - suggested by follow The Owl.
  scp500_cures_trauma: false
  # Jailbird Custom Settings.
  enable_custom_jailbird_settings: false
  jailbird_effect: Flashed
  jailbird_effect_duration: 4
  jailbird_effect_intensity: 1
```
For any issues, reach out on Discord: **Half__**