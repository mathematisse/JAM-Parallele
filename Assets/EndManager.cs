using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public AudioSource win;
    public AudioSource lose;

    [SerializeField] private AttackableEntity ally;
    [SerializeField] private AttackableEntity ennemy;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject defaite;

    public float delay = 3;
    float timer;

    private bool _screenOpened = false;

    void Update()
    {
        if (ally.IsDead()) {
            if (!_screenOpened)
            {
                lose.Play();
                GameObject screen = Instantiate(defaite, new Vector3(0, 0, 0), Quaternion.identity);
                _screenOpened = true;
            }
            timer += Time.deltaTime;
            if (timer > delay)
            {
                callScene();
            }
        } else if (ennemy.IsDead()) {
            if (!_screenOpened)
            {
                win.Play();
                GameObject screen = Instantiate(victory, new Vector3(0, 0, 0), Quaternion.identity);
                _screenOpened = true;
            }
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
