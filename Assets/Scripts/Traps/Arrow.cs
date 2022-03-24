using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    readonly float speed = 9f;
    float limit = 0f;

    public void SetValues(Vector3 direction, float limit)
    {
        this.direction = direction;
        this.limit = limit;
    }

    void Update()
    {
        if ((transform.position.x < limit && limit > 0) || (transform.position.x > limit && limit < 0))
            transform.position += direction * speed * Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Boulder"))
        {
            Destroy(gameObject);
        }
    }
}
