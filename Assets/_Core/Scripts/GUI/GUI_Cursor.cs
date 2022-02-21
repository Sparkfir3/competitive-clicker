using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Cursor : MonoBehaviour
{
    public Color color;
    public Image[] images;

    private void Update()
    {
        foreach (Image img in images)
        {
            img.color = color;
        }
    }
}
