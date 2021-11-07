using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateMap : MonoBehaviour
{
    public int BoardRows, BoardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject FloorTile;
    public GameObject CorridorTile;

    [SerializeField]
    private GameObject[,] boardPositionsFloor;
    
    public class DungeonTree
    {
        public DungeonTree left, right;
        public Rect rect;
        public Rect room = new Rect(-1, -1, 0, 0);

        public List<Rect> corridors = new List<Rect>();

        public GameObject corridorParents = null;
        bool toleft = false;
        bool toright = false;

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

        public Rect GetRoom(out int id)
        {
            if (isLeaf())
            {
                id = DebugID;
                return room;
            }

            if (left != null)
            {
                Rect lroom = left.GetRoom(out id);
                if (lroom.x != -1)
                    return lroom;
            }
            if (right != null)
            {
                Rect rroom = right.GetRoom(out id);
                if (rroom.x != -1)
                    return rroom;
            }

            id = -1;
            return new Rect(-1, -1, 0, 0);

        }

        public void CreateRoom()
        {
            if (left != null)
                left.CreateRoom();
            if (right != null)
                right.CreateRoom();

            if (isLeaf())
            {
                int rWidth = (int)Random.Range(rect.width / 2, rect.width - 2);
                int rHeight = (int)Random.Range(rect.height / 2, rect.height - 2);
                int rPosX = (int)Random.Range(1, rect.width - rWidth - 1);
                int rPosY = (int)Random.Range(1, rect.height - rHeight - 1);

                room = new Rect(rect.x + rPosX, rect.y + rPosY, rWidth, rHeight);

                GameObject parent = new GameObject($"Room_{DebugID}");
                parent.transform.position = new Vector2(room.x, room.y);
                parent.transform.SetParent(GameObject.Find("Rooms").transform);
#if UNITY_EDITOR
                Debug.Log($"Create Room Image {room} in DungeonTree {DebugID} : {rect}");
                Debug.DrawLine(new Vector3(room.x, room.y), new Vector3(room.x + room.width, room.y), Color.green, 100);
                Debug.DrawLine(new Vector3(room.x + room.width, room.y), new Vector3(room.x + room.width, room.y + room.height), Color.green, 100);
                Debug.DrawLine(new Vector3(room.x + room.width, room.y + room.height), new Vector3(room.x, room.y + room.height), Color.green, 100);
                Debug.DrawLine(new Vector3(room.x, room.y + room.height), new Vector3(room.x, room.y), Color.green, 100);

                Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y), Color.red, 100);
                Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y), new Vector3(rect.x + rect.width, rect.y + rect.height), Color.red, 100);
                Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x, rect.y + rect.height), Color.red, 100);
                Debug.DrawLine(new Vector3(rect.x, rect.y + rect.height), new Vector3(rect.x, rect.y), Color.red, 100);
