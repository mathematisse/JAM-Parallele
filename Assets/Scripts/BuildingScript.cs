using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    public ScriptableBuilding Building;
    public GameObject mirroredObject;

    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Building.Sprite;

        mirroredObject = new GameObject(this.GetType().Name + " mirrored");
        mirroredObject.transform.position = new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z);
        SpriteRenderer mirroredSpriteRenderer = mirroredObject.AddComponent<SpriteRenderer>();
        mirroredSpriteRenderer.sprite = Building.Sprite;
        mirroredSpriteRenderer.flipY = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
