using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ScreenshotSaveHandler
{
    private static string GeneratePath(string path, string filename)
    {
        string res = "";
        int id = 0;
        
        while (true)
        {
            res = PathBuilder(path, filename, id);
            
            if (!File.Exists(res))
            {
                break;
            }

            id++;
        }

        return res;
    }
    
    private static string PathBuilder(string path, string filename, int id)
    {
        if (!path.EndsWith('/'))
        {
            path += "/";
        }

        string res = path + filename + "-" + id + ".png";
        return res;
    }

    public static void SaveFile(string path, string name, byte[] imgData)
    {
        string fullPath = GeneratePath(path, name);
        File.WriteAllBytes(fullPath, imgData);
        
        AssetDatabase.Refresh();
    }
}
