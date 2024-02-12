using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemiesScript : MonoBehaviour
{
    [SerializeField] float maxHealthEnemy;
    [SerializeField] float minHealthEnemy;

    public static float EnemyHealthPoint;

    [SerializeField] Sprite lihoSprite;
    [SerializeField] Text EnemyHealth;

    [SerializeField] Image EnemyImage;
    [SerializeField] Sprite[] ElementsSprites;
    [SerializeField] Image EnemyElement;
    [SerializeField] float EnemyMinDamage;
    [SerializeField] float EnemyMaxDamage;

    public static int EnemyElementType;

    private bool alive;

    public UnityEvent attackPlayer;
    private bool attackReady;

    [SerializeField] GameObject WinImage;

    void Start()
    {
        EnemyHealthPoint = Mathf.Ceil(Random.Range(minHealthEnemy, maxHealthEnemy));
        alive = true;
       EnemyHealth.text = EnemyHealthPoint.ToString();
        EnemyImage.sprite = lihoSprite;
        EnemyElement.sprite = ElementsSprites[2];
        EnemyElementType = 2;
        attackReady = true;
        
    }

    private void Update()
    {
        if (EnemyHealthPoint <= 0)
        {
            alive = false;
        }

        if (alive)
        {
            if (attackReady)
            {
                attackReady = false;
                Coroutine coroutine = StartCoroutine(EnemyAttack());
            }
        }


        if (!alive)
        {
            StopAllCoroutines();
            EnemyImage.color = Color.red;
            EnemyHealthPoint = 0;
            EnemyHealth.text = EnemyHealthPoint.ToString();
            Time.timeScale = 0;
            WinImage.SetActive(true);
        }
    }

    public void takingDamage()
    {
        EnemyHealth.text = EnemyHealthPoint.ToString();
        Coroutine coroutine = StartCoroutine(DamageColor());
        
    }

    IEnumerator DamageColor()
    {
        EnemyImage.color = Color.red;
        yield return new WaitForSeconds(0.7f);
        EnemyImage.color = new Color(255,255,255,255);
        StopCoroutine(DamageColor());
    }

    IEnumerator EnemyAttack()
    {
        float timeAttack = Random.Range(3, 7);
        yield return new WaitForSeconds(timeAttack);
        float EnemyAttackDamage = Mathf.Ceil(Random.Range(EnemyMinDamage, EnemyMaxDamage));
        MainGameScript.playerHealthPoint -= EnemyAttackDamage;
        attackPlayer.Invoke();
        attackReady = true;
    }




}
