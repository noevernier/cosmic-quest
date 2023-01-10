using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings.RigidNoiseSettings settings;

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings) {

        this.settings = settings;
    }

    /* 
    cette fonction applique le bruit du fichier Noise.cs lors de la génération de la planète
    Le principe pour avoir une forme réel et qu'on part d'une planète plutôt lisse, puis on la combine
    avec une planète moins lisse et etc (on fait ça numberOfLayers fois)
    à la fin la planète obtenue est beaucoup plus réaliste
    */
    public float Evaluate(Vector3 point) {

        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numberOfLayers; i++)
        {
            //pour rendre les montagnes plus abruptes
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));

            v *= v; //on rend encore ça plus abruptes en mettant au carré

            v *= weight; //pour avoir plus des détails
            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}