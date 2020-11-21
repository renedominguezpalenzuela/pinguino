using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//------------------------------------------------------------------------------------------
// Vida y animacion del cubo, asignar al cubo
//------------------------------------------------------------------------------------------
public class CuboVida : MonoBehaviour
{

    //debe modificarse el prefab de cada cubo
    public int vidaRestante = 10;

    Animator anim;

  
    // Start is called before the first frame update
    void Start()
    {
           anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(" Vida del Cubo "+vidaRestante);
    }


    //llamado por el script PinguinoVida cuando colisiona con un bloque
    public void saltoRecibido(int potenciaDeSalto =1 ) {
        
        vidaRestante-=potenciaDeSalto;
        Debug.Log("Salto recibido. Vida del Cubo "+vidaRestante);
        
        if (vidaRestante<=0) {
           // Debug.Log("Explotando");
            anim.SetTrigger ("Explotar_Cubo");
            //ejecutar la animacion

            //Destruir el objeto


            //Destroy(gameObject);
        }
    }

    public void PrintEvent(string s) 
    {
        //Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
        Destroy(gameObject);
    }
}
