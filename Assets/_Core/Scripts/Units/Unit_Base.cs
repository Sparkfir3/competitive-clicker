using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Base : MonoBehaviour
{
    public int player;
    public Color unitColor;
    public Image mainImage;

    public GameObject HPDisplay;
    public float maxHP;
    public float currentHP;

    protected void Update()
    {
        CustomUpdate();
    }

    protected virtual void CustomUpdate()
    {

    }

    public void Setup()
    {
        mainImage.color = unitColor;
    }

    public void DisplayHP()
    {
        if (currentHP < maxHP)
        {
            HPDisplay.SetActive(true);
        }
        else
        {
            HPDisplay.SetActive(false);
        }
    }
}
