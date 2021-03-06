﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardController:MonoBehaviour {
	/*############################## Variables ##############################*/
	int totalunits =0;
	bool paso = false;
	public float multiplierX, multiplierY;
	public Canvas canvasPlayer1, canvasPlayer2;
	public Text textTiempo, textTurnoPlayer, textTurnoGeneral, textTurnoPlayer2, textGanador;
	public Button playAgain;
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
		TimeControl();
		SetTilesTag();
		MostrarUICompra();
		CambiarTurno();
		ResetearAccionesUnidades();
		GanoAlguien();
	}
	private void Awake() {
		playAgain.gameObject.SetActive( false );
		SetPlayers();
		TiempoRestante = 60;
		TurnoGeneral = 0;
		TurnoPlayer = 1;
		NextRoyale = 10;
		textGanador.enabled = false;
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
			jugadores[x].NumeroPlayer = x + 1;
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
				CheckearVidaUnidad();
				textTurnoPlayer.enabled = false;
				textTurnoPlayer2.enabled = true;
				TurnoPlayer = 2;
				TiempoRestante = 30;
				TurnoGeneral += 1;
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
				unidadSeleccionada = null;
				unidadDestino = null;
				tileSeleccionada = null;
				ShowRanges();
			} else {
				CheckearVidaUnidad();
				textTurnoPlayer.enabled = true;
				textTurnoPlayer2.enabled = false;
				turnoPlayer = 1;
				tiempoRestante = 30;
				TurnoGeneral += 1;
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
				unidadSeleccionada = null;
				unidadDestino = null;
				tileSeleccionada = null;
				ShowRanges();
			}
		}
	}
	/*Recorre el listado de jugadores si alguno de ellos tiene sus unidades
	 * totales en 0 o menos se da por terminada la partida y ese jugador pierde*/
	public void GanoAlguien() {
		totalunits = 0;
		if( turnoGeneral > 0 ) {
			for( int x = 0; x < jugadores.Length; x++ ) {
				if( jugadores[x].UnidadesTotales <= 0 ) {
					textGanador.enabled = true;
					playAgain.gameObject.SetActive( true );
					if( x == 0 ) {
						textGanador.text = "Ganador: \n Jugador 2";
					} else {
						textGanador.text = "Ganador: \n Jugador 1";
					}
				}
			}
			for( int x = 0; x < jugadores.Length; x++ ) {
				totalunits += jugadores[x].Guerreros.Count;
				totalunits += jugadores[x].Jinetes.Count;
				totalunits += jugadores[x].Arqueros.Count;
				if( totalunits <= 0 ) {
					textGanador.enabled = true;
					playAgain.gameObject.SetActive( true );
					if( x == 0 ) {
						textGanador.text = "Ganador: \n Jugador 2";
					} else {
						textGanador.text = "Ganador: \n Jugador 1";
					}
				}
			}
			
		}
	}
	/*Muestra la UI de compra durante el turno 0 luego de este empiezan a jugar
	 * y pone los datos necesarios para el proximo turno al vencerse el tiempo*/
	public void MostrarUICompra() {

		if( TurnoGeneral == 0 && TiempoRestante >= 0 ) {
			if( TiempoRestante > 30 ) {
				turnoPlayer = 1;
				canvasPlayer1.enabled = true;
				canvasPlayer2.enabled = false;
				textTurnoPlayer.enabled = true;
				textTurnoPlayer2.enabled = false;
				textTurnoGeneral.text = "Turno Nro: " + turnoGeneral.ToString();
			} else {
				if( paso == false ) {
					CheckearVidaUnidad();
					paso = true;
				}
				turnoPlayer = 2;
				canvasPlayer1.enabled = false;
				canvasPlayer2.enabled = true;
				textTurnoPlayer.enabled = false;
				textTurnoPlayer2.enabled = true;
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
	/*Al terminar el turno se resetean las acciones de todas las unidades 
	 * para que se puedan mover nuevamente en el proximo turno */
	public void ResetearAccionesUnidades() {
		if( TurnoPlayer == 2 ) {
			for( int x = 0; x < jugadores[0].Guerreros.Count; x++ ) {
				jugadores[0].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[0].Jinetes.Count; x++ ) {
				jugadores[0].Jinetes[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[0].Arqueros.Count; x++ ) {
				jugadores[0].Arqueros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
		} else {
			for( int x = 0; x < jugadores[1].Guerreros.Count; x++ ) {
				jugadores[1].Guerreros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[1].Jinetes.Count; x++ ) {
				jugadores[1].Jinetes[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
			for( int x = 0; x < jugadores[1].Arqueros.Count; x++ ) {
				jugadores[1].Arqueros[x].GetComponent<Units>().SeRealizoUnaAccion = false;
			}
		}
	}
	/*Con este metodo se obtiene la informacion del objeto sobre el cual 
	 * se esta haciendo click*/
	public void CheckActions() {
		Ataque();
		MoverUnidad();
		ShowRanges();
		if( Input.GetMouseButtonDown( 0 ) ) {
			RaycastHit2D hit2D = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ) , Vector2.zero );
			if( hit2D.collider != null && hit2D.collider.tag == "Units" ) {
				SeleccionarUnidad( hit2D );
				Ataque();
				MoverUnidad();
				ShowRanges();

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
		GameObject destruir;
		for( int l = 0; l < 20; l++ ) {
			for( int i = 0; i < jugadores.Length; i++ ) {
				for( int x = 0; x < jugadores[i].Guerreros.Count; x++ ) {
					if( jugadores[i].Guerreros[x].GetComponent<Units>().Vida <= 0 || jugadores[i].Guerreros[x].GetComponent<Units>().EstaEnTablero == false ) {
						destruir = jugadores[i].Guerreros[x];
						jugadores[i].Guerreros.Remove( jugadores[i].Guerreros[x] );
						jugadores[i].UnidadesTotales--;
						Destroy( destruir );
					}
				}
				for( int x = 0; x < jugadores[i].Jinetes.Count; x++ ) {
					if( jugadores[i].Jinetes[x].GetComponent<Units>().Vida <= 0 || jugadores[i].Jinetes[x].GetComponent<Units>().EstaEnTablero == false ) {
						destruir = jugadores[i].Jinetes[x];
						jugadores[i].Jinetes.Remove( jugadores[i].Jinetes[x] );
						jugadores[i].UnidadesTotales--;
						Destroy( destruir );
					}
				}
				for( int x = 0; x < jugadores[i].Arqueros.Count; x++ ) {
					if( jugadores[i].Arqueros[x].GetComponent<Units>().Vida <= 0 || jugadores[i].Arqueros[x].GetComponent<Units>().EstaEnTablero == false ) {
						destruir = jugadores[i].Arqueros[x];
						jugadores[i].Arqueros.Remove( jugadores[i].Arqueros[x] );
						jugadores[i].UnidadesTotales--;
						Destroy( destruir );
					}
				}
			}
		}

	}
	/*Convert Dictionary from board to Array */
	public void SetTilesTag() {
		if( !alreadyDone ) {
			foreach( KeyValuePair<Vector2Int , Tile> entry in board.TilesDictionary ) {
				//Debug.Log( "Estoy en " + entry.Value.name );
				entry.Value.tag = "Tiles";
			}
		}
		alreadyDone = true;
	}
	/*Ataque*/
	public void Ataque() {
		if( unidadSeleccionada != null && unidadDestino != null && unidadSeleccionada.SeRealizoUnaAccion == false ) {
			if( CheckDistance( unidadDestino.transform.position.x , unidadSeleccionada.transform.position.x , unidadSeleccionada.RangoAtaque * multiplierX ) ) {
				if( CheckDistance( unidadDestino.transform.position.y , unidadSeleccionada.transform.position.y , unidadSeleccionada.RangoAtaque * multiplierY ) ) {
					if( unidadSeleccionada.tipounidad == TipoUnidad.Guerrero && unidadDestino.tipounidad == TipoUnidad.Jinete ) {
						unidadDestino.Vida -= unidadSeleccionada.Damage * 3;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						CheckearVidaUnidad();
						ShowRanges();
						unidadSeleccionada = null;
						unidadDestino = null;
						//	Debug.Log( "Ataque x 3 " );
					} else if( unidadSeleccionada.tipounidad == TipoUnidad.Arquero && unidadDestino.tipounidad == TipoUnidad.Guerrero ) {
						unidadDestino.Vida -= unidadSeleccionada.Damage * 3;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						CheckearVidaUnidad();
						ShowRanges();
						unidadSeleccionada = null;
						unidadDestino = null;
						//	Debug.Log( "Ataque x 3 " );
					} else if( unidadSeleccionada.tipounidad == TipoUnidad.Jinete && unidadDestino.tipounidad == TipoUnidad.Arquero ) {
						unidadDestino.Vida -= unidadSeleccionada.Damage * 3;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						CheckearVidaUnidad();
						ShowRanges();
						unidadSeleccionada = null;
						unidadDestino = null;
						//	Debug.Log( "Ataque x 3" );
					} else {
						unidadDestino.Vida -= unidadSeleccionada.Damage;
						unidadSeleccionada.SeRealizoUnaAccion = true;
						CheckearVidaUnidad();
						ShowRanges();
						unidadSeleccionada = null;
						unidadDestino = null;
						///	Debug.Log( "Ataque" );
					}
				}
			}

		}

	}
	/*Mover La unidad*/
	public void MoverUnidad() {
		bool isAvailable = true;
		if( turnoGeneral == 0 ) {
			if( unidadSeleccionada != null && tileSeleccionada != null && unidadSeleccionada.EstaEnTablero == false ) {
				if( tileSeleccionada.SpawneableForPlayerNumber == unidadSeleccionada.PerteneJugador ) {
					for( int i = 0; i < jugadores.Length; i++ ) {
						for( int x = 0; x < jugadores[i].Guerreros.Count; x++ ) {
							if( tileSeleccionada.transform.position == jugadores[i].Guerreros[x].transform.position ) {
								ShowRanges();
								isAvailable = false;
							}
						}
						for( int x = 0; x < jugadores[i].Jinetes.Count; x++ ) {
							if( tileSeleccionada.transform.position == jugadores[i].Jinetes[x].transform.position ) {
								ShowRanges();
								isAvailable = false;
							}
						}
						for( int x = 0; x < jugadores[i].Arqueros.Count; x++ ) {
							if( tileSeleccionada.transform.position == jugadores[i].Arqueros[x].transform.position ) {
								ShowRanges();
								isAvailable = false;
							}
						}
					}

					if( isAvailable ) {
						unidadSeleccionada.transform.position = tileSeleccionada.transform.position;
						ShowRanges();
						unidadSeleccionada.EstaEnTablero = true;
						unidadSeleccionada = null;
						tileSeleccionada = null;
					} else {
						tileSeleccionada = null;
					}

				}
			}

		} else {
			if( unidadSeleccionada != null && tileSeleccionada != null && unidadSeleccionada.SeRealizoUnaAccion == false ) {
				if( unidadSeleccionada.EstaEnTablero ) {
					if( CheckDistance( tileSeleccionada.transform.position.x , unidadSeleccionada.transform.position.x , unidadSeleccionada.Movimiento * multiplierX ) ) {
						if( CheckDistance( tileSeleccionada.transform.position.y , unidadSeleccionada.transform.position.y , unidadSeleccionada.Movimiento * multiplierY ) ) {
							for( int i = 0; i < jugadores.Length; i++ ) {
								for( int x = 0; x < jugadores[i].Guerreros.Count; x++ ) {
									if( tileSeleccionada.transform.position == jugadores[i].Guerreros[x].transform.position ) {
										ShowRanges();
										isAvailable = false;
									}
								}
								for( int x = 0; x < jugadores[i].Jinetes.Count; x++ ) {
									if( tileSeleccionada.transform.position == jugadores[i].Jinetes[x].transform.position ) {
										ShowRanges();
										isAvailable = false;
									}
								}
								for( int x = 0; x < jugadores[i].Arqueros.Count; x++ ) {
									if( tileSeleccionada.transform.position == jugadores[i].Arqueros[x].transform.position ) {
										ShowRanges();
										isAvailable = false;
									}
								}
							}

							if( isAvailable ) {
								unidadSeleccionada.transform.position = tileSeleccionada.transform.position;
								ShowRanges();
								unidadSeleccionada.SeRealizoUnaAccion = true;
								unidadSeleccionada.EstaEnTablero = true;
								unidadSeleccionada = null;
								tileSeleccionada = null;
							} else {
								tileSeleccionada = null;
							}
						}
					}
				}
			}
		}

	}
	/*Seleccionar Unidad*/
	public void SeleccionarUnidad( RaycastHit2D hit2D ) {
		if( UnidadSeleccionada == null && hit2D.collider.GetComponent<Units>().PerteneJugador == jugadores[turnoPlayer - 1].NumeroPlayer && !hit2D.collider.GetComponent<Units>().SeRealizoUnaAccion ) {
			UnidadSeleccionada = hit2D.collider.GetComponent<Units>();
			//		Debug.Log( "Se ha seleccionado una unidad" );
		} else if( UnidadSeleccionada != null && hit2D.collider.GetComponent<Units>().PerteneJugador == UnidadSeleccionada.PerteneJugador && !hit2D.collider.GetComponent<Units>().SeRealizoUnaAccion ) {
			tileSeleccionada = null;
			UnidadSeleccionada = hit2D.collider.GetComponent<Units>();
			//		Debug.Log( "Se ha seleccionado una unidad" );
		} else if( UnidadSeleccionada != null && hit2D.collider.GetComponent<Units>().PerteneJugador != UnidadSeleccionada.PerteneJugador ) {
			UnidadDestino = hit2D.collider.GetComponent<Units>();
			//		Debug.Log( "Se ha seleccionado una unidad'enemiga" );
		}
	}
	/*Selecciona las tiles*/
	public void SeleccionarTile( RaycastHit2D hit2D ) {
		if( unidadSeleccionada != null ) {
			if( tileSeleccionada != hit2D.collider.GetComponent<Tile>() && tileSeleccionada != null ) {
				tileSeleccionada.HighlightColor = Color.clear;
				tileSeleccionada = hit2D.collider.GetComponent<Tile>();
				tileSeleccionada.HighlightColor = Color.black;
			} else {
				tileSeleccionada = hit2D.collider.GetComponent<Tile>();
				tileSeleccionada.HighlightColor = Color.black;
			}
		}

		//Debug.Log( "Se ha seleccionado una tile" );
	}
	/*Cheackear distancia en base a los rangos de movieminto y ataque si se encuentra dentro del rango devuelve true*/
	public bool CheckDistance( float pos1 , float pos2 , float distancia ) {
		if( Mathf.Abs( pos1 - pos2 ) < distancia ) {
			return true;
		} else {
			return false;
		}
	}
	/*ShowRange*/
	public void ShowRanges() {
		Color tmp;
		ClearHighligths();
		SetColorSpawns();
		if( unidadSeleccionada != null && unidadSeleccionada.EstaEnTablero ) {

			foreach( KeyValuePair<Vector2Int , Tile> entry in board.TilesDictionary ) {
				if( CheckDistance( entry.Value.transform.position.x , unidadSeleccionada.transform.position.x , unidadSeleccionada.Movimiento * multiplierX ) ) {
					if( CheckDistance( entry.Value.transform.position.y , unidadSeleccionada.transform.position.y , unidadSeleccionada.Movimiento * multiplierY ) ) {
						tmp = entry.Value.HighlightColor;
						tmp = Color.green;
						tmp.a = 0.2f;
						entry.Value.HighlightColor = tmp;
					}
				}
			}
			foreach( KeyValuePair<Vector2Int , Tile> entry in board.TilesDictionary ) {
				if( CheckDistance( entry.Value.transform.position.x , unidadSeleccionada.transform.position.x , unidadSeleccionada.RangoAtaque * multiplierX ) ) {
					if( CheckDistance( entry.Value.transform.position.y , unidadSeleccionada.transform.position.y , unidadSeleccionada.RangoAtaque * multiplierY ) ) {
						tmp = entry.Value.BaseColor;
						tmp = Color.red;
						tmp.a = 0.3f;
						entry.Value.HighlightColor = tmp;
					}
				}
			}
		}


	}
	/*Limpia todo el tablero*/
	public void ClearHighligths() {
		foreach( KeyValuePair<Vector2Int , Tile> entry in board.TilesDictionary ) {
			entry.Value.Sprite = Resources.Load<Sprite>( "Textures/Caminable" );
			entry.Value.HighlightColor = Color.clear;

		}
	}
	/*Redibuja los spawns de color */
	public void SetColorSpawns() {
		Color tmp;
		if( turnoGeneral == 0 ) {
			foreach( KeyValuePair<Vector2Int , Tile> entry in board.TilesDictionary ) {
				if( entry.Value.SpawneableForPlayerNumber == 1 ) {
					entry.Value.Sprite = Resources.Load<Sprite>( "Textures/Spawn" );
					tmp = entry.Value.HighlightColor;
					tmp.r = 0;
					tmp.b = 255;
					tmp.g = 0;
					tmp.a = 0.2f;
					entry.Value.HighlightColor = tmp;


				}
				if( entry.Value.SpawneableForPlayerNumber == 2 ) {
					entry.Value.Sprite = Resources.Load<Sprite>( "Textures/Spawn" );
					tmp = entry.Value.HighlightColor;
					tmp.r = 255;
					tmp.b = 0;
					tmp.g = 0;
					tmp.a = 0.2f;
					entry.Value.HighlightColor = tmp;
				}
			}
		} else {

			foreach( KeyValuePair<Vector2Int , Tile> entry in board.TilesDictionary ) {
				if( entry.Value.SpawneableForPlayerNumber == 1 ) {
					entry.Value.Sprite = Resources.Load<Sprite>( "Textures/Spawn" );
				}
				if( entry.Value.SpawneableForPlayerNumber == 2 ) {
					entry.Value.Sprite = Resources.Load<Sprite>( "Textures/Spawn" );
				}
			}
		}
	}
	/*Pasar Turno*/
	public void PasarTurno() {
		if( tiempoRestante > 30 ) {
			tiempoRestante = 30;
		} else {
			tiempoRestante = 1;
		}
	}
	/*Volver a cargar la escena*/
	public void PlayAgain() {
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}
}
