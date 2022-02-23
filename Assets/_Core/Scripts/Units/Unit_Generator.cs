using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Generator : Unit_Base
{
    
    [Header("Generator")]
    public Text clickText;
    public GameObject clickSquare;
    public Image clickImage;
    public GameObject genLevel;
    public Text genLevelText;
    public int upgradeLevel = 1;

    public bool isActive = false;
    public bool canClick = false;
    public bool isAuto = false;

    [Header("Stats")]
    [SerializeField] private int generationRate;
    [SerializeField] private int currentLevel = 0;

    protected void Awake() {
        GameController.Instance.OnCycleReady.AddListener(GenerateResources);
    }

    public void Activate()
    {
        isActive = true;
        currentHP = maxHP;
    }

    protected override void CustomUpdate()
    {
        if (isActive)
        {
            canAttack = true;
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
            canAttack = false;
            clickSquare.SetActive(false);
            genLevel.SetActive(false);
            mainImage.gameObject.SetActive(false);
            clickText.gameObject.SetActive(false);

            /*currentHP = 0;
            HPDisplay.SetActive(false);*/
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

            canAttack = false;
        }
    }
    // ------------------------------------------------------------------------------------------

    private void GenerateResources() {
        if(!isActive)
            return;

        GameController.Instance.Players[player].CurrentResources += generationRate * currentLevel;
        Debug.Log($"Generating {generationRate * currentLevel} for player {player}");
    }

    public void LevelUp() {
        if(!isActive) {
            isActive = true;
        }
        currentLevel++;
        genLevelText.text = $"{currentLevel}";
    }

    public int GetCost() {
        if(currentLevel == 0)
            return 5;
        return currentLevel * 5;
    }

}
