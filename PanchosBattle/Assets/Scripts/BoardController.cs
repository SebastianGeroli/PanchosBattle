using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController:MonoBehaviour {
	/*############################## Variables ##############################*/
	public Canvas canvasPlayer1, canvasPlayer2;
	public Text textTiempo, textTurnoPlayer, textTurnoGeneral, textTurnoPlayer2;
	public Board board;
	public Player[] jugadores;
	BattleRoyale battleRoyale = new BattleRoyale();
	float tiempoRestante;
	int turnoPlayer, turnoGeneral;
	int nextRoyale;
	bool alreadyDone = false;
	[SerializeField]
	private Units unidadDestino;
	[SerializeField]
	private Units unidadSeleccionada;
	[SerializeField]
	public Tile tileSeleccionada;

	/*############################## Getters && Setters ##############################*/
	public int TurnoPlayer { get => turnoPlayer; set => turnoPlayer = value; }
	public int TurnoGeneral { get => turnoGeneral; set => turnoGeneral = value; }
	public float TiempoRestante { get => tiempoRestante; set => tiempoRestante = value; }
	public int NextRoyale { get => nextRoyale; set => nextRoyale = value; }
	public Units UnidadSeleccionada { get => unidadSeleccionada; set => unidadSeleccionada = value; }
	public Units UnidadDestino { get => unidadDestino; set => unidadDestino = value; }

	/*############################## Metodos ##############################*/
	private void Update() {
		CheckActions();
		ArrayTiles( board );
		TimeControl();
		MostrarUICompra();
		CambiarTurno();
	}
	private void Awake() {
		SetPlayers();
		TiempoRestante = 60;
		TurnoGeneral = 0;
		TurnoPlayer = 1;
		NextRoyale = 10;
	}
	/*copiar direccion de una a otra*/
	public void SetearPosUnidad() {
		if( unidadSeleccionada != null && tileSeleccionada != null ) {
			Debug.Log( "entree" );
			unidadSeleccionada.transform.position = tileSeleccionada.transform.position;
			unidadSeleccionada = null;
			tileSeleccionada = null;
		}
	}
	/*Le asinga su numero de player a cada jugador en el array*/
	public void SetPlayers() {
		for( int x = 0; x < jugadores.Length; x++ ) {
			jugadores[x].NumeroPlayer = x+1;
			jugadores[x].Dinero = 2000;
		}
	}
	/*Tiempo*/
	public void TimeControl() {
		TiempoRestante -= Time.deltaTime;
		textTiempo.text = Math.Truncate( tiempoRestante ).ToString();
	}
	/*Al acabarse el tiempo este vuelve a el tiempo original
	 * tambien cambia el turno de player para saber quien puede jugar
	 * y agrega un turno al general para saber si empieza el battleroyale */
	public void CambiarTurno() {

		if( TiempoRestante <= 0 ) {
			if( TurnoPlayer == 1 ) {
				textTurnoPlayer.enabled = false;
				textTurnoPlayer2.enabled = true;
				TurnoPlayer = 2;
				TiempoRestante = 30;
				TurnoGeneral += 1;
				Debug.Log("Why isnt it changing?: "+turnoPlayer);
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
			} else {
				textTurnoPlayer.enabled = true;
				textTurnoPlayer2.enabled = false;
				turnoPlayer = 1;
				tiempoRestante = 30;
				TurnoGeneral += 1;
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
			}
		}
	}
	/*Recorre el listado de jugadores si alguno de ellos tiene sus unidades
	 * totales en 0 o menos se da por terminada la partida y ese jugador pierde*/
	public void GanoAlguien() {
		for( int x = 0; x < jugadores.Length; x++ ) {
			if( jugadores[x].UnidadesTotales <= 0 ) {
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
			if( TiempoRestante > 30 ) {
				canvasPlayer1.enabled = true;
				canvasPlayer2.enabled = false;
				textTurnoPlayer.enabled = true;
				textTurnoPlayer2.enabled = false;
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
			} else {
				canvasPlayer1.enabled = false;
				canvasPlayer2.enabled = true;
				textTurnoPlayer.enabled = false;
				textTurnoPlayer2.enabled = true;
				turnoPlayer=2;
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
			}
		} else if( turnoGeneral < 1 ) {
			canvasPlayer1.enabled = false;
			canvasPlayer2.enabled = false;
			/*Desabilitar UI Compra*/
		}
	}
	/*Al cumplirse los requisitos de battleroyale, 2 turnos antes se avisa
	 * que casillas se van a destruir y luego al llegar el turno de destruccion
	 * se destruyen */
	public void EsBattleRoyale() {
		if( NextRoyale - TurnoGeneral <= 2 ) {
			battleRoyale.MostrarIndicador();
		} else if( NextRoyale - TurnoGeneral <= 2 ) {
			battleRoyale.DestruirTiles();
			NextRoyale += 4;
		}

	}
	/*Al terminar el turno de cada juego los colores se resetean para 
	 * que se vea todo de forma original*/
	public void ResetearColores() {
	}
	/*Al terminar el turno se resetean las acciones de todas las unidades 
	 * para que se puedan mover nuevamente en el proximo turno */
	public void ResetearAccionesUnidades() {
		if( TurnoPlayer == 2 ) {
			for( int x = 0; x < jugadores[0].Guerreros.Count; x++ ) {
				jugadores[0].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[0].Jinetes.Count; x++ ) {
				jugadores[0].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[0].Arqueros.Count; x++ ) {
				jugadores[0].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
		} else {
			for( int x = 0; x < jugadores[1].Guerreros.Count; x++ ) {
				jugadores[1].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[1].Jinetes.Count; x++ ) {
				jugadores[1].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[1].Arqueros.Count; x++ ) {
				jugadores[1].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
		}
	}
	/*Con este metodo se obtiene la informacion del objeto sobre el cual 
	 * se esta haciendo click*/
	public void CheckActions() {
		if( Input.GetMouseButtonDown( 0 ) ) {
			RaycastHit2D hit2D = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ) , Vector2.zero );
			if( hit2D.collider != null && hit2D.collider.tag == "Units" ) {
				SeleccionarUnidad( hit2D );
				Ataque();
				MoverUnidad();

				//Debug.Log( "Se ha seleccionado a: " + hit2D.collider.gameObject.name + " que esta en la posicion: " + hit2D.collider.transform.position );
			}
			if( hit2D.collider != null && hit2D.collider.tag == "Tiles" ) {
				SeleccionarTile( hit2D );
				MoverUnidad();
				//Debug.Log( "Se ha seleccionado a: " + hit2D.collider.gameObject.name + " que esta en la posicion: " + hit2D.collider.transform.position );
			}

		}
	}
	/*Verifia si la unidad a llegado a 0 de vida si es asi la destruye*/
	public void CheckearVidaUnidad() {
		if( unidadDestino.Vida <= 90 ) {
			unidadDestino.gameObject.SetActive( false );
			if( jugadores[0].NumeroPlayer == unidadDestino.PerteneJugador ) {
				jugadores[0].UnidadesTotales -= 1;
			} else {
				jugadores[1].UnidadesTotales -= 1;
			}
			//Debug.Log( "Entre Enabled" );
		}
	}
	/*Convert Dictionary from board to Array */
	public void ArrayTiles( Board board1 ) {
		if( !alreadyDone ) {
			foreach( KeyValuePair<Vector2Int , Tile> entry in board1.TilesDictionary ) {
				//Debug.Log( "Estoy en " + entry.Value.name );
				entry.Value.tag = "Tiles";
			}
		}
		alreadyDone = true;
	}
	/*Ataque*/
	public void Ataque() {
		if( unidadSeleccionada != null && unidadDestino != null && unidadSeleccionada.SeRealizoUnaAccion == false ) {
			if( CheckDistance( unidadDestino.transform.position.x , unidadSeleccionada.transform.position.x , unidadSeleccionada.RangoAtaque ) ) {
				if( CheckDistance( unidadDestino.transform.position.y , unidadSeleccionada.transform.position.y , unidadSeleccionada.RangoAtaque ) ) {
					if( unidadSeleccionada.tipounidad == TipoUnidad.Guerrero && unidadDestino.tipounidad == TipoUnidad.Jinete ) {
						unidadDestino.Vida -= unidadSeleccionada.Damage * unidadSeleccionada.MultiplicadorDeDaño;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						//	Debug.Log( "Ataque x 3 " );
					} else if( unidadSeleccionada.tipounidad == TipoUnidad.Arquero && unidadDestino.tipounidad == TipoUnidad.Guerrero ) {
						unidadDestino.Vida -= unidadSeleccionada.Damage * unidadSeleccionada.MultiplicadorDeDaño;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						//	Debug.Log( "Ataque x 3 " );
					} else if( unidadSeleccionada.tipounidad == TipoUnidad.Jinete && unidadDestino.tipounidad == TipoUnidad.Arquero ) {
						unidadDestino.Vida -= unidadSeleccionada.Damage * unidadSeleccionada.MultiplicadorDeDaño;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						//	Debug.Log( "Ataque x 3" );
					} else {
						unidadDestino.Vida -= unidadSeleccionada.Damage;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						///	Debug.Log( "Ataque" );
					}
				}
			}
			CheckearVidaUnidad();
		}

	}
	/*Mover La unidad*/
	public void MoverUnidad() {
		if( unidadSeleccionada != null && tileSeleccionada != null && unidadSeleccionada.SeRealizoUnaAccion == false ) {
			if( CheckDistance( tileSeleccionada.transform.position.x , unidadSeleccionada.transform.position.x , unidadSeleccionada.Movimiento ) ) {
				if( CheckDistance( tileSeleccionada.transform.position.y , unidadSeleccionada.transform.position.y , unidadSeleccionada.Movimiento ) ) {
					unidadSeleccionada.transform.position = tileSeleccionada.transform.position;
					unidadSeleccionada.SeRealizoUnaAccion = true;
					//	Debug.Log( "Entre" );
				}
			}
		}
	}
	/*Seleccionar Unidad*/
	public void SeleccionarUnidad( RaycastHit2D hit2D ) {
		if( UnidadSeleccionada == null && hit2D.collider.GetComponent<Units>().PerteneJugador  == jugadores[turnoPlayer-1].NumeroPlayer) {
			UnidadSeleccionada = hit2D.collider.GetComponent<Units>();
			//		Debug.Log( "Se ha seleccionado una unidad" );
		} else if( UnidadSeleccionada != null && hit2D.collider.GetComponent<Units>().PerteneJugador == UnidadSeleccionada.PerteneJugador ) {
			UnidadSeleccionada = hit2D.collider.GetComponent<Units>();
			//		Debug.Log( "Se ha seleccionado una unidad" );
		} else {
			UnidadDestino = hit2D.collider.GetComponent<Units>();
			//		Debug.Log( "Se ha seleccionado una unidad'enemiga" );
		}
	}
	/*Selecciona las tiles*/
	public void SeleccionarTile( RaycastHit2D hit2D ) {
		tileSeleccionada = hit2D.collider.GetComponent<Tile>();
		//Debug.Log( "Se ha seleccionado una tile" );
	}
	/*Cheackear distancia*/
	public bool CheckDistance( float pos1 , float pos2 , int distancia ) {
		if( Mathf.Abs( pos1 - pos2 ) < distancia ) {
			return true;
		} else {
			return false;
		}
	}


}
