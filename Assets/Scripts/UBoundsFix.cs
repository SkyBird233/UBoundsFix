using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class UBoundsFix : MonoBehaviour
{
    public GameObject obj;
    public List<SkinnedMeshRendererWithRootBone> skinnedMeshRendererList = new List<SkinnedMeshRendererWithRootBone>();

    private void Awake()
    {
        obj = gameObject;
        Detect();
    }

    public void Detect()
    {
        Debug.Log("Detecting...");
        if (!obj)
            Debug.LogError("No model selected");

        var skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        if (skinnedMeshRenderers.Length == 0)
        {
            Debug.Log("No Skinned Mesh Renderer detected");
            return;
        }

        skinnedMeshRendererList.Clear();
        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            Debug.Log($"Detected: {skinnedMeshRenderer.name}");
            var rootBone = skinnedMeshRenderer.rootBone;
            skinnedMeshRendererList.Add(new SkinnedMeshRendererWithRootBone
            {
                skinnedMeshRenderer = skinnedMeshRenderer,
                originalRootBone = rootBone,
                newRootBone = rootBone
            });
        }
    }

    public void Fix()
    {
        Debug.Log("Fixing...");

        var fixCount = 0;
        foreach (var s in skinnedMeshRendererList.Where(s => s.originalRootBone != s.newRootBone))
        {
            Debug.Log($"Fixing {s.skinnedMeshRenderer.name}");
            fixCount++;
            
            var newBounds = s.skinnedMeshRenderer.localBounds;
            var combinedMatrix = s.newRootBone.worldToLocalMatrix * s.originalRootBone.localToWorldMatrix;
            
            s.skinnedMeshRenderer.rootBone = s.newRootBone;
            newBounds.center = combinedMatrix.MultiplyPoint(newBounds.center);
            newBounds.extents = combinedMatrix.MultiplyVector(newBounds.extents);
            s.skinnedMeshRenderer.localBounds = newBounds;
        }

        Debug.Log($"Fixed {fixCount} bounds");
    }
}

[Serializable]
public struct SkinnedMeshRendererWithRootBone
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform originalRootBone;
    public Transform newRootBone;
}