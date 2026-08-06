using UnityEngine;

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

        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }

        for (int x = 0; x < width; x++)
        {
            heights[x, 0] += 1;
            heights[x, height - 1] += 1;
        }

        for (int y = 0; y < height; y++)
        {
            heights[0, y] += 1;
            heights[height - 1, y] += 1;
        }

        Debug.Log(heights[0,0]);

        Debug.Log(heights[7, 7]);
        heights[7, 7] *= 2;
        heights[7, 8] *= 2;
        heights[8, 7] *= 2;
        heights[8, 8] *= 2;


        terrainData.SetHeights(0, 0, heights);
    }

}

