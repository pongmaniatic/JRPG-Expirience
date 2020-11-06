using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Image bossHealthBar;
    public bool thisIsBossHealth = true;
    private void Start()
    {
        if (thisIsBossHealth == true){ OnEnableDamagedBoss();}
    }
    void UpdateBarBoss()
    {
        float currentHelathPorcentage = (float)BossManager.bossManager.currentHealth / (float)BossManager.bossManager.maxhealth;
        bossHealthBar.fillAmount = currentHelathPorcentage;
    }


    private void OnEnableDamagedBoss() { BossManager.bossManager.OnHealthChanged += UpdateBarBoss; }
    private void OnDisableDamagedBoss() { BossManager.bossManager.OnHealthChanged -= UpdateBarBoss; }

}
