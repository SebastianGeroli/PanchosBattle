using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map:MonoBehaviour {
	/*############################## Variables ##############################*/
	int columnas, filas;
	[SerializeField]
	bool useRandom;
	private GameObject[,] tiles;
	private int counter;
	private Vector2 posicionAnterior;
	/*############################## Getters && Setters ##############################*/
	public int Columnas { get => columnas; set => columnas = value; }
	public int Filas { get => filas; set => filas = value; }
	public GameObject[,] Tiles { get => tiles; set => tiles = value; }

	/*############################## Metodos ##############################*/
	public void Awake() {
		columnas = 20;
		filas = 11;
		Tiles = new GameObject[Columnas , Filas];
	}
	public void GeneradorMapa() {
		for( int x = 0; x < columnas; x++ ) {
			for( int y = 0; y < filas; y++ ) {
				tiles[x , y] = new GameObject();
				tiles[x , y].name = "Tile" + counter;
				tiles[x , y].tag = "Tiles";
				tiles[x , y].transform.position = new Vector2( x , y );
				tiles[x , y].AddComponent<Tile2>();
				tiles[x , y].GetComponent<Tile2>().PosX = (int)tiles[x , y].transform.position.x;
				tiles[x , y].GetComponent<Tile2>().PosY = (int)tiles[x , y].transform.position.y;
				tiles[x , y].GetComponent<Tile2>().TipoDeSuelo = TipoDeSuelo.Caminable;
				tiles[x , y].GetComponent<Tile2>().SetSpriteRenderer();
				tiles[x , y].GetComponent<Tile2>().SetSprite( tiles[x , y].GetComponent<Tile2>().TipoDeSuelo );
				tiles[x , y].AddComponent<BoxCollider2D>();
				//Debug.Log( tiles[x , y].name + "Posicion en array: " + x + " " + y );
				counter++;
			}
		}
		//AcomodarTiles();
	}
	public void SpawnTiles() {
		for( int x = 0; x < 2; x++ ) {
			for( int y = 0; y < filas; y++ ) {
				tiles[x , y].GetComponent<Tile2>().SpriteRender2.color = Color.green;
			}
		}
		for( int x = tiles.Length-2; x < tiles.Length; x++ ) {
			for( int y = 0; y < filas; y++ ) {
				tiles[x , y].GetComponent<Tile2>().SpriteRender2.color = Color.green;
			}
		}
	}
	public void GeneradorAguaObstaculos() {

	}
	public void GeneradorDeBuffos() {

	}

}
