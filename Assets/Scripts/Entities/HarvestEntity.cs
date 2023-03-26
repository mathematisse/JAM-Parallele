using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestEntity : Entity
{

    public GameObject HarvestTarget;
    public bool IsTargetUpsideDown;
    public GameObject HomeTarget;

    public float HarvestTime = 5f;

    private bool Harvested = false;

    protected new void Start()
    {
        if (IsTargetUpsideDown)
        {
            if (HarvestTarget.transform.position.x < 0)
            {
                WalkLeft();
            } else
            {
                WalkRight();
            }
        }
        else
        {
            WalkTo(HarvestTarget);
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Harvested && collision.gameObject == HarvestTarget)
        {
            Harvested = true;
            HideSprite();
            Invoke("LeaveHarvest", HarvestTime);
        }
        if (Harvested && collision.gameObject == HomeTarget)
        {
            HideSprite();
        }
    }

    public void LeaveHarvest()
    {
        ShowSprite();
        if (IsTargetUpsideDown)
        {
            if (transform.position.x < 0)
            {
                WalkLeft();
            }
            else
            {
                WalkRight();
            }
        }
        else
        {
            WalkTo(HomeTarget);
        }
    }

    public void WalkTo(GameObject target)
    {
        if (transform.position.x > target.transform.position.x)
        {
            WalkLeft();
        } else
        {
            WalkRight();
        }
    }
}
