using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] AudioClip arrowSound;
    AudioSource audioSource;

    [SerializeField] GameObject arrow;
    [SerializeField] GameObject boulder;

    readonly float minArrowX = -14f;
    readonly float maxArrowX = 14f;

    readonly List<float> arrowY = new List<float>()
    {
        0, 0, 0, 2, 4, 6
    };

    readonly float boulderSpawnY = 8.5f;

    readonly float minBoulderX = -11f;
    readonly float maxBoulderX = 11f;

    float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer % 60f > 2f)
        {
            int arrowIterator = Random.Range(1, 4);

            for (int i = 0; i < arrowIterator; i++)
            {
                audioSource.PlayOneShot(arrowSound);
                SpawnArrow();
            }

            int boulderIterator = Random.Range(1, 3);

            for (int i = 0; i < boulderIterator; i++)
                SpawnBoulder();

            timer = 0f;
        }
    }

    void SpawnArrow()
    {
        float xPos;

        if (Random.value < 0.5f)
            xPos = minArrowX;
        else
            xPos = maxArrowX;

        Vector3 arrowAngle = Vector3.zero;
        Vector3 direction = Vector3.right;
        float limit = maxArrowX;
        int arrowYIndex = Random.Range(0, arrowY.Count);

        if (xPos == maxArrowX)
        {
            arrowAngle = new Vector3(0f, 0f, 180f);
            direction = Vector3.left;
            limit = minArrowX;
        }

        Vector3 spawnPoint = new Vector3(xPos, arrowY[arrowYIndex], 0f);

        GameObject arrowObj = Instantiate(arrow, spawnPoint, Quaternion.Euler(arrowAngle));
        arrowObj.GetComponent<Arrow>().SetValues(direction, limit);
    }

    void SpawnBoulder()
    {
        float xPos = Random.Range(minBoulderX, maxBoulderX);

        Vector3 spawnPoint = new Vector3(xPos, boulderSpawnY, 0f);

        Instantiate(boulder, spawnPoint, Quaternion.identity);
    }
}
