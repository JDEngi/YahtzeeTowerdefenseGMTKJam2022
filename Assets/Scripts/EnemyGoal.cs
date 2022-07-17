using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGoal : MonoBehaviour
{
    public float HealthPoints {
        get { return _healthPoints; }
        set { _healthPoints = value;
            healthBar.fillAmount = _healthPoints / startHealth; 
        }
    }
    private float _healthPoints;
    private float startHealth;

    public Image healthBar;

    public static event Action<float> OnHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = _healthPoints;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            HealthPoints -= 1;
            Destroy(collision.gameObject);
        }

        OnHealthChanged?.Invoke(HealthPoints);
        Debug.Log("OUCH!");
    }
}
