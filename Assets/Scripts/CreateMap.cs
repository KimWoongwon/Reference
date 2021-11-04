using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public int BoardRows, BoardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject FloorTile;

    private GameObject[,] boardPositionsFloor;

    public class DungeonTree
    {
        public DungeonTree left, right;
        public Rect rect;
        public Rect room = new Rect(-1, -1, 0, 0);

#if UNITY_EDITOR
        public int DebugID;
        private static int DebugCounter = 0;
#endif

        public DungeonTree(Rect _rect)
        {
            rect = _rect;
#if UNITY_EDITOR
            DebugID = DebugCounter;
            ++DebugCounter;
#endif
        }

        public bool isLeaf()
        {
            return left == null && right == null;
        }

        public bool split(int minSize, int maxSize)
        {
            if (!isLeaf())
                return false;

            bool splitFlag = false;
            if (rect.width / rect.height >= 1.25f)
                splitFlag = false;
            else if (rect.height / rect.width >= 1.25f)
                splitFlag = true;
            else
                splitFlag = Random.Range(0.0f, 1.0f) >= 0.5f;

            if(Mathf.Min(rect.height, rect.width) / 2 < minSize)
            {
#if UNITY_EDITOR
                Debug.Log($"DungeonTree {DebugID} is leaf");
#endif
                return false;
            }

            if(splitFlag)
            {
                int split = Random.Range(minSize, (int)(rect.width - minSize));

                left = new DungeonTree(new Rect(rect.x, rect.y, rect.width, split));
                right = new DungeonTree(new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
            }
            else 
            {
                int split = Random.Range(minSize, (int)(rect.height - minSize));

                left = new DungeonTree(new Rect(rect.x, rect.y, split, rect.height));
                right = new DungeonTree(new Rect(rect.x + split, rect.y, rect.width - split, rect.height));
            }

            return true;

        }

        public void CreateRoom()
        {
            if (left != null)
                left.CreateRoom();
            if (right != null)
                right.CreateRoom();
            
            if(isLeaf())
            {
                int rWidth = (int)Random.Range(rect.width / 2, rect.width - 2);
                int rHeight = (int)Random.Range(rect.height / 2, rect.height - 2);
                int rPosX = (int)Random.Range(1, rect.width - rWidth - 1);
                int rPosY = (int)Random.Range(1, rect.height - rHeight - 1);

                room = new Rect(rect.x + rPosX, rect.y + rPosY, rWidth, rHeight);

#if UNITY_EDITOR
                Debug.Log($"Create Room Image {room} in DungeonTree {DebugID} : {rect}");
                Debug.DrawLine(new Vector3(room.x, room.y), new Vector3(room.x + room.width, room.y), Color.green, 100);
                Debug.DrawLine(new Vector3(room.x + room.width, room.y), new Vector3(room.x + room.width, room.y + room.height), Color.green, 100);
                Debug.DrawLine(new Vector3(room.x + room.width, room.y + room.height), new Vector3(room.x, room.y + room.height), Color.green, 100);
                Debug.DrawLine(new Vector3(room.x, room.y + room.height), new Vector3(room.x, room.y), Color.green, 100);
#endif
            }
        }

        public void DrawRoom
        

    }

    public void CreateBSP(DungeonTree dungeonTree)
    {
#if UNITY_EDITOR
        Debug.Log($"Split DungeonTree {dungeonTree.DebugID} : {dungeonTree.rect}");
#endif
        if (dungeonTree.isLeaf())
        {
            if(dungeonTree.rect.width > maxRoomSize || dungeonTree.rect.height > maxRoomSize || Random.Range(0.0f, 1.0f) > 0.25f)
            {
                if(dungeonTree.split(minRoomSize, maxRoomSize))
                {
#if UNITY_EDITOR
                    Debug.Log(  $"Split DungeonTree Success {dungeonTree.left.DebugID} : {dungeonTree.left.rect}\n" +
                                $"{dungeonTree.right.DebugID} : {dungeonTree.right.rect}");
#endif
                    CreateBSP(dungeonTree.left);
                    CreateBSP(dungeonTree.right);
                }

            }
        }

    }

    

    public void Start()
    {
        DungeonTree Root = new DungeonTree(new Rect(0, 0, BoardRows, BoardColumns));
        CreateBSP(Root);
        Root.CreateRoom();
    }
}

