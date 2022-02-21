using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {
    
    [SerializeField] private GameObject indicator;
    public int selectedPosition;
    public List<Transform> spawnPositions;

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

}
