using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Defender : Unit_Base
{
    protected override void CustomUpdate()
    {
        mainImage.color = unitColor;
    }
}
