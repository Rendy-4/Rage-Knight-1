using UnityEngine;

public class FixFont : MonoBehaviour
{
    void Awake()
    {
        var fonts = Resources.FindObjectsOfTypeAll<Font>();
        foreach (var font in fonts)
        {
            if (font.material != null)
                font.material = new Material(font.material);
        }
    }
}
