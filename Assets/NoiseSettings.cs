using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType {Simple, Rigid};
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RigidNoiseSettings rigidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings {

        public float strength = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        [Range(1,10)]
        public int numberOfLayers = 1;
        public float persistence = .5f;
        public Vector3 centre;
        public float minValue; //plus elle est grande, plus les bruits faibles seront pas pris en compte (dans chaque itération)
    }

    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings {
        public float weightMultiplier = .8f; //pour le RigidNoiseFilter
    }
}


