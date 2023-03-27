using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject objectShooter;
    public bool Built;
    public ScriptableBuilding Building;
    public GameObject mirroredObject;
    public GameObject hpBar;

    private GameObject hpBarAlly;
    private GameObject hpBarEnemy;
    public float yOffset;
    private float StartyOffset = 4f;

    private CursorManager _cursorManager;
    private AttackableEntity AttackEnt;
    private AttackableEntity Mirror;
    private BoxCollider2D MirrorCollider;

    // Start is called before the first frame update
    void Start()
    {
        _cursorManager = FindObjectOfType<CursorManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsHovered(Vector2 point)
    {
        var contact = GetComponent<BoxCollider2D>().bounds.Contains(point);
        var overlapPoint = Physics2D.OverlapPoint(point);
        if (contact)// && overlapPoint.gameObject == gameObject)
        {
            return true;
        }
        return false;
    }

    public void Build()
    {
        if (_cursorManager.selected is not { type: SelectableType.Building })
            return;
        Building = _cursorManager.selected as ScriptableBuilding;

        GetComponent<SpriteRenderer>().sprite = Building.Sprite;
        transform.position = new Vector3(transform.position.x, transform.position.y + StartyOffset, transform.position.z);
        GetComponent<BoxCollider2D>().offset += new Vector2(0, -StartyOffset);
        mirroredObject = new GameObject(this.GetType().Name + " mirrored");
        mirroredObject.transform.position = new Vector3(transform.position.x, transform.position.y - yOffset, -0.3f);
        SpriteRenderer mirroredSpriteRenderer = mirroredObject.AddComponent<SpriteRenderer>();
        mirroredSpriteRenderer.sprite = Building.Sprite;
        mirroredSpriteRenderer.flipY = true;
        mirroredSpriteRenderer.sortingLayerName = "Buildings";
        Built = true;

        hpBarAlly = Instantiate(hpBar, gameObject.transform.position, Quaternion.identity);
        hpBarEnemy = Instantiate(hpBar, mirroredObject.transform.position, Quaternion.identity);
        AttackEnt = gameObject.AddComponent<AttackableEntity>() as AttackableEntity;
        AttackEnt.Hp = Building.Hp;
        AttackEnt.Attack = Building.AttackPower;
        AttackEnt.AttackSpeed = 0;
        AttackEnt.Type = AttackableEntity.AttackableEntityType.Ally;
        AttackEnt.animations = false;
        hpBarAlly.GetComponent<HealthBar>().maxHealth = Building.Hp;
        hpBarAlly.GetComponent<HealthBar>().currentHealth = Building.Hp;
        hpBarAlly.GetComponent<HealthBar>().Size = new Vector2(5, 0.3f);
        AttackEnt.hpBar = hpBarAlly.GetComponent<HealthBar>();
        Mirror = mirroredObject.AddComponent<AttackableEntity>() as AttackableEntity;
        mirroredObject.tag = "AttackableEntity";
        Mirror.Hp = Building.Hp;
        Mirror.Attack = Building.AttackPower;
        Mirror.AttackSpeed = 0;
        Mirror.Type = AttackableEntity.AttackableEntityType.Enemy;
        Mirror.animations = false;
        hpBarEnemy.GetComponent<HealthBar>().maxHealth = Building.Hp;
        hpBarEnemy.GetComponent<HealthBar>().currentHealth = Building.Hp;
        hpBarEnemy.GetComponent<HealthBar>().Size = new Vector2(5, 0.3f);
        Mirror.hpBar = hpBarEnemy.GetComponent<HealthBar>();
        MirrorCollider = mirroredObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        MirrorCollider.size = new Vector2(3, MirrorCollider.size.y);
        if (Building.BuildingType == BuildingType.Tower)
        {
            var objectShooter = Instantiate(this.objectShooter, transform.position, Quaternion.identity, transform);
            objectShooter.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, Building.ShooterOffset, true);
            var mirroredShooter = Instantiate(this.objectShooter, mirroredObject.transform.position, Quaternion.identity, mirroredObject.transform);
            mirroredShooter.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, - Building.ShooterOffset, false);
            AttackEnt.AttackSpeed = Building.AttackSpeed;
            Mirror.AttackSpeed = Building.AttackSpeed;
        }
    }

    void Upgrade()
    {
        if (Building.nextBuilding == null) return;
        Building = Building.nextBuilding;

        GetComponent<SpriteRenderer>().sprite = Building.Sprite;
        mirroredObject.GetComponent<SpriteRenderer>().sprite = Building.Sprite;
    }
}
