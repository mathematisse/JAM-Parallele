using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private BuildingScript[] _buildings;
    private CursorManager _cursorManager;
    private RessourceManager _ressourceManager;
    private bool isTraining = false;

    // Start is called before the first frame update
    void Start()
    {
        _buildings = GetComponentsInChildren<BuildingScript>();
        _cursorManager = FindObjectOfType<CursorManager>();
        _ressourceManager = FindObjectOfType<RessourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_cursorManager.selected is not { type: SelectableType.Unit } && isTraining)
        {
            Debug.Log("Is training false");
            isTraining = false;
        }
        else if (_cursorManager.selected is { type: SelectableType.Unit } && !isTraining)
        {
            Debug.Log("Is training true");
            isTraining = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _cursorManager.selected = null;
        }

        if (!isTraining) return;

        foreach (var building in _buildings)
        {
            if (!building.Built || building.Building.BuildingType != BuildingType.Barrack)
                continue;
            if (building.IsHovered(_cursorManager.mousePosition))
            {
                Debug.Log("Showing is hovored barrak");
                ScriptableUnit unit = _cursorManager.selected as ScriptableUnit;
                if (_ressourceManager.CanAfford(unit))
                {
                    Debug.Log("You can buy");
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject troop = Instantiate(unit.Prefab);
                        troop.transform.position = new Vector3(building.gameObject.transform.position.x, 2, 0);
                        AttackableEntity entity = troop.GetComponent<AttackableEntity>();
                        entity.FakeStart();
                        entity.Attack = unit.AttackPower;
                        entity.AttackSpeed = unit.AttackSpeed;
                        entity.WalkSpeed = unit.WalkSpeed;
                        entity.Hp = unit.Hp;
                        if (troop.transform.position.x > 0)
                        {
                            entity.WalkRight();
                        }
                        else
                        {
                            entity.WalkLeft();
                        }
                    }
                } else
                {
                    Debug.Log("You too poor");
                }
            }
        }
    }
}
