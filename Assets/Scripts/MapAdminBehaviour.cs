using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapadminscript : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float bubble_speed = 2f;
    [SerializeField] private float bubble_spawn_rate = 1f;
    private Transform spawnpoint;

    [SerializeField] public float rushspeed = -1f; // speed of platforms falling    
    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = transform.GetChild(2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bubble_spawn_rate -= Time.deltaTime;
        if (bubble_spawn_rate <= 0)
        {
            InstantiateBubble();
            bubble_spawn_rate = 1f;
        }
    }


    Vector3 Relrandspawn(int maxdist)
    {
        int truemaxdist = Random.Range(-maxdist, maxdist) * 3;
        return new Vector3(truemaxdist, spawnpoint.transform.position.y, 0);
    }

    void InstantiateBubble()
    {

        GameObject newbubble = Instantiate(bubblePrefab, Relrandspawn(2), Quaternion.identity);
        // Get newbubble's rigidbody
        Rigidbody2D newbubble_rb = newbubble.GetComponent<Rigidbody2D>();
        newbubble_rb.velocity = new Vector2(0, bubble_speed);

    }
}
