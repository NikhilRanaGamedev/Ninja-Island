using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] Player player;
    TMPro.TextMeshPro text;
    float timer = 0f;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshPro>();
    }

    void Update()
    {
        if (player.GetLives() >= 1)
        {
            timer += Time.deltaTime;

            text.text = "Score: " + timer.ToString("F1") + "s";
        }
    }
}
