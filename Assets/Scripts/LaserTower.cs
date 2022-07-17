using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : SingleTargetTower
{
    [Header("Unity")]
    public Transform firePoint;
    public LineRenderer lineRenderer;

    //[Header("Attributes laser tower")]
    private float fireCountdown = 0f;

    private static float DAMAGE_DELAY = 0.1f;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        if (targetEntity != null)
        {
            // if the target is still in range, draw the line
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, targetEntity.transform.position);
        }
        else
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;
        }

        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            // if we have no new target, do nothing
            if (targetEntity == null)
            {
                fireCountdown = 0;
                return;
            }

            Shoot();
            fireCountdown = DAMAGE_DELAY;
        }
    }
    protected void Shoot()
    {
        //GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        //bullet.Seek(targetEntity.transform);

        targetEntity.ApplyDamage(Damage * DAMAGE_DELAY);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
    }
}
