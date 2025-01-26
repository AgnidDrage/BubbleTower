using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillBubbleBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitcolliders = Physics2D.OverlapBoxAll(transform.position,new Vector2(17.8f, 0.7f),1<<3);
        foreach (var hitcollider in hitcolliders){
          if (hitcollider.gameObject.GetComponent<spawninnercontent>()){
            Destroy(hitcollider.gameObject);
          }
        }
    }
}
