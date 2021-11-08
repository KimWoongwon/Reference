using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		return DirectionList[Random.Range(0, DirectionList.Count)];
	}
}
