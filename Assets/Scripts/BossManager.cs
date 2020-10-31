using UnityEngine;

public class BossManager : MonoBehaviour
{
    private int bossHealth = 100;

    void LoseHealth(int damage)
    {
        bossHealth -= damage;
    }
}
