using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;

    [Header("------TILES------")]
    [SerializeField] private TileBase FloorTile;
    
    [Header("-- Basic Tiles --")]
    [SerializeField] private List<TileBase> Top;
    [SerializeField] private List<TileBase> Right;
    [SerializeField] private List<TileBase> Left;
    [SerializeField] private List<TileBase> Bottom;
    [SerializeField] private List<TileBase> Full;

    [Header("-- Corner Tiles --")]
    [SerializeField] private List<TileBase> InnerDownLeft;
    [SerializeField] private List<TileBase> InnerDownRight;
    [SerializeField] private List<TileBase> DiagonalDownLeft;
    [SerializeField] private List<TileBase> DiagonalDownRight;
    [SerializeField] private List<TileBase> DiagonalUpLeft;
    [SerializeField] private List<TileBase> DiagonalUpRight;

    private TileBase getRandomTile(List<TileBase> tilelist)
    {
        return tilelist[Random.Range(0, tilelist.Count)];
    }
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, FloorTile);
    }

	private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int pos in positions)
        {
            PaintSingleTile(tilemap, tile, pos);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
    {
        Vector3Int tilePos = tilemap.WorldToCell((Vector3Int)pos);
        tilemap.SetTile(tilePos, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBaiscWall(Vector2Int pos, string binarycode)
    {
        int codeAsInt = Convert.ToInt32(binarycode, 2);

        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(codeAsInt))
            tile = getRandomTile(Top);
        else if (WallTypesHelper.wallSideRight.Contains(codeAsInt))
            tile = getRandomTile(Right);
        else if (WallTypesHelper.wallSideLeft.Contains(codeAsInt))
            tile = getRandomTile(Left);
        else if (WallTypesHelper.wallBottom.Contains(codeAsInt))
            tile = getRandomTile(Bottom);
        else if (WallTypesHelper.wallFull.Contains(codeAsInt))
            tile = getRandomTile(Full);

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, pos);
    }

    internal void PaintSingleCornerWall(Vector2Int pos, string binarycode)
    {
        int codeAsInt = Convert.ToInt32(binarycode, 2);

        TileBase tile = null;
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(codeAsInt))
            tile = getRandomTile(InnerDownLeft);
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(codeAsInt))
            tile = getRandomTile(InnerDownRight);
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(codeAsInt))
            tile = getRandomTile(DiagonalDownRight);
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(codeAsInt))
            tile = getRandomTile(DiagonalDownLeft);
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(codeAsInt))
            tile = getRandomTile(DiagonalUpRight);
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(codeAsInt))
            tile = getRandomTile(DiagonalUpLeft);
        else if (WallTypesHelper.wallFullEightDirections.Contains(codeAsInt))
            tile = getRandomTile(Full);
        else if (WallTypesHelper.wallBottomEightDirections.Contains(codeAsInt))
            tile = getRandomTile(Bottom);


        if (tile != null)
            PaintSingleTile(wallTilemap, tile, pos);
    }

    
}

