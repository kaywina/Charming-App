#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(InspectorTools))]
public class InspectorToolsGUI : Editor
{
    public override void OnInspectorGUI()
    {
        //EditorGUILayout.LabelField("Click on this window to update values:");
        //EditorGUILayout.LabelField("Lives Left", InspectorTools.GetLivesLeft().ToString());

        if (GUILayout.Button("Delete PlayerPrefs"))
        {
            InspectorTools.DeleteAllData();
        }

        if (GUILayout.Button("Delete Persistent Data"))
        {
            InspectorTools.DeletePersistentData();
        }

        if (GUILayout.Button("Take Screenshot"))
        {

            InspectorTools.TakeScreenshotInEditor();
        }
    }
}
#endif