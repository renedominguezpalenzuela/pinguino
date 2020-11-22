using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------
//  Variables globales del pinguino
//-------------------------------------------
[CreateAssetMenu(fileName="Pinguino", menuName="Scriptable Objects/Pinguino")]
public class PinguinoDefinicion : ScriptableObject 
{
    public int potenciaSalto = 1; //Fuerza que le imprime al bloque
    public bool isGrounded=false;  //Variable que indica si esta tocando el piso o no   
    public bool estaSaltando = false; //variable que indica si realizo un salto
    public bool estaSubiendo = false; //variable que indica si esta subiendo producto de un salto
    public  float AlturaSalto = 1f; //

  /* public  float AlturaMaximaRelativaPermitida = 1f;      
   public  float gravity = - 3.5f; // - Velocidad Gravedad
   public  float jumpSpeed =0.7f; // Velocidad de salto
   public  float directionalJump = 0.3f; //3*/
}
