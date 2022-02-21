using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="Scrobs/UnitTier", order = 1)]
public class UnitTier : ScriptableObject {
    
    [Header("Offense")]
    public GameObject offenseUnit;
    public int offCost;

    [Header("Defense")]
    public GameObject defenseUnit;
    public int defCost;

}
