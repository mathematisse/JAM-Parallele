using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Wave Thief", menuName = "Waves Thief")]
public class ScriptableWaveThief : ScriptableObject
{
    public int AttackPower;
    public int AttackSpeed;
    public int Hp;
    public float WalkSpeed;
}
