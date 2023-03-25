using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    public int wood;
    public int stone;
    public int mushroom;
    public int soul;

    [SerializeField]
    private RessourceRuntimeUI _ressourceRuntimeUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWood(int amount)
    {
        wood += amount;
        _ressourceRuntimeUI.UpdateWood(wood);
    }

    public void RemoveWood(int amount)
    {
        wood -= amount;
        _ressourceRuntimeUI.UpdateWood(wood);
    }

    public void AddStone(int amount)
    {
        stone += amount;
        _ressourceRuntimeUI.UpdateStone(stone);
    }

    public void RemoveStone(int amount)
    {
        stone -= amount;
        _ressourceRuntimeUI.UpdateStone(stone);
    }

    public void AddMushroom(int amount)
    {
        mushroom += amount;
        _ressourceRuntimeUI.UpdateMushroom(mushroom);
    }

    public void RemoveMushroom(int amount)
    {
        mushroom -= amount;
        _ressourceRuntimeUI.UpdateMushroom(mushroom);
    }

    public void AddSoul(int amount)
    {
        soul += amount;
        _ressourceRuntimeUI.UpdateSoul(soul);
    }

    public void RemoveSoul(int amount)
    {
        soul -= amount;
        _ressourceRuntimeUI.UpdateSoul(soul);
    }
}
