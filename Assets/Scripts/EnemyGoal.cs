using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyGoal : MonoBehaviour
{
    public float HealthPoints;

    public static event Action<float> OnHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        
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
