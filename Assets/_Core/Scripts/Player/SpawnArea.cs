using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {
    
    [SerializeField] private GameObject indicator;
    public int selectedPosition;
    public List<Transform> spawnPositions;

    public List<Unit_Generator> generators;

    // ---

    private PlayerController player;
    [HideInInspector] public GameObject[] readyUnits;

    // ----------------------------------------------------------------------------------------

    private void Awake() {
        GameController.Instance.OnCycleReady.AddListener(OnCycleReady);
        player = GetComponentInParent<PlayerController>();
        readyUnits = new GameObject[spawnPositions.Count];

        foreach(Unit_Generator generator in generators) {
            generator.player = player.playerNumber;
        }
    }

    private void OnCycleReady() {
        for(int i = 0; i < readyUnits.Length; i++) {
            if(!readyUnits[i])
                continue;

            Unit_Generator gen = readyUnits[i].GetComponent<Unit_Generator>();
            if(gen) {
                Debug.Log($"Level up {gen.name}");
                gen.LevelUp();
            } else {
                readyUnits[i].SetActive(true);
            }
            readyUnits[i] = null;
        }
    }

    // ----------------------------------------------------------------------------------------

    public void SetIndicator(bool value) {
        indicator.SetActive(value);
        selectedPosition = Mathf.RoundToInt(spawnPositions.Count / 2);
        PositionIndicator();
    }

    private void PositionIndicator() {
        indicator.transform.position = spawnPositions[selectedPosition].position;
    }

    public void MoveLeft() {
        selectedPosition--;
        if(selectedPosition < 0)
            selectedPosition = spawnPositions.Count - 1;
        PositionIndicator();
    }

    public void MoveRight() {
        selectedPosition++;
        if(selectedPosition >= spawnPositions.Count)
            selectedPosition = 0;
        PositionIndicator();
    }

    // ----------------------------------------------------------------------------------------

    public void PrepareUnit(GameObject unit) {
        if(readyUnits[selectedPosition] != null)
            Destroy(readyUnits[selectedPosition]);
        readyUnits[selectedPosition] = unit;
    }

    public bool PrepareGenerator() {
        int cost = generators[selectedPosition].GetCost();
        if(player.CurrentResources >= cost) {
            readyUnits[selectedPosition] = generators[selectedPosition].gameObject;
            player.CurrentResources -= cost;
            return true;
        }
        return false;
    }

}
