﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInserter : MonoBehaviour
{
    public Player jugador;
    private Vector3 Posicion;
    
    //variables de control
    private bool puedeInsertar=false;
    private bool esGuerrero=false;
    private bool esJinete=false;
    private bool esArquero=false;
    
    private void Start() {
        GameObject.Find("AddGuerrero").GetComponentInChildren<Text>().text="Guerreros:"+0;
        GameObject.Find("AddJinete").GetComponentInChildren<Text>().text="Jinetes:"+0;
        GameObject.Find("AddArquero").GetComponentInChildren<Text>().text="Arqueros:"+0;
        
    }

    void Update()
    {
        if (puedeInsertar && Input.GetMouseButtonDown( 0 )){
            RaycastHit2D hit2D = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ) , Vector2.zero );
            if (hit2D.collider != null && hit2D.collider.tag == "Tiles"){
        		var tileSeleccionada = hit2D.collider.GetComponent<Transform>();
                this.Posicion = tileSeleccionada.position;
                Debug.Log(this.Posicion);
                if(this.esGuerrero){
                    var unidades = this.jugador.Guerreros;
                    if (unidades.Count > 0){
                        Instantiate(unidades[0],this.Posicion,Quaternion.identity);
                        unidades.RemoveAt(0);

                        GameObject.Find("AddGuerrero").GetComponentInChildren<Text>().text="Guerreros:"+unidades.Count;
                    }
                    this.esGuerrero=false;                  
                } else if(this.esJinete){
                    var unidades = this.jugador.Jinetes;
                    if (unidades.Count > 0){
                        Instantiate(unidades[0],this.Posicion,Quaternion.identity);
                        unidades.RemoveAt(0);
                        GameObject.Find("AddJinete").GetComponentInChildren<Text>().text="Jinetes:"+unidades.Count;
                    }
                    this.esJinete=false;
                }else {
                    var unidades = this.jugador.Arqueros;
                    if (unidades.Count > 0){
                        Instantiate(unidades[0],this.Posicion,Quaternion.identity);
                        unidades.RemoveAt(0);
                        GameObject.Find("AddArquero").GetComponentInChildren<Text>().text="Arqueros:"+unidades.Count;                        
                    }             
                    this.esArquero=false;
                }
                this.puedeInsertar=false;
            }
        }
    }

    public void ActualizarCant(){
        
    }

    public void InsertarGuerrero(){
        this.puedeInsertar=true;
        this.esGuerrero=true;
    }

    
    public void InsertarJinete(){
        this.puedeInsertar=true;
        this.esJinete=true;
    }
    
    public void InsertarArquero(){
        this.puedeInsertar=true;
        this.esArquero=true;
    }

}