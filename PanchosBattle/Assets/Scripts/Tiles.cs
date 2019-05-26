using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles:MonoBehaviour {
	SpriteRenderer spriteRenderer;
	private int posX;
	private int posY;
	private bool powerUp;
	[SerializeField]
	TypeOfFloor typeOfFloor = TypeOfFloor.None;
	/*############################## Constructor ##############################*/
	public Tiles( int x , int y , TypeOfFloor typeOf ) {
		posX = x;
		posY = y;
		typeOfFloor = typeOf;
	}
	/*############################## Getters && Setters ##############################*/
	public void SetPosX( int x ) {
		posX = x;
	}
	public int GetPosX() {
		return posX;
	}
	public void SetPosY( int y ) {
		posY = y;
	}
	public int GetPosY() {
		return posY;
	}
	public void SetTypeOfFloor( TypeOfFloor typeOf ) {
		typeOfFloor = typeOf;
	}
	public TypeOfFloor GetTypeOfFloor() {
		return typeOfFloor;
	}
	/*############################## Methods ##############################*/
	//Awake se ejecuta antes de los frames 
	private void Awake() {
		if( GetComponent<SpriteRenderer>() != null ) {
			spriteRenderer = GetComponent<SpriteRenderer>();
		} else {
			gameObject.AddComponent<SpriteRenderer>();
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}
	}
	//Este metodo cambia las texturras y pone en la posicion al sprite que le corresponde
	public void SpriteSelector() {
		transform.position = new Vector2( posX , posY );
		if( GetComponent<SpriteRenderer>() != null ) {
			spriteRenderer = GetComponent<SpriteRenderer>();
		} else {
			gameObject.AddComponent<SpriteRenderer>();
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}
		switch( (int)typeOfFloor ) {
			case 0:
				spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Floor" );
				break;
			case 1:
				spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Damage" );
				break;
			case 7:
				spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Floor" );
				break;
			case 8:
				spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Water" );
				break;
			case 9:
				spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Grass" );
				break;

		}
	}
	//Metodo que debe devolver el objeto clickeado para despues poder mandar esa informacion a las unidades y moverse respectivamente 
	public void Click() {
	}
}
//Enum que sirve para diferenciar que tipo de piso se va a utilizar
public enum TypeOfFloor {
    None = 0,
    Damage = 1,
    DmgReduction = 2,
    LifeUp = 3,
    LifeDown = 4,
    SpeedBoost = 5,
    SpeedBoostDown = 6,
    Floor = 7,
    Water = 8,
    Grass = 9
}
