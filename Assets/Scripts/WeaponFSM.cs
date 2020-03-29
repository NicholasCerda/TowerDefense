using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponStates
{
  Idle,
  Target,
  Shoot
}

public enum WeaponParameters
{
  isTarget,
  targetDistroyed,
  newTarget
}


[RequireComponent(typeof(Animator))]
public class WeaponFSM : MonoBehaviour
{
    public float range;
    public int cost;
    private Animator weapon;
    public GameObject bullet;
    public GameObject target;
    //public GameObject[] targets;
    public float RotationSpeed=1.5f;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    /*
     okay so i need to set it so there is an invisible ring around each tower (tower range) that OnCollisionEnter, add object to queue;
     if target dies or object is out of range, pop it off queue
     if next target in list is already dead or out of range, pop it off queue
         */
    void Awake()
    {
        weapon = GetComponent<Animator>();
        target = null;
        RotationSpeed = 3.0f;
    }
    void Update()
    {
        if (target != null)
        {
            if (range < Vector3.Distance(target.transform.position, transform.position))
            {
                target = null;
            }
        }
        if (target == null)
        {
            targetFinder();
        }
        if (target!=null)
        {
            weapon.SetBool("isTarget",true);
        }
        else
        {
            weapon.SetBool("isTarget", false);
        }
        if (target != null)
        {
            _direction = (target.transform.position - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
            //Debug.Log("LookAt Target" + target.name+" "+target.transform.position);
            //gameObject.transform.LookAt(target.transform);
        }

    }
    void targetFinder()
    {
        weapon.SetBool("isTarget", false);
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject nEnemy in targets)
        {
            if (target == null)
            {
                if (range >= (Vector3.Distance(nEnemy.transform.position, transform.position)))
                {
                    target = nEnemy;
                }
            }
            else if ((Vector3.Distance(target.transform.position, transform.position)) >= (Vector3.Distance(nEnemy.transform.position, transform.position)))
            {
                target = nEnemy;
            }
        }
        if (target != null)
        {
            weapon.SetTrigger("newTarget");
            weapon.ResetTrigger("destroyedTarget");
        }
        else
        {
            weapon.SetTrigger("destroyedTarget");
            weapon.ResetTrigger("newTarget");
        }
    }
    void Bang()
    {
        Debug.Log("Pow");
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        if (target != null)
        {
            newBullet.GetComponent<Bullet>().target = target;
            newBullet.transform.LookAt(target.transform);
            newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * 5.0f;
        }
        //newBullet.transform.rotation = transform.rotation;
    }

}
