#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(InspectorTools))]
public class InspectorToolsGUI : Editor
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnInspectorGUI()
    {
        //EditorGUILayout.LabelField("Click on this window to update values:");
        //EditorGUILayout.LabelField("Lives Left", InspectorTools.GetLivesLeft().ToString());

        if (GUILayout.Button("Delete All Data"))
        {
            InspectorTools.DeleteAllData();
        }

        if (GUILayout.Button("Take Screenshot"))
        {

            InspectorTools.TakeScreenshotInEditor();
        }
    }
}
#endif