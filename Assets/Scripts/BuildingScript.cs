using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject objectShooter;
    public bool Built;
    public ScriptableBuilding Building;
    public GameObject mirroredObject;

    public float yOffset;
    private float StartyOffset = 4f;

    private CursorManager _cursorManager;

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
        var overlapPoint = Physics2D.OverlapPoint(point);
        if (overlapPoint != null && overlapPoint.gameObject == gameObject)
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

        if (Building.BuildingType == BuildingType.Tower)
        {
            var objectShooter = Instantiate(this.objectShooter, transform.position, Quaternion.identity, transform);
            objectShooter.GetComponent<ObjectShooter>().SetUp(Building.AttackPower, Building.AttackSpeed, Building.AttackRange, Building.ShooterOffset);
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
