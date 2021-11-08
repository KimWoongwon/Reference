using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase floorTile, wallTop;
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

	internal void PaintSingleBaiscWall(Vector2Int pos)
    {
        PaintSingleTile(wallTilemap, wallTop, pos);
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
}
