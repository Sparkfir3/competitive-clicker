using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {
    
    [SerializeField] private GameObject indicator;
    public int selectedPosition;
    public List<Transform> spawnPositions;

    public List<Generator> generators;

    [HideInInspector] public GameObject[] readyUnits;

    // ----------------------------------------------------------------------------------------

    private void Awake() {
        readyUnits = new GameObject[spawnPositions.Count];
    }
    private void OnCycleReady() {
        for(int i = 0; i < readyUnits.Length; i++) {
            if(!readyUnits[i])
                continue;

            Generator gen = readyUnits[i].GetComponent<Generator>();
            if(gen) {
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
        Debug.Log(spawnPositions[selectedPosition]);
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

    public void PrepareGenerator() {
        readyUnits[selectedPosition] = generators[selectedPosition].gameObject;
    }

}
