using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Tower,
    Stock,
    Casern
}

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class ScriptableBuilding : ScriptableObject
{
    public Sprite Sprite;
    public int WoodCost;
    public int StoneCost;
    public int MushroomCost;
    public int SoulCost;
    public BuildingType BuildingType;

    public float AttackPower;
    public float AttackSpeed;
    public float AttackRange;
    public float Hp;

    public int WoodStorage;
    public int StoneStorage;
}
