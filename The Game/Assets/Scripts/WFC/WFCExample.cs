using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollaps;

public class WFCExample : MonoBehaviour
{
    public static WFCExample Instance { get; private set; }

    public Tilemap inputImage;
    public Tilemap[] inputImageArray;
    public Tilemap outputImage;
    public Tilemap[] outputImageArray;
    [Tooltip("For tiles usualy set to 1. If tile contain just a color can set to higher value")]
    public int patternSize;
    [Tooltip("How many times algorithm will try creating the output before quiting")]
    public int maxIterations;
    [Tooltip("Output image width")]
    public int outputWidth = 5;
    [Tooltip("Output image height")]
    public int outputHeight = 5;
    [Tooltip("Don't use tile frequency - each tile has equal weight")]
    public bool equalWeights = false;

    public string tileMapName = null;
    WaveFunctionCollapse wfc;

    public RoomManager currentRoom;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        GenerateWFC();
        //SaveTilemap();


    }
    private void Update() {
        //if (Input.GetKey(KeyCode.H)) {
        //    CreateWFC();
        //    CreateTilemap();
        //}


        if (Input.GetKey(KeyCode.H)) {
            for (int i = 0; i < outputImageArray.Length; i++) {
                CreateWFCArray(i);
                CreateTilemap();
            }
        }

    }

    public void CreateWFC()
    {
        wfc = new WaveFunctionCollapse(this.inputImage, this.outputImage, patternSize, this.outputWidth, this.outputHeight, this.maxIterations, this.equalWeights);
    }

    public void CreateWFCArray(int index) {
        wfc = new WaveFunctionCollapse(this.inputImageArray[UnityEngine.Random.Range(0, inputImageArray.Length - 1)], this.outputImageArray[index], patternSize, this.outputWidth, this.outputHeight, this.maxIterations, this.equalWeights);
    }

    public void CreateTilemap()
    {
        var startTime = Time.realtimeSinceStartup;
        wfc.CreateNewTileMap();
        Debug.Log("Time to generate tile map: " + (Time.realtimeSinceStartup - startTime));
    }

    public void GenerateWFC() {
        for (int i = 0; i < outputImageArray.Length; i++) {
            CreateWFCArray(i);
            CreateTilemap();
        }
    }

    public void SaveTilemap()
    {
        var output = wfc.GetOutputTileMap();
        if (output != null && tileMapName != null)
        {
            outputImage = output;
            GameObject objectToSave = outputImage.gameObject;

            //PrefabUtility.SaveAsPrefabAsset(objectToSave, "Assets/Resources/" + tileMapName + ".prefab");
            tileMapName = null;
        }
    }

    public Tile[] highlightTile;
    public Tilemap highlightMap;

    public Vector3Int previous;

    public int x;
    public int y;

    public void CreateRandomGrid() {
        highlightMap.ClearAllTiles();
        //Debug.Log("Hello");
        //for (int col = 0; col < y; col++) {
        //    for (int row = 0; row < x + 1; row++) {
        //        Vector3Int currentCell = highlightMap.WorldToCell(new Vector3(row, col, 0));
        //        highlightMap.SetTile(currentCell, highlightTile[UnityEngine.Random.Range(0, highlightTile.Length - 1)]);
        //        Debug.Log("Row: " + row + " Col: " + col);
        //    }
        //}

        for (int row = 0; row < x; ++row) {
            for (int col = 0; col < y; ++col) {
                Vector3Int currentCell = highlightMap.WorldToCell(new Vector3(row, col, 0));
                highlightMap.SetTile(currentCell, highlightTile[UnityEngine.Random.Range(0, highlightTile.Length - 1)]);
                Debug.Log("Row: " + row + " Col: " + col);
            }
        }
    }

}
