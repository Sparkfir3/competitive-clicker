using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GUI_Player : MonoBehaviour
{
    private GUI_Control guicontrol;

    public int playerNumber;
    [HideInInspector]
    public Color playerColor;

    public Image healthImage;
    public Text moneyAmountText;

    public bool setup = false;

    public Unit_GeneratorHandler genHandler;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (guicontrol == null)
        {
            guicontrol = GUI_Control.instance;
            if (guicontrol == null)
            {
                Debug.Log("guicontrol is null.");
            }
        }

        if (!setup)
        {
            playerColor = GUI_Control.instance.playerColors[playerNumber];
            healthImage.color = playerColor;
        }
    }

    public void UpdateMoneyText(int value) {
        moneyAmountText.text = $"{value}";
    }
}
