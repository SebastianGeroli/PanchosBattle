using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyale{
	/*############################## Variables ##############################*/
	int contadorDeVeces = 0;
	/*############################## Getters&&Setters ##############################*/
	public int ContadorDeVeces { get => contadorDeVeces; set => contadorDeVeces = value; }
	/*############################## Metodos ##############################*/
	/*Este metodo recorre el array y pinta las casillas avisando cuales van a ser destruidas luego*/
	public void MostrarIndicador( Tile[,] tiles) {
		for( int x = contadorDeVeces; x < tiles.GetLength( 0 ); x++ ) {
			for( int y = contadorDeVeces; y < tiles.GetLength( 1 ); y++ ) {
				tiles[x , y].SpriteRenderer.color = Color.red;
			}
		}
		contadorDeVeces += 1;
		for( int x = contadorDeVeces; x < tiles.GetLength( 0 )-contadorDeVeces; x++ ) {
			for( int y = contadorDeVeces; y < tiles.GetLength( 1 )-contadorDeVeces; y++ ) {
				tiles[x , y].SpriteRenderer.color = Color.white;
			}

		}
	}
	/*Este metodo recorre el array y las tiles marcadas por color son destruidas*/
	public void DestruirTiles( Tile[,]tiles) {
		for(int x = 0;x<tiles.GetLength(0);x++){
			for( int y = 0; y < tiles.GetLength( 1 ); y++ ) {
				if( tiles[x , y].SpriteRenderer.color == Color.red ) {
					//tiles[x , y].SpriteRenderer.enabled = false;
					tiles[x , y].enabled = false;
				}
			}
		}
	}
}
