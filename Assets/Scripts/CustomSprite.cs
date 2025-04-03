using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB; // StandaloneFileBrowser package
using System.IO;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class CustomSprite : MonoBehaviour
{
    public Image investigatorImg;
    private string path = null;
    public Sprite investigatorSprite;

    public void OpenFilePicker()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select an Image", "", "png", false);
        if (paths.Length > 0)
        {
            path = paths[0];
            if (!string.IsNullOrEmpty(path))
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                // Convert Texture2D to Sprite
                investigatorSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

                investigatorImg.sprite = investigatorSprite;
            }
        }
    }
}