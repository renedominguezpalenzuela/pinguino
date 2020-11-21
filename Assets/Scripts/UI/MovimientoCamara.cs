using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float distanceToTarget = 16.5f;
    private Vector3 previousPosition;
    private float rotationAroundXAxis = 0f;
    private float rotationAroundYAxis = 0f;
    

    [SerializeField] private float MaxAnguloHaciaArriba = 30.0f;
    [SerializeField] private float MaxAnguloHaciaAbajo = 2.0f;

    
    private float xAxisClamp;

    private void Start() {
        xAxisClamp = 0;
        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
       //NOTA: en el primer clic la camara se ajusta hacia abajo provocando un salto molesto
       //Solucion: ubicar inicialmente la camara en la posicion ajustada hacia abajo
    }

    void Update()  {
        if (Input.GetMouseButtonDown(0))  {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            // textoDebug.text = "Iniciando movimiento de Camara SWIPE";
        }
        else if (Input.GetMouseButton(0) ) {

            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);

            
            Vector3 direction = previousPosition - newPosition;
            rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            rotationAroundXAxis = direction.y * 180; // camera moves vertically

            //Limitar moviviemtto
            
            xAxisClamp += rotationAroundXAxis;
            if (xAxisClamp >MaxAnguloHaciaArriba)   {
                xAxisClamp = MaxAnguloHaciaArriba;
                rotationAroundXAxis = 0.0f; //no rotar llego al limite             
                ClampXAxisRotationToValue(MaxAnguloHaciaArriba); //Valores absolutos
                Debug.Log("Hacia Arriba " + xAxisClamp+" "+rotationAroundXAxis);
            }
            else if (xAxisClamp <MaxAnguloHaciaAbajo)  {
                xAxisClamp = MaxAnguloHaciaAbajo;
                rotationAroundXAxis = 0.0f; //no rotar  llego al limite  
                ClampXAxisRotationToValue(MaxAnguloHaciaAbajo);      
                Debug.Log("Hacia Abajo " + xAxisClamp+" "+rotationAroundXAxis);
            }
            

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
            previousPosition = newPosition;
           
        }
    }

        private void ClampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = cam.transform.eulerAngles;
        eulerRotation.x = value;
        cam.transform.eulerAngles = eulerRotation;
    }

}
