

To run the game:
Go to LobbyScene and press play

The LobbyScene:
All sprites are loaded from asset bundles in run time. Press the icon to start a new session.

The SlotGameScene:
The loading screen was created as a quick dirty trick to let all sprites and audioclip load with the player unsuspecting. The loading screen will show for one second. To disable the loading uncheck in inspector (scene manager object in hierarchy).


Memory management:
All texture sizes were reduced. 
All sprites and the audioclip are loaded and unload in runtime from script:

AudioClipAddressableHandler.cs
SpriteAddressableHandler.cs


Resolutions:
The resolution of the game is set for pc and mobile (Landscape). Builds for mobile should restrict only landscape mode.

