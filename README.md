# Icy Maze

A top-down puzzle game built in Unity 3D as a college project in 2015, remastered for
Unity 6.3 LTS.

You wander a frozen maze looking for three portals. Each one drops you into a trial —
sliding rune blocks, a lightning storm, a corridor of fire — and clearing all three
finishes the run.

| | |
|---|---|
| **Engine** | Unity 6.3 LTS (`6000.3.12f1`), built-in render pipeline |
| **Original** | Unity 5.1.0f3, 2015 |
| **Character** | [Unity-chan!](https://unity-chan.com/) © Unity Technologies Japan/UCL |

## Playing

Open `Icy Maze/` in Unity and press Play from `Assets/Scenes/scene_three.unity`.
The trials load additively on top of the maze, so any scene can be entered directly
during development.

| Input | Action |
|---|---|
| `WASD` / arrows / left stick | Move |
| `K` / `E` / `Space` | Use switches, tubes, levers |
| `J` / `F` / left mouse | Water gun (Trial of Embers) |
| `Esc` | Pause |
| `F1` / `H` | Toggle the objectives panel |

## What the remaster changed

The scenes, prefabs, models, textures and audio are **byte-for-byte the 2015 assets**.
Everything below is code: every script was rewritten in place, keeping its class name,
asset GUID and serialised field names so the original scene wiring still resolves.

### Engine upgrade

* Retargeted from Unity 5.1 to Unity 6.3 LTS.
* Replaced `Application.LoadLevelAdditive` and friends — removed from the engine in
  2019 — with `SceneManager`. Trials are now genuinely unloaded instead of having
  their root object destroyed while the scene stayed resident.
* `Rigidbody.velocity` → `linearVelocity`, string-typed `GetComponent` calls narrowed,
  `FindObjectOfType` → `FindFirstObjectByType`.
* Dropped the obsolete `Assets/UnityVS` plugin (bundled with Unity since 5.2) and
  ported the Unity-chan sample `SceneLoader` off the removed level-loading API.

### Bugs fixed

Each of these was a real defect in the original, not a style change.

| Where | What was wrong |
|---|---|
| `AlterableBoxScript` | Interpolated from a fixed start vector instead of the block's live position, so toggled blocks moved one step and stopped. The lever puzzle never worked. |
| `SlipperyGroundScript` | `isMoving()` advanced its own reference position on every call, so asking it twice in a frame returned false and control was handed straight back. The ice did nothing. |
| `BlockerScript` | Compared the blocker's *own* name against the player's, so the shove never fired; and started its patrol coroutine twice (`Start` *and* `OnEnable`), leaving blockers jittering between two targets. |
| `CMasterScript` | Read the third magic circle from the fourth's GameObject, so the trial could be finished with one circle uncovered. Also queued a fresh `Invoke` every frame after the win. |
| `GameObjectScript` | Guarded a seven-entry table with `loop > 7`, so the eighth flame box threw `IndexOutOfRangeException` on every single run. |
| `ThunderScript` | Sized its position buffer from `numberOfThunder` but always spawned at least eight markers, overflowing the array; and swept the scene by tag, picking up other spawners' markers. |
| `PlayerScript` | Assigned the spawned water bolt back over the prefab field, so every shot after the first cloned the previous bolt. |
| `FireBallScript`, `WaterGunScript` | Moved a fixed distance per *frame* with no delta time. Correct on the 60 Hz monitor this was built on, two and a half times too fast on a 144 Hz one. |
| `GateScript` | Recomputed its target from its live position, so a second beam hit drove the gate another 29.5 units into the level. |
| `ArrowTubeScript` | Rounded `eulerAngles.y` to an int and missed the 360 case, leaving a fully-rotated tube firing in its old direction. |
| `RayEmitterScript` | Assumed the first thing the beam hit was always a tube; anything else threw a null reference. |
| `MagicCircleScript` | A plain bool, so two blocks overlapping one circle cleared it as soon as either stepped off. |
| `MagnetScript` | Built its four rays once in `Start` from the position the magnet had at load time. |
| `TrapBoxScript` | Overlapping turn triggers flipped direction twice, sticking the box against the wall it had just bounced off. |
| `SwitchScript`, `OnPortalScript` | Called `SetActive` / destroyed the scene root every frame from `Update`. |
| Spawned objects | Trials run as additive scenes while the maze stays *active*, so plain `Instantiate` dropped every fireball, flame box and lightning bolt into the hub scene, where they outlived the trial. |
| Action key | Read with `Input.GetKeyDown` from `OnTriggerStay`, which runs per physics step — a single tap toggled a switch twice whenever a frame carried two steps. |

### Gameplay and presentation

* **Pause menu** (`Esc`) with resume, restart trial, return to the maze, restart run,
  quit and a volume slider. The original had none of these; a trial you could not solve
  could only be left by killing the process.
* **Scalable HUD** replacing the `OnGUI` boxes, which were positioned with literals like
  `Screen.width / 2 + 350` and fell off the screen on anything but a 2015 monitor. It
  tracks the three trials, the run timer and your fall count.
* **Progress is saved** to `PlayerPrefs` and is resettable, so the game can be replayed
  without relaunching — the 2015 build kept run state in mutable statics.
* **Movement** is now a real 2D vector: diagonals work and travel at the same speed as
  the cardinals, instead of applying both translations at once for 41% extra speed.
  Turning is smoothed rather than snapped to one of four hard-coded angles.
* **Input** accepts WASD, arrow keys and a gamepad instead of four literal `KeyCode`s.
* **Camera** follows in `LateUpdate` with damping; it used to snap in `Update`, one frame
  behind the physics step that had moved the player, which read as permanent jitter.
* The blocker shove now works, at a bounded strength — restoring it at the original
  30-unit-impulse-per-physics-step would have fired the player out of the level.

## Layout

```
Icy Maze/Assets/Scripts/
  Core/                     remaster runtime: progress, input, scene flow, HUD, pause menu
  MasterScript.cs           hub presenter
  MainCameraScript.cs       follow camera
  PlayerMovementScript.cs   character controller
  Scene3/  Scene4/  scene6/  SceneYang/    per-trial behaviours
Tools/CompileCheck/         Unity API stubs + csproj that type-checks every script
Tools/check_legacy_apis.py  fails on APIs Unity has removed
```

Nothing in `Core/` is referenced from a scene file. `GameBootstrap` spawns it through
`[RuntimeInitializeOnLoadMethod]`, which is what let the HUD, pause menu, save system and
input layer drop into scenes that were never re-saved.

## Building and checking without Unity

Unity is not needed to type-check the gameplay code:

```sh
dotnet build Tools/CompileCheck/CompileCheck.csproj          # runtime build
dotnet build Tools/CompileCheck/CompileCheck.Editor.csproj   # UNITY_EDITOR branches
python3 Tools/check_legacy_apis.py                           # removed-API sweep
```

`Tools/CompileCheck/Stubs/` holds hand-written stand-ins for the Unity APIs this project
touches, with signatures mirroring the real ones. It catches typos, wrong argument counts
and members that no longer exist; it cannot catch anything about serialisation, physics
or rendering, so it is a gate, not a substitute for opening the editor. Both run in CI on
every push.

`Packages/manifest.json` is deliberately absent: the project predates the Package Manager
and Unity writes a correct manifest itself the first time it opens the project.
