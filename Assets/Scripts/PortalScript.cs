using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Entity>().PassPortal();
        if (collision.gameObject.transform.position.y > 0) {
            collision.gameObject.transform.position -= new Vector3(0, 5f, 0);
        } else {
            collision.gameObject.transform.position += new Vector3(0, 5f, 0);
        }
    }
}
