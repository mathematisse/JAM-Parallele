using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject Castle;
    private GameObject Harvest;

    private int maxHarvest = 5;

    public void spawn(GameObject Target, bool upDown)
    {
        if (maxHarvest == 0) return;
        Harvest = Instantiate(Prefab, new Vector3(0, 3, -0.5f), Quaternion.identity);
        Harvest.GetComponent<HarvestEntity>().HomeTarget = Castle;
        Harvest.GetComponent<HarvestEntity>().setTarget(Target);
        Harvest.GetComponent<HarvestEntity>().IsTargetUpsideDown = upDown;
        Harvest.GetComponent<HarvestEntity>().first_Start();
        maxHarvest -= 1;
    }

    public void addHarvest()
    {
        maxHarvest += 1;
    }
}
