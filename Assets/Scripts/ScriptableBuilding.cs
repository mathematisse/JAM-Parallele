using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Tower,
    Stock,
    Barrack
}

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class ScriptableBuilding : ScriptableObject, ISelectable
{
    public SelectableType type => SelectableType.Building;
    public Sprite Sprite;
    public int WoodCost;
    public int StoneCost;
    public int MushroomCost;
    public int SoulCost;
    public BuildingType BuildingType;

    public float AttackPower;
    public float AttackSpeed;
    public float AttackRange;
    public float ShooterOffset;
    public float Hp;

    public int WoodStorage;
    public int StoneStorage;

    public ScriptableBuilding nextBuilding;
}
