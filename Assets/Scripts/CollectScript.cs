using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    private float collectable = -1;
    [SerializeField] private RessourceManager ressources;
    [SerializeField] private GameRuntimeUI gameRuntime;
    public float wood = 0;
    public float stone = 0;
    public float Soul = 0;
    public float Mushroom = 0;
    [SerializeField] private GameObject Harvest;
    [SerializeField] private GameObject Castle;
    
    void OnMouseDown()
    {
        Debug.Log(wood + " " + stone);
        Debug.Log(this.gameObject.name);
        if (collectable == 1)
        {
            if (wood == 1) {
                Instantiate(Harvest, new Vector3(0, 3, 0), Quaternion.identity);
                Harvest.GetComponent<HarvestEntity>().HomeTarget = Castle;
                Harvest.GetComponent<HarvestEntity>().HarvestTarget = gameObject;
                Harvest.GetComponent<HarvestEntity>().IsTargetUpsideDown = false;
                Harvest.GetComponent<HarvestEntity>().first_Start();
            }
            if (stone == 1) {
                Instantiate(Harvest, new Vector3(0, 3, 0), Quaternion.identity);
                Harvest.GetComponent<HarvestEntity>().HomeTarget = Castle;
                Harvest.GetComponent<HarvestEntity>().HarvestTarget = gameObject;
                Harvest.GetComponent<HarvestEntity>().IsTargetUpsideDown = false;
                Harvest.GetComponent<HarvestEntity>().first_Start();
            }
            if (Soul == 1) {
                Instantiate(Harvest, new Vector3(0, 3, 0), Quaternion.identity);
                Harvest.GetComponent<HarvestEntity>().HomeTarget = Castle;
                Harvest.GetComponent<HarvestEntity>().HarvestTarget = gameObject;
                Harvest.GetComponent<HarvestEntity>().IsTargetUpsideDown = true;
                Harvest.GetComponent<HarvestEntity>().first_Start();
            }
            if (Mushroom == 1) {
                Instantiate(Harvest, new Vector3(0, 3, 0), Quaternion.identity);
                Harvest.GetComponent<HarvestEntity>().HomeTarget = Castle;
                Harvest.GetComponent<HarvestEntity>().HarvestTarget = gameObject;
                Harvest.GetComponent<HarvestEntity>().IsTargetUpsideDown = true;
                Harvest.GetComponent<HarvestEntity>().first_Start();
            }
            gameRuntime.resetHarvest();
        }
    }

    public bool IsHovered(Vector2 point)
    {
        var overlapPoint = Physics2D.OverlapPoint(point);
        if (overlapPoint != null && overlapPoint.gameObject == gameObject)
        {
            return true;
        }
        return false;
    }

    public void switchCollectable()
    {
        collectable = 1;
    }

    public void resetCollectable()
    {
        collectable = -1;
    }

    public void addRessources()
    {
        if (wood == 1)
            ressources.AddWood(5);
        if (stone == 1)
            ressources.AddStone(5);
        if (Soul == 1)
            ressources.AddSoul(5);
        if (Mushroom == 1)
            ressources.AddMushroom(5);
    }
}
