
# Clock Application

The application is a Unity scene, on which there is a canvas with controls.
Application architecture made with MVP (reactive) pattern with **UniRx** and uses DI with **ZenJect**


## Features

- **Simple Clock**
- **Timer**
    - User can set desire time by sliding digits
    - Can Start
    - Can Pause
    - Can Stop
    - After Stop can play sound
- **Stopwatch**
    - Can Start
    - Can Pause
    - Can Stop
    - Can lap time
    - Uses milliseconds

All features can be used independently of each other, which means that the user can turn on the **timer**, switch to a **stopwatch** for example, but the timer will continue its work independently.



## Usage/Examples
The application can be used both by means of UI elements and by calling corresponding methods from the outside via *ClockApplication.cs* facade:

```C#
// Turn on desired time entity (Clock, Timer, StopWatch)
// The method will show the desired entity
ClockApplication.ShowTimeEntity(TimeEntities timeEntity)
```
```C#
// Turn on all entities (uer input buttons panel still visible)
ClockApplication.HideAllTimeEntities()
```
```C#
// Turn off all application UI 
// Entities still working!
ClockApplication.HideApplication()
```
```C#
// Turn on all application UI 
ClockApplication.ShowApplication()
```


## Suggestions for Future Changes
- It is assumed that at the moment you need to interact with timeEntities only through the UI. In the future it is possible to modify the ClockApplication facade to be able to interact via code (Start, Stop, Pause etc.)
- Since the application is now made for desktop platforms, there are suggestions to pay attention to when porting to **IOS/Android**:
  - Screen Size and Resolution. Unity's Canvas Scaler component can help with this.
  - Mobile devices can be used in both portrait and landscape orientations. We might want to design our UI to work well in both orientations, or lock the app to one orientation.
  - We must ensure our app is accessible to as many users as possible. This could involve things like adding larger touch targets for users with motor impairments, or adding a high contrast color scheme for users with visual impairments.
- Allow the user to change the appearance of the UI (font, analog display)
- Add to lap time history the output of the difference between the current and previous lap time 
