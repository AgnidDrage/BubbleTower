using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float attackangle;
    public Rigidbody2D rbk;

    void Start()
    {
        
        
    }


    public void Throw(int inverter)
    {
        Debug.Log("Throwing");
        rbk.AddForce(new Vector2(inverter*8,4), ForceMode2D.Impulse);
        rbk.angularVelocity = -inverter*1000;
    
    }


    // Update is called once per frame
    void Update()
    {

    }
}
