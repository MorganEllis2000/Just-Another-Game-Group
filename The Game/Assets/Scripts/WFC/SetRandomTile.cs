using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class SetRandomTile : MonoBehaviour {
    public Tile[] highlightTile;
    public Tilemap highlightMap;

    private Vector3Int previous;

    public int x;
    public int y;

    //// do late so that the player has a chance to move in update if necessary
    //private void LateUpdate() {
    //    // get current grid location
    //    Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
    //    // add one in a direction (you'll have to change this to match your directional control)
    //    currentCell.x += 1;

    //    // if the position has changed
    //    if (currentCell != previous) {
    //        // set the new tile
    //        highlightMap.SetTile(currentCell, highlightTile[UnityEngine.Random.Range(0, highlightTile.Length)]);

    //        // erase previous
    //        highlightMap.SetTile(previous, null);

    //        // save the new position for next frame
    //        previous = currentCell;
    //    }
    //}

    private void Update() {
        if (Input.GetMouseButton(4)) {
            CreateRandomGrid();
        }
    }

    public void CreateRandomGrid() {
        for (int col = 0; col < y; col++) {
            for (int row = 0; row < x; row++) {
                Vector3Int currentCell = highlightMap.WorldToCell(new Vector3(x, y, 0));

                // if the position has changed
                if (currentCell != previous) {
                    // set the new tile
                    highlightMap.SetTile(currentCell, highlightTile[UnityEngine.Random.Range(0, highlightTile.Length)]);

                    // erase previous
                    highlightMap.SetTile(previous, null);

                    // save the new position for next frame
                    previous = currentCell;
                }
            }
        }
    }
}
