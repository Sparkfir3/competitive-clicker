using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Generator : Unit_Base
{
    public Text clickText;
    public GameObject clickSquare;
    public Image clickImage;
    public GameObject genLevel;
    public Text genLevelText;
    public int upgradeLevel = 1;

    public bool isActive = false;
    public bool canClick = false;
    public bool isAuto = false;

    protected override void CustomUpdate()
    {
        if (isActive)
        {
            genLevel.SetActive(true);
            mainImage.gameObject.SetActive(true);
            mainImage.color = unitColor;

            if (isAuto)
            {
                clickText.gameObject.SetActive(false);
                clickSquare.SetActive(true);
                clickImage.fillAmount = 0.3f; // set this to the cycle timer
                return;
            }
            else
            {
                clickImage.fillAmount = 1;
            }

            if (canClick)
            {
                clickText.gameObject.SetActive(true);
                clickText.text = "A";
                clickSquare.SetActive(true);
            }
            else
            {
                clickText.gameObject.SetActive(false);
                clickSquare.SetActive(false);
            }
        }
        else
        {
            clickSquare.SetActive(false);
            genLevel.SetActive(false);
            mainImage.gameObject.SetActive(false);
            clickText.gameObject.SetActive(false);
        }
    }

    protected override void CheckAlive()
    {
        if (currentHP <= 0)
        {
            isActive = false;
            canClick = false;
            isAuto = false;
            // reset level
            upgradeLevel = 1;
            genLevelText.text = upgradeLevel.ToString();
        }
    }
}
