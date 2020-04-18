using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent(typeof(NavMeshAgent))]
public class SmartEnemy : MonoBehaviour, iMovement
{

    public bool dead = false;
    public float StartHealth;
    private float hp;
    public int scoreValue, livesCost, goldValue;
    private NavMeshAgent agent;
    public Image healthBar;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        hp = StartHealth;
    }

    public void Initialize(WaypointManager waypointManager)
    {
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(waypointManager.waypoints[0].transform.position, out closestHit, 100f, NavMesh.AllAreas))
        {
            transform.position = closestHit.position;
            StartCoroutine(Spawn(waypointManager));
        }

    }

    IEnumerator Spawn(WaypointManager waypointManager)
    {
        yield return new WaitForSeconds(.2f);
        agent.enabled = true;
        agent.SetDestination(waypointManager.waypoints[waypointManager.waypoints.Length - 1].transform.position);
    }
    public void takeDamage(float amt)
    {
        hp -= amt;
        healthBar.fillAmount = hp / StartHealth;
        if (hp <= 0)
        {
            death();
        }
    }
    public void death()
    {
        if (!dead)
        {
            dead = true;
            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<GuiScript>().addScore(scoreValue);
            cam.GetComponent<GuiScript>().addGold(goldValue);
            Destroy(gameObject);
        }
    }
}
    