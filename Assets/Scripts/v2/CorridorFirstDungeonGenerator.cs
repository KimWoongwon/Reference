using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : MapGeneratior
{
	[SerializeField] private int corridorLength = 14;
	[SerializeField] private int corridorCount = 5;
	[SerializeField, Range(0.1f, 1.0f)] private float roomPercent = 0.8f;
	protected override void RunProceduralGeneration()
	{
		CorridorFirstGeneration();
	}

	private void CorridorFirstGeneration()
	{
		HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
		HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

		CreateCorridors(floorPositions, potentialRoomPositions);

		HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

		floorPositions.UnionWith(roomPositions);

		tilemapVisualizer.PaintFloorTiles(floorPositions);
		WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
	}

	private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
	{
		HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
		int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

		List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

		foreach (Vector2Int roomPos in roomsToCreate)
		{
			HashSet<Vector2Int> roomFloor = RunRandomWalk(randomWalkParams, roomPos);
			roomPositions.UnionWith(roomFloor);
		}

		return roomPositions;
	}

	private void CreateCorridors(HashSet<Vector2Int> positions, HashSet<Vector2Int> roomPositions)
	{
		Vector2Int curPos = startPosition;
		roomPositions.Add(curPos);

		for (int i = 0; i < corridorCount; i++)
		{
			List<Vector2Int> corridor = BSPAlgorithm.RandomWalkCorridor(curPos, corridorLength);
			curPos = corridor[corridor.Count - 1];
			roomPositions.Add(curPos);
			positions.UnionWith(corridor);
		}
	}
}
