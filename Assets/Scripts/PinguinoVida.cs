using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinguinoVida : MonoBehaviour
{

     public Transform CombrobarSiColisionConCubo; //Asignar objeto creado en la base del player, por debajo de la capsula del Character controller

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
