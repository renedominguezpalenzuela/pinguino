using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CerrarApp : MonoBehaviour
{
    


    // Update is called once per frame
    public void Cerrar()
    {
        
               #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
        #else
           Application.Quit();
        #endif

    }
}
