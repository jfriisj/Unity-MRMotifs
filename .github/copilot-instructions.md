# MR Motifs - Copilot Instructions

## Project Overview
Unity 6 Mixed Reality sample project demonstrating Meta Quest MR features using URP and OpenXR. Contains 4 independent "motifs" (reusable MR patterns) under `Assets/MRMotifs/`.

## AI-Assisted Unity Development (Unity MCP)

This project integrates the **AI Game Developer (Unity-MCP)** tool for direct AI manipulation of Unity Editor. When making changes to GameObjects, scenes, prefabs, or components, prefer using Unity MCP tools over generating code files.

### Key MCP Tools Available
- **Scene/Hierarchy**: `Scene_GetHierarchy`, `GameObject_Create`, `GameObject_Find`, `GameObject_Modify`, `GameObject_AddComponent`
- **Assets**: `Assets_Find`, `Assets_Read`, `Assets_Prefab_Create`, `Assets_Prefab_Instantiate`, `Assets_Material_Create`
- **Scripts**: `Script_CreateOrUpdate`, `Script_Read`, `Script_Execute` (instant Roslyn compilation)
- **Reflection**: `Reflection_MethodFind`, `Reflection_MethodCall` - call any Unity API method directly
- **Editor**: `Editor_GetApplicationInformation`, `Editor_SetApplicationState` (play/pause)
- **Console**: `Console_GetLogs` - check for errors after changes

### Workflow Guidelines
1. **Check scene state first**: Use `Scene_GetHierarchy` to understand current setup before modifications
2. **Use MCP for GameObject operations**: Create, modify, parent, add components via MCP tools rather than writing scripts
3. **Instant code execution**: Use `Script_Execute` for one-off operations using Roslyn compilation
4. **Validate changes**: Call `Console_GetLogs` after operations to catch errors
5. **Create scripts only when needed**: For persistent behaviors, use `Script_CreateOrUpdate` with proper namespaces

### MCP + MR Motifs Example Patterns
```csharp
// When Script_Execute is needed, follow project conventions:
public class Script
{
    public static object Main()
    {
        // Use m_ prefix for local variables matching project style
        var m_cameraRig = UnityEngine.Object.FindAnyObjectByType<OVRCameraRig>();
        return $"Found camera rig: {m_cameraRig != null}";
    }
}
```

## Architecture

### Motif Structure
Each motif is self-contained in `Assets/MRMotifs/{MotifName}/` with subfolders:
- `Scripts/` - C# MonoBehaviours, organized by feature
- `Shaders/` - HLSL shaders and ShaderGraph assets
- `Prefabs/` - Reusable prefab configurations
- `Scenes/` - Sample scene files

### Four Core Motifs
1. **PassthroughTransitioning** - VR↔MR fading via `PassthroughFader.cs` and custom shaders
2. **SharedActivities** - Photon Fusion 2 multiplayer with Meta Avatars (requires `#if FUSION2` directive)
3. **InstantContentPlacement** - MRUK Depth API raycasting and depth shaders
4. **ColocatedExperiences** - Spatial Anchors + Colocation Discovery + Space Sharing

### Shared Assets
`Assets/MRMotifs/Shared Assets/` contains cross-motif components:
- `MenuPanel.cs` - Scene navigation and UI controls
- `ConstraintInjectorMotif.cs` - Runtime constraint injection

## Code Conventions

### Naming Patterns
- Public classes use `*Motif` suffix: `AvatarMovementHandlerMotif`, `SpawnManagerMotif`
- Shader files use `*Motif` suffix: `DepthLookingGlassMotif.shader`
- Private fields: `m_camelCase` prefix
- Shader property IDs: `s_propertyName` prefix with `Shader.PropertyToID()`

### Meta SDK Patterns
```csharp
// Passthrough readiness check
m_oVRPassthroughLayer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);

// Spatial anchor async operations
await anchor.SaveAnchorAsync();
await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, unboundAnchors);

// Camera rig alignment for colocation
cameraRigTransform.position = anchor.transform.InverseTransformPoint(Vector3.zero);
cameraRigTransform.eulerAngles = new Vector3(0, -anchor.transform.eulerAngles.y, 0);
```

### Multiplayer (Photon Fusion 2)
All networked code wrapped in `#if FUSION2`:
```csharp
[Networked, Capacity(8)]
private NetworkArray<Vector3> AvatarPositions => default;

[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
private void SendPositionAndRotationRpc(int index, Vector3 pos, Quaternion rot) { }
```

### Code Samples Attribute
All sample scripts use Meta's code sample attribute for documentation:
```csharp
[MetaCodeSample("MRMotifs-PassthroughTransitioning")]
public class PassthroughFader : MonoBehaviour
```

## Key Dependencies
- Meta XR Core SDK (`com.meta.xr.sdk.core`) - OVRManager, OVRPassthroughLayer
- Meta MR Utility Kit (`com.meta.xr.mrutilitykit`) - MRUK, EnvironmentRaycastManager
- Meta Avatars SDK (`com.meta.xr.sdk.avatars`) - AvatarEntity, AvatarBehaviourFusion
- Photon Fusion 2 + Voice - NetworkRunner, NetworkBehaviour
- Oculus Interaction SDK - InteractableUnityEventWrapper, RayInteractable, PokeInteractable

## Build & Test
- Target: Meta Quest devices (Android)
- Render Pipeline: URP (Universal Render Pipeline Asset in Assets/)
- Entry point: `Assets/MRMotifs/MRMotifsHome.unity`
- Multiplayer testing: Use ParrelSync for multiple editor instances

## Common Patterns to Follow
1. Use `FindAnyObjectByType<T>()` for singleton discovery (Unity 6 pattern)
2. Await spatial anchor operations before proceeding
3. Parent remote avatars to "object of interest" for synchronized movement
4. Use UnityEvents for fade callbacks and interaction hooks
5. Shader render queue `Transparent-1 (2999)` for passthrough spheres with `Cull Off`
