using UnityEditor;
using UnityEngine;

namespace IconBuilder.Editor.EditorComponents
{
    public class IconProperties
    {
        public string Name { get; set; } = "Icon";
        public int Width { get; private set; } = 256;
        public int Height { get; private set; } = 256;
        public bool NameSet => !string.IsNullOrWhiteSpace(Name);

        public void DrawIconProperties()
        {
            GUILayout.Label ("Properties", EditorStyles.boldLabel);
            Name = EditorGUILayout.TextField("Name", Name);
        
            GUILayout.Space(5);
            Width = EditorGUILayout.IntField("Width", Width);
            Height = EditorGUILayout.IntField("Height", Height);
        }
    }
}
