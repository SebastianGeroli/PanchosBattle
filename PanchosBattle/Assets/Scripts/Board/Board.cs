using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Grid))]
public class Board : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab = default;
    [SerializeField]
    private Transform tilesContainer = default;


    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    private ReadOnlyDictionary<Vector2Int, Tile> tilesDictionary = null;
    public ReadOnlyDictionary<Vector2Int, Tile> TilesDictionary =>
        tilesDictionary ?? (tilesDictionary = new ReadOnlyDictionary<Vector2Int, Tile>(tiles));

    public int UsedTilesCount => tiles.Count;
    public Tile this[Vector2Int coordinates] => HasTile(coordinates) ? tiles[coordinates] : null;


    private Grid grid;


    public bool HasTile(Vector2Int coordinates) => tiles.ContainsKey(coordinates);


    public Vector2Int GetTileCoordinatesFromWorldPoint(Vector2 position) => (Vector2Int)grid.WorldToCell(position);

    public Tile GetTileFromWorldPoint(Vector2 position) => this[GetTileCoordinatesFromWorldPoint(position)];



    private void Awake()
    {
        grid = GetComponent<Grid>();
    }


    public void CreateBoard(IEnumerable<Vector2Int> coordinates)
    {
        foreach (Vector2Int coordinate in coordinates)
            CreateTile(coordinate);
    }

    public Tile CreateTile(Vector2Int coordinates)
    {
        if (HasTile(coordinates))
            return this[coordinates];

        Tile newTile = Instantiate(tilePrefab, tilesContainer);
        newTile.name = $"Tile ({coordinates.x}, {coordinates.y})";
        newTile.transform.localPosition = grid.GetCellCenterLocal(new Vector3Int(coordinates.x, coordinates.y, 0));

        newTile.Board = this;
        newTile.Coordinates = coordinates;

        tiles.Add(coordinates, newTile);
        return newTile;
    }

    public Tile CreateTile(Vector2Int coordinates, Tile.Info info)
    {
        Tile newTile = CreateTile(coordinates);

        newTile.TileType = info.type;
        newTile.SpawneableForPlayerNumber = info.spawneableForPlayerNumber;

        return newTile;
    }
}
