using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//------------------------------------------------------------------------
//  Comprobar si pinguino esta tocando el piso
//  Setea variable global
//------------------------------------------------------------------------
public class IsGroundedControlador : MonoBehaviour {

   
    public PinguinoDefinicion pinguino;

    public float groundDistance = 0.1f; //radio de la esfera para chequear piso si es muy chica, se mueve solo ejemplo 0.02
                                        // si es muy grande se queda atascado en el borde de los cubos ejemplo 0.2
    public LayerMask groundMask; //Asignar layer del piso (previamente crear Layer y asignarle al piso)
    public Transform groundCheck; //Asignar objeto creado en la base del player, por debajo de la capsula del Character controller

    
    // Update is called once per frame
    void Update() {
        //Compruebo si punto debajo del pinguino (groundCheck.position)  
        //esta a una distancia menor que (groundDistance)
        //del objeto del layer (groundMask)
        pinguino.isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
