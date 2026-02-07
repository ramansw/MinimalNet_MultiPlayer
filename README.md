# MinimalNet Multiplayer (Unity)

This repository is a small Unity project that demonstrates a very minimal “send position / receive position” flow.

It is not a full multiplayer stack (no sockets / transport layer). The sample focuses on:

- Moving a local player with keyboard input
- Compressing position values into `short` to reduce payload size
- Passing the compressed values through a simple bridge and applying them to a remote player with smoothing

## Unity version

The project was created with:

- Unity `6000.0.49f1`
- URP enabled (Universal Render Pipeline)

## How to open and run

1. Open Unity Hub
2. Add this folder as a project
3. Open the project
4. Open the scene:

   `Assets/Scenes/SampleScene.unity`

5. Press Play

Controls:

- `WASD` / Arrow keys: move the local player

While running, check the Console to see logs for sent/received positions and the calculated bit size.

## What the sample does

There are two main objects in the scene conceptually:

- **Local player**: reads input, moves, and “sends” its position every frame
- **Remote player**: receives the compressed position and smoothly lerps towards it

The flow is:

1. `LocalPlayerController.Update()` reads `Input.GetAxis("Horizontal")` / `Input.GetAxis("Vertical")` and moves the local player.
2. The local player calls `NetworkBridge.SendPosition()`.
3. `NetworkBridge` compresses position components to `short` using `PositionCompressor`.
4. `RemotePlayerController.ReceivePosition(...)` decompresses back to floats and updates a target position.
5. `RemotePlayerController.Update()` lerps to that target position.

### Position compression

`PositionCompressor` uses a simple fixed precision (`100f`). That means:

- Value is stored as `short` after multiplying by 100 and rounding
- When receiving, it divides by 100 to get back the float

The bridge can optionally include `Y`:

- If `includeY` is `false`, it sends X/Z only (`32` bits)
- If `includeY` is `true`, it sends X/Y/Z (`48` bits)

This is mostly to show how the payload size changes depending on what you include.

## Project layout

- `Assets/Scenes/SampleScene.unity`
  - Main scene for the demo
- `Assets/Problem_1/Scripts/`
  - `LocalPlayerController.cs`
  - `NetworkdBridge.cs` (contains `NetworkBridge`)
  - `PositionCompressor.cs`
  - `RemotePlayerController.cs`

## Notes / troubleshooting

- **Input**: The sample uses `Input.GetAxis(...)` (old Input Manager). If you have issues with input in newer Unity versions, check:
  - `Edit > Project Settings > Player > Active Input Handling`
  - Set to **Both** or **Input Manager (Old)**

- **Remote player reference**: `NetworkBridge` needs a reference to the `RemotePlayerController` in the inspector.



No license file is included. If you plan to reuse this beyond the assignment/demo, add a license that matches what you want.
