using UnityEngine;


public class Tile:MonoBehaviour {
	[System.Serializable]
	public struct Info {
		public Type type;
		public int spawneableForPlayerNumber;

		public Info( Type type , int spawneableForPlayerNumber ) {
			this.type = type;
			this.spawneableForPlayerNumber = spawneableForPlayerNumber;
		}
	}

	public enum Type { Walkable, NonWalkable }


	[SerializeField]
	private Type tileType = Type.Walkable;
	public Type TileType { get => tileType; set => tileType = value; }

	[SerializeField]
	private int spawneableForPlayerNumber = 0;
	public int SpawneableForPlayerNumber { get => spawneableForPlayerNumber; set => spawneableForPlayerNumber = value; }
	public bool CanSpawn => spawneableForPlayerNumber > 0;

	[SerializeField]
	private SpriteRenderer highlightSprite = default;
	public Color HighlightColor { get => highlightSprite.color; set => highlightSprite.color = value; }
	public SpriteRenderer HighlightSprite { get => highlightSprite; set => highlightSprite = value; }

	public Board Board { get; internal set; }
	public Vector2Int Coordinates { get; internal set; }


	public void HideHighlight() => HighlightColor = Color.clear;

}