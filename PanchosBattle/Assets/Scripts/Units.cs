using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
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
	bool SeRealizoUnaAccion;
	/*############################## Constructor ##############################*/
	public Units( TipoUnidad tipoUnidad , int playerNumero ) {
		perteneJugador=playerNumero;
		switch (tipoUnidad)
		{
			case TipoUnidad.Guerrero:
				vida=80;
				damage=20;
				multiplicadorDeDaño=3;
				movimiento=3;
				rangoAtaque=1;
				break;
			case TipoUnidad.Arquero:
				vida=60;
				damage=14;
				multiplicadorDeDaño=3;
				movimiento=3;
				rangoAtaque=3;
				break;
			case TipoUnidad.Jinete:
				vida=100;
				damage=20;
				multiplicadorDeDaño=3;
				movimiento=5;
				rangoAtaque=1;
				break;			
			default:
				Debug.Log("Error! No existe este tipo de unidad.");
				break;
		}
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
	/*############################## Metodos ##############################*/
	public void MostrarRangoDeMovimiento() {
		int rango = this.movimiento;
		int x = this.posX;
		int y = this.posY;
		Board tilemap = GetComponent<Board>();

		BoundsInt bounds = tilemap.cellBounds;
		for (int i = bounds.xMin; x < x+rango; x++){
			for (int j=bounds.yMin; y< y+rango; y++){
				if (tilemap.HasTile(new Vector3Int(i,j,0))){
					Tile mov_tile = tilemap[new Vector2Int(i,j)];
					mov_tile.color = Color.green;
				}
			}
		}
	}
	public void MostrarRangoDeAtaque(){

	}
}
public enum TipoUnidad {
	Jinete = 0,
	Arquero = 1,
	Guerrero = 2
}
