using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;

public class spawninnercontent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] arrayofobjects;
    [SerializeField] GameObject bubbleParticles;
    GameObject prefabtargetobject; // List of possible platforms
    GameObject platformpreview;
    
        void Start()
    {
        prefabtargetobject= arrayofobjects[Random.Range(0, arrayofobjects.Length)];
        platformpreview = Instantiate(prefabtargetobject,transform.position,Quaternion.identity);
        platformpreview.transform.parent = transform;
        platformpreview.transform.localScale = 0.3f*Vector3.one;
        platformpreview.layer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitcolliders = Physics2D.OverlapCircleAll(transform.position,1.1f,1<<3);
        foreach (var hitcollider in hitcolliders){
          if (hitcollider.gameObject.GetComponent<ProjectileBehaviour>()){
            isPopped();
            Destroy(hitcollider.gameObject);
          }
        }
    }

    void isPopped(){
      platformpreview.transform.localScale = Vector3.one*0.5f;
      platformpreview.transform.parent = transform.parent.transform;
      // platformpreview.AddComponent<BoxCollider2D>();
      // platformpreview.GetComponent<BoxCollider2D>().excludeLayers = 3;
      platformpreview.AddComponent<Rigidbody2D>();
      platformpreview.GetComponent<Rigidbody2D>().isKinematic = true;
      platformpreview.GetComponent<Rigidbody2D>().velocity = new Vector2(0,platformpreview.transform.parent.GetComponent<mapadminscript>().rushspeed);
      platformpreview.layer = 8;
      Destroy(gameObject); // agregar particulas de explosion
      GameObject particlesInstance = Instantiate (bubbleParticles, transform.position, quaternion.identity);
      Destroy(particlesInstance, 1.0f);
    
    }
}
