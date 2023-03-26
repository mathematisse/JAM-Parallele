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
    private int left;


    private void Update()
    {
        if (HarvestTarget.GetComponent<CollectScript>().IsHovered(transform.position)) {
            if (!Harvested)
            {
                Harvested = true;
                HideSprite();
                WalkStop();
                Invoke("LeaveHarvest", HarvestTime);
            }
        }

        if ((transform.position.x > -1 && transform.position.x < 1)) {
            if (Harvested)
            {
                HarvestTarget.GetComponent<CollectScript>().addRessources();
                Destroy(gameObject);
            }
        }
    }

    public void first_Start()
    {
        if (left == 1)
        {
            WalkLeft();
        } else
            WalkRight();
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
}
