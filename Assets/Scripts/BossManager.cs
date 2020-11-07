using UnityEngine;
using System.Collections;
using System;

public class BossManager : MonoBehaviour
{
    public static BossManager bossManager;
    public int maxhealth = 100;
    public int currentHealth;
    public event Action OnHealthChanged = delegate { };
    public event Action OnDamageDealt = delegate { };
    public bool bossTurn = false;
    public Transform attackingPosition;
    public Transform startingPosition;
    private float moveSpeed = 2.5f;
    public GameObject endMenu;

    public Character Cube;
    public Character Capsule;
    public Character Sphere;

    private void Start()
    {
        bossManager = this;
        currentHealth = maxhealth;
    }
    public void LoseHealth()
    {
        FactoryText.factoryText.ActivateText(10,0);
        currentHealth -= 10;
        OnHealthChanged();
    }
    private void Update()
    {
        if (currentHealth <= 0) { endMenu.SetActive(true); }
        if (bossTurn == true)
        {

            if (transform.position != attackingPosition.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, attackingPosition.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                EndBossTurn();
                int randomNumber = UnityEngine.Random.Range(0, 2);
                if (randomNumber == 0) { Cube.LoseHealth(); FactoryText.factoryText.ActivateText(15, 1); }
                if (randomNumber == 1) { Capsule.LoseHealth(); FactoryText.factoryText.ActivateText(15, 2); }
                if (randomNumber == 2) { Sphere.LoseHealth(); FactoryText.factoryText.ActivateText(15, 3); }
                bossTurn = false;
            }
        }
        else
        {
            if (transform.position != startingPosition.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, Time.deltaTime * moveSpeed);
            }
        }
    }
    void EndBossTurn()
    {
        GameManager.gameManager.NewTurn();
    }

}
