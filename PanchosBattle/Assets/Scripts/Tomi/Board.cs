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
    public Tile this[Vector2Int coordinates] => tiles[coordinates];


    private Grid grid;


    public bool HasTile(Vector2Int coordinates) => tiles.ContainsKey(coordinates);


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
        newTile.transform.localPosition = grid.GetCellCenterLocal(new Vector3Int(coordinates.x, coordinates.y, 0));
        newTile.Coordinates = coordinates;

        newTile.name = $"Tile ({coordinates.x}, {coordinates.y})";

        tiles.Add(coordinates, newTile);
        return newTile;
    }
}
