using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingScript[] _buildings;
    private CursorManager _cursorManager;
    private RessourceManager _ressourceManager;
    private bool _unbuiltEnabled = false;

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
        if (_cursorManager.selected is not { type: SelectableType.Building } && _unbuiltEnabled)
        {
            DisableUnbuilt();
            _unbuiltEnabled = false;
        }
        else if (_cursorManager.selected is { type: SelectableType.Building } && !_unbuiltEnabled)
        {
            EnableUnbuilt();
            _unbuiltEnabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableUnbuilt();
            _unbuiltEnabled = false;
            _cursorManager.selected = null;
        }

        if (_unbuiltEnabled)
        {
            foreach (var building in _buildings)
            {
                if (building.Built)
                    continue;
                if (building.IsHovered(_cursorManager.mousePosition))
                {
                    if (_ressourceManager.CanAfford(_cursorManager.selected as ScriptableBuilding))
                    {
                        building.GetComponent<SpriteRenderer>().color = Color.green;
                        if (Input.GetMouseButtonDown(0))
                        {
                            building.Build();
                            _ressourceManager.Spend(building.Building);
                            building.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                    else
                    {
                        building.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
                else
                {
                    building.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    public void EnableUnbuilt()
    {
        foreach (var building in _buildings )
        {
            if (building.Built == false)
                building.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void DisableUnbuilt()
    {
        foreach (var building in _buildings)
        {
            if (building.Built == false)
                building.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
