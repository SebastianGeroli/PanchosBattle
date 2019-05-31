using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour {
	/*############################## Variables ##############################*/
	[SerializeField]
	int numeroPlayer;
	[SerializeField]
	int unidadesTotales;
	[SerializeField]
	int dinero;
	bool puedeJugar;
	[SerializeField]
	List<GameObject> arqueros;
	[SerializeField]
	List<GameObject> jinetes;
	[SerializeField]
	List<GameObject> guerreros;
	/*############################## Getters && Setters ##############################*/
	public int NumeroPlayer { get => numeroPlayer; set => numeroPlayer = value; }
	public bool PuedeJugar { get => puedeJugar; set => puedeJugar = value; }
	public int Dinero { get => dinero; set => dinero = value; }
	public int UnidadesTotales { get => unidadesTotales; set => unidadesTotales = value; }
	public List<GameObject> Guerreros { get => guerreros; set => guerreros = value; }
	public List<GameObject> Jinetes { get => jinetes; set => jinetes = value; }
	public List<GameObject> Arqueros { get => arqueros; set => arqueros = value; }

	/*############################## Metodos ##############################*/
	public void ComprarGuerrero() {
		GameObject GO;
		if( NumeroPlayer % 2 == 0 ) {
			GO = Instantiate( Resources.Load( "Unidad2" ) ) as GameObject;
		} else {
			GO = Instantiate( Resources.Load( "Unidad" ) ) as GameObject;
		}
		Guerreros.Add( GO );
		Guerreros[guerreros.Count - 1].transform.SetParent( transform );
		Guerreros[guerreros.Count - 1].GetComponent<Units>().UnitsInicial( TipoUnidad.Guerrero , numeroPlayer );
		UnidadesTotales++;
	}
	public void ComprarJinete() {
		GameObject GO;
		if( NumeroPlayer % 2 == 0 ) {
			GO = Instantiate( Resources.Load( "Unidad2" ) ) as GameObject;
		} else {
			GO = Instantiate( Resources.Load( "Unidad" ) ) as GameObject;
		}
		Jinetes.Add( GO );
		Jinetes[jinetes.Count - 1].transform.SetParent( transform );
		Jinetes[jinetes.Count - 1].GetComponent<Units>().UnitsInicial( TipoUnidad.Jinete , numeroPlayer );
		UnidadesTotales++;
	}
	public void ComprarArquero() {
		GameObject GO;
		if( NumeroPlayer % 2 == 0 ) {
			GO = Instantiate( Resources.Load( "Unidad2" ) ) as GameObject;
		} else {
			GO = Instantiate( Resources.Load( "Unidad" ) ) as GameObject;
		}
		Arqueros.Add( GO );
		Arqueros[arqueros.Count - 1].transform.SetParent( transform );
		Arqueros[arqueros.Count - 1].GetComponent<Units>().UnitsInicial( TipoUnidad.Arquero , numeroPlayer );
		UnidadesTotales++;
		UnidadesTotales++;
	}
}
