using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement:MonoBehaviour {
	[SerializeField]
	private int limitUp;
	private int limitDown;
	[SerializeField]
	private int limitRight;
	private int limitLeft;
	/*############################## Getters && Setters ##############################*/
	public int LimitUp { get => limitUp; set => limitUp = value; }
	public int LimitDown { get => limitDown; set => limitDown = value; }
	public int LimitRight { get => limitRight; set => limitRight = value; }
	public int LimitLeft { get => limitLeft; set => limitLeft = value; }
	/*############################## Methods ##############################*/
	//Copia las posiciones de los tiles y se las pasa como posicion a la unidad de combate
	public void Move( int x , int y ) {
		transform.position = new Vector2( x , y );
	}
}
