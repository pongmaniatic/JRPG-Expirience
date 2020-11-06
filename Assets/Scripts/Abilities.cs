using UnityEngine;


public class Abilities : MonoBehaviour { }
public interface IAbility { int UseAbility1(); int UseAbility2(); }
public class CapsuleAttack : MonoBehaviour, IAbility
{
    public int UseAbility1()
    {
        Debug.Log("Capusule ability 1");
        BossManager.bossManager.LoseHealth();
        return (1);
    }
    public int UseAbility2()
    {
        Debug.Log("Capusule ability 2");
        BossManager.bossManager.LoseHealth();
        return (2);
    }
}
public class CubeAttack : MonoBehaviour, IAbility
{
    public int UseAbility1()
    {
        Debug.Log("Cube ability 1");
        BossManager.bossManager.LoseHealth();
        return (3);
    }
    public int UseAbility2()
    {
        Debug.Log("Cube ability 2");
        BossManager.bossManager.LoseHealth();
        return (4);
    }
}
public class SphereAttack : MonoBehaviour, IAbility
{
    public int UseAbility1()
    {
        Debug.Log("Sphere ability 1");
        BossManager.bossManager.LoseHealth();
        return (5);
    }
    public int UseAbility2()
    {
        Debug.Log("Sphere ability 2");
        BossManager.bossManager.LoseHealth();
        return (6);
    }
}
