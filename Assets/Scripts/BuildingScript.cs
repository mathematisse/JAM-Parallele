using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    public ScriptableBuilding Building;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Building.Sprite;
        Debug.Log("Sprite name: " + Building.Sprite.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
