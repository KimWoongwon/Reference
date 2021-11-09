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

		List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

		CreateRoomsAtDeadEnd(deadEnds, roomPositions);

		floorPositions.UnionWith(roomPositions);

		tilemapVisualizer.PaintFloorTiles(floorPositions);
		WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
	}

	private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
	{
		foreach (Vector2Int pos in deadEnds)
		{
			if (roomFloors.Contains(pos) == false)
			{
				HashSet<Vector2Int> room = RunRandomWalk(randomWalkParams, pos);
				roomFloors.UnionWith(room);
			}
		}
	}

	private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
	{
		List<Vector2Int> deadEnds = new List<Vector2Int>();
		foreach (Vector2Int pos in floorPositions)
		{
			int neighbourCount = 0;
			foreach (var direction in Direction2D.DirectionList)
			{
				if(floorPositions.Contains(pos + direction))
					++neighbourCount;
			}
			if (neighbourCount == 1)
				deadEnds.Add(pos);
		}
		return deadEnds;
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
