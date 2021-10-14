using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
	public T Data;
	public Node<T> Left { get; set; }
	public Node<T> Right { get; set; }

	public Node(T data)
	{
		this.Data = data;
	}
}

public class Tree<T>
{
	public Node<T> Root { get; set; }
}


public class BSP : MonoBehaviour
{
	int maxHeight = 50;
	int maxWidth = 50;
	int minHeight = 5;
	int minWidth = 5;

	public GameObject sprite;
	public bool TestFlag;

	Tree<GameObject> MapList = new Tree<GameObject>();

	Vector2 OriginalSize;

	private void Start()
	{
		Create_Map_Test();
	}

	void Create_Map_Test()
	{
		GameObject temp = Instantiate(sprite);
		temp.transform.SetParent(gameObject.transform);

		OriginalSize = temp.GetComponent<SpriteRenderer>().size;
		Vector2 Size = OriginalSize * maxHeight;
		temp.GetComponent<SpriteRenderer>().size = Size;

		MapList.Root = new Node<GameObject>(temp);
		GameObject[] tempnode = Splite_Sprite(MapList.Root.Data, TestFlag);
		MapList.Root.Left = new Node<GameObject>(tempnode[0]);
		MapList.Root.Right = new Node<GameObject>(tempnode[1]);

	}

	GameObject[] Splite_Sprite(GameObject obj, bool flag)	// flag == true >> Horizental Cut
	{
		Vector2 ObjSize = obj.GetComponent<SpriteRenderer>().size;

		GameObject[] temp = new GameObject[2];


		if (flag)	
		{
			int RandomCut = Random.Range((int)(minHeight * OriginalSize.y), (int)ObjSize.y);

			temp[0] = Instantiate(obj);
			temp[0].transform.SetParent(gameObject.transform);

			Vector2 size = temp[0].GetComponent<SpriteRenderer>().size;
			size.y = RandomCut;
			temp[0].GetComponent<SpriteRenderer>().size = size;

			temp[1] = Instantiate(obj);
			temp[1].transform.SetParent(gameObject.transform);

			Vector3 Pos = temp[1].transform.position;
			Pos.y -= size.y / OriginalSize.y;
			temp[1].transform.position = Pos;

			size = temp[1].GetComponent<SpriteRenderer>().size;
			size.y = ObjSize.y - RandomCut;
			temp[1].GetComponent<SpriteRenderer>().size = size;
		}
		else
		{
			int RandomCut = Random.Range((int)(minWidth * OriginalSize.x), (int)ObjSize.x);

			temp[0] = Instantiate(obj);
			temp[0].transform.SetParent(gameObject.transform);

			Vector2 size = temp[0].GetComponent<SpriteRenderer>().size;
			size.x = RandomCut;
			temp[0].GetComponent<SpriteRenderer>().size = size;

			temp[1] = Instantiate(obj);
			temp[1].transform.SetParent(gameObject.transform);

			Vector3 Pos = temp[1].transform.position;
			Pos.x += size.x / OriginalSize.x;
			temp[1].transform.position = Pos;

			size = temp[1].GetComponent<SpriteRenderer>().size;
			size.x = ObjSize.x - RandomCut;
			temp[1].GetComponent<SpriteRenderer>().size = size;
		}

		obj.SetActive(false);


		return temp;
	}
}
