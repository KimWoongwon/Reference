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
	int maxHeight = 100;
	int maxWidth = 100;
	int minHeight = 15;
	int minWidth = 15;

	public GameObject sprite;
	public GameObject Room;

	Tree<GameObject> MapList = new Tree<GameObject>();
	public List<GameObject> SpriteList = new List<GameObject>();
	Vector2 OriginalSize;

	private void Start()
	{
		Create_Map_Test();
		Initialize_Room();
	}

	void Create_Map_Test()
	{
		GameObject temp = Instantiate(sprite);
		temp.transform.SetParent(gameObject.transform);

		OriginalSize = temp.GetComponent<SpriteRenderer>().size;
		Vector2 Size = OriginalSize * maxHeight;
		temp.GetComponent<SpriteRenderer>().size = Size;

		MapList.Root = new Node<GameObject>(temp);
		Splite_Sprite(MapList.Root);
	}

	void Splite_Sprite(Node<GameObject> obj)	// flag == true >> Horizental Cut
	{
		if (obj == null)
			return;

		Debug.Log("Active");

		Vector2 ObjSize = obj.Data.GetComponent<SpriteRenderer>().size;

		int flag;
		if (ObjSize.x > minWidth * 2 && ObjSize.y > minHeight * 2)
			flag = Random.Range(0, 2);
		else if (ObjSize.x <= minWidth * 2)
			flag = 1;
		else if (ObjSize.y <= minHeight * 2)
			flag = 0;
		else
			return;

		GameObject[] temp = new GameObject[2];

		if (flag == 1)	
		{
			if (ObjSize.y <= minHeight * 2)
				return;

			int RandomCut = Random.Range((int)(minHeight * OriginalSize.y), (int)(ObjSize.y - (minHeight * OriginalSize.y) - 1));

			temp[0] = Instantiate(obj.Data);
			temp[0].transform.SetParent(gameObject.transform);

			Vector2 size = temp[0].GetComponent<SpriteRenderer>().size;
			size.y = RandomCut;
			temp[0].GetComponent<SpriteRenderer>().size = size;

			temp[1] = Instantiate(obj.Data);
			temp[1].transform.SetParent(gameObject.transform);

			Vector3 Pos = temp[1].transform.position;
			Pos.y -= size.y / OriginalSize.y;
			temp[1].transform.position = Pos;

			size = temp[1].GetComponent<SpriteRenderer>().size;
			size.y = ObjSize.y - RandomCut;
			temp[1].GetComponent<SpriteRenderer>().size = size;
		}

		if (flag == 0)
		{
			if (ObjSize.x <= minWidth * 2)
				return;

			int RandomCut = Random.Range((int)(minWidth * OriginalSize.x), (int)(ObjSize.x - (minWidth * OriginalSize.x) - 1));

			temp[0] = Instantiate(obj.Data);
			temp[0].transform.SetParent(gameObject.transform);

			Vector2 size = temp[0].GetComponent<SpriteRenderer>().size;
			size.x = RandomCut;
			temp[0].GetComponent<SpriteRenderer>().size = size;

			temp[1] = Instantiate(obj.Data);
			temp[1].transform.SetParent(gameObject.transform);

			Vector3 Pos = temp[1].transform.position;
			Pos.x += size.x / OriginalSize.x;
			temp[1].transform.position = Pos;

			size = temp[1].GetComponent<SpriteRenderer>().size;
			size.x = ObjSize.x - RandomCut;
			temp[1].GetComponent<SpriteRenderer>().size = size;
		}

		//Destroy(obj.Data);
		obj.Data.SetActive(false);
		obj.Left = new Node<GameObject>(temp[0]);
		obj.Right = new Node<GameObject>(temp[1]);

		Splite_Sprite(obj.Left);
		Splite_Sprite(obj.Right);

	}

	void Initialize_Room()
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject temp = gameObject.transform.GetChild(i).gameObject;
			if (temp.activeSelf == true)
				SpriteList.Add(temp);
			else
				Destroy(temp);
		}

		GameObject RoomParents = GameObject.Find("Rooms");

		for (int i = 0; i < SpriteList.Count; i++)
		{
			Vector2 SpriteSize = SpriteList[i].GetComponent<SpriteRenderer>().size;

			GameObject TempRoom = Instantiate(Room);
			TempRoom.transform.SetParent(RoomParents.transform);
			TempRoom.transform.position = SpriteList[i].transform.position;

			Vector2 TempSize = TempRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().size;

			Vector2 Pos = TempRoom.transform.position;
			Pos.x += TempSize.x * 0.5f;
			Pos.y -= TempSize.y * 0.5f;
			TempRoom.transform.position = Pos;

			Pos = TempRoom.transform.position;
			Pos.x += Random.Range(0.0f, SpriteSize.x - TempSize.x);
			Pos.y -= Random.Range(0.0f, SpriteSize.y - TempSize.y);
			TempRoom.transform.position = Pos;
		}

		foreach (var item in SpriteList)
		{
			//item.SetActive(false);
		}



	}
}
