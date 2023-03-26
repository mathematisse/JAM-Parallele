using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestEntity : Entity
{

    public GameObject HarvestTarget;
    public GameObject HomeTarget;
    public float HarvestTime = 5f;

    private bool Harvested = false;
    public bool IsTargetUpsideDown;

    protected new void Start()
    {
        Debug.Log("New Merchant !");
    }

    private void Update()
    {
        if ((transform.position.x > -1 && transform.position.x < 1)) {
            if (Harvested)
            {
                HarvestTarget.GetComponent<CollectScript>().addRessources();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Harvested && collision.gameObject == HarvestTarget)
        {
            Harvested = true;
            HideSprite();
            WalkStop();
            Invoke("LeaveHarvest", HarvestTime);
        }
    }

    public void first_Start()
    {
        base.Start();
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

    public void LeaveHarvest()
    {
        ShowSprite();
        if (IsTargetUpsideDown)
        {
            if (transform.position.x < 0)
            {
                WalkRight();
            }
            else
            {
                WalkLeft();
            }
        }
        else
        {
            WalkTo(HomeTarget);
        }
    }
}
