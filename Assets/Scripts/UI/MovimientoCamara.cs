using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovimientoCamara : MonoBehaviour
{

    /*[SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;*/

    public Camera cam;
    public Transform target;
    public float distanceToTarget = 16.5f;
    public TextMeshProUGUI textoDebug;
    
    private Vector3 previousPosition;
    private float rotationAroundXAxis = 0f;
    private float rotationAroundYAxis = 0f;
    private float anguloCamara;


    // float minRotation = -5;
    // float maxRotation = 35;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            textoDebug.text = "Iniciando movimiento de Camara SWIPE";

        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;
                     
           
            rotationAroundYAxis = -direction.x * 180; // camera moves horizontally                  
            rotationAroundXAxis = direction.y * 180; // camera moves vertically

            
           

           
             
              textoDebug.text = "Angulo Camara "+newPosition;

                          
              
              
            
              cam.transform.position = target.position;

              cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);                                                             
              cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!
              cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
              previousPosition = newPosition;
            

            
          

           
            
           
        }

 
    }

        


  public static float ConvertToAngle180(float input)
     {       
         while (input > 360)
         {
             input = input - 360;
         } 
         while (input < -360)
         {
             input = input + 360;
         }
         if (input > 180)
         {
             input = input - 360;        
         }
         if (input < -180)
             input = 360+ input;
         return input;
     }
   

}
