using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainGameScript : MonoBehaviour
{
    [SerializeField] Text playerHealthText;
    [SerializeField] Text playerCoinsText;
    [SerializeField] Image PlayerImage;

    [SerializeField] float playerHealthMax;
    [SerializeField] float playerStartCoins;

    [SerializeField] GameObject EnemyGameObject;
    [SerializeField] GameObject LoseImage;

    public UnityEvent attackTheEnemy;

    private float ratioAttack;

    public static float playerHealthPoint;
    public static float playerCoins;

    [SerializeField] Button attackWaterButton;
    [SerializeField] Button attackFireButton;
    [SerializeField] Button attackEarthButton;
    [SerializeField] Button attackAirButton;
    private float maxTimeReload = 2;
    private bool alive;


    void Start()
    {
        playerHealthPoint = playerHealthMax;
        playerCoins = playerStartCoins;
        alive = true;

        UpdateHealthPoint();
        playerCoinsText.text = playerStartCoins.ToString();
    }

    private void Update()
    {
        if (playerHealthPoint <= 0)
        {
            alive = false;
        }

        if (!alive)
        {
            PlayerImage.color = Color.red;
            playerHealthPoint = 0;
            playerHealthText.text = playerHealthPoint.ToString();
            Time.timeScale = 0;
            LoseImage.SetActive(true);
        }
    }

    public void AttackWater()
    {
        if(EnemiesScript.EnemyElementType == 1)
        {
            ratioAttack = 1.5f;
        }
        else { ratioAttack = 1; }

        EnemiesScript.EnemyHealthPoint -= Mathf.Ceil(5* ratioAttack);
        attackTheEnemy.Invoke();
        BlockAttackButton(attackWaterButton);
    }

    public void AttackFire()
    {
        if (EnemiesScript.EnemyElementType == 2)
        {
            ratioAttack = 1.5f;
        }
        else { ratioAttack = 1; }

        EnemiesScript.EnemyHealthPoint -= Mathf.Ceil(5 * ratioAttack);
        attackTheEnemy.Invoke();
        BlockAttackButton(attackFireButton);
    }

    public void AttackEarth()
    {
        if (EnemiesScript.EnemyElementType == 3)
        {
            ratioAttack = 1.5f;
        }
        else { ratioAttack = 1; }

        EnemiesScript.EnemyHealthPoint -= Mathf.Ceil(5 * ratioAttack);
        attackTheEnemy.Invoke();
        BlockAttackButton(attackEarthButton);
    }

    public void AttackAir()
    {
        if (EnemiesScript.EnemyElementType == 0)
        {
            ratioAttack = 1.5f;
        }
        else { ratioAttack = 1; }

        EnemiesScript.EnemyHealthPoint -= Mathf.Ceil(5 * ratioAttack);
        attackTheEnemy.Invoke();
        BlockAttackButton(attackAirButton);
    }

    private void BlockAttackButton(Button currentAttack)
    {
        attackWaterButton.interactable = false;
        attackFireButton.interactable = false;
        attackEarthButton.interactable = false;
        attackAirButton.interactable = false;
        Coroutine coroutine = StartCoroutine(ReloadAttack(currentAttack));
    }

    IEnumerator ReloadAttack(Button currentAttack)
    {
        yield return new WaitForSeconds(maxTimeReload);
        ActiveAttackButton(currentAttack);
        StopCoroutine(ReloadAttack(currentAttack));
    }
    private void ActiveAttackButton(Button currentAttack)
    {
        attackWaterButton.interactable = true;
        attackFireButton.interactable = true;
        attackEarthButton.interactable = true;
        attackAirButton.interactable = true;
    }



    public void UpdateHealthPoint()
    {
        playerHealthText.text = playerHealthPoint.ToString();
    }

    public void TakingDamage()
    {
        Coroutine coroutine = StartCoroutine(DamageColor());
    }
    IEnumerator DamageColor()
    {
        PlayerImage.color = Color.red;
        yield return new WaitForSeconds(0.7f);
        PlayerImage.color = new Color(255, 255, 255, 255);
        StopCoroutine(DamageColor());
    }
}
