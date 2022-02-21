using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    // ---
    
    public bool GameActive;
    public List<PlayerController> Players;

    [Header("Game Settings")]
    public float gameLength;
    public float cycleLength;

    private float gameTimer, cycleTimer;
    [HideInInspector] public UnityEvent OnCycleReady;

    // -----------------------------------------------------------------------------------------------------------------

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

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
