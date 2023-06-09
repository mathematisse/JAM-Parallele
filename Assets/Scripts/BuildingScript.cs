using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public AudioSource upgraded;
    public AudioSource denied;
    public GameObject objectShooter;
    public bool Built;
    public ScriptableBuilding Building;
    public GameObject mirroredObject;
    public GameObject hpBar;

    private GameObject hpBarAlly;
    private GameObject hpBarEnemy;
    public float yOffset;
    private float StartyOffset = 4f;

    private GameObject objectShooterObject;
    private GameObject mirrorObjectShooterObject;
    private CursorManager _cursorManager;
    private RessourceManager _ressourceManager;
    private AttackableEntity AttackEnt;
    private AttackableEntity Mirror;
    private BoxCollider2D MirrorCollider;
    private SpriteRenderer mirroredSpriteRenderer;
    private Sprite initSprite;

    private bool _tooltipShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        _cursorManager = FindObjectOfType<CursorManager>();
        _ressourceManager = FindObjectOfType<RessourceManager>();
        initSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Built || !_cursorManager || !Building.nextBuilding)
        {
            if (_tooltipShowing)
            {
                _cursorManager.HideToolTip();
                _tooltipShowing = false;
            }
            return;
        }

        if (_cursorManager.isUpgrading && IsHovered(_cursorManager.mousePosition))
        {
            if (!_tooltipShowing)
            {
                var next = Building.nextBuilding;
                _cursorManager.SetToolTipColor(_ressourceManager.CanAfford(next));
                _cursorManager.ShowToolTip(next.WoodCost + " Wood, " + next.StoneCost + " Stone, " + next.MushroomCost + " Flower, " + next.SoulCost + " Soul");
                _tooltipShowing = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (_ressourceManager.CanAfford(Building.nextBuilding))
                {
                    upgraded.Play();
                    _ressourceManager.Spend(Building.nextBuilding);
                    Upgrade();
                }
                else
                {
                    denied.Play();
                }
            }
        }
        else if (_tooltipShowing)
        {
            _cursorManager.HideToolTip();
            _tooltipShowing = false;
        }
    }

    public bool IsHovered(Vector2 point)
    {
        var contact = GetComponent<BoxCollider2D>().bounds.Contains(point);
        if (contact)
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
        mirroredSpriteRenderer = mirroredObject.AddComponent<SpriteRenderer>();
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
        Mirror.GetComponent<SpriteRenderer>().color = new Color(1, 0.75f, 0.75f, 1);
        hpBarEnemy.GetComponent<HealthBar>().maxHealth = Building.Hp;
        hpBarEnemy.GetComponent<HealthBar>().currentHealth = Building.Hp;
        hpBarEnemy.GetComponent<HealthBar>().Size = new Vector2(5, 0.3f);
        Mirror.hpBar = hpBarEnemy.GetComponent<HealthBar>();
        MirrorCollider = mirroredObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        MirrorCollider.size = new Vector2(Building.colliderSizeX, MirrorCollider.size.y);
        if (Building.BuildingType == BuildingType.Tower)
        {
            var objectShooterObject = Instantiate(this.objectShooter, transform.position, Quaternion.identity, transform);
            objectShooterObject.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, Building.ShooterOffset, true);
            var mirrorObjectShooterObject = Instantiate(this.objectShooter, mirroredObject.transform.position, Quaternion.identity, mirroredObject.transform);
            mirrorObjectShooterObject.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, - Building.ShooterOffset, false);
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
        AttackEnt.Hp = Building.Hp;
        AttackEnt.Attack = Building.AttackPower;
        AttackEnt.AttackSpeed = 0;
        Mirror.Hp = Building.Hp;
        Mirror.Attack = Building.AttackPower;
        Mirror.AttackSpeed = 0;
        hpBarAlly.GetComponent<HealthBar>().maxHealth = Building.Hp;
        hpBarAlly.GetComponent<HealthBar>().currentHealth = Building.Hp;
        hpBarEnemy.GetComponent<HealthBar>().maxHealth = Building.Hp;
        hpBarEnemy.GetComponent<HealthBar>().currentHealth = Building.Hp;
        if (Building.BuildingType == BuildingType.Tower)
        {
            var objectShooterObject = GetComponentInChildren<ObjectShooter>();
            objectShooterObject.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, Building.ShooterOffset, true);
            var mirrorObjectShooterObject = mirroredObject.transform.GetChild(0).gameObject;
            mirrorObjectShooterObject.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, - Building.ShooterOffset, false);
            AttackEnt.AttackSpeed = Building.AttackSpeed;
            Mirror.AttackSpeed = Building.AttackSpeed;
        }
    }
}
