using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] float scale = 20f, offX = 100f, offY = 100f, rate = 5f;
    [SerializeField] int width = 256, length = 256, height = 10;



    // Start is called before the first frame update
    private void Start()
    {
        offX = Random.Range(1f, 999f);
        offY = Random.Range(1f, 999f);
    }
    void Update()
    {
        offX += Time.deltaTime * rate;
        Terrain perlingField = GetComponent<Terrain>();
        perlingField.terrainData = Perlinify(perlingField.terrainData);
    }

    TerrainData Perlinify(TerrainData data)
    {
        data.heightmapResolution = width + 1;

        

        data.size = new Vector3(width, height, length);

        data.SetHeights(0, 0, GenerateAltitudes());

        return data;
    }

    float[,] GenerateAltitudes()
    {
        float[,] altitudes = new float[width, length];
        for(int x= 0; x<width; x++)
        {
            for(int y =0; y<length; y++)
            {
                altitudes[x, y] = NewAltValue(x, y);
            }
        }
        return altitudes;
    }

    float NewAltValue(int x, int y)
    {
        float xPos = (float)x / width * scale + offX;
        float yPos = (float)y / length * scale + offY;
        return Mathf.PerlinNoise(xPos, yPos);
    }
}
