# UBoundsFix

Fix the bounds of the Skinned Mesh Renderer.

## Description

Changing the `Root Bone` in the `Skinned Mesh Renderer` doesn't affect the `Bounds`, but the `Bounds` are relative to the `Root Bone`.
This script helps you maintain the (world) position of bounds when changing the `Root Bone`.

## Usage

1. Put the `Scripts/UBoundsFix.cs` in the `Scripts` folder and the `Editor/UboundsFixEditor.cs` in the `Editor` folder.
2. Duplicate your GameObject to back it up.
3. Attach the `UBoundsFix` script to a GameObject (usually the parent of the Skinned Mesh Renderer, or select `Obj` manually). If everything works fine, you will see a list of Skinned Mesh Renderers in the Inspector (or you can assign them manually).
4. (Optional) Click the `Detect` button to detect Skinned Mesh Renderers and original root bones.
5. Assign new root bones. Note that the script won't change anything if the new root bone is the same as the original root bone.
6. Click `Fix`.

## Why

It seems that the root bones are always the same bone from the imported model, so the bounds do not change with animations. As a result, when meshes are in the camera but bounds are not, Unity will not render them. I didn't find a way to keep the bounds while changing the root bone in the Editor, so I wrote this script.
