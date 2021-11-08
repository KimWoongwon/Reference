using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class WallGenerator 
{
	public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
	{
		HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.DirectionList);
		foreach (Vector2Int pos in basicWallPositions)
		{
			tilemapVisualizer.PaintSingleBaiscWall(pos);
		}
	}

	private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
	{
		HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
		foreach (Vector2Int pos in floorPositions)
		{
			foreach (Vector2Int direction in directions)
			{
				Vector2Int neighbourPosition = pos + direction;
				if (floorPositions.Contains(neighbourPosition) == false)
					wallPositions.Add(neighbourPosition);
			}
		}
		return wallPositions;
	}
}
