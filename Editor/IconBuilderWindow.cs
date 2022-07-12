using System;
using IconBuilder.Editor.EditorComponents;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IconBuilder.Editor
{
    public class IconBuilderWindow : EditorWindow
    {
        public bool Initialized { get; set; }
        
        private IconProperties _iconProperties;
        private CameraSettings _cameraSettings;
        private DestinationPicker _destinationPicker;
        private ScreenshotButton _screenshotButton; 
    
        [MenuItem("Window/General/Icon Builder")]
        public static IconBuilderWindow ShowWindow()
        {
            return (IconBuilderWindow)EditorWindow.GetWindow(typeof(IconBuilderWindow), false, "Icon Builder", true);
        }
        
        public void Initialize()
        {
            _cameraSettings = new CameraSettings(Camera.main);
            _iconProperties = new IconProperties();
            _destinationPicker = new DestinationPicker();
            _screenshotButton = new ScreenshotButton(_iconProperties, _destinationPicker, _cameraSettings);

            Initialized = true;
        }
        
        private void CreateGUI()
        {
            Initialize();
            EditorSceneManager.newSceneCreated += EditorSceneManagerOnnewSceneCreated;
            EditorSceneManager.sceneOpened += EditorSceneManagerOnsceneOpened;
        }

        private void EditorSceneManagerOnsceneOpened(Scene scene, OpenSceneMode mode)
        {
            Initialize();
        }

        private void EditorSceneManagerOnnewSceneCreated(Scene scene, NewSceneSetup setup, NewSceneMode mode)
        {
            Initialize();
        }

        private void OnDestroy()
        {
            EditorSceneManager.sceneOpened -= EditorSceneManagerOnsceneOpened;
            EditorSceneManager.newSceneCreated -= EditorSceneManagerOnnewSceneCreated;
        }

        private void OnGUI()
        {
            if (!Initialized) return;
            
            _iconProperties?.DrawIconProperties();
        
            EditorGUILayout.Space(10);
            _cameraSettings?.DrawCameraSettings();
        
            EditorGUILayout.Space(20);
            _destinationPicker?.DrawDestinationSettings();
            _screenshotButton?.DrawScreenshotButton();
        }
    
        private void OnInspectorUpdate()
        {
            _cameraSettings?.UpdateCamera();
        }

        public void SetWindow(string name = "", Transform target = null)
        {
            _iconProperties.Name = name;
            _cameraSettings?.SetTarget(target);
        }
    }
}