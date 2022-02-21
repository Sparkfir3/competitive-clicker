using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Clicking, Placing }
public enum SelectionType { None, Offense, Defense, Economy }

public class PlayerController : MonoBehaviour {

    [SerializeField] private GameController game;
    [SerializeField] private PlayerController enemyPlayer;
    [SerializeField] private InputKeys Inputs;

    [Header("State")]
    private PlayerState _state;
    private PlayerState state {
        get {
             return _state;
        }
        set {
            _state = value;
            switch(_state) {
                case PlayerState.Clicking:
                    spawnArea.SetIndicator(false);
                    break;
                case PlayerState.Placing:
                    spawnArea.SetIndicator(true);
                    break;
            }
        }
    }

    private SelectionType _selection;
    private SelectionType selection {
        get {
            return _selection;
        }
        set {
            _selection = value;
        }
    }

    [Header("Player References")]
    [SerializeField] private PlayerUI ui;
    [SerializeField] private SpawnArea spawnArea;

    [Header("Resources")]
    [SerializeField] private int currentResources;
    public int resourcesPerClick;

    [Header("Units")]
    [SerializeField] private UnitTier units;

    // ---

    private List<GameObject> readyUnits = new List<GameObject>();
    private bool offenseReady, defenseReady;

    // ---

    [System.Serializable]
    private struct InputKeys {
        public KeyCode Click;
        public KeyCode Offense, Defense, Econ;
        public KeyCode Left, Right, Confirm, Cancel;
    }

    // -----------------------------------------------------------------------------------------------------------------

    private void Awake() {
        game.OnCycleReady.AddListener(OnCycleReady);
        state = PlayerState.Clicking;
    }

    private void Update() {
        switch(state) {
            #region Clicking
            case PlayerState.Clicking:
                if(Input.GetKeyDown(Inputs.Click)) {
                    GainResource();
                }
                if(Input.GetKeyDown(Inputs.Offense)) {
                    if(currentResources >= units.offCost) {
                        state = PlayerState.Placing;
                        selection = SelectionType.Offense;
                        currentResources -= units.offCost;
                    }
                }
                if(Input.GetKeyDown(Inputs.Defense)) {
                    if(currentResources >= units.defCost) {
                        state = PlayerState.Placing;
                        selection = SelectionType.Defense;
                        currentResources -= units.defCost;
                    }
                }
                if(Input.GetKeyDown(Inputs.Econ)) {
                    selection = SelectionType.Economy;
                }
                break;
            #endregion

            #region Placing
            case PlayerState.Placing:
                if(Input.GetKeyDown(Inputs.Left)) {
                    spawnArea.MoveLeft();
                }
                if(Input.GetKeyDown(Inputs.Right)) {
                    spawnArea.MoveRight();
                }
                if(Input.GetKeyDown(Inputs.Confirm)) {
                    switch(selection) {
                        case SelectionType.Offense:
                            SpawnOffense();
                            offenseReady = true;
                            break;
                        case SelectionType.Defense:
                            SpawnDefense();
                            defenseReady = true;
                            break;
                    }
                    state = PlayerState.Clicking;
                }
                if(Input.GetKeyDown(Inputs.Cancel)) {
                    switch(selection) {
                        case SelectionType.Offense:
                            currentResources += units.offCost;
                            break;
                        case SelectionType.Defense:
                            currentResources += units.defCost;
                            break;
                    }

                    selection = SelectionType.None;
                    state = PlayerState.Clicking;
                }
                break;
            #endregion
        }
    }

    // -----------------------------------------------------------------------------------------------------------------

    private void OnCycleReady() {
        foreach(GameObject unit in readyUnits) {
            unit.SetActive(true);
        }

        /*if(offenseReady) {
            SpawnOffense();
        }
        if(defenseReady) {
            SpawnDefense();
        }*/
    }

    // -----------------------------------------------------------------------------------------------------------------

    

    // -----------------------------------------------------------------------------------------------------------------

    private void GainResource() {
        currentResources += resourcesPerClick;
        //ui.UpdateResources(currentResources);
    }

    private void SpawnOffense() {
        GameObject unit = Instantiate(units.offenseUnit, spawnArea.spawnPositions[spawnArea.selectedPosition].position, Quaternion.identity);
        unit.SetActive(false);
        readyUnits.Add(unit);
        Debug.Log($"{name} Offense");
    }
    
    private void SpawnDefense() {
        GameObject unit = Instantiate(units.defenseUnit, spawnArea.spawnPositions[spawnArea.selectedPosition].position, Quaternion.identity);
        unit.SetActive(false);
        readyUnits.Add(unit);
        Debug.Log($"{name} Defense");
    }

}
