using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public GameObject[] UpParticles;
    public GameObject[] DownParticles;

    public GameObject switcherParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticles(bool up)
    {
        if (up)
        {
            foreach (GameObject particle in UpParticles)
            {
                particle.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject particle in DownParticles)
            {
                particle.SetActive(true);
            }
        }
    }

    public void StopParticles(bool up)
    {
        if (up)
        {
            foreach (GameObject particle in UpParticles)
            {
                particle.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject particle in DownParticles)
            {
                particle.SetActive(false);
            }
        }
    }

    public void switchToUp()
    {
        StopParticles(false);
        PlayParticles(true);
        switcherParticles.GetComponent<ParticleSystem>().Play();
    }

    public void switchToDown()
    {
        StopParticles(true);
        PlayParticles(false);
        switcherParticles.GetComponent<ParticleSystem>().Play();
    }
}
