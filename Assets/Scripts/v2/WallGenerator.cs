using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class WallGenerator 
{
	public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
	{
		HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
		HashSet<Vector2Int> cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);

		CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
		CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
	}

	private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
	{
		foreach (Vector2Int pos in cornerWallPositions)
		{
			string nearTile_binarycode = "";
			foreach (Vector2Int direction in Direction2D.eightDirectionsList)
			{
				Vector2Int nearTile_pos = pos + direction;
				if (floorPositions.Contains(nearTile_pos))
					nearTile_binarycode += "1";
				else
					nearTile_binarycode += "0";
			}

			tilemapVisualizer.PaintSingleCornerWall(pos, nearTile_binarycode);
		}
		
	}

	private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
	{
		foreach (Vector2Int pos in basicWallPositions)
		{
			string nearTile_binarycode = "";
			foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
			{
				Vector2Int nearTile_pos = pos + direction;
				if (floorPositions.Contains(nearTile_pos))
					nearTile_binarycode += "1";
				else
					nearTile_binarycode += "0";
			}

			tilemapVisualizer.PaintSingleBaiscWall(pos, nearTile_binarycode);
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
