using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
    
    public bool GameActive;
    [SerializeField] private List<PlayerController> Players;

    [Header("Game Settings")]
    public float gameLength;
    public float cycleLength;

    private float gameTimer, cycleTimer;
    [HideInInspector] public UnityEvent OnCycleReady;

    // -----------------------------------------------------------------------------------------------------------------

    private void FixedUpdate() {
        if(!GameActive)
            return;

        gameTimer += Time.fixedDeltaTime;
        cycleTimer += Time.fixedDeltaTime;
        if(gameTimer >= gameLength) {
            GameActive = false;
        } else if(cycleTimer >= cycleLength) {
            Debug.Log("Cycle ending");
            OnCycleReady?.Invoke();
            //OnCycleReady.RemoveAllListeners();
            cycleTimer = 0f;
        }
    }

}
