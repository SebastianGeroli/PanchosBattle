using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashFile : MonoBehaviour
{
	///*SINGLETON solo puede haber uno en ejecucion durante todo el juego y controla actualmente la inicializacion del escenario y los movimientos de las unidades */
	//public static BoardController boardController;
	//public Movement movement;
	////Cantidad de columnas(x) y cantidad de filas (y) que va a tener el array, y por lo tanto el mapa
	//private int col = 20;
	//private int row = 10;
	////Array Bidimensional que contiene GameObjects
	//public GameObject[,] tiles;
	////Posiciones X e Y  que se utilizan actualmente para mover la unidad a traves del array bidimensional 
	//private int posX;
	//private int posY;
	////Contador solo sirve actualmente para nombrar los tiles genardos con un numero unico
	//int counter = 0;
	////Awake ejecuta antes de iniciar el juego
	//private void Awake() {
	//	//Verifica si no hay un singleton en el juego, de no haber lo crea, y de no haber uno destruye el que se encuentra en la escena
	//	if( boardController == null ) {
	//		DontDestroyOnLoad( gameObject );
	//		//boardController = this;
	//	} else if( boardController != this ) {
	//		Destroy( gameObject );
	//	}
	//	posX = 0;
	//	posY = 0;
	//	tiles = new GameObject[col , row];
	//	movement.LimitUp = tiles.GetLength( 1 ) - 1;
	//	movement.LimitDown = 0;
	//	movement.LimitLeft = 0;
	//	movement.LimitRight = tiles.GetLength( 0 ) - 1;
	//}
	////Start se inicia en el primer frame del juego 
	//private void Start() {
	//	InstantiateBoard();
	//	InstantiateDamage();
	//	InstantiateWater();
	//	InstantiateGrass();


	//}
	////Se ejecuta constantemente en cada frame del juego 
	//private void Update() {
	//	//Verifica las teclas presionadas y si el jugador se encuentra dentro de los limites de juego para mover, caso contrario no ejecuta nada.
	//	if( Input.GetKeyDown( KeyCode.W ) && movement.transform.position.y < movement.LimitUp ) {
	//		posY += 1;
	//		movement.Move( tiles[posX , posY].GetComponent<Tile>().GetPosX() , tiles[posX , posY].GetComponent<Tile>().GetPosY() );
	//	}
	//	if( Input.GetKeyDown( KeyCode.S ) && movement.transform.position.y > movement.LimitDown ) {
	//		posY -= 1;
	//		movement.Move( tiles[posX , posY].GetComponent<Tile>().GetPosX() , tiles[posX , posY].GetComponent<Tile>().GetPosY() );
	//	}
	//	if( Input.GetKeyDown( KeyCode.A ) && movement.transform.position.x > movement.LimitLeft ) {
	//		posX -= 1;
	//		movement.Move( tiles[posX , posY].GetComponent<Tile>().GetPosX() , tiles[posX , posY].GetComponent<Tile>().GetPosY() );
	//	}
	//	if( Input.GetKeyDown( KeyCode.D ) && movement.transform.position.x < movement.LimitRight ) {
	//		posX += 1;
	//		movement.Move( tiles[posX , posY].GetComponent<Tile>().GetPosX() , tiles[posX , posY].GetComponent<Tile>().GetPosY() );
	//	}
	//}
	////Inicia todo el escenario base
	//public void InstantiateBoard() {
	//	/*Recorre el array bidimensional creando los GameObject
	//		 * dandole las propiedades necesarias para su uso, posteriormente
	//		 * En esta parte solo crea el mapa todo con las mismas texturas de Floor
	//		 */
	//	for( int x = 0; x < tiles.GetLength( 0 ); x++ ) {
	//		for( int y = 0; y < tiles.GetLength( 1 ); y++ ) {

	//			tiles[x , y] = new GameObject();
	//			tiles[x , y].name = "Tile" + counter;
	//			tiles[x , y].AddComponent<Tile>();
	//			tiles[x , y].GetComponent<Tile>().SetPosX( x );
	//			tiles[x , y].GetComponent<Tile>().SetPosY( y );
	//			tiles[x , y].GetComponent<Tile>().SetTypeOfFloor( TypeOfFloor.Floor );
	//			tiles[x , y].GetComponent<Tile>().SpriteSelector();
	//			//Debug.Log( tiles[x , y].name + "Posicion en array: " + x + " " + y );
	//			counter++;
	//		}
	//	}
	//}
	////Cambia las texturas de algunos Floor a Grass de forma random
	//public void InstantiateGrass() {
	//	for( int i = 0; i < 10; i++ ) {
	//		int randomX = Random.Range( 0 , 19 );
	//		int randomY = Random.Range( 0 , 9 );
	//		tiles[randomX , randomY].GetComponent<Tile>().SetTypeOfFloor( TypeOfFloor.Grass );
	//		tiles[randomX , randomY].GetComponent<Tile>().SpriteSelector();
	//	}
	//}
	////Cambia las texturas de algunos Floor a Water de forma random
	//public void InstantiateWater() {
	//	for( int i = 0; i < 10; i++ ) {
	//		int randomX = Random.Range( 4 , 15 );
	//		int randomY = Random.Range( 0 , 9 );
	//		tiles[randomX , randomY].GetComponent<Tile>().SetTypeOfFloor( TypeOfFloor.Water );
	//		tiles[randomX , randomY].GetComponent<Tile>().SpriteSelector();
	//	}
	//}
	////Cambia las texturas de algunos Floor a Damage de forma random
	//public void InstantiateDamage() {
	//	for( int i = 0; i < 10; i++ ) {
	//		int randomX = Random.Range( 0 , 19 );
	//		int randomY = Random.Range( 0 , 9 );
	//		tiles[randomX , randomY].GetComponent<Tile>().SetTypeOfFloor( TypeOfFloor.Damage );
	//		tiles[randomX , randomY].GetComponent<Tile>().SpriteSelector();
	//	}
	//}
	//SpriteRenderer spriteRenderer;
	//private int posX;
	//private int posY;
	//private bool powerUp;
	//[SerializeField]
	//TypeOfFloor typeOfFloor = TypeOfFloor.None;
	///*############################## Constructor ##############################*/
	//public Tile( int x , int y , TypeOfFloor typeOf ) {
	//	posX = x;
	//	posY = y;
	//	typeOfFloor = typeOf;
	//}
	///*############################## Getters && Setters ##############################*/
	//public void SetPosX( int x ) {
	//	posX = x;
	//}
	//public int GetPosX() {
	//	return posX;
	//}
	//public void SetPosY( int y ) {
	//	posY = y;
	//}
	//public int GetPosY() {
	//	return posY;
	//}
	//public void SetTypeOfFloor( TypeOfFloor typeOf ) {
	//	typeOfFloor = typeOf;
	//}
	//public TypeOfFloor GetTypeOfFloor() {
	//	return typeOfFloor;
	//}
	///*############################## Methods ##############################*/
	////Awake se ejecuta antes de los frames 
	//private void Awake() {
	//	if( GetComponent<SpriteRenderer>() != null ) {
	//		spriteRenderer = GetComponent<SpriteRenderer>();
	//	} else {
	//		gameObject.AddComponent<SpriteRenderer>();
	//		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	//	}
	//}
	////Este metodo cambia las texturras y pone en la posicion al sprite que le corresponde
	//public void SpriteSelector() {
	//	transform.position = new Vector2( posX , posY );
	//	if( GetComponent<SpriteRenderer>() != null ) {
	//		spriteRenderer = GetComponent<SpriteRenderer>();
	//	} else {
	//		gameObject.AddComponent<SpriteRenderer>();
	//		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	//	}
	//	switch( (int)typeOfFloor ) {
	//		case 0:
	//			spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Floor" );
	//			break;
	//		case 1:
	//			spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Damage" );
	//			break;
	//		case 7:
	//			spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Floor" );
	//			break;
	//		case 8:
	//			spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Water" );
	//			break;
	//		case 9:
	//			spriteRenderer.sprite = Resources.Load<Sprite>( "Textures/Grass" );
	//			break;

	//	}
	//}
	////Metodo que debe devolver el objeto clickeado para despues poder mandar esa informacion a las unidades y moverse respectivamente 
	//public void Click() {
	//}

}
