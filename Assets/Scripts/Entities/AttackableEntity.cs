using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackableEntity : Entity
{
    public enum AttackableEntityType { Ally, Enemy };

    public float AttackSpeed = 1;
    public int Attack = 1;
    public int Hp = 1;
    public AttackableEntityType Type = AttackableEntityType.Ally;

    private AttackableEntity FocusedEntity;
    private DateTime LastAttackUpdate;
    private float Radius;

    protected new void Start()
    {
        base.Start();
        Radius = GetComponent<CircleCollider2D>().radius;
    }

    protected void Update()
    {
        if (Hp <= 0) return;
        AttackUpdate();
    }

    void AttackUpdate()
    {
        if (!FocusedEntity)
        {
            FocusedEntity = GetNearbyEnemy();
            LastAttackUpdate = DateTime.Now;
            if (FocusedEntity)
            {
                animator.SetBool("Fighting", true);
                WalkStop();
            }
            return;
        }
        TimeSpan diff = DateTime.Now - LastAttackUpdate;
        if (diff.TotalSeconds <= AttackSpeed) return;
        LastAttackUpdate = DateTime.Now;
        bool isAlive = FocusedEntity.ReceiveDamage(Attack);
        if (!isAlive)
        {
            animator.SetBool("Fighting", false);
            FocusedEntity = null;
            WalkReverse();
        }
    }

    AttackableEntity GetNearbyEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == this) continue;
            if (collider.gameObject.CompareTag("AttackableEntity"))
            {
                AttackableEntity Entity = collider.gameObject.GetComponent<AttackableEntity>();
                if (Entity.Hp > 0 && Entity.Type != Type) return Entity;
            }
        }
        return null;
    }
    public bool ReceiveDamage(int damage)
    {
        if (damage > Hp)
        {
            Hp = 0;
            animator.SetBool("Die", true);
            Invoke(nameof(SelfDestruct), 3.0f);
            return false;
        }
        animator.SetBool("Hurted", true);
        Hp -= damage;
        return true;
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
