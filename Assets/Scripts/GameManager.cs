using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Duck;
    public GameObject lifebuoyPrefab;
    private GameObject myLifebuoy;
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myLifebuoy = (GameObject)Instantiate(lifebuoyPrefab, new Vector2(Random.Range(-5.5f, 5.5f), Duck.transform.position.y + (14 + Random.Range(0.5f, 1f))), Quaternion.identity);
        Destroy(collision.gameObject);

    }


}