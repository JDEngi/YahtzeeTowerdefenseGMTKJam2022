using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : SingleTargetTower
{
    [Header("Unity")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            base.Update();

            // if we have no new target, do nothing
            if (targetEntity == null)
            {
                fireCountdown = 0;
                return;
            }

            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }
    protected override void Shoot()
    {
        GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.Seek(targetEntity.transform);
        
        targetEntity.ApplyDamage(Damage);
    }
}
