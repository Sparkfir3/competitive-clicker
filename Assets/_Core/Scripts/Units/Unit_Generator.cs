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

    public bool isActive = false;
    public bool canClick = false;
    public bool isAuto = false;

    [Header("Stats")]
    [SerializeField] private int generationRate;
    [SerializeField] private int currentLevel = 0;

    protected void Awake() {
        GameController.Instance.OnCycleReady.AddListener(GenerateResources);
    }

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
