using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllyScript : MonoBehaviour
{
    public enum AttackableEntityType { Ally, Enemy };

    public enum WalkDirection { Stoped, Right, Left };

    public GameObject Prefab;
    public float Radius = 1;
    public AttackableEntityType Type;

    public int Attack = 0;
    public float AttackSpeed = 1f;
    public int Hp = 1;

    public bool OnUpsideDown = true;

    public WalkDirection Direction = WalkDirection.Stoped;

    private GameObject ResultPrefab;
    private AllyScript FocusedEnemy;
    private DateTime LastAttackTimestamp;

    private bool Left = true;
    private bool Up = true;

    // Start is called before the first frame update
    void Start()
    {
        ReplaceSprite();
    }

    void ReplaceSprite()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        ResultPrefab = Instantiate(Prefab, transform.position + Vector3.down, transform.rotation);
        ResultPrefab.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Render();
        if (Hp <= 0) return;
        bool attacking = AttackInCombat();
        if (!attacking) Walk();
    }

    bool AttackInCombat()
    {
        if (!FocusedEnemy)
        {
            FocusedEnemy = SearchForEnemies();
            LastAttackTimestamp = DateTime.Now;
            return true;
        }
        if (!FocusedEnemy) return false;
        TimeSpan diff = DateTime.Now - LastAttackTimestamp;
        if (diff.TotalSeconds <= AttackSpeed) return true;
        LastAttackTimestamp = DateTime.Now;
        bool isAlive = FocusedEnemy.ReceiveDamage(Attack);
        if (!isAlive) FocusedEnemy = null;
        return isAlive;
    }

    void Walk()
    {
        if (Direction == WalkDirection.Right)
        {

        }

        if (Direction == WalkDirection.Left)
        {

        }
    }

    bool Render()
    {
        if (Hp <= 0)
        {
            if (ResultPrefab)
                Destroy(ResultPrefab);
            ResultPrefab = null;
            return false;
        }

        if (OnUpsideDown && Up)
        {
            Up = false;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }

        if (!OnUpsideDown && !Up)
        {
            Up = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }

        if (Direction == WalkDirection.Right && Left)
        {
            Left = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        if (Direction == WalkDirection.Left && !Left)
        {
            Left = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        return true;
    }

    public bool ReceiveDamage(int damage)
    {
        if (damage > Hp)
        {
            Hp = 0;
            return false;
        }
        Hp -= damage;
        return true;
    }

    AllyScript SearchForEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to a relevant object (for example, check its tag).
            if (collider.gameObject.CompareTag("AttackableEntity"))
            {
                AllyScript AttackableEntity = gameObject.GetComponent<AllyScript>();
                if (AttackableEntity.Type != Type) return AttackableEntity;
            }
        }
        return null;
    }
}
