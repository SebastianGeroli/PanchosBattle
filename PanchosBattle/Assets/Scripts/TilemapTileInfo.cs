using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu( fileName = "New Board Tile" , menuName = "Board Tile" )]
public class TilemapTileInfo:TileBase {
	[SerializeField]
	private Sprite sprite = default;

	[SerializeField]
	private Color color = Color.white;

	[SerializeField]
	private Vector3 scale = Vector3.one;

	[SerializeField]
	private Tile.Info tileInfo = default;
	public Tile.Info TileInfo => tileInfo;


	public override void GetTileData( Vector3Int position , ITilemap tilemap , ref TileData tileData ) {
		tileData.sprite = sprite;
		tileData.flags = TileFlags.LockAll;
		tileData.color = color;
		tileData.transform = Matrix4x4.Scale( scale );
	}
}
