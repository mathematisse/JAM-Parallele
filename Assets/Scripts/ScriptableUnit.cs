using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Knight,
    Soldier
}

[CreateAssetMenu(fileName = "New Unit", menuName = "Units")]
public class ScriptableUnit : ScriptableObject, ISelectable
{
    public SelectableType type => SelectableType.Unit;
    public GameObject Prefab;
    public int WoodCost;
    public int StoneCost;
    public int MushroomCost;
    public int SoulCost;

    public int AttackPower;
    public float AttackSpeed;
    public int Hp;
    public float WalkSpeed;
}
