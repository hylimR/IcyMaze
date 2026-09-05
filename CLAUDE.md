# Icy Maze — working notes

Unity 6.3 LTS (`6000.3.12f1`) remaster of a 2015 Unity 5.1 college project. Built-in
render pipeline, legacy Input Manager.

## The constraint that shapes everything

**Scenes and prefabs must not be edited.** They were authored in Unity 5.1 and there is
no editor available in this repo's tooling to re-save them. So:

* Keep every gameplay script's **class name**, **`.cs.meta` GUID** and **serialised field
  names** exactly as they are. Scene YAML resolves `m_Script` by GUID and matches fields
  by name; rename either and the wiring silently drops.
* Adding a new `[SerializeField]` is safe — Unity keeps the C# initialiser for any key
  absent from the YAML. Renaming an existing one needs `[FormerlySerializedAs]`.
* New behaviour that would otherwise need a scene reference goes in `Assets/Scripts/Core/`
  and is spawned by `GameBootstrap` via `[RuntimeInitializeOnLoadMethod]`.
* Two scenes serialise `canMove: 0` on the player. `PlayerMovementScript.Start` has to
  keep forcing it true, exactly as the original did.

## Architecture

`Core/` is the remaster layer and is never referenced from a scene file:

| Type | Role |
|---|---|
| `GameBootstrap` | `RuntimeInitializeOnLoadMethod` entry point; loads progress, spawns `GameSystems` |
| `GameSystems` | `DontDestroyOnLoad` host for the HUD and pause menu; run timer; pause key |
| `GameProgress` | Run state (trials cleared, deaths, elapsed), `PlayerPrefs`-backed, resettable, raises `Changed` |
| `GameScenes` | Scene names, display names and objective text |
| `SceneFlow` | Hub ⇄ trial transitions through `SceneManager`; owns the hub root reference |
| `GameInput` | All input. `GameplayEnabled` is cleared while a menu owns the screen |
| `GameInput.ActionLatch` | One-shot guard for action presses read from `OnTriggerStay` / `OnCollisionStay` |
| `PlayerRef` | Player identity. Players self-register; legacy tag `Playerchan` is the fallback |
| `Spawner` | `Instantiate` into the *owner's* scene, not the active one |
| `UI/UiBuilder` | Runtime uGUI construction |
| `UI/GameHud` | Objectives, timer, fall count, hints, win screen |
| `UI/PauseMenu` | Resume / restart trial / return to maze / restart run / quit / volume |

Per-trial scripts live in `Scene3/` (hub), `Scene4/` (Trial of Sigils), `scene6/`
(Trial of Storms) and `SceneYang/` (Trial of Embers). The folder names are the 2015
ones and match the scene file names.

## Scene topology

`scene_three` is the hub. Its whole world hangs off one root GameObject named `Main`,
which carries `MasterScript`. Each trial scene likewise has a single root named after
the scene. Entering a portal deactivates `Main` and loads the trial **additively**; the
hub stays the *active* scene throughout, which is why `Spawner.InSceneOf` exists.

## Player identification

The original used three different tests — the name `unitychan`, the name `subunitychan`,
and the tag `Playerchan` — which is why several traps silently did nothing in the scenes
that used the other spelling. Always go through `PlayerRef.Is(...)`.

## Conventions

* Framerate independence: `Time.deltaTime` in `Update`, `Time.fixedDeltaTime` in
  `FixedUpdate`. Never a bare per-frame constant.
* Never use `??` or `?.` on a `UnityEngine.Object` — the overloaded `==` that reports
  destroyed objects as null is bypassed by both.
* No `GetComponent` / `GameObject.Find` / string formatting in a per-frame path.
* Comments explain the non-obvious. Where a rewrite fixed a real defect, the comment says
  what was wrong, so the change is not later "simplified" back.

## Verifying a change

There is no Unity in CI, so before pushing:

```sh
dotnet build Tools/CompileCheck/CompileCheck.csproj -warnaserror
dotnet build Tools/CompileCheck/CompileCheck.Editor.csproj -warnaserror
python3 Tools/check_legacy_apis.py
```

`Tools/CompileCheck/Stubs/` are hand-written Unity API stand-ins. When the build fails on
a missing member, check the real Unity docs first: it is usually a genuine mistake, but
occasionally the stub is simply incomplete and needs the real signature added.

This proves the code type-checks. It proves nothing about serialisation, physics tuning,
UI layout or rendering — those still need someone to open the editor and play it.
