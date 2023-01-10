using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10; //pour modifier la "qualité" de la planète
    public bool autoUpdate;

    public ShapeSettings shapeSettings; //paramètres de forme (rayon)
    public ColourSettings colourSettings; //paramètres de couleurs
    [HideInInspector]

    //juste pour savoir si la flèche du menu de modification est ouverte ou non
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    public void GeneratePlanet() {

        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated() {

        if (autoUpdate) {

            Initialize();
            GenerateMesh();
        }
    }
    public void OnColourSettingsUpdated() {

        if (autoUpdate) {

            Initialize();
            GenerateColours();
        }
    }

    void Initialize() {

        shapeGenerator = new ShapeGenerator(shapeSettings);

        if (meshFilters == null || meshFilters.Length == 0) {

            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null) {

                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

            }

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    
        }

    void GenerateMesh() {

        foreach (TerrainFace face in terrainFaces) {
            face.ConstructMesh();
        }
    }

    void GenerateColours() {

        foreach (MeshFilter m in meshFilters) {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColor;
        }
    }
}
