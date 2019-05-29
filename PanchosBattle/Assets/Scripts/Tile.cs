using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile:MonoBehaviour {
	/*############################## Variables ##############################*/
	int posX, posY;
	SpriteRenderer spriteRenderer;
	/*############################## Getters && Setters ##############################*/
	public int PosX { get => posX; set => posX = value; }
	public int PosY { get => posY; set => posY =  value ; }
	public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer =  value ; }

	/*############################## Constructor ##############################*/
	public Tile(int x,int y,TipoDeSuelo tipoDeSuelo) {

	}
	/*############################## Metodos ##############################*/
	public void SetSprite() {

	}
	public void SetSprite(TipoDeSuelo tipoDeSuelo) {

	}
}
//Enum que sirve para diferenciar que tipo de piso se va a utilizar
public enum TipoDeSuelo {
    Spawn = 0,
    Caminable = 1,
    NoCaminable = 2,
    Agua = 3,
    Deco1 = 4,
    Deco2 = 5,
    Deco3 = 6
}