#endif

            }

            if (right != null && left != null)
            {
                CreateCorridorBetween(left, right);
                
            }
                

        }

        public void CreateCorridorBetween(DungeonTree left, DungeonTree right)
        {
            int lid, rid;
            Rect lroom = left.GetRoom(out lid);
            Rect rroom = right.GetRoom(out rid);

            corridorParents = new GameObject($"Corridor_{lid}_to_{rid}");
            corridorParents.transform.SetParent(GameObject.Find("Corridors").transform);
#if UNITY_EDITOR
            Debug.Log($"Create Corridor Between {lid} ( {lroom} ) & {rid} ( {rroom} )");
#endif
            Vector2 lpoint = new Vector2((int)Random.Range(lroom.x + 1, lroom.xMax - 1), (int)Random.Range(lroom.y + 1, lroom.yMax - 1));
            Vector2 rpoint = new Vector2((int)Random.Range(rroom.x + 1, rroom.xMax - 1), (int)Random.Range(rroom.y + 1, rroom.yMax - 1));


            if (lpoint.x > rpoint.x)
            {
                Vector2 temp = lpoint;
                lpoint = rpoint;
                rpoint = temp;
            }

            int w = (int)(lpoint.x - rpoint.x);
            int h = (int)(lpoint.y - rpoint.y);

#if UNITY_EDITOR
            Debug.Log($"lpoint : {lpoint}, rpoint : {rpoint}, w : {w}, h : {h}");
#endif
            if (w != 0)
            {
                if (Random.Range(0, 1) > 2)
                {
                    corridors.Add(new Rect(lpoint.x, lpoint.y, Mathf.Abs(w) + 1, 1));

                    if (h < 0)
                        corridors.Add(new Rect(rpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                    else
                        corridors.Add(new Rect(rpoint.x, lpoint.y, 1, -Mathf.Abs(h)));
                }
                else
                {
                    if (h < 0)
                        corridors.Add(new Rect(lpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                    else
                        corridors.Add(new Rect(lpoint.x, rpoint.y, 1, Mathf.Abs(h)));

                    corridors.Add(new Rect(lpoint.x, rpoint.y, Mathf.Abs(w) + 1, 1));
                }
            }
            else
            {
                if (h < 0)
                    corridors.Add(new Rect((int)lpoint.x, (int)lpoint.y, 1, Mathf.Abs(h)));
                else
                    corridors.Add(new Rect((int)rpoint.x, (int)rpoint.y, 1, Mathf.Abs(h)));
            }

            

#if UNITY_EDITOR
            Debug.Log($"Corridors : ");
            foreach (Rect corridor in corridors)
                Debug.Log($"corridor : {corridor}");
#endif
        }

    }

    /// <summary>
    /// 방을 나누기 위한 tree의 split을 실행시키는 재귀함수
    /// </summary>
    /// <param name="dungeonTree"></param>
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
                    Debug.Log($"Split DungeonTree Success {dungeonTree.left.DebugID} : {dungeonTree.left.rect}\n" +
                                $"{dungeonTree.right.DebugID} : {dungeonTree.right.rect}");
#endif
                    CreateBSP(dungeonTree.left);
                    CreateBSP(dungeonTree.right);
                }

            }
        }

    }

    /// <summary>
    /// 재귀함수
    /// FloorTile을 Room 사이즈만큼 루프를 돌면서 1개씩 생성
    /// FloorTile을 여러개 준비하고 Random으로 여러 형태의 방 구조 제작가능
    /// Tile을 배치하고 나면 재귀함수로 인해 left와 right까지 Draw
    /// </summary>
    /// <param name="dungeonTree"></param>
    public void DrawRoom(DungeonTree dungeonTree)
    {
        if (dungeonTree == null)
            return;

        if (dungeonTree.isLeaf())
        {
            for (int i = (int)dungeonTree.room.x; i < dungeonTree.room.xMax; i++)
            {
                for (int j = (int)dungeonTree.room.y; j < dungeonTree.room.yMax; j++)
                {
                    GameObject temp = Instantiate(FloorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    temp.name = $"{i} , {j}";
                    temp.transform.SetParent(GameObject.Find($"Room_{dungeonTree.DebugID}").transform);
                    boardPositionsFloor[i, j] = temp;
                }

            }
        }
        else 
        {
            DrawRoom(dungeonTree.left);
            DrawRoom(dungeonTree.right);
        }
    }
    void DrawCorridors(DungeonTree dungeonTree)
    {
        if (dungeonTree == null)
            return;

        DrawCorridors(dungeonTree.left);
        DrawCorridors(dungeonTree.right);


        foreach (Rect corridor in dungeonTree.corridors)
        {
            for (int i = (int)corridor.x; i < corridor.xMax; i++)
            {
                for (int j = (int)corridor.y; j < corridor.yMax; j++)
                {
                    if (boardPositionsFloor[i, j] == null)
                    {
                        GameObject temp = Instantiate(CorridorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        temp.transform.SetParent(dungeonTree.corridorParents.transform);
                        boardPositionsFloor[i, j] = temp;
                    }
                }
            }
        }
    }



    public void Start()
    {
        DungeonTree Root = new DungeonTree(new Rect(0, 0, BoardRows, BoardColumns));
        boardPositionsFloor = new GameObject[BoardRows, BoardColumns];
        CreateBSP(Root);
        Root.CreateRoom();

        DrawRoom(Root);
        DrawCorridors(Root);

    }
}

