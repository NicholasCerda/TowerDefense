using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, iMovement
{
    public Waypoint currentDestination;
    public WaypointManager waypointManager;
    private int currentIndexWaypoint = 0;
    public float speed = 1, StartHealth;
    private float hp;
    public int scoreValue, livesCost, goldValue;
    public bool dead = false;
    public bool smart = false;
    public Image healthBar;
    public void Initialize(WaypointManager waypointManager)
    {
        this.waypointManager = waypointManager;
        GetNextWaypoint();
        transform.position = currentDestination.transform.position; // Move to WP0
        GetNextWaypoint();
        hp = StartHealth;
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
    public void takeDamage(float amt)
    {
        hp -= amt;
        healthBar.fillAmount = hp/StartHealth;
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