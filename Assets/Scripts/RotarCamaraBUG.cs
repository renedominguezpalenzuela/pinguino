using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RotarCamara : MonoBehaviour
{

    public Camera cam;
    public Transform target;

    public float distanceToTarget = 16.5f;
    public TextMeshProUGUI textoDebug;
    
    private Vector3 previousPosition;
    private float rotationAroundXAxis = 0f;
    private float rotationAroundYAxis = 0f;
    //private float anguloCamara;




     public float RotationSensitivity = 35.0f;
     public float minAngle = -45.0f;
     public float maxAngle = 45.0f;
     float yRotate=0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

            //Rotate Y view
            
         yRotate += Input.GetAxis ("Mouse Y") * RotationSensitivity * Time.deltaTime;
         yRotate = Mathf.Clamp (yRotate, minAngle, maxAngle);
         transform.eulerAngles = new Vector3 (yRotate, 0.0f, 0.0f);




              cam.transform.position = target.position;

              cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
              previousPosition = newPosition;
            



           

        }
        
    }
}
