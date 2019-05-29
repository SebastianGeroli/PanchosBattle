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
        public Tile this [int x, int y] => this[new Vector2Int(x, y)];
        public Tile this[Vector2Int coordinates] => tiles[coordinates];

        private BoundsInt? tileBounds = null;
        public BoundsInt TileBounds => tileBounds ?? default;


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

            EncapsulateToBounds(coordinates);

            tiles.Add(coordinates, newTile);
            return newTile;
        }

        /// <summary>
        /// To extend the bounds to that point.
        /// </summary>
        /// <param name="point">The point to extend the bounds.</param>
        private void EncapsulateToBounds(Vector2Int point)
        {
            if (tileBounds != null)
            {
                BoundsInt bounds = tileBounds.Value;
                bounds.xMin = Mathf.Min(bounds.xMin, point.x);
                bounds.xMax = Mathf.Max(bounds.xMax, point.x);
                bounds.yMin = Mathf.Min(bounds.yMin, point.y);
                bounds.yMax = Mathf.Max(bounds.yMax, point.y);
                tileBounds = bounds;
            }
            else tileBounds = new BoundsInt((Vector3Int)point, Vector3Int.zero);
        }


        public Tile CreateTile(Vector2Int coordinates, Tile.Info info)
        {
            Tile newTile = CreateTile(coordinates);

            newTile.TileType = info.type;
            newTile.SpawneableForPlayerNumber = info.spawneableForPlayerNumber;

            return newTile;
        }
    }
