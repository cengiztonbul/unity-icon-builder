using UnityEngine;

namespace IconBuilder.Editor.EditorComponents
{
    public class ScreenshotButton
    {
        private readonly IconProperties _iconProperties;
        private readonly DestinationPicker _destinationPicker;
        private readonly CameraSettings _cameraSettings;

        public ScreenshotButton(IconProperties iconProperties, DestinationPicker destinationPicker, CameraSettings cameraSettings)
        {
            _iconProperties = iconProperties;
            _destinationPicker = destinationPicker;
            _cameraSettings = cameraSettings;
        }

        public void DrawScreenshotButton()
        {
            bool previousGUIState = GUI.enabled;
            GUI.enabled = _destinationPicker.PathAssigned && _iconProperties.NameSet;

            if (GUILayout.Button("Screenshot"))
            {
                TakeScreenshot();
            }
        
            GUI.enabled = previousGUIState;
        }
    
        private void TakeScreenshot()
        {
            using (ScreenshotHandler sc = new ScreenshotHandler(_iconProperties.Width, _iconProperties.Height, Camera.main))
            {
                byte[] image = sc.TakeScreenshot(_cameraSettings.Flags);
                ScreenshotSaveHandler.SaveFile(_destinationPicker.Path, _iconProperties.Name, image);
            }
        }
    }
}
