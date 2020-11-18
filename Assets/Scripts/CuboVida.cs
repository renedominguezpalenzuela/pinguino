using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboVida : MonoBehaviour
{

    public int vidaRestante = 10;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disminuirVida(int totalADisminuir =1 ) {

        vidaRestante-=totalADisminuir;
        if (vidaRestante<=0) {
            Debug.Log("Explotando");
        }
    }
}
