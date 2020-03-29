using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Waypoint currentDestination;
    public WaypointManager waypointManager;
    private int currentIndexWaypoint = 0;
    public float speed = 1;
    public int scoreValue,hp,livesCost,goldValue;
    public bool dead = false;
    public void Initialize(WaypointManager waypointManager)
    {
        this.waypointManager = waypointManager;
        GetNextWaypoint();
        transform.position = currentDestination.transform.position; // Move to WP0
        GetNextWaypoint();
    }

    void Update()
    {
        if (currentDestination == null)
        {
            dead = true;
            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<GuiScript>().loseLives(livesCost);
            Destroy(gameObject);
        }
        else
        {
            Vector3 direction = currentDestination.transform.position - transform.position;
            if (direction.magnitude < .2f)
            {
                GetNextWaypoint();
            }

            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    private void GetNextWaypoint()
    {
        currentDestination = waypointManager.GetNeWaypoint(currentIndexWaypoint);
        currentIndexWaypoint++;
    }
    public void takeDamage(int amt)
    {
        hp -= amt;
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
