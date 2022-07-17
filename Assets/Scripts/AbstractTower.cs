
using System;
using Unity.VisualScripting;
using UnityEngine;

public class AbstractTower : MonoBehaviour
{
    private GameObject entityContainer;
    private AbstractEnemy targetEntity;

    [Header("Attributes")]
    public int BuildCost;
    public float Damage;
    public float Range;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity")]
    public string NameOfEntityContainer;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    public void Start()
    {
        entityContainer = GameObject.Find(NameOfEntityContainer);
        if (!entityContainer) throw new Exception("Could not find object with name " + NameOfEntityContainer);
    }

    // Update is called once per frame
    public void Update()
    {
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            // if we have a target, check whether it is valid
            if (targetEntity != null && !targetEntity.IsDestroyed())
            {
                float currentDistance = Vector3.Distance(targetEntity.transform.position, transform.position);
                if (currentDistance > Range)
                {
                    targetEntity = null;
                }
            }

            // if we have no valid target, search a new one
            if (targetEntity == null)
            {
                targetEntity = SearchEnemyToShoot();
            }

            // if we have no new target, do nothing
            if (targetEntity == null)
            {
                return;
            }

            // Shoot
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(targetEntity.transform);
        }


        Debug.Log("Shoot!");
        targetEntity.ApplyDamage(Damage);
    }

    private AbstractEnemy SearchEnemyToShoot()
    {
        AbstractEnemy[] allEnemies = entityContainer.GetComponentsInChildren<AbstractEnemy>();
        foreach (AbstractEnemy enemy in allEnemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < Range) return enemy;
        }

        return null;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
