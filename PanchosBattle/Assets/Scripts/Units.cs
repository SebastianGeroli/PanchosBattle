using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Units:MonoBehaviour {
	/*############################## Variables ##############################*/
	int
		posX,
		posY, 
		multiplicadorDeDaño, 
		damage, 
		perteneJugador, 
		rangoAtaque, 
		vida, 
		movimiento;
	bool seRealizoUnaAccion;
	bool estaEnTablero = false;
	public TipoUnidad tipounidad;
	/*############################## Constructor ##############################*/
	public void UnitsInicial( TipoUnidad tipoUnidad , int playerNumero ) {
		perteneJugador = playerNumero;
		switch( tipoUnidad ) {
			case TipoUnidad.Guerrero:
				tipounidad = TipoUnidad.Guerrero;
				vida = 80;
				damage = 20;
				multiplicadorDeDaño = 3;
				movimiento = 3;
				rangoAtaque = 1;
				break;
			case TipoUnidad.Arquero:
				tipounidad = TipoUnidad.Arquero;
				vida = 60;
				damage = 14;
				multiplicadorDeDaño = 3;
				movimiento = 3;
				rangoAtaque = 3;
				break;
			case TipoUnidad.Jinete:
				tipounidad = TipoUnidad.Jinete;
				vida = 100;
				damage = 20;
				multiplicadorDeDaño = 3;
				movimiento = 5;
				rangoAtaque = 1;
				break;
			default:
				Debug.Log( "Error! No existe este tipo de unidad." );
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
	public int PerteneJugador { get => perteneJugador; set => perteneJugador = value; }
	public bool SeRealizoUnaAccion { get => seRealizoUnaAccion; set => seRealizoUnaAccion = value; }
	public bool EstaEnTablero { get => estaEnTablero; set => estaEnTablero =  value ; }

	/*############################## Metodos ##############################*/
	public void MostrarRangoDeMovimiento() {

	}

	private bool FindExtra( Vector2Int[] casos_extra , Vector2Int vector ) {
		for( int i = 0; i < casos_extra.Length; i++ ) {
			if( casos_extra[i] == vector ) {
				return true;
			}
		}
		return false;
	}

	public void MostrarRangoDeAtaque() {
		int rango = this.rangoAtaque;
		int x = this.posX;
		int y = this.posY;

		Board board = GameObject.FindWithTag( "Board" ).GetComponent<Board>();

		Tile mov_tile = board[new Vector2Int( x , y )];
		var tile_color = mov_tile.transform.Find( "Outline/Fill" ).GetComponent<SpriteRenderer>();
		tile_color.color = Color.green;

		//estos son los casos que no son parte del perimetro del hexagono principal 
		Vector2Int[] extra_par = new Vector2Int[3];
		extra_par[0] = new Vector2Int( x , y );
		extra_par[1] = new Vector2Int( x + 1 , y + 1 );
		extra_par[2] = new Vector2Int( x + 1 , y - 1 );

		Vector2Int[] extra_impar = new Vector2Int[3];
		extra_impar[0] = new Vector2Int( x - 1 , y - 1 );
		extra_impar[1] = new Vector2Int( x - 1 , y + 1 );
		extra_impar[2] = new Vector2Int( x , y );

		Vector2Int pos_tile;
		List<Tile> clickable_tiles = new List<Tile>();

		Vector2Int[] comparador = ( y % 2 == 0 ) ? extra_par : extra_impar;
		for( int i = -1; i <= 1; i++ ) {
			for( int j = -1; j <= 1; j++ ) {
				pos_tile = new Vector2Int( x + i , y + j );
				if( board.HasTile( pos_tile ) & !( ( FindExtra( comparador , pos_tile ) ) ) ) {
					Tile click_tile = board[new Vector2Int( x + i , y + j )];
					tile_color = click_tile.transform.Find( "Outline/Fill" ).GetComponent<SpriteRenderer>();
					clickable_tiles.Add( click_tile );
					tile_color.color = Color.red;
				}
			}
		}
	}

}
public enum TipoUnidad {
	Jinete = 0,
	Arquero = 1,
	Guerrero = 2
}
