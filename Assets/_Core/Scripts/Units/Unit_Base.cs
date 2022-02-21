using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Base : MonoBehaviour
{
    public int player;
    public Color unitColor;
    public Image mainImage;

    public Color healthBarBGColor;
    public Color healthBarHurtColor;

    public GameObject HPDisplay;
    public float maxHP = 1000;
    public float currentHP = 1000;

    public Image backgroundHPDisplay;
    public Image currentHPDisplay;

    public float moveSpeed = 1;
    public float attackSpeed = 1; // using Time.time
    public float attackRange = 1;
    public float damage = 100;

    private float attackTimer = 0;

    public int friendlyTerritory = 1; // this will be -1 in enemy territory

    public Unit_Targeting targeting;
    public GameObject target = null;

    public bool attackingHealth = false;

    protected void Update()
    {
        DisplayHP();
        CustomUpdate();
        Targeting();
        AI();
        CheckAlive();
    }

    protected virtual void CheckAlive()
    {
        if (currentHP <= 0) Destroy(this.gameObject);
    }

    public virtual void GetHurt(float dmg)
    {
        currentHP -= dmg;
        StopAllCoroutines();
        backgroundHPDisplay.color = healthBarHurtColor;
        StartCoroutine(HurtCoroutine());
    }

    protected virtual void ResetGettingHurt()
    {
        StopAllCoroutines();
        backgroundHPDisplay.color = healthBarBGColor;
    }

    protected virtual IEnumerator HurtCoroutine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 0.5f)
        {
            backgroundHPDisplay.color = Color.Lerp(backgroundHPDisplay.color, healthBarBGColor, Time.deltaTime * 8);
            yield return null;
        }

        ResetGettingHurt();
        yield return null;
    }

    protected virtual void CustomUpdate()
    {

    }

    public void Setup()
    {
        mainImage.color = unitColor;
    }

    public void DisplayHP()
    {
        if (currentHP < maxHP)
        {
            HPDisplay.SetActive(true);
            currentHPDisplay.fillAmount = currentHP / maxHP;
        }
        else
        {
            HPDisplay.SetActive(false);
        }
    }

    protected virtual void Targeting()
    {
        if (targeting == null) return;

        if (targeting.baseUnit == null)
        {
            targeting.baseUnit = this;
        }
    }

    protected virtual void AI()
    {
        if (targeting != null)
        {
            if (target != null) // if there is a target
            {
                if (attackTimer >= attackSpeed)
                    attackTimer = 0;

                if (attackTimer == 0)
                {
                    // attack
                    target.GetComponent<Unit_Base>().GetHurt(damage);
                }

                // cooldown
                attackTimer += Time.deltaTime;

                return;
            }
        }

        if (moveSpeed > 0)
        {
            // move
            if (transform.position.y > Screen.height / 5.5)
            {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime * friendlyTerritory, 0);
            }
            else
            {
                attackingHealth = true;
            }
        }

        if (transform.position.y > Screen.height - (Screen.height/10))
        {
            SwitchScreens();
        }

        attackTimer += Time.deltaTime;
    }

    protected void SwitchScreens()
    {
        //float xOffset = 

        transform.position = new Vector3(
            transform.position.x - (Screen.width / 2), Screen.height - (Screen.height / 9.8f), 0);

        friendlyTerritory *= -1;

        transform.rotation = Quaternion.Euler(0,0,180);
    }
}
