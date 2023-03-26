using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{

    public GameObject Prefab;
    
    private GameObject ResultPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        ResultPrefab = Instantiate(Prefab, transform.position + Vector3.down, transform.rotation);
        ResultPrefab.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
