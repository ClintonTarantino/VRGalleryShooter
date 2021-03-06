﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Collider))]
public class EnemyController : MonoBehaviour {

    public Text scoreText;

    CardboardHead head;
    Vector3 startPosition;
    SpriteRenderer enemyRenderer;
    Collider enemyCollider;
    AudioSource deadSound;

    static int score;

    void Start ()
    {
        head = Camera.main.GetComponent<StereoController>().Head;
        startPosition = transform.localPosition;

        deadSound = GetComponent<AudioSource>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider>();

        enemyRenderer.enabled = false;
        enemyCollider.enabled = false;


        StartCoroutine(SpawnRate());
    }

    void Update ()
    {
        RaycastHit hit;
        bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
        /*if (Cardboard.SDK.Triggered && isLookedAt)
        
            EnemyDead();
     */
        if (isLookedAt)
            EnemyDead();
    }

    IEnumerator SpawnRate()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        enemyCollider.enabled = true;
        enemyRenderer.enabled = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        enemyCollider.enabled = false;
        enemyRenderer.enabled = false;
        StartCoroutine(SpawnRate());
    }

    public void EnemyDead()
    {
        enemyCollider.enabled = false;
        enemyRenderer.enabled = false;

        deadSound.Play();

        score++;
        scoreText.text = " " + score;
    }

}
