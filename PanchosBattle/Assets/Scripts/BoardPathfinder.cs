using System.Collections.Generic;
using UnityEngine;

public class BoardPathfinder:MonoBehaviour {
	public class BreadCrumb:System.IDisposable {
		public BreadCrumb Previous { get; private set; }

		public void Dispose() { }
	}

	public enum Direction { NorthEast, East, SouthEast, SouthWest, West, NorthWest }


	public static Vector2Int GetNeighbourPosition( Vector2Int origin , Direction direction ) {
		switch( direction ) {
			case Direction.NorthEast:
				return origin + new Vector2Int( 0 , 1 );
			case Direction.East:
				return origin + new Vector2Int( 1 , 0 );
			case Direction.SouthEast:
				return origin + new Vector2Int( 0 , -1 );
			case Direction.SouthWest:
				return origin + new Vector2Int( -1 , -1 );
			case Direction.West:
				return origin + new Vector2Int( -1 , 0 );
			case Direction.NorthWest:
				return origin + new Vector2Int( -1 , 1 );
			default:
				return origin;
		}
	}
}
