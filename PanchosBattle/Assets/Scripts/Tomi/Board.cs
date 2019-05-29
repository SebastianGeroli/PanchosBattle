using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Grid))]
public class Board : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab = default;
    [SerializeField]
    private Transform tilesContainer = default;

    //tiles es un diccionario que contiene cad tile con sus coordenadas
    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
    //metodo para obtener una tile a partir de sus coordenadas?
    public Tile this[Vector2Int coordinates] => tiles[coordinates];

    private Grid grid;

    public bool HasTile(Vector2Int coordinates) => tiles.ContainsKey(coordinates);


    private void Awake()
    {
        grid = GetComponent<Grid>();
    }


    public void CreateBoard(IEnumerable<Vector2Int> coordinates)
    {
        //recibe un cjto de coordenadas y crea tiles en base a esa lista
        foreach (Vector2Int coordinate in coordinates)
            CreateTile(coordinate);
    }

    public Tile CreateTile(Vector2Int coordinates)
    {
        if (HasTile(coordinates))
            return this[coordinates];

        Tile newTile = Instantiate(tilePrefab, tilesContainer);
        //obtiene las coordenadas centrales de una celda en un espacio local. Para que?
        newTile.transform.localPosition = grid.GetCellCenterLocal(new Vector3Int(coordinates.x, coordinates.y, 0));
        newTile.Coordinates = coordinates;
        
        newTile.name = $"Tile ({coordinates.x}, {coordinates.y})";

        //esto es re ilegal 
        tiles.Add(coordinates, newTile);
        //por que retorna tiles en vez de hacerlo un void si las añade arriba de esta linea?
        return newTile;
    }
}
