# MediaKeyBinding

## Description
This is a little C# application which lets you assign the multimedia keys like "Play/Pause", "Stop", "Next" and "Previous" on any key on your keyboard.
It´s main focus is for Citrix XA/XD deployments since the multimedia key wont be redirected and passing the keyboard as native USB is allways a bad idea.

The application starts in systray and at firstrun the config windows (GUI) will appear where you can choose your bindings. 
Once you pressed save the GUI closes and the settings are beeing saved into the registry (HKCU:\Software\MediaKeyBinding). 
Keep in mind that the assigned buttons wont work while the GUI is still open.
If you start the application the second time the saved config gets loaded automatically and the GUI wont show up again.



## Example:
```
F6 = Previous
F7 = Play/Pause
F8 = Next
F9 = Stop

```


## Roadmap:
- Support for key combinations
- ...
