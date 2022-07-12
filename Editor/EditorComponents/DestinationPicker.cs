using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestinationPicker
{
    public string Path { get; private set; }
    private static string SavedPath
    {
        get => EditorPrefs.GetString("IconDestinationPath", "");
        set => EditorPrefs.SetString("IconDestinationPath", value);
    }
    public bool PathAssigned => !string.IsNullOrWhiteSpace(Path);

    public DestinationPicker()
    {
        Path = SavedPath;
    }
    
    public void DrawDestinationSettings()
    {
        GUILayout.Label ("Save", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();

        bool previousGuiState = GUI.enabled;
        GUI.enabled = false;
        
        EditorGUILayout.TextField("Path", Path);
        
        GUI.enabled = previousGuiState; 
        
        if (GUILayout.Button("Set Destination"))
        {
            string res = EditorUtility.OpenFolderPanel("Save Directory", Application.dataPath, "icon");
            
            if (!string.IsNullOrWhiteSpace(res))
            {
                Path = res;
                SavedPath = Path;
            }
        }
        
        GUILayout.EndHorizontal();
    }
}
