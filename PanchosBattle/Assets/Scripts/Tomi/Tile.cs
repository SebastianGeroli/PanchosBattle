using UnityEngine;


public class Tile : MonoBehaviour
{
    [System.Serializable]
    public struct Info
    {
        public Type type;
        public int spawneableForPlayerNumber;

        public Info(Type type, int spawneableForPlayerNumber)
        {
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

    public Vector2Int Coordinates { get; internal set; }

}
