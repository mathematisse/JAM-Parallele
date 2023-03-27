using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public RessourceRuntimeUI ressourceUi;
    public AudioSource ordered;
    public AudioSource cant;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject Castle;
    private GameObject Harvest;

    private int maxHarvest = 5;

    public void Start()
    {
        ressourceUi.UpdateMerchant(maxHarvest);
    }

    public void spawn(GameObject Target, bool upDown)
    {
        if (maxHarvest <= 0)
        {
            cant.Play();
            return;
        }
        ordered.Play();
        Harvest = Instantiate(Prefab, new Vector3(0, 2, -0.5f), Quaternion.identity);
        Harvest.GetComponent<HarvestEntity>().HomeTarget = Castle;
        Harvest.GetComponent<HarvestEntity>().HarvestTarget = Target;
        Harvest.GetComponent<HarvestEntity>().IsTargetUpsideDown = upDown;
        Harvest.GetComponent<HarvestEntity>().first_Start();
        maxHarvest -= 1;
        ressourceUi.UpdateMerchant(maxHarvest);
    }

    public void addHarvest()
    {
        maxHarvest += 1;
        ressourceUi.UpdateMerchant(maxHarvest);
    }
}
