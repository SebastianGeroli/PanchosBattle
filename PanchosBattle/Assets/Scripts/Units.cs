using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units:MonoBehaviour {
	/*############################## Variables ##############################*/
	int vida, 
		damage, 
		multiplicadorDeDaño, 
		movimiento, 
		rangoAtaque, 
		posX, 
		posY, 
		perteneJugador;
	bool seRealizoUnaAccion;
	/*############################## Constructor ##############################*/
	public Units( TipoUnidad tipoUnidad , int playerNumero ) {

	}
	/*############################## Getters && Setters ##############################*/
	public int Vida { get => vida; set => vida = value; }
	public int Damage { get => damage; set => damage = value; }
	public int MultiplicadorDeDaño { get => multiplicadorDeDaño; set => multiplicadorDeDaño = value; }
	public int Movimiento { get => movimiento; set => movimiento = value; }
	public int RangoAtaque { get => rangoAtaque; set => rangoAtaque = value; }
	public int PosX { get => posX; set => posX = value; }
	public int PosY { get => posY; set => posY = value; }
	public int PerteneJugador { get => perteneJugador; set => perteneJugador =  value ; }
	public bool SeRealizoUnaAccion { get => seRealizoUnaAccion; set => seRealizoUnaAccion =  value ; }

	/*############################## Metodos ##############################*/
	public void MostrarRangoDeMovimiento() {

	}
	public void MostrarRanboDeAtaque(){

	}
}
public enum TipoUnidad {
	Jinete = 0,
	Arquero = 1,
	Guerrero = 2
}
