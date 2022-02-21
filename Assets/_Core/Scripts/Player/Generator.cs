using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public PlayerController player;
    public bool isActive;
    public int generationRate;
    public int level;

    private void Awake() {
        GameController.Instance.OnCycleReady.AddListener(() => { 
            if(isActive)
                GenerateResources();
        });
        level = 0;
    }

    private void GenerateResources() {
        player.CurrentResources += generationRate * level;
    }

    public void LevelUp() {
        if(!isActive) {
            isActive = true;
        }
        level++;
    }

}
