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
    private float moveSpeed = 5f;

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
        currentHealth -= 10;
        OnHealthChanged();
    }
    private void Update()
    {
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
                if (randomNumber == 0) { Cube.LoseHealth(); }
                if (randomNumber == 1) { Capsule.LoseHealth(); }
                if (randomNumber == 2) { Sphere.LoseHealth(); }
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
