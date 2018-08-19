﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [HeaderAttribute("Enemy Attributes")]
    public float currentHealth;
    public float maxHealth;
    public float walkSpeed = 10.0f;
    public float currentSpeed = 0.0f;
    public float expReward = 25f;

    private Pathfinding pathfinding;
    private float pathPointThreshhold = 0.7f;
    private GameObject player;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
    }
    // Use this for initialization
    void Start () {
        currentSpeed = walkSpeed;
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (pathfinding.pathToFollow != null && pathfinding.pathToFollow.Count >0) FollowPath();
	}

    private void FollowPath()
    {
        Vector3 currentPosition = transform.position;
        currentPosition = Vector3.MoveTowards(currentPosition,
            pathfinding.pathToFollow[0].WorldPosition + new Vector3(0.5f, 0.5f, 0),
            Time.deltaTime * currentSpeed);

        //if (Vector3.Distance(currentPosition, pathfinding.pathToFollow[0].WorldPosition) <= pathPointThreshhold)
        //    currentPosition = Vector3.MoveTowards(currentPosition,
        //        pathfinding.pathToFollow[1].WorldPosition,
        //        Time.deltaTime * currentSpeed);


        transform.position = currentPosition;
    }

    public void AlterHealth(float amount)
    {
        if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth + amount <= 0)
        {
            player.GetComponent<PlayerStats>().GainExp(expReward);
            Destroy(this.gameObject);
        }
        else
        {
            currentHealth += amount;
        }
    }
}
