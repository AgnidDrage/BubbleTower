using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapadminscript : MonoBehaviour
{   
    [SerializeField] private GameObject playerprefab;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float bubble_speed = 2f;
    [SerializeField] private float bubble_spawn_rate = 1f;
    public GameObject player;
    private Transform spawnpoint;
   
    private Transform playerspawnpoint;

    private GameObject currentspawnedplayer;
    [SerializeField] public float rushspeed = -1f; // speed of platforms falling    
    // Start is called before the first frame update
    void Start()
    {
        
        spawnpoint = transform.GetChild(3);
        playerspawnpoint = transform.GetChild(4);
        SpawnPlayer();
    }
  


    void SpawnPlayer(){
       player = Instantiate(playerprefab,playerspawnpoint.position,Quaternion.identity);
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


    Vector3 Relrandspawn(int maxdist,float exclude)
    {
        
        int exclude2= Mathf.FloorToInt(exclude);
        int[] iArray= new int[4];
        for(int x = 0;x<iArray.Length;x++){
          if( (x-maxdist)*3 == exclude2){
            
            iArray[x] = 99;

          }
            else{
                iArray[x] = (x-maxdist)*3;
            }
        }

        for(int x = 0;x<iArray.Length;x++){
            
        }
        
        var truemaxdist = iArray[Random.Range(0,iArray.Length)];



        return new Vector3(truemaxdist, spawnpoint.transform.position.y, 0);
    }

    void InstantiateBubble()
    {
        Vector3 spawnpointforbubble = Relrandspawn(2,player.transform.position.x);

        if (spawnpointforbubble.x != 99){
         GameObject newbubble = Instantiate(bubblePrefab, spawnpointforbubble, Quaternion.identity);
         newbubble.transform.parent = transform;
         Rigidbody2D newbubble_rb = newbubble.GetComponent<Rigidbody2D>();
         newbubble_rb.velocity = new Vector2(0, bubble_speed);
}
    }
}
