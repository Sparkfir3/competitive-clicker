using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Targeting : MonoBehaviour
{
    public Unit_Base baseUnit;

    public List<Unit_Base> unitsTargeted;

    private void Update()
    {
        if (baseUnit != null)
        {
            ReportTarget();
        }
    }

    private void ReportTarget()
    {
        baseUnit.target = Target();
    }

    private GameObject Target()
    {
        if (unitsTargeted == null || unitsTargeted.Count < 1) return null;

        foreach (Unit_Base unit in unitsTargeted)
        {
            if (unit is Unit_Defender)
            {
                //Debug.Log("Found a Defender.");
                return unit.gameObject;
            }
        }

        foreach (Unit_Base unit in unitsTargeted)
        {
            if (unit is Unit_Attacker)
            {
                //Debug.Log("Found an Attacker.");
                return unit.gameObject;
            }
        }

        foreach (Unit_Base unit in unitsTargeted)
        {
            if (unit is Unit_Generator)
            {
                //Debug.Log("Found a Generator.");
                return unit.gameObject;
            }
        }

        return null;
    }

    private GameObject FindClosest()
    {
        return null;
    }

    private bool CheckTargetingArray(Unit_Base unit)
    {
        for (int i = 0; i < unitsTargeted.Count; i++)
        {
            if (unitsTargeted[i] == unit)
            {
                return true;
            }
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            if (collision.gameObject == baseUnit.gameObject) return;

            if (!CheckTargetingArray(collision.gameObject.GetComponent<Unit_Base>()))
            {
                unitsTargeted.Add(collision.gameObject.GetComponent<Unit_Base>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckTargetingArray(collision.gameObject.GetComponent<Unit_Base>()))
        {
            unitsTargeted.Remove(collision.gameObject.GetComponent<Unit_Base>());
        }
    }

}
