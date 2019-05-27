using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Grid))]
public class Board : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab = default;
    [SerializeField]
    private Transform tilesContainer = default;


    #region Testing
    // Temporary, for testing
    [Header("Testing values")]

    public Vector2Int startSize = Vector2Int.one;

    //public Unit unitPrefab;
    //public Vector2Int[] unitPositions = new Vector2Int[0];
    #endregion


    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
    public Tile this[Vector2Int position] => tiles[position];


    private Grid grid;


    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        CreateBoard(startSize);

        /*
        foreach (Vector2Int position in unitPositions)
        {
            Debug.Log($"");
            Unit newUnit = Instantiate(unitPrefab);
            GetTile(position).CurrentUnit = newUnit;
        }
        */
    }

    private void CreateBoard(Vector2Int size)
    {
        for (int y = -size.y; y < size.y; y++)
        {
            for (int x = -size.x; x < size.x; x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                Tile newTile = Instantiate(tilePrefab, tilesContainer);
                newTile.transform.localPosition = grid.GetCellCenterLocal(new Vector3Int(x, y, 0));
                newTile.Coordinates = position;

                newTile.name = $"Tile ({x}, {y})";

                tiles.Add(position, newTile);
            }
        }
    }
}
