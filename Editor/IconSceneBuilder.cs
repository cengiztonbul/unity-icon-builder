using System.Collections;
using System.Collections.Generic;
using IconBuilder.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class IconSceneBuilder
{
    [MenuItem("Assets/Create/Icon Builder", true)]
    private static bool ObjectValidator()
    {
        return Selection.activeGameObject != null;
    }

    [MenuItem("Assets/Create/Icon Builder", false)]
    private static void CreateIconBuilderScene()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        
        IconBuilderWindow wd = IconBuilderWindow.ShowWindow();
        
        wd.Initialized = false;
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        GameObject createdObj = Object.Instantiate(obj, Vector3.zero, Quaternion.identity);
        
        wd.SetWindow(obj.name, createdObj.transform);
    }
}
