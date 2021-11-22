using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : FloorGenerator
{
	[SerializeField] private int minRoomWidth = 4;
	[SerializeField] private int minRoomHeight = 4;

	[SerializeField] private int dungeonWidth = 20;
	[SerializeField] private int dungeonHeight = 20;

	[SerializeField, Range(0, 10)] private int offset = 1;
	[SerializeField] private bool RandomWalk = false;

	protected override void RunProceduralGeneration()
	{
		CreateRooms();
	}

	private void CreateRooms()
	{
		var roomsList =	BasicAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

		HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
		if (RandomWalk)
			floor = CreateRoomsRandomly(roomsList);
		else
			floor = CreateSimpleRooms(roomsList);

		List<Vector2Int> roomCenters = new List<Vector2Int>();
		foreach (var room in roomsList)
			roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));

		HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
		floor.UnionWith(corridors);

		tilemapVisualizer.PaintFloorTiles(floor);
		WallGenerator.CreateWalls(floor, tilemapVisualizer);
	}

	private Vector2Int FindClosestPointTo(Vector2Int curCenter, List<Vector2Int> roomCenters)
	{
		Vector2Int closest = Vector2Int.zero;
		float distance = float.MaxValue;

		foreach (var pos in roomCenters)
		{
			float curDist = Vector2.Distance(pos, curCenter);
			if (curDist < distance)
			{
				distance = curDist;
				closest = pos;
			}
		}

		return closest;
	}

	private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
	{
		HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
		for (int i = 0; i < roomsList.Count; i++)
		{
			var roomBounds = roomsList[i];
			var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
			var roomFloor = RunRandomWalk(randomWalkParams, roomCenter);
			foreach (var pos in roomFloor)
			{
				if (pos.x >= (roomBounds.xMin + offset) && pos.x <= (roomBounds.xMax - offset) && pos.y >= (roomBounds.yMin - offset) && pos.y <= (roomBounds.yMax - offset))
					floor.Add(pos);
			}
		}

		return floor;
	}

	private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
	{
		HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
		foreach (var room in roomsList)
		{
			for (int col = offset; col < room.size.x - offset; col++)
			{
				for (int row = offset; row < room.size.y - offset; row++)
				{
					Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
					floor.Add(position);
				}
			}
		}

		return floor;
	}

	private HashSet<Vector2Int> CreateCorridor(Vector2Int curCenter, Vector2Int destination)
	{
		HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
		var position = curCenter;
		corridor.Add(position);

		while (position.y != destination.y)
		{
			if (destination.y > position.y)
				position += Vector2Int.up;
			else if (destination.y < position.y)
				position += Vector2Int.down;
			corridor.Add(position);
		}
		while (position.x != destination.x)
		{
			if (destination.x > position.x)
				position += Vector2Int.right;
			else if (destination.x < position.x)
				position += Vector2Int.left;
			corridor.Add(position);
		}
		return corridor;
	}

	private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
	{
		HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
		var curCenter = roomCenters[Random.Range(0, roomCenters.Count)];
		roomCenters.Remove(curCenter);

		while (roomCenters.Count > 0)
		{
			Vector2Int closest = FindClosestPointTo(curCenter, roomCenters);
			roomCenters.Remove(closest);
			HashSet<Vector2Int> newCorridor = CreateCorridor(curCenter, closest);
			curCenter = closest;
			corridors.UnionWith(newCorridor);
		}
		return corridors;
	}
}
