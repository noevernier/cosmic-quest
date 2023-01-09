using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape Settings", menuName = "Planet Settings/Shape Settings")]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer {

        public bool enabled;
        public bool useFirstLayerAsMask;
        public NoiseSettings noiseSettings;
    }
}
