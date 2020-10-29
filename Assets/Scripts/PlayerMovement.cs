using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Asignar este script al objeto que tiene el Character controller
    CharacterController controller;
    public GameObject malla_pinguino; //Asignar la malla del pinguino

    //Ground Check
    public Transform groundCheck; //Asignar objeto creado en la base del player
    bool isGrounded; //Variable para verificar si esta en el piso o no
     float groundDistance = 0.2f; //radio de la esfera para chequear piso
    public LayerMask groundMask; //Asignar layer del piso (previamente crear Layer y asignarle al piso)

    //Salto
    float jumpHeight = 1f; //Altura minima para saltar un cubo

    //Gravedad
    Vector3 playerDirectionVector;
    float gravity = -10 * 9.81f;
    float JumpSpeed = 3f;

    int clickedAmount = 0; //Comprobar si simple o doble clic?

    //Camara
    public Camera cam;//Asignar camara

    void Start()
    {
        controller = GetComponent<CharacterController>();       
    }


    void Update()
    {
        //Chequeando si esta groundded (Crear Esfera centrada en objeto groundCheck y de radio groundDistance
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && playerDirectionVector.y < 0)
        {
            playerDirectionVector.y = 0f;
            playerDirectionVector = Vector3.zero;          
        }

/*      movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Coordeandas globales
        //Vector3 move = new Vector3(x,0f, z);
        //Coordeandas locales del player (se mueve hacia alante hacia la direccion que apundte)
        Vector3 move = transform.right * x + transform.forward * z;

        //Realizar el movinineto
        controller.Move(move * speed * Time.deltaTime);
        */
     
        //Salto
        if (Input.GetButtonDown("Jump") && isGrounded) {
            //playerDirectionVector.y = jumpHeigth;  
             Saltar();      
        }

      
        //Compruebo si se hizo clic en la pantalla
        if (Input.GetMouseButtonDown(0))
        {
            clickedAmount += 1;
            MyMouseFunction();
            //Comprobar si el salto es muy alto
            //if (playerDirectionVector.y >= jumpHeight) {playerDirectionVector.y = jumpHeight;} 
        }

 
        //Gravedad
        playerDirectionVector.y += gravity * Time.deltaTime;
        controller.Move (playerDirectionVector*Time.deltaTime);    

        
    }



void Saltar() {

     playerDirectionVector.y += Mathf.Sqrt(jumpHeight * -JumpSpeed * gravity); 
}

     void MyMouseFunction()
    {
        if (clickedAmount == 1)
        {
            clickedAmount = 0;

            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //Comprobar si el rayo intercepta con algun objeto
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                //Obtengo el objeto sobre el que hice clic
                var DatosObjeto = hitInfo.collider.GetComponent<Transform>();

                if (DatosObjeto != null) {
                    Vector3 newPos = Vector3.zero;


                    if (DatosObjeto.name == "piso") {
                         //Obtener punto exacto donde hizo clic
                        newPos =  hitInfo.point;
                        
                        //Vector direccion del salto hacia el cubo
                        float delta = 2f;
                        playerDirectionVector.x = newPos.x- transform.position.x ;
                        playerDirectionVector.z = newPos.z - transform.position.z ;
                        playerDirectionVector = playerDirectionVector * delta;

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
                        float delta = 2f;
                        playerDirectionVector.x = newPos.x- transform.position.x ;
                        playerDirectionVector.z = newPos.z - transform.position.z ;
                        playerDirectionVector = playerDirectionVector * delta;

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
