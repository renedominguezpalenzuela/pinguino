using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltosSobreCubos : MonoBehaviour {

    //Variables globales del pinguino
    public PinguinoDefinicion pinguino;

    // Start is called before the first frame update
    void Start() {
       pinguino.estaSaltando = false; 
    }

    // Update is called once per frame
    void Update() {
       
    }

void OnTriggerEnter(Collider other) {
       // Debug.Log("ColisionCCC "+other.gameObject.name+" "+other.gameObject.tag);
       //
           // Debug.Log("ColisionZZZ "+other.gameObject.name + " "+other.gameObject.tag); 
                if  (other.gameObject.CompareTag("CUBO")) {
                    if (pinguino.estaSaltando) {
                        //Debug.Log("HERE");
                        other.gameObject.GetComponent<CuboVida>().saltoRecibido(2);
                        pinguino.estaSaltando = false;         
                    }
                }
                    
         //   }    
       // Destroy(other.gameObject);
        
    }


/*    void OnCollisionEnter(Collision collision) {

           Debug.Log("OnColisionEnter" + collision.collider.name + " "+collision.collider.tag);

      }*/

    /* void OnControllerColliderHit(ControllerColliderHit hit) {
      
        if (pinguino.estaSaltando) {
            Debug.Log("ColisionZZZ "+hit.gameObject.name + " "+hit.gameObject.tag); 
                if  (hit.gameObject.tag =="CUBO") {
                hit.gameObject.GetComponent<CuboVida>().saltoRecibido(2);
                }
                    pinguino.estaSaltando = false;         
            }       
     }*/
}
/*
void OnTriggerEnter(Collider other) {
        Debug.Log("Colision" + other.name + " "+other.tag);
        //If (gameObject.tag == "VALOR") { . . . }
        //Destroy(other.gameObject);
        
    }*/
/*
      void OnCollisionEnter(Collision collision) {

           Debug.Log("OnColisionEnter" + collision.collider.name + " "+collision.collider.tag);

      }*/
/*
       void OnCollisionStay(Collision collision) {
           Debug.Log("OnCollisionStay" + collision.collider.name + " "+collision.collider.tag);
       }*/


/*
    void OnCollisionEnter(Collision collision)
    {
         Debug.Log("Choqeue");
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
           
        }
    }*/

