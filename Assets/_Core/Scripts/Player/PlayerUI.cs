using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI resources;

    // -----------------------------------------------------------------------------------------------------------------

    public void UpdateResources(int count) {
        resources.text = $"Resources: {count}";
    }

}
