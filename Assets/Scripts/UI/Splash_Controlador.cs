using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Splash_Controlador : MonoBehaviour
{

   

    // Use this for initialization
    IEnumerator  Start()
    {


         yield return new WaitForSeconds (2.5f);    
        GestorEscenasGlobal.Instance.Unload ("00_Splash");

         GestorEscenasGlobal.Instance.Load("SampleScene");
             
    }

    
}
