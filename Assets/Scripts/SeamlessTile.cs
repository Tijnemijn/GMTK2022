using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

// I wrote this a while ago. the lookup table is generated by a tool I made to
// create the seamless tilemaps from 5 reference tiles.

public class SeamlessTile : Tile {
    private int[] lut = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 1, 2, 3, 17, 5, 6, 7, 18, 9, 10, 11, 19, 13, 14, 15, 20, 1, 21, 3, 4, 5, 6, 7, 22, 9, 23, 11, 12, 13, 14, 15, 24, 1, 21, 3, 17, 5, 6, 7, 25, 9, 23, 11, 19, 13, 14, 15, 26, 27, 2, 3, 28, 29, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 30, 27, 2, 3, 31, 29, 6, 7, 18, 9, 10, 11, 19, 13, 14, 15, 32, 27, 21, 3, 28, 29, 6, 7, 22, 9, 23, 11, 12, 13, 14, 15, 33, 27, 21, 3, 31, 29, 6, 7, 25, 9, 23, 11, 19, 13, 14, 15, 34, 35, 36, 37, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 38, 35, 36, 37, 17, 5, 6, 7, 18, 9, 10, 11, 19, 13, 14, 15, 39, 35, 40, 37, 4, 5, 6, 7, 22, 9, 23, 11, 12, 13, 14, 15, 41, 35, 40, 37, 17, 5, 6, 7, 25, 9, 23, 11, 19, 13, 14, 15, 42, 43, 36, 37, 28, 29, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 44, 43, 36, 37, 31, 29, 6, 7, 18, 9, 10, 11, 19, 13, 14, 15, 45, 43, 40, 37, 28, 29, 6, 7, 22, 9, 23, 11, 12, 13, 14, 15, 46, 43, 40, 37, 31, 29, 6, 7, 25, 9, 23, 11, 19, 13, 14, 15 };
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite preview;

    public override void RefreshTile (Vector3Int position, ITilemap tilemap) {
        base.RefreshTile(position, tilemap);
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);
                if (HasNeighbour(tilemap, nPos)) {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }
    public override void GetTileData (Vector3Int location, ITilemap tilemap, ref TileData tileData) {
        base.GetTileData(location, tilemap, ref tileData);
        int composite = 0;

        // what was i thinking back then???
        // oh right, the order of bitwise flags made putting this in a for loop
        // not as straight forward... nevermind.
        if (!HasNeighbour(tilemap, new Vector3Int(location.x, location.y + 1, location.z))) composite += 1;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x - 1, location.y, location.z))) composite += 2;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x + 1, location.y, location.z))) composite += 4;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x, location.y - 1, location.z))) composite += 8;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x - 1, location.y + 1, location.z))) composite += 16;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x + 1, location.y + 1, location.z))) composite += 32;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x - 1, location.y - 1, location.z))) composite += 64;
        if (!HasNeighbour(tilemap, new Vector3Int(location.x + 1, location.y - 1, location.z))) composite += 128;
        tileData.sprite = sprites[lut[composite]];
    }
    private bool HasNeighbour (ITilemap tilemap, Vector3Int pos) { return tilemap.GetTile(pos) == this; }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/Seamless Tile")]
    public static void CreateTile () {
        string path = EditorUtility.SaveFilePanelInProject("Create New Seamless Tile", "New Seamless Tile", "asset", "Create New Seamless Tile", "Assets");
        if (path == "") return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SeamlessTile>(), path);
    }
#endif
}
