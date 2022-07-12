using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScreenshotHandler : IDisposable
{
    private readonly int _width;
    private readonly int _height;
    private readonly Camera _camera;

    private readonly RenderTexture _renderTexture;
    private readonly Texture2D _screenshot;
    
    public ScreenshotHandler(int width, int height, Camera camera)
    {
        _width = width;
        _height = height;
        _camera = camera;
        
        _renderTexture = new RenderTexture(_width, _height, 16);
        _screenshot = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
    }

    public byte[] TakeScreenshot(CameraClearFlags flags = CameraClearFlags.Nothing)
    {
        if (_camera == null)
        {
            Debug.LogError("There is no camera!");
        }
        
        _camera.targetTexture = _renderTexture;
        RenderTexture.active = _renderTexture;
        CameraClearFlags temp = _camera.clearFlags;
        _camera.clearFlags = flags;
        _camera.Render();
        _camera.clearFlags = temp;
        _screenshot.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);

        _camera.targetTexture = null;
        RenderTexture.active = null;
        
        byte[] imgData = _screenshot.EncodeToPNG();
        return imgData;
    }

    public void Dispose()
    {
        _renderTexture.Release();
        
        if (Application.isEditor)
        {
            Object.DestroyImmediate(_renderTexture);
        }
        else
        {
            Object.Destroy(_renderTexture);
        }
    }
}
