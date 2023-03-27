using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneShooter : MonoBehaviour
{
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            var shooter = GetComponent<ObjectShooter>();
            shooter.SetUp(shooter.attackPower, shooter.shootSpeed, shooter.shootRange, shooter.shooterOffset, shooter.isOurs);
            done = true;
        }
    }
}
