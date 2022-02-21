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
    [SerializeField] private Image timerBG;

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

    private void Update() {
        if(!GameActive)
            return;

        gameTimer += Time.deltaTime;
        cycleTimer += Time.deltaTime;
        timerImage.fillAmount = Mathf.Clamp(cycleTimer / cycleLength, 0f, 1f);

        timerBG.color = Color.Lerp(timerBG.color, Color.white, Time.deltaTime*3);

        if(gameTimer >= gameLength && gameLength > 0) {
            GameActive = false;

        } else if(cycleTimer >= cycleLength)
        {
            /*Color tempColor = timerImage.color;
            timerImage.color = timerBG.color;
            timerBG.color = tempColor;*/

            timerBG.color = timerImage.color;

            Debug.Log("Cycle ending");
            OnCycleReady?.Invoke();
            //OnCycleReady.RemoveAllListeners();
            cycleTimer = 0f;

        }
    }

}
