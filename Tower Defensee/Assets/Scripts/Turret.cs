using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;


    [Header("Attributes")]
    [SerializeField] private float targetInRange = 4f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletPerSecond = 1f;
    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null) 
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        } else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bulletPerSecond)
            {
                Shoot();
                timeUntilFire = 0f;
            }       
            
        }
    }

    private void Shoot()
    {
        GameObject bulletB = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletscript = bulletB.GetComponent<Bullet>();
        bulletscript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetInRange, (Vector2)
            transform.position, 0f, enemyMask);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetInRange;
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x)* Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color= Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetInRange);
    }

  
    // Start is called before the first frame update


    // Update is called once per frame

}
