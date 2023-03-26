using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Wave", menuName = "Waves")]
public class ScriptableWave : ScriptableObject
{
    public ScriptableWaveThief[] army;
    public int until;
}
