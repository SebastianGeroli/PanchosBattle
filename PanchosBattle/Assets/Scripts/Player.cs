using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour {
	/*############################## Variables ##############################*/
	int numeroPlayer;
	int unidadesTotales;
	int dinero;
	bool puedeJugar;
	public Units unidadSeleccioanda;
	List<Units> arqueros;
	List<Units> jinetes;
	List<Units> guerreros;
	/*############################## Getters && Setters ##############################*/
	public int NumeroPlayer { get => numeroPlayer; set => numeroPlayer = value; }
	public bool PuedeJugar { get => puedeJugar; set => puedeJugar = value; }
	public int Dinero { get => dinero; set => dinero = value; }
	public int UnidadesTotales { get => unidadesTotales; set => unidadesTotales = value; }
	public List<Units> Guerreros { get => Guerreros; set => Guerreros =  value ; }
	public List<Units> Jinetes { get => Jinetes; set => Jinetes =  value ; }
	public List<Units> Arqueros { get => Arqueros; set => Arqueros =  value ; }

	/*############################## Metodos ##############################*/
	public void ComprarGuerrero(){
		Guerreros.Add( new Units(TipoUnidad.Guerrero, NumeroPlayer) );
	}
	public void ComprarJinete(){
		Jinetes.Add( new Units(TipoUnidad.Jinete,NumeroPlayer) );
	}
	public void ComprarArquero() {
		Arqueros.Add( new Units(TipoUnidad.Arquero,numeroPlayer) );
	}
	public Units SeleccionarUnidad() {
		
		return unidadSeleccioanda;
	}
	public void MoverUnidad() {

	}
	public void Atacar() {

	}
	public void DesplegarUnidadSobreMapa() {

	}
}
