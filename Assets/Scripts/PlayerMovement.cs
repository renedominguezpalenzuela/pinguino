﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;


//[RequireComponent(typeof(ARSessionOrigin))]
public class PlayerMovement : MonoBehaviour{

    //Introducir una demora en cada frame que simula error en dispositivos lentos
    [SerializeField] private bool activarDemora = false;
    private float AlturaMaximaRelativaPermitida = 1.0f;
    private float AlturaMaximaAbsolutaPermitida = 0.0f;

    //Variables globales del pinguino
    public PinguinoDefinicion pinguino_datos;

    //Asignar este script al objeto que tiene el Character controller
    CharacterController controller;

    //Animator_Controller
    Animator animatorController ;

    public GameObject malla_pinguino; //Asignar la malla del pinguino

    public TextMeshProUGUI textoDebug;
    public TextMeshProUGUI textoDebug1;
    public TextMeshProUGUI textoFrames;

    //Ground Check
    
    bool isGrounded; //Variable para verificar si esta en el piso o no
    public GameObject Pinguino_Controller; 

    public float groundDistance = 0.1f; //radio de la esfera para chequear piso si es muy chica, se mueve solo ejemplo 0.02
                                        // si es muy grande se queda atascado en el borde de los cubos ejemplo 0.2
    public LayerMask groundMask; //Asignar layer del piso (previamente crear Layer y asignarle al piso)
    public Transform groundCheck; //Asignar objeto creado en la base del player, por debajo de la capsula del Character controller
    

   
//Salto
    
    //Gravedad
    Vector3 playerMovementVector;
    
     float gravity = - 9.81f; // - 3 Gravedad
     float jumpHeight =2f; //NO USado 50 Altura minima para saltar un cubo
     float directionalJump = 0.5f; //3

    int clickedAmount = 0; //Comprobar si simple o doble clic?

    //Camara
    public Camera cam;//Asignar camara 

    int totalSaltos = 0;
    float maxY = 0;
    float maxYPinguino = 0;

    int frame = 0;

    void Start()    {
        controller = GetComponent<CharacterController>();       
        animatorController = malla_pinguino.GetComponent<Animator>(); 
        //Inicializando vector
         playerMovementVector = Vector3.zero;      
    }

    void Update()    {

        frame++;
        //Chequeando si esta groundded (Crear Esfera centrada en objeto groundCheck y de radio groundDistance
       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       // isGrounded =  isGroundedScriptObject.GetComponent<IsGroundedControlador>().isGrounded;
       //isGrounded = controller.isGrounded; 
       pinguino_datos.isGrounded = isGrounded;

        if (isGrounded && playerMovementVector.y < 0) {
             playerMovementVector = Vector3.zero;          
        }
     
        //Salto con barra espaciadora (Eliminar)
        if (Input.GetButtonDown("Jump") && isGrounded) { 
             Saltar();      
        }

      
        //Compruebo si se hizo clic en la pantalla
        if (Input.GetMouseButtonDown(0))  {
            clickedAmount += 1;
            MyMouseFunction();  
            //Debug.Log("Clic en pantalla");
        }


        if (Pinguino_Controller.transform.position.y>AlturaMaximaAbsolutaPermitida ) { 
            pinguino_datos.estaSubiendo = false;
        }   
         // playerMovementVector = Vector3.Normalize(playerMovementVector);   
        if (pinguino_datos.estaSubiendo) {
           // Vector3 incremento = new Vector3(0,1,0);
           // Pinguino_Controller.transform.Translate(incremento);   
                                      
           Pinguino_Controller.transform.Translate(playerMovementVector);
        } else {        
           playerMovementVector.y += gravity;
           controller.Move (playerMovementVector*Time.deltaTime);    
        }

       // controller.transform.Translate(playerMovementVector*Time.deltaTime);
        
        
        
        //hacer que el pinguino siempre mire la camara
        Vector3 back = cam.transform.position;
        back.y = 0;
        malla_pinguino.transform.rotation =  Quaternion.LookRotation(back);   

        //Mostrar Altura maxima
        if (playerMovementVector.y>maxY)   {
            maxY = playerMovementVector.y;
        }

        //Mostrar Altura maxima
        if (Pinguino_Controller.transform.position.y >=maxYPinguino) {
            maxYPinguino = Pinguino_Controller.transform.position.y;
        }


        textoDebug1.text ="MaxY Vector: "+maxY+ 
                          "\nMaxY Pinguino: "+ maxYPinguino + 
                          "\nVector: "+playerMovementVector+
                          "\nPos Y Pinguino: "+Pinguino_Controller.transform.position.y+
                          "\n PosY Max Permitida: "+AlturaMaximaAbsolutaPermitida; 


        textoFrames.text = "Frame: "+frame + " Saltando "+pinguino_datos.estaSubiendo;

        //Demora
        if (activarDemora) {
            int a  = 0;
            for (int i = 0; i<=100000000; i++){
            a = a^2; 
            }
        }

    }



public void Saltar() {
    //Debug.Log("Is Grounded "+isGrounded);
    if (!isGrounded) return;
    pinguino_datos.estaSaltando = true;
    pinguino_datos.estaSubiendo = true;
     animatorController.SetTrigger("Saltar");
     playerMovementVector.y = jumpHeight;
    // playerMovementVector = Vector3.Normalize(playerMovementVector);
  


     AlturaMaximaAbsolutaPermitida = Pinguino_Controller.transform.position.y+AlturaMaximaRelativaPermitida; 
 
     totalSaltos++;
     //Debug.Log("Saltando " + totalSaltos);
      textoDebug.text = "Salto No: "+totalSaltos + " Vector: "+playerMovementVector;
      maxY = 0;
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

                        playerMovementVector = playerMovementVector * directionalJump;

                        //Saltar hacia el cubo
                       // Debug.DrawLine(newPos, transform.position, Color.blue, 3f);

                        if (isGrounded) {Saltar();}  
                        
                    }  else if (DatosObjeto.name == "Pinguino_Controllador") { //si hace clic en el pinguino
                         //Saltar en el lugar
                         if (isGrounded) {Saltar();}                        
                    }  else  { //poner condicion que sea un cubo
                         //Obtener posicion del centro superior del cubo
                        Vector3 CentroSuperiorCubo =DatosObjeto.transform.position +  DatosObjeto.transform.up * DatosObjeto.transform.lossyScale.y / 2f;
                        newPos = CentroSuperiorCubo;
                        //Obtener punto exacto donde hizo clic
                                               
                        //Vector direccion del salto hacia el cubo
                        
                        playerMovementVector.x = newPos.x- transform.position.x ;
                        playerMovementVector.z = newPos.z - transform.position.z ;
                  
                       playerMovementVector = playerMovementVector * directionalJump;

                        //Saltar hacia el cubo
                       // Debug.DrawLine(newPos, transform.position, Color.blue, 3f);

                       if (isGrounded) {Saltar();}                        
                    }   

                    
                   
                    // Debug.Log("Clic on " + DatosObjeto.name);        
                    // Debug.Log("Clic on Pos " + newPos);
                }
            }
        }
    }
}
