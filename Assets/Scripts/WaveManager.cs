using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct WaveThief
{
    public int AttackPower;
    public int AttackSpeed;
    public int Hp;
    public float WalkSpeed;
}

[Serializable]
public struct Wave
{
    public WaveThief[] army;
    public int until;
}

public class WaveManager : MonoBehaviour
{
    public WaveRuntimeUI waveUi;
    public AudioSource waveSound;
    [SerializeField]
    public Wave[] waves;

    public GameObject PrefabThief;

    private DateTime LastWaveTimestamp;
    private DateTime StartWaveTimestamp;

    private int waveIndex = 0;

    private void Start()
    {
        StartWaveTimestamp = DateTime.Now;
        LastWaveTimestamp = DateTime.Now;
        waveUi.UpdateWave(0);
        waveUi.UpdateTimer(waves[0].until);
    }

    // Update is called once per frame
    void Update()
    {
        if (waves.Length <= 0) return;
        bool lastWave = waveIndex == waves.Length - 1;
        int each = 0;
        if (lastWave)
        {
            each = waves[waveIndex].until;

            if (waves.Length > 1)
            {
                each = waves[waveIndex].until - waves[waveIndex - 1].until;
            }
        }
        SpawnWave(waves[waveIndex], each);
        waveUi.UpdateTimer((int)(StartWaveTimestamp.AddSeconds(waves[waveIndex].until).Subtract(DateTime.Now).TotalSeconds));
    }

    void SpawnWave(Wave wave, int wait)
    {
        if (wait > 0)
        {
            if ((DateTime.Now - LastWaveTimestamp).TotalSeconds < wait) return;
        }
        else
        {
            if (DateTime.Now < StartWaveTimestamp.AddSeconds(wave.until)) return;
        }
        if (waveIndex != waves.Length - 1) waveIndex++;

        float xOffset = 5f;
        waveSound.Play();
        waveUi.UpdateWave(waveIndex);
        foreach (WaveThief thief in wave.army)
        {
            SpawnThief(thief, xOffset);
            xOffset += 0.5f;
        }
        LastWaveTimestamp = DateTime.Now;
    }

    void SpawnThief(WaveThief thief, float xOffset)
    {
        GameObject troop = Instantiate(PrefabThief);
        troop.transform.position = new Vector3(xOffset, -1, 0);
        AttackableEntity entity = troop.GetComponent<AttackableEntity>();
        entity.FakeStart();
        entity.Attack = thief.AttackPower;
        entity.AttackSpeed = thief.AttackSpeed;
        entity.WalkSpeed = thief.WalkSpeed;
        entity.Hp = thief.Hp;
        entity.PassPortal();
        entity.WalkLeft();

        troop = Instantiate(PrefabThief);
        troop.transform.position = new Vector3(-xOffset, -1, 0);
        entity = troop.GetComponent<AttackableEntity>();
        entity.FakeStart();
        entity.Attack = thief.AttackPower;
        entity.AttackSpeed = thief.AttackSpeed;
        entity.WalkSpeed = thief.WalkSpeed;
        entity.Hp = thief.Hp;
        entity.PassPortal();
        entity.WalkRight();
    }
}
