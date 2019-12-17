﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    //The Source for this script can be found at: https://www.youtube.com/watch?v=vFvwyu_ZKfU
    //Put simply this script makes use of the Terrain functionality of Unity
    //We feed the terrain object new terrain data, which gives altitudes for each of the x,z coordinates of the plane terrain
    //The altitudes are generated by perlin noise based on the x,z coordinates
    [SerializeField] float scale = 20f, offX = 100f, offY = 100f, rate = 5f;
    [SerializeField] int width = 256, length = 256, height = 10;
    
    private void Start()
    {
        offX = Random.Range(1f, 999f);
        offY = Random.Range(1f, 999f);
    }
    //moving the terrain updating the terrain as time goes on
    void Update()
    {
        offX += Time.deltaTime * rate;
        Terrain perlingField = GetComponent<Terrain>();
        perlingField.terrainData = Perlinify(perlingField.terrainData);
    }

    //returns Terrain data to feed into the terrain object component
    TerrainData Perlinify(TerrainData data)
    {
        data.heightmapResolution = width + 1;
        
        data.size = new Vector3(width, height, length);

        data.SetHeights(0, 0, GenerateAltitudes());

        return data;
    }

    //2D Array to contain all the points of a flat terrain, x and z coordinates(named x and y for ease of use)
    //Each point of the array has a new altitude generated, based on its coordinates and put through a perlin noise calculator
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

    //float method to return an altitude value based on the coordinates of the point in the x-z plane
    float NewAltValue(int x, int y)
    {
        float xPos = (float)x / width * scale + offX;
        float yPos = (float)y / length * scale + offY;
        return Mathf.PerlinNoise(xPos, yPos);
    }
}
