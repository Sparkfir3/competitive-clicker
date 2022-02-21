using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GUI_Control : MonoBehaviour
{
    public static GUI_Control instance;

    public Transform[] playerGUIs;
    public float mainXOffset = 100f;
    public float mainYOffset = 100f;

    public GUI_Player[] playerDisplays;
    public Color[] playerColors = { Color.black, Color.black };

    public bool setup = false;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (playerGUIs == null || playerGUIs.Length != 2)
        {
            Debug.Log("Incorrect length of playerGUIs");
            return;
        }

        int direction = -1;

        for (int i = 0; i < 2 ; i++)
        {
            playerGUIs[i].position = new Vector3((Screen.width / 2) + (Screen.width * mainXOffset * direction), (Screen.height * mainYOffset), 0);
            direction *= -1;
        }

        if (!setup)
        {
            if (playerDisplays == null || playerDisplays.Length != 2)
            {
                Debug.Log("Incorrect length of playerDisplays");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                playerDisplays[i].playerNumber = i + 1;
                playerDisplays[i].playerColor = playerColors[i];
            }

            setup = true;
        }
    }

}