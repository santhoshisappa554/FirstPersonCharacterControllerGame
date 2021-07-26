using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyMoveSpeed;
    public Rigidbody rb;
    public Transform target;
    public bool chasing;
    public float distanceTochase, distanceToloose,distannceToStop;
    public NavMeshAgent agent;
    Vector3 startPoint;
    public float keepChasingTime,chasecounter;
    public GameObject enemyBulletPrefab;
    public Transform enemyFirePoint;
    public float fireRate,firecount,fireWaitCounter,waitBetweenShoots,ShootTimecounter;//enemy fire Rate controller
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        ShootTimecounter = 1.0f;
        fireWaitCounter = waitBetweenShoots;
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasing)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position)<distanceTochase)
            {
                chasing = true;
                //firecount = 1.0f;
                ShootTimecounter = 1f;
                fireWaitCounter = waitBetweenShoots;
            }
            if (chasecounter > 0)
            {
                chasecounter -= Time.deltaTime;
                if (chasecounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
            
            
        }
        else
        {

            //transform.LookAt(PlayerController.instance.transform.position);
            //rb.velocity = transform.forward * enemyMoveSpeed;
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distannceToStop)
            {

            }
            agent.destination = PlayerController.instance.transform.position;
            
            
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceTochase)
            {
                chasing = false;
                agent.destination = startPoint;
                chasecounter = keepChasingTime;
            }
            if (fireWaitCounter > 0)
            {
                fireWaitCounter -= Time.deltaTime;
                if (fireWaitCounter <= 0)
                {
                    ShootTimecounter = 1f;
                }
            }

            else
            {
                ShootTimecounter -= Time.deltaTime;
                if (ShootTimecounter > 0)
                {
                    firecount -= Time.deltaTime;
                    if (firecount <= 0)
                    {
                        firecount = fireRate;
                        enemyFirePoint.LookAt(PlayerController.instance.transform.position+new Vector3(0,1.5f,0));
                        Vector3 targetDirection = PlayerController.instance.transform.position - transform.position;
                        float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                        if (Mathf.Abs(angle) < 40f)
                        {
                            Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
                        }
                        else
                        {
                            fireWaitCounter = waitBetweenShoots;
                        }

                       
                    }
                }
                else
                {
                    fireWaitCounter = waitBetweenShoots;
                }
            }

           

        }

    }
}
