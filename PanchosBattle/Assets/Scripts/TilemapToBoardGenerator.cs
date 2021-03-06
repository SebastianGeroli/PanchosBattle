﻿using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent( typeof( Tilemap ) )]
public class TilemapToBoardGenerator:MonoBehaviour {
	[Serializable]
	private struct TileToTileType {
		public TileBase tile;
		public Tile.Info info;

		public TileToTileType( TileBase tile , Tile.Info info ) {
			this.tile = tile ?? throw new ArgumentNullException( nameof( tile ) );
			this.info = info;
		}
	}

	[SerializeField]
	private Board fillBoard = default;

	[SerializeField]
	private bool destroyOnGeneration = true;



	private void Start() {
		GenerateFromTilemap();
	}

	private void GenerateFromTilemap() {
		// Fill the tiles with the corresponding tile types
		Tilemap tilemap = GetComponent<Tilemap>();

		Debug.Log( "Creating map from tilemap..." );
		var stopwatch = System.Diagnostics.Stopwatch.StartNew();
		BoundsInt bounds = tilemap.cellBounds;
		for( int y = bounds.yMin; y < bounds.yMax; y++ ) {
			for( int x = bounds.xMin; x < bounds.xMax; x++ ) {
				Vector3Int position = new Vector3Int( x , y , 0 );
				if( tilemap.HasTile( position ) ) {
					// Try to get the corresponding tile info depending on the tilemap tile
					if( tilemap.GetTile( position ) is TilemapTileInfo tileWithInfo )
						fillBoard.CreateTile( new Vector2Int( x , y ) , tileWithInfo.TileInfo );
					else
						fillBoard.CreateTile( new Vector2Int( x , y ) );
				}
			}
		}
		stopwatch.Stop();
		Debug.Log( $"Created map with bounds ({fillBoard.TileBounds}) in {stopwatch.Elapsed.TotalMilliseconds} ms" +
			$" from a tilemap with bounds ({bounds})." );

		if( destroyOnGeneration )
			Destroy( gameObject );
		else
			gameObject.SetActive( false );
	}
}