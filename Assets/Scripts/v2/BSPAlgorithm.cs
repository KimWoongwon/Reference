using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

static public class BSPAlgorithm
{
	public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
	{
		HashSet<Vector2Int> path = new HashSet<Vector2Int>();

		path.Add(startPos);
		Vector2Int prePos = startPos;

		for (int i = 0; i < walkLength; i++)
		{
			Vector2Int newPos = prePos + Direction2D.GetRandomDirection();
			path.Add(newPos);
			prePos = newPos;
		}

		return path;
	}

	public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
	{
		List<Vector2Int> corridor = new List<Vector2Int>();
		Vector2Int direction = Direction2D.GetRandomDirection();
		Vector2Int curPos = startPos;
		corridor.Add(curPos);

		for (int i = 0; i < corridorLength; i++)
		{
			curPos += direction;
			corridor.Add(curPos);
		}

		return corridor;
	}

	public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
	{
		Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
		List<BoundsInt> roomsList = new List<BoundsInt>();

		roomsQueue.Enqueue(spaceToSplit);
		while (roomsQueue.Count > 0)
		{
			BoundsInt room = roomsQueue.Dequeue();
			if(room.size.x >= minWidth && room.size.y >= minHeight)
			{
				if(Random.value < 0.5f)	
				{
					if (room.size.y >= minHeight * 2)   // split horizentally
						SplitHorizontally(minHeight, roomsQueue, room);
					else if (room.size.x >= minWidth * 2)   // split Vertically
						SplitVertically(minWidth, roomsQueue, room);
					else if (room.size.x >= minWidth && room.size.y >= minHeight)
						roomsList.Add(room);
				}
				else
				{
					if (room.size.x >= minWidth * 2)   // split Vertically
						SplitVertically(minWidth, roomsQueue, room);
					else if (room.size.y >= minHeight * 2)   // split horizentally
						SplitHorizontally(minHeight, roomsQueue, room);
					else if (room.size.x >= minWidth && room.size.y >= minHeight)
						roomsList.Add(room);
				}
			}
		}

		foreach (var room in roomsList)
		{
			Debug.DrawLine(room.min, new Vector3(room.min.x + room.size.x, room.min.y, 0), Color.green, 0.5f);
			Debug.DrawLine(new Vector3(room.min.x + room.size.x, room.min.y, 0), new Vector3(room.min.x + room.size.x, room.min.y + room.size.y, 0), Color.green, 0.5f);
			Debug.DrawLine(new Vector3(room.min.x + room.size.x, room.min.y + room.size.y, 0), new Vector3(room.min.x, room.min.y + room.size.y, 0), Color.green, 0.5f);
			Debug.DrawLine(new Vector3(room.min.x, room.min.y + room.size.y, 0), room.min, Color.green, 0.5f);
		}

		return roomsList;
	}

	private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
	{
		var xSplit = Random.Range(1, room.size.x);
		BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
		BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
										new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
		roomsQueue.Enqueue(room1);
		roomsQueue.Enqueue(room2);
	}

	private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
	{
		var ySplit = Random.Range(1, room.size.y); // (minHeight, room.size.y - minHeight)
		BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
		BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
										new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
		roomsQueue.Enqueue(room1);
		roomsQueue.Enqueue(room2);
	}
}

static public class Direction2D
{
	static public List<Vector2Int> DirectionList = new List<Vector2Int>
	{
		new Vector2Int(0, 1),	//UP
		new Vector2Int(1, 0),	//RIGHT
		new Vector2Int(0, -1),	//DOWN
		new Vector2Int(-1, 0)	//LEFT
	};

	static public Vector2Int GetRandomDirection()
	{
		return DirectionList[UnityEngine.Random.Range(0, DirectionList.Count)];
	}
}
