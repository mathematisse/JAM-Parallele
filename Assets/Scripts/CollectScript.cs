using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    private float collectable = -1;
    [SerializeField] private RessourceManager ressources;
    [SerializeField] private GameRuntimeUI gameRuntime;
    [SerializeField] private HarvestManager harvest;
    public float wood = 0;
    public float stone = 0;
    public float Soul = 0;
    public float Mushroom = 0;

    private CursorManager _cursor;

    public bool IsHovered(Vector2 point)
    {
        var contact = GetComponent<BoxCollider2D>().bounds.Contains(point);
        return contact;
    }

    void Start()
    {
        _cursor = FindObjectOfType<CursorManager>();
    }

    void Update()
    {
        if (!IsHovered(_cursor.mousePosition) || !Input.GetMouseButtonDown(0))
            return;
        if (collectable == 1 && Input.GetMouseButton(0))
        {
            if (wood == 1 || stone == 1) {
                harvest.spawn(gameObject, false);
            }
            if (Soul == 1 || Mushroom == 1) {
                harvest.spawn(gameObject, true);
            }
            //gameRuntime.resetHarvest();
        }
    }

    public void switchCollectable()
    {
        collectable = 1;
    }

    public void resetCollectable()
    {
        collectable = -1;
    }

    public void addRessources()
    {
        if (wood == 1)
            ressources.AddWood(5);
        if (stone == 1)
            ressources.AddStone(5);
        if (Soul == 1)
            ressources.AddSoul(5);
        if (Mushroom == 1)
            ressources.AddMushroom(5);
        harvest.addHarvest();
    }
}
