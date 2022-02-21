using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    // ---
    
    public bool GameActive;
    public List<PlayerController> Players;

    [Header("Game Settings")]
    public float gameLength;
    public float cycleLength;

    [Header("References")]
    [SerializeField] private Image timerImage;

    // ---

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
        timerImage.fillAmount = Mathf.Clamp(cycleTimer / cycleLength, 0f, 1f);

        if(gameTimer >= gameLength && gameLength > 0) {
            GameActive = false;

        } else if(cycleTimer >= cycleLength) {
            Debug.Log("Cycle ending");
            OnCycleReady?.Invoke();
            //OnCycleReady.RemoveAllListeners();
            cycleTimer = 0f;
        }
    }

}
