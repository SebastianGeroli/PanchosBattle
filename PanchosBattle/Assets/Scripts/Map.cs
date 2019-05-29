using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map:MonoBehaviour {
	/*############################## Variables ##############################*/
	int columnas, filas;
	[SerializeField]
	bool useRandom;
	private Tile[,] tiles;
	/*############################## Getters && Setters ##############################*/
	public int Columnas { get => columnas; set => columnas = value; }
	public int Filas { get => filas; set => filas =  value ; }
	public Tile[,] Tiles { get => tiles; set => tiles =  value ; }

	/*############################## Metodos ##############################*/
	public void Awake() {
		Tiles = new Tile[Columnas , Filas];
	}
	public void GeneradorMapa() {

	}
	public void GeneradorAguaObstaculos() {

	}
	public void GeneradorDeBuffos() {

	}
	
}
