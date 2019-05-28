using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapToBoardGenerator : MonoBehaviour
{
    private static readonly TileBase[] tiles = new TileBase[512];

    [SerializeField]
    private Board fillBoard = default;

    [SerializeField]
    private bool destroyOnGeneration = true;

    private void Start()
    {
        GenerateFromTilemap();
    }

    private void GenerateFromTilemap()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                    fillBoard.CreateTile(new Vector2Int(x, y));
            }
        }

        if (destroyOnGeneration)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
