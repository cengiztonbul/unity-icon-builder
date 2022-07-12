using UnityEditor;
using UnityEngine;

namespace IconBuilder.Editor.EditorComponents
{
    public class CameraSettings
    {
        private readonly Camera _camera;
        private Transform _target;
        private Vector3 _offset;
    
        private float _distance = 8;
        private float _xAngle = 55;
        private float _yAngle = 25;
        
        public CameraClearFlags Flags { get; private set; }
    
        public CameraSettings(Camera camera)
        {
            _camera = camera;
            Flags = CameraClearFlags.Nothing;
        }

        public void DrawCameraSettings()
        {
            GUILayout.Label("Camera Settings", EditorStyles.boldLabel);
        
            _target = (Transform)EditorGUILayout.ObjectField("Target", _target, typeof(Transform), true);
        
            EditorGUILayout.Space(5);
            _xAngle = EditorGUILayout.FloatField("X Angle", _xAngle);
            _yAngle = EditorGUILayout.FloatField("Y Angle", _yAngle);
            _distance = EditorGUILayout.Slider("Distance", _distance, 0, 15);
            _offset = EditorGUILayout.Vector3Field("Offset", _offset);

            EditorGUILayout.Space(5);
            _camera.orthographic = EditorGUILayout.Toggle("Orthographic", _camera.orthographic);
            
            if (_camera.orthographic)
            {
                _camera.orthographicSize = EditorGUILayout.FloatField("Size", _camera.orthographicSize);
            }
            else
            {
                _camera.fieldOfView = EditorGUILayout.FloatField("FOV", _camera.fieldOfView);
            }
        
            EditorGUILayout.Space(5);
            Flags = (CameraClearFlags)EditorGUILayout.EnumPopup("Background", Flags);
        
            UpdateCamera();
        }

        public void UpdateCamera()
        {
            Vector3 resultPosition = SphericalToCartesian(_distance, _xAngle, _yAngle);
            Vector3 focusPos = _target != null ? _target.position : Vector3.zero;
        
            _camera.transform.position = focusPos + resultPosition + _offset;
            _camera.transform.LookAt(focusPos + _offset);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private static Vector3 SphericalToCartesian(float radius, float polar, float elevation)
        {
            polar *= Mathf.Deg2Rad;
            elevation *= Mathf.Deg2Rad;

            Vector3 res;
        
            float a = radius * Mathf.Cos(elevation);
            res.x = a * Mathf.Cos(polar);
            res.y = radius * Mathf.Sin(elevation);
            res.z = a * Mathf.Sin(polar);
        
            return res;
        }
    }
}
