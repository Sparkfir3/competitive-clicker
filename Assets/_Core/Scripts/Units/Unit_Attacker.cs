using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Attacker : Unit_Base
{
    protected override void CustomUpdate()
    {
        //Debug.Log(transform.position.ToString());

        mainImage.color = unitColor;
    }
}
