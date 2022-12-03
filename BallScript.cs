using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool isRestarting = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HitCube") StartCoroutine(RestartAfterHit(1.3f));
        if (collision.gameObject.tag == "Edge") StartCoroutine(RestartAfterHit(0f));
    }

    IEnumerator RestartAfterHit(float time)
    {
        if (!isRestarting)
        {
            Debug.Log("RestartAfterHit");
            isRestarting = true;
            yield return new WaitForSeconds(time);
            GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>().SpawnBall();
        }
    }
}