using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CastleDefense : MonoBehaviour
{
    public enum AttackableEntityType { Ally, Enemy };

    public int Hp = 1;
    public AttackableEntityType Type = AttackableEntityType.Ally;

    protected void Update()
    {
        if (Hp <= 0) return;
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);


        SceneManager.LoadScene(0);
    }

    public bool ReceiveDamage(int damage)
    {
        if (damage > Hp)
        {
            Hp = 0;
            SelfDestruct();
            return false;
        }
        Hp -= damage;
        return true;
    }
}
