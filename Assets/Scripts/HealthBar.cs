using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer rdr;
    public Gradient HealthColor;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public Vector2 Size = new Vector2(10, 0.3f);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUpBar(float maxHealth, float currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        UpdateBar(currentHealth);
    }

    // Update is called once per frame
    public void UpdateBar(float newCurrentHealth)
    {
        currentHealth = newCurrentHealth;
        float percent = currentHealth / maxHealth;
        rdr.transform.localScale = new Vector3(percent * Size.x, Size.y, 1);
        rdr.color = HealthColor.Evaluate(1 - percent);
        rdr.sortingOrder = 1 + (100 - (int)(percent * 100));
    }
}
