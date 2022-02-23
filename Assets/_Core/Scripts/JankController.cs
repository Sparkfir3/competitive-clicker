using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankController : MonoBehaviour
{
    public static JankController instance;

    [SerializeField] private GameObject attacker;
    [SerializeField] private GameObject defender;

    public bool setup = false;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        //Setup();
        JankSpawn();
    }

    private void Setup()
    {
        if (!setup)
        {
            for (int i = 0; i < GUI_Control.instance.playerGUIs.Length; i++)
            {
                for (int p = 0; p < GUI_Control.instance.playerGUIs[i].GetComponent<GUI_Player>().genHandler.generators.Length; p++)
                {
                    if (p != 2)
                    {
                        GUI_Control.instance.playerGUIs[i].GetComponent<GUI_Player>().genHandler.generators[p].isActive = false;
                    }
                }
            }

            setup = true;
        }
    }

    private void JankSpawn()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Spawn(attacker);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Spawn(defender);
        }
    }

    private void Spawn(GameObject unit)
    {
        Debug.Log("Trying to spawn unit.");

        GameObject spawnedUnit = Object.Instantiate(unit, transform, true);
        spawnedUnit.transform.position = Input.mousePosition;

        Unit_Base unitScript = spawnedUnit.GetComponent<Unit_Base>();

        if (Input.mousePosition.x > Screen.width/ 2)
        {
            unitScript.player = 1;
        }
        else
        {
            unitScript.player = 0;
        }
    }
}
