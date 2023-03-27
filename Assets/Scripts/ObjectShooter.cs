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

    private List<GameObject> _objects = new List<GameObject>();
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
            var isAlive = _focused.ReceiveDamage((int) attackPower);
            if (!isAlive)
            {
                _focused = _objects.Count > 0 ? _objects[0].GetComponent<AttackableEntity>() : null;
                if (_objects.Count > 0)
                    _objects.RemoveAt(0);
            }
        }
    }

    private bool should_shoot(AttackableEntity.AttackableEntityType type)
    {
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
        if(!ent)
            return;
        if (!should_shoot(ent.Type))
            return;
        if (!_focused)
        {
            _focused = ent;
        }
        else if (!_objects.Contains(other.gameObject))
        {
            _objects.Add(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
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
            _focused = _objects.Count > 0 ? _objects[0].GetComponent<AttackableEntity>() : null;
            if (_objects.Count > 0)
                _objects.RemoveAt(0);
        }
    }
}
