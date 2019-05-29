using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapToBoardGenerator : MonoBehaviour
{
    [Serializable]
    private struct TileToTileType
    {
        public TileBase tile;
        public Tile.Info info;

        public TileToTileType(TileBase tile, Tile.Info info)
        {
            this.tile = tile ?? throw new ArgumentNullException(nameof(tile));
            this.info = info;
        }
    }

    private static readonly TileBase[] tiles = new TileBase[512];

    [SerializeField]
    private Board fillBoard = default;

    [SerializeField]
    private bool destroyOnGeneration = true;

    [Space]

    [SerializeField]
    private TileToTileType[] tileToTypeRemapping = new TileToTileType[0];



    private void Start()
    {
        GenerateFromTilemap();
    }

    private void GenerateFromTilemap()
    {
        // First create a remapping dictionary to transform a tile to its corresponding type
        var remappingDictionary = new System.Collections.Generic.Dictionary<TileBase, Tile.Info>();
        foreach (TileToTileType remap in tileToTypeRemapping)
        {
            remappingDictionary.Add(remap.tile, remap.info);
        }

        // Fill the tiles with the corresponding tile types
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(position))
                {
                    // Try to get the corresponding tile type depending on the tilemap tile
                    TileBase tile = tilemap.GetTile(position);
                    Tile.Info info = remappingDictionary.ContainsKey(tile) ? remappingDictionary[tile] : default;

                    fillBoard.CreateTile(new Vector2Int(x, y), info);
                }
            }
        }

        if (destroyOnGeneration)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
