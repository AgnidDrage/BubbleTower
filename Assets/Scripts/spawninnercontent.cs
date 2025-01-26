using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawninnercontent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] arrayofobjects;
    GameObject prefabtargetobject; // List of possible platforms
    GameObject platformpreview;
    
        void Start()
    {
        prefabtargetobject= arrayofobjects[Random.Range(0, arrayofobjects.Length)];
        platformpreview = Instantiate(prefabtargetobject,transform.position,Quaternion.identity);
        platformpreview.transform.parent = transform;
        platformpreview.transform.localScale = 0.5f*Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitcolliders = Physics2D.OverlapCircleAll(transform.position,0.2f,1<<3);
        foreach (var hitcollider in hitcolliders){
          if (hitcollider.gameObject.GetComponent<ProjectileBehaviour>()){
            isPopped();
          }
        }
    }

    void isPopped(){
      platformpreview.transform.localScale = Vector3.one;
      platformpreview.transform.parent = transform.parent.transform;
      platformpreview.AddComponent<BoxCollider2D>();
      platformpreview.AddComponent<Rigidbody2D>();
      platformpreview.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-transform.parent.GetComponent<mapadminscript>().rushspeed);
      platformpreview.layer = 0;
      Destroy(gameObject); // agregar particulas de explosion
    
    }
}
