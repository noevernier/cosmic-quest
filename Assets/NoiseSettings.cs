using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float strength = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    [Range(1,10)]
    public int numberOfLayers = 1;
    public float persistence = .5f;
    public Vector3 centre;
    public float minValue; //plus elle est grande, plus les bruits faibles seront pas pris en compte (dans chaque itération)
}
