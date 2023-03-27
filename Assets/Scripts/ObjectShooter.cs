using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    public bool isOurs;
    public float attackPower = 1;
    public float shootSpeed = 1;
    public float shootRange = 1;
    public float shooterOffset = 0;
    public BoxCollider2D col;

    private float _shootTimer = 0;

    private AttackableEntity _focused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= shootSpeed && _focused)
        {
            _shootTimer = 0;
            Debug.Log("Shot");
            var isAlive = _focused.ReceiveDamage((int) attackPower);
            if (!isAlive)
            {
                _focused = null;
            }
        }
    }

    private bool should_shoot(AttackableEntity.AttackableEntityType type)
    {
        Debug.Log("Should shoot " + type + "?");
        Debug.Log("Is " + (isOurs ? "ours" : "not ours"));
        if (isOurs && type == AttackableEntity.AttackableEntityType.Ally)
            return false;
        if (!isOurs && type == AttackableEntity.AttackableEntityType.Enemy)
            return false;
        return true;
    }

    public void SetUp(float attackPower, float shootSpeed, float shootRange, float shooterOffset, bool isOurs)
    {
        this.attackPower = attackPower;
        this.shootSpeed = shootSpeed;
        this.shootRange = shootRange;
        this.shooterOffset = shooterOffset;
        this.isOurs = isOurs;

        col.size = new Vector2(shootRange, 5);
        col.offset = new Vector2(0, -shooterOffset);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var ent = other.GetComponent<AttackableEntity>();
        if (!_focused && ent && should_shoot(ent.Type))
        {
            _focused = ent;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Detects " + other.gameObject.name);
        var ent = other.GetComponent<AttackableEntity>();
        if (!_focused && ent && should_shoot(ent.Type))
        {
            _focused = ent;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_focused == other.GetComponent<AttackableEntity>())
        {
            _focused = null;
        }
    }
}
