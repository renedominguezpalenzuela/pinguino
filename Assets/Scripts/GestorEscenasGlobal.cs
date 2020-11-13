using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;


public class GestorEscenasGlobal : MonoBehaviour
{
    public static GestorEscenasGlobal Instance{ set; get;}
    //public string NombreEscena { get => nombreEscena; set => nombreEscena = value; }

    //private string nombreEscena;


    //----------------------------------------------------------------- 
    //  Cargar escenas iniciales, splash, menu, etc…
    //----------------------------------------------------------------- 

    private void Awake(){
        //Instance = this;
          
          if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        Load("00_Splash");
        // GestorEscenasGlobal.Instance.Load ("SampleScene");
    }


    //----------------------------------------------------------
    // Load a scene (Additive)
    //----------------------------------------------------------
    // Parameters:
    // sceneName -- Scene Name
    public void Load(string sceneName){
        if (!SceneManager.GetSceneByName (sceneName).isLoaded) {
            SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
        }        
    }

    public void Unload(string sceneName){
        if (SceneManager.GetSceneByName(sceneName).isLoaded) {         
            SceneManager.UnloadSceneAsync (sceneName);
        }
    }



}
