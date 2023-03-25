using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertMap : MonoBehaviour
{
    public GameObject childPrefab;

    private void Start()
    {
        // Get all child objects of this game object
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        // Loop through all child objects (excluding the parent)
        for (int i = 1; i < children.Length; i++)
        {
            Transform child = children[i];

            // Duplicate the child object
            GameObject newChild = Instantiate(childPrefab, transform);

            // Set the position and rotation of the new child object
            newChild.transform.position = new Vector3(child.position.x, child.position.y - 5f, child.position.z);
            newChild.transform.rotation = Quaternion.Euler(child.rotation.eulerAngles.x, child.rotation.eulerAngles.y + 180f, child.rotation.eulerAngles.z);
        }
    }
}
