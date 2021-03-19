# VRShowroom

Temp

## How to Switch from SteamVR to Oculus and vice Versa

Sadly the OpenVR provider used by SteamVR Applications,
is not included in the XR Plugin Manager yet (hopefully). 
To bypass that problem we have to disable said Plugin Manager
which in return disables the Oculus Provider.
Ergo: We have to hop between packages to switch platforms:

### Quest > SteamVR
- Package Manager > remove Oculus XR Plugin
- Build Settings > PC > switch Platform
- Player Settings > Deprecated Settings > Check VR Support
- Done

 (Sometimes some waiting and restarting of the Unity Player is necessary to detect the HMD)

### SteamVR > Quest:
- Build Settings > Android > Switch Platform
- Connect Quest to PC, start Quest
- Build Settings > Run Device > Refresh > select Quest
- Player Settings > XR Settings > Deprecated Settings > uncheck Virtual Reality Support
- Project Settings > XR Plugin Managment > Select Oculus Provider
- Done
