using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : SingleTargetTower
{
    [Header("Unity")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public LineRenderer lineRenderer;

    [Header("Attributes laser tower")]
    public float fireRate = 50f;
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
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
                return;
            }

            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }
    protected override void Shoot()
    {
        //GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        //bullet.Seek(targetEntity.transform);

        targetEntity.ApplyDamage(Damage);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetEntity.transform.position);

    }
}
