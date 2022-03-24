using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBoulder : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyItself());
    }

    IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(10 / 60f);

        Destroy(gameObject);
    }
}
