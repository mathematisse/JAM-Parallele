using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
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
            _focused.ReceiveDamage((int) attackPower);
        }
    }

    public void SetUp(float attackPower, float shootSpeed, float shootRange, float shooterOffset)
    {
        this.attackPower = attackPower;
        this.shootSpeed = shootSpeed;
        this.shootRange = shootRange;
        this.shooterOffset = shooterOffset;

        col.size = new Vector2(shootRange, 5);
        col.offset = new Vector2(0, -shooterOffset);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_focused)
        {
            _focused = other.GetComponent<AttackableEntity>();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!_focused)
        {
            _focused = other.GetComponent<AttackableEntity>();
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
