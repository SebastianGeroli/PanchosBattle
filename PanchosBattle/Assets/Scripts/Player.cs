 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	// private bool InsertUnit {get => insertUnit; set => insertUnit = value;}
	public int Dinero { get => dinero; set => dinero = value; }
	public int UnidadesTotales { get => unidadesTotales; set => unidadesTotales = value; }
	public List<GameObject> Guerreros { get => guerreros; set => guerreros = value; }
	public List<GameObject> Jinetes { get => jinetes; set => jinetes = value; }
	public List<GameObject> Arqueros { get => arqueros; set => arqueros = value; }

	/*############################## Metodos ##############################*/

	public void ComprarGuerrero() {
		GameObject GO = Resources.Load( "Unidad" ) as GameObject;
		Guerreros.Add( GO );
		for( int x = 0; x < guerreros.Count; x++ ) {
			Guerreros[x].AddComponent<Units>();
			Guerreros[x].GetComponent<Units>().UnitsInicial( TipoUnidad.Guerrero , numeroPlayer );
		}
        GameObject.Find("AddGuerrero").GetComponentInChildren<Text>().text="Guerreros:"+guerreros.Count;
		UnidadesTotales++;
	}
	public void ComprarJinete() {
		GameObject GO = Resources.Load( "Unidad" ) as GameObject;
		Jinetes.Add( GO );
		for( int x = 0; x < jinetes.Count; x++ ) {
			Jinetes[x].AddComponent<Units>();
			Jinetes[x].GetComponent<Units>().UnitsInicial( TipoUnidad.Jinete , numeroPlayer );
		}
        GameObject.Find("AddJinete").GetComponentInChildren<Text>().text="Jinetes:"+jinetes.Count;
		UnidadesTotales++;
	}
	public void ComprarArquero() {
		GameObject GO = Resources.Load( "Unidad" ) as GameObject;
		Arqueros.Add( GO );
		for( int x = 0; x < arqueros.Count; x++ ) {
			Arqueros[x].AddComponent<Units>();
			Arqueros[x].GetComponent<Units>().UnitsInicial( TipoUnidad.Arquero , numeroPlayer );
		}
        GameObject.Find("AddArquero").GetComponentInChildren<Text>().text="Arqueros:"+arqueros.Count;
		UnidadesTotales++;
	}
	public void AñadirGuerrero() {

	}

}
