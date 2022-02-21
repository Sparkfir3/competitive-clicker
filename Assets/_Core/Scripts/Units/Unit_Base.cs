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
    public float maxHP = 1000;
    public float currentHP = 1000;

    public Image currentHPDisplay;

    public float moveSpeed = 1;
    public float attackSpeed = 1;
    public float attackRange = 1;
    public float damage = 100;

    protected void Update()
    {
        DisplayHP();
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
            currentHPDisplay.fillAmount = currentHP / maxHP;
        }
        else
        {
            HPDisplay.SetActive(false);
        }
    }
}
