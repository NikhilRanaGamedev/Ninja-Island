using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    Player playerScript;

    Vector3 direction;
    Vector3 origin;
    readonly float speed = 30f;
    float range = 0f;

    public void SetValues(Vector3 dir, Vector3 source, float distance, Player player)
    {
        direction = dir;
        origin = source;
        range = distance;
        playerScript = player;
    }

    void Update()
    {
        if (Vector3.Distance(origin, transform.position) < range)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            playerScript.Teleport(transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KunaiBoundary"))
        {
            playerScript.Teleport(transform.position);
            Destroy(gameObject);
        }
    }
}

/*
&&
transform.position.y >= 0f && transform.position.y <= 6f &&
transform.position.x >= -11.75f && transform.position.x <= 11.75f
 */