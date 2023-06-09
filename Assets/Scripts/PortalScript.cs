using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>() == null) return;
        collision.gameObject.GetComponent<Entity>().PassPortal();
        audioSource.Play();
        if (collision.gameObject.transform.position.y > 0) {
            if (collision.gameObject.transform.position.x > 0) {
                collision.gameObject.transform.position -= new Vector3(0.5f, 3f, 0);
            } else {
                collision.gameObject.transform.position -= new Vector3(-0.5f, 3f, 0);
            }
        } else {
            if (collision.gameObject.transform.position.x > 0) {
                collision.gameObject.transform.position += new Vector3(-0.5f, 3f, 0);
            } else {
                collision.gameObject.transform.position += new Vector3(0.5f, 3f, 0);
            }
        }
    }
}
