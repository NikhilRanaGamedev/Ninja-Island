using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulders : MonoBehaviour
{
    [SerializeField] Object prefab;

    readonly float speed = 6f;
    readonly float limit = 0f;

    void Update()
    {
        if (transform.position.y > limit)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
