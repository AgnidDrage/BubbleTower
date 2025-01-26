using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killocean : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
        void Update()
    {
        // Collider2D[] hitcolliders = Physics2D.OverlapBoxAll(transform.position,new Vector2(17.8f, 0.7f),8);
        // foreach (var hitcollider in hitcolliders){
        //   if (hitcollider.gameObject.GetComponent<PlataformBehaviour>()){
        //     Destroy(hitcollider.gameObject);
        //   }

        //   if(hitcollider.gameObject.GetComponent<PlayerMovement>()){

        //     hitcollider.gameObject.GetComponent<PlayerMovement>().rip();

        //   }
        // }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()){
            other.gameObject.GetComponent<PlayerMovement>().rip();
        }
        else if (other.gameObject.GetComponent<spawninnercontent>()){
            Destroy(other.gameObject);
        }
    }
}
