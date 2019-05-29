using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile2: MonoBehaviour{
	/*############################## Variables ##############################*/
	int posX, posY;
	SpriteRenderer spriteRenderer;
	TipoDeSuelo tipoDeSuelo;
	/*############################## Getters && Setters ##############################*/
	public int PosX { get => posX; set => posX = value; }
	public int PosY { get => posY; set => posY =  value ; }
	public SpriteRenderer SpriteRender2 { get => spriteRenderer; set => spriteRenderer =  value ; }
	public TipoDeSuelo TipoDeSuelo { get => tipoDeSuelo; set => tipoDeSuelo =  value ; }

	/*############################## Constructor ##############################*/
	public Tile2(int x,int y,TipoDeSuelo tipoDeSuelo) {
		PosX = x;
		PosY = y;
		TipoDeSuelo = tipoDeSuelo;
		SetSprite( TipoDeSuelo );
	}

	/*############################## Metodos ##############################*/
	private void Awake() {
		SetSpriteRenderer();
		SetSprite( TipoDeSuelo.Caminable );
	}
	public void SetSpriteRenderer() {
		if( GetComponent<SpriteRenderer>() != null ) {
			spriteRenderer = GetComponent<SpriteRenderer>();
		} else {
			gameObject.AddComponent<SpriteRenderer>();
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}

		}
		public void SetSprite(TipoDeSuelo tipoDeSuelo) {
		switch( tipoDeSuelo ) {
			case TipoDeSuelo.Spawn:
				SpriteRender2.sprite = Resources.Load<Sprite>( "Textures/Grass" );
				break;
			case TipoDeSuelo.Caminable:
				SpriteRender2.sprite = Resources.Load<Sprite>( "Textures/Hexagon" );
				break;
			case TipoDeSuelo.NoCaminable:
				SpriteRender2.sprite = Resources.Load<Sprite>( "Textures/Water" );
				break;


		}

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
