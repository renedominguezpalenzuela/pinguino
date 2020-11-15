using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    //Asignar este script al objeto que tiene el Character controller
    CharacterController controller;

    //Animator_Controller
    Animator animatorController ;


    public GameObject malla_pinguino; //Asignar la malla del pinguino

    //Ground Check
    public Transform groundCheck; //Asignar objeto creado en la base del player, por debajo de la capsula del Character controller
    bool isGrounded; //Variable para verificar si esta en el piso o no
    public float groundDistance = 0.1f; //radio de la esfera para chequear piso si es muy chica, se mueve solo ejemplo 0.02
                                        // si es muy grande se queda atascado en el borde de los cubos ejemplo 0.2
    public LayerMask groundMask; //Asignar layer del piso (previamente crear Layer y asignarle al piso)

    

//Salto
    
    //Gravedad
    Vector3 playerMovementVector;
    //float gravity = -10 * 9.81f * 2; //Gravedad
    [SerializeField]
    float gravity = - 9.81f; //Gravedad

    [SerializeField]
    float jumpHeight = 50f; //Altura minima para saltar un cubo

      [SerializeField]
     float directionalJump = 3f;


   

    int clickedAmount = 0; //Comprobar si simple o doble clic?

    //Camara
    public Camera cam;//Asignar camara

  

    void Start()    {
        controller = GetComponent<CharacterController>();       
        animatorController = malla_pinguino.GetComponent<Animator>();
    }


    void Update()    {
        //Chequeando si esta groundded (Crear Esfera centrada en objeto groundCheck y de radio groundDistance
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (isGrounded && playerMovementVector.y < 0)
        {
            playerMovementVector.y = 0f; //Sobra
            playerMovementVector = Vector3.zero;          
        }
     
        //Salto con barra espaciadora (Eliminar)
        if (Input.GetButtonDown("Jump") && isGrounded) {
            //playerMovementVector.y = jumpHeigth;  
             Saltar();      
        }

      
        //Compruebo si se hizo clic en la pantalla
        if (Input.GetMouseButtonDown(0))
        {
            clickedAmount += 1;
            MyMouseFunction();
            //Comprobar si el salto es muy alto
            //if (playerMovementVector.y >= jumpHeight) {playerMovementVector.y = jumpHeight;} 
        }

 
        //Gravedad
       // playerMovementVector.y += gravity* GravitySpeed * Time.deltaTime;
        playerMovementVector.y += gravity;// * Time.deltaTime;
        controller.Move (playerMovementVector*Time.deltaTime);    
        
//hacer que el pinguino siempre mire la camara
Vector3 back = cam.transform.position;
back.y = 0;
//transform.rotation = Quaternion.LookRotation(fwd);

        malla_pinguino.transform.rotation =  Quaternion.LookRotation(back);
        
    }



void Saltar() {
     animatorController.SetTrigger("Saltar");
     //playerMovementVector.y += Mathf.Sqrt(jumpHeight * -JumpSpeed * gravity); 
     //playerMovementVector.y += Mathf.Sqrt(jumpHeight * JumpSpeed); 
    // playerMovementVector.y += Mathf.Sqrt(jumpHeight*-gravity); 
     playerMovementVector.y += jumpHeight; 
}

 void MyMouseFunction()    {
        if (clickedAmount == 1)  {
            clickedAmount = 0;
        
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //Comprobar si el rayo intercepta con algun objeto
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))   {
                //Obtengo el objeto sobre el que hice clic
                var DatosObjeto = hitInfo.collider.GetComponent<Transform>();

                if (DatosObjeto != null) {
                    Vector3 newPos = Vector3.zero;


                    if (DatosObjeto.name == "piso") {
                         //Obtener punto exacto donde hizo clic
                        newPos =  hitInfo.point;
                        
                        //Vector direccion del salto hacia el cubo
                       


                        playerMovementVector.x = newPos.x- transform.position.x ;
                        playerMovementVector.z = newPos.z - transform.position.z ;

                      //  playerMovementVector = transform.TransformDirection(playerMovementVector);
                        playerMovementVector = playerMovementVector * directionalJump;

                        //Saltar hacia el cubo
                       // Debug.DrawLine(newPos, transform.position, Color.blue, 3f);

                        if (isGrounded) {Saltar();}  
                        
                    } 
                    else if (DatosObjeto.name == "Pinguino_Controllador") {
                         //Saltar en el lugar
                         if (isGrounded) {Saltar();}                        
                    } else  { //poner condicion que sea un cubo
                         //Obtener posicion del centro superior del cubo
                        Vector3 CentroSuperiorCubo =DatosObjeto.transform.position +  DatosObjeto.transform.up * DatosObjeto.transform.lossyScale.y / 2f;
                        newPos = CentroSuperiorCubo;
                        //Obtener punto exacto donde hizo clic
                        //newPos =  hitInfo.point;
                        
                        //Vector direccion del salto hacia el cubo
                        
                        playerMovementVector.x = newPos.x- transform.position.x ;
                        playerMovementVector.z = newPos.z - transform.position.z ;
                      //  playerMovementVector = transform.TransformDirection(playerMovementVector);
                        playerMovementVector = playerMovementVector * directionalJump;

                        //Saltar hacia el cubo
                       // Debug.DrawLine(newPos, transform.position, Color.blue, 3f);

                        if (isGrounded) {Saltar();}                        
                    }   

                     Debug.Log("Clic on " + DatosObjeto.name);        
                     Debug.Log("Clic on Pos " + newPos);
                }
            }
        }
    }
}
