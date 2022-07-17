using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractEnemy : MonoBehaviour
{
    public float maxSpeed;

    public float startHealth;
    private float health;

    public int killValue = 1;

    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;

    private Vector3 velocity;
    private float acceleration = 0.2f;
    private float waypointGrabDist;

    public void Start()
    {
        target = Waypoints.points[0];
        waypointGrabDist = (0.02f * maxSpeed) / (acceleration);

        int waveNumber = FindObjectOfType<WaveManager>().WaveNumber;
        float waveExponentDivFactor = GameManager.WaveExponentDivFactor;
        float waveNumberExponentFactor = 1f + (float)waveNumber / waveExponentDivFactor;

        health = Mathf.Pow(startHealth, waveNumberExponentFactor);

        velocity = new Vector3(0, 0, 0);
    }

    public void Update()
    {
        Vector3 dir = target.position - transform.position;
        Vector3 targetVelocity = dir.normalized * maxSpeed;
        velocity += (targetVelocity - velocity) * acceleration;

        transform.Translate(velocity * Time.deltaTime, Space.World);
        
        float distance = Vector3.Distance(transform.position, target.position);

        // Update waypoint
        if (distance <= waypointGrabDist)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length -1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        GameManager.AddScore(killValue);

        Destroy (gameObject);
    }
}

public enum EnemyTypes { grunt, soldier, tank };
