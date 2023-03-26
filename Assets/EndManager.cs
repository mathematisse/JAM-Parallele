using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField] private AttackableEntity ally;
    [SerializeField] private AttackableEntity ennemy;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject defaite;

    public float delay = 3;
    float timer;

    void Update()
    {
        if (ally.IsDead()) {
            Debug.Log("Defaite");
            GameObject screen = Instantiate(defaite, new Vector3(0, 0, 0), Quaternion.identity);
            timer += Time.deltaTime;
            if (timer > delay)
            {
                callScene();
            }
        } else if (ennemy.IsDead()) {
            Debug.Log("Victoire");
            GameObject screen = Instantiate(victory, new Vector3(0, 0, 0), Quaternion.identity);
            timer += Time.deltaTime;
            if (timer > delay)
            {
                callScene();
            }
        }
    }

    void callScene()
    {
        SceneManager.LoadScene(0);
    }
}
