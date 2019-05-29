using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(WaitMap());
        CrearUnit();
    }

    IEnumerator WaitMap(){
        yield return new WaitForSeconds(2);
    }

    void CrearUnit(){
        Units caballero = new Units(TipoUnidad.Guerrero,1);
        caballero.PosX=0;
        caballero.PosY=0;
        caballero.MostrarRangoDeAtaque();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
