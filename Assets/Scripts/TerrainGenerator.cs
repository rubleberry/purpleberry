using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;       
    public int height = 256;
    public float scale = 30f;
    public float heightMultiplier = 3f;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private Terrain terrain;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, heightMultiplier, height);
        int layers = terrainData.alphamapLayers;

        float[,] altitude = new float[width, height];
        float[,,] splatmapData = new float[height, width, layers];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                altitude[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
                splatmapData[x, y, 0] = 1;
                splatmapData[x, y, 1] = 1;
            }
        }

        for (int x = 0; x < width; x++)
        {
            altitude[x, 0] += 1;
            altitude[x, height - 1] += 1;
            splatmapData[x, 0, 0] = 0;
            splatmapData[x, height - 1, 1] = 1;
        }

        for (int y = 0; y < height; y++)
        {
            altitude[0, y] += 1;
            altitude[width - 1, y] += 1;
            splatmapData[0, y, 0] = 0;
            splatmapData[width - 1, y, 1] = 1;
        }

        Debug.Log(altitude[0,0]);

        Debug.Log(altitude[7, 7]);
        altitude[7, 7] *= 2;
        altitude[7, 8] *= 2;
        altitude[8, 7] *= 2;
        altitude[8, 8] *= 2;

        splatmapData[7, 7, 0] = 0;
        splatmapData[7, 8, 0] = 0;
        splatmapData[8, 7, 0] = 0;
        splatmapData[8, 8, 0] = 0;
        splatmapData[7, 7, 1] = 1;
        splatmapData[7, 8, 1] = 1;
        splatmapData[8, 7, 1] = 1;
        splatmapData[8, 8, 1] = 1;


        terrainData.SetHeights(0, 0, altitude);
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

}

