using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UBoundsFix))]
public class UBoundsFixEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UBoundsFix uBoundsFix = (UBoundsFix)target;

        
        DrawDefaultInspector();
        
        if(GUILayout.Button("Detect"))
            uBoundsFix.Detect();
        if(GUILayout.Button("Fix"))
            uBoundsFix.Fix();
    }
}
