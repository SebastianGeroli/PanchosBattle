﻿using UnityEngine;

public class BoardController:MonoBehaviour {
	/*############################## Variables ##############################*/
	public Player[] jugadores;
	public Map mapa;
	BattleRoyale battleRoyale = new BattleRoyale();
	float tiempoRestante;
	int turnoPlayer, turnoGeneral;
	int nextRoyale;
	/*############################## Getters && Setters ##############################*/
	public int TurnoPlayer { get => turnoPlayer; set => turnoPlayer = value; }
	public int TurnoGeneral { get => turnoGeneral; set => turnoGeneral = value; }
	public float TiempoRestante { get => tiempoRestante; set => tiempoRestante = value; }
	public int NextRoyale { get => nextRoyale; set => nextRoyale = value; }

	/*############################## Metodos ##############################*/
	private void Update() {
		GetInfoOnClick();
	}
	private void Awake() {
		TiempoRestante = 60;
		TurnoGeneral = 0;
		TurnoPlayer = 3;
		NextRoyale = 10;
	}
	/*Le asinga su numero de player a cada jugador en el array*/
	public void SetPlayerNumero() {
		for(int x = 0; x < jugadores.Length;x++){
			jugadores[x].NumeroPlayer = x;
		}
	}
	/*Al acabarse el tiempo este vuelve a el tiempo original
	 * tambien cambia el turno de player para saber quien puede jugar
	 * y agrega un turno al general para saber si empieza el battleroyale */
	public void CambiarTurno() {
		TiempoRestante -= Time.deltaTime;
		if( TiempoRestante <= 0 ) {
			if( TurnoPlayer == 1 ) {
				TurnoPlayer = 2;
				TiempoRestante = 30;
				TurnoGeneral += 1;
			} else {
				turnoPlayer = 1;
				tiempoRestante = 30;
				TurnoGeneral += 1;
			}
		}
	}
	/*Recorre el listado de jugadores si alguno de ellos tiene sus unidades
	 * totales en 0 o menos se da por terminada la partida y ese jugador pierde*/
	public void GanoAlguien() {
		for(int x = 0; x<jugadores.Length;x++ ) {
			if(jugadores[x].UnidadesTotales <=0){
				/*Cargar La UI diciendo que el jugador perdio y el otro gano*/
			}
		}
	}
	/*Mostraria u ocultaria el mapa, en principio no es necesario por ahora*/
	public void MostrarMapa() {
		/*Por ahora es inncesario*/
	}
	/*Muestra la UI de compra durante el turno 0 luego de este empiezan a jugar
	 * y pone los datos necesarios para el proximo turno al vencerse el tiempo*/
	public void MostrarUICompra() {
		if( TurnoGeneral == 0 && TiempoRestante >= 0 ) {
			/*Enable UI Compra*/

		} else {
			/*Desabilitar UI Compra*/
			TurnoGeneral = 1;
			TiempoRestante = 30;
			turnoPlayer = 1;
		}
	}
	/*Al cumplirse los requisitos de battleroyale, 2 turnos antes se avisa
	 * que casillas se van a destruir y luego al llegar el turno de destruccion
	 * se destruyen */
	public void EsBattleRoyale() {
		if( NextRoyale - TurnoGeneral <= 2 ) {
			battleRoyale.MostrarIndicador( mapa.Tiles );
		} else if( NextRoyale - TurnoGeneral <= 2 ) {
			battleRoyale.DestruirTiles( mapa.Tiles );
			NextRoyale += 4;
		}

	}
	/* Esta tendria la logica de que va pasnado en cada click del juego*/
	public void CheackearAccionPlayer() {

	}
	/*Al terminar el turno de cada juego los colores se resetean para 
	 * que se vea todo de forma original*/
	public void ResetearColores() {
		for( int x = 0; x < mapa.Tiles.GetLength( 0 ); x++ ) {
			for( int y = 0; y < mapa.Tiles.GetLength( 1 ); y++ ) {
				mapa.Tiles[x , y].SpriteRenderer.color = Color.white;
			}
		}
	}
	/*Al terminar el turno se resetean las acciones de todas las unidades 
	 * para que se puedan mover nuevamente en el proximo turno */
	public void ResetearAccionesUnidades() {
		if( TurnoPlayer == 2 ) {
			for( int x = 0; x < jugadores[0].Guerreros.Count; x++ ) {
				jugadores[0].Guerreros[x].SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[0].Jinetes.Count; x++ ) {
				jugadores[0].Guerreros[x].SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[0].Arqueros.Count; x++ ) {
				jugadores[0].Guerreros[x].SeRealizoUnaAccion = false;
			}
		} else {
			for( int x = 0; x < jugadores[1].Guerreros.Count; x++ ) {
				jugadores[1].Guerreros[x].SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[1].Jinetes.Count; x++ ) {
				jugadores[1].Guerreros[x].SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[1].Arqueros.Count; x++ ) {
				jugadores[1].Guerreros[x].SeRealizoUnaAccion = false;
			}
		}
	}
	/*Con este metodo se obtiene la informacion del objeto sobre el cual 
	 * se esta haciendo click*/
	public void GetInfoOnClick() {
		if( Input.GetMouseButtonDown( 0 ) ) {
			RaycastHit2D hit2D = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ) , Vector2.zero );
			if( hit2D.collider != null ) {
				Debug.Log( "Se ha seleccionado a: " + hit2D.collider.gameObject.name + " que esta en la posicion: " + hit2D.collider.transform.position );
			}
			

		}
	}
}
