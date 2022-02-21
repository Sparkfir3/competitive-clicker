using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Clicking, Placing }
public enum SelectionType { None, Offense, Defense, Economy }

public class PlayerController : MonoBehaviour {

    #region Variables

    public int playerNumber;
    [SerializeField] private PlayerController enemyPlayer;
    [SerializeField] private InputKeys Inputs;

    // ---

    [Header("State")]
    public int maxHealth;
    public int health;

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

    // ---

    [Header("Player References")]
    [SerializeField] private GUI_Player ui;
    [SerializeField] private SpawnArea spawnArea;

    // ---

    [Header("Resources")]
    private int _currentResources;
    public int CurrentResources {
        get {
            return _currentResources;
        }
        set {
            _currentResources = value;
            ui.UpdateMoneyText(CurrentResources);
        }
    }
    public int resourcesPerClick;

    // ---

    [Header("Units")]
    [SerializeField] private UnitTier units;

    // ---


    // ---

    [System.Serializable]
    private struct InputKeys {
        public KeyCode Click;
        public KeyCode Offense, Defense, Econ;
        public KeyCode Left, Right, Confirm, Cancel;
    }

    #endregion

    // -----------------------------------------------------------------------------------------------------------------

    private void Awake() {
        //GameController.Instance.OnCycleReady.AddListener(OnCycleReady);
        state = PlayerState.Clicking;

        ui.UpdateMoneyText(CurrentResources);
    }

    private void Update() {
        switch(state) {
            #region Clicking
            case PlayerState.Clicking:
                if(Input.GetKeyDown(Inputs.Click)) {
                    GainResource();
                }
                if(Input.GetKeyDown(Inputs.Offense)) {
                    if(CurrentResources >= units.offCost) {
                        state = PlayerState.Placing;
                        selection = SelectionType.Offense;
                    }
                }
                if(Input.GetKeyDown(Inputs.Defense)) {
                    if(CurrentResources >= units.defCost) {
                        state = PlayerState.Placing;
                        selection = SelectionType.Defense;
                    }
                }
                if(Input.GetKeyDown(Inputs.Econ)) {
                    state = PlayerState.Placing;
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
                            state = PlayerState.Clicking;
                            break;
                        case SelectionType.Defense:
                            SpawnDefense();
                            state = PlayerState.Clicking;
                            break;
                        case SelectionType.Economy:
                            if(UpgradeGenerator())
                                state = PlayerState.Clicking;
                            break;
                    }
                }
                if(Input.GetKeyDown(Inputs.Cancel)) {
                    selection = SelectionType.None;
                    state = PlayerState.Clicking;
                }
                break;
            #endregion
        }
    }

    // -----------------------------------------------------------------------------------------------------------------

    

    // -----------------------------------------------------------------------------------------------------------------

    private void GainResource() {
        CurrentResources += resourcesPerClick;
    }

    private void SpawnOffense() {
        CurrentResources -= units.offCost;

        GameObject unit = Instantiate(units.offenseUnit, spawnArea.spawnPositions[spawnArea.selectedPosition].position, Quaternion.identity);
        unit.SetActive(false);
        spawnArea.PrepareUnit(unit);
    }
    
    private void SpawnDefense() {
        CurrentResources -= units.defCost;

        GameObject unit = Instantiate(units.defenseUnit, spawnArea.spawnPositions[spawnArea.selectedPosition].position, Quaternion.identity);
        unit.SetActive(false);
        spawnArea.PrepareUnit(unit);
    }

    private bool UpgradeGenerator() {
        return spawnArea.PrepareGenerator();
    }

}
