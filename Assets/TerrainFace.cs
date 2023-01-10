using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    int resolution; //nombre de triangles sur la sphère
    Vector3 localUp;
    ShapeGenerator shapeGenerator;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp) {
        
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    //des maths pour générer la planète (il utilise des triangles pour générer la sphère)
    public void ConstructMesh() {

        Vector3[] vertices = new Vector3[resolution * resolution];

        /*
        La formule donnant la dimension du tableau triangles et tout simplement le nombre de triangles
        sur le cube de côté r
        */
        int[] triangles = new int[(resolution-1)*(resolution-1)*2*3];
        int triIndex = 0; //pour compter

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution; //équivalent à mettre i++ à la fin de cette boucle
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x-.5f)*2*axisA + (percent.y-.5f)*2*axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

                /*
                grâce au calcul de chaque point sur les sommets des triangles qui générent le cube, on calcul
                chaque vecteur normal à la surface en ce point (c'est pour la lumière)
                */
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                if (x != resolution - 1 && y != resolution - 1) {

                    triangles[triIndex] = i;
                    triangles[triIndex+1] = i + resolution + 1;
                    triangles[triIndex+2] = i + resolution;
                    triangles[triIndex+3] = i;
                    triangles[triIndex+4] = i + 1;
                    triangles[triIndex+5] = i + resolution + 1;

                    triIndex += 6;
                }
            }
        }

        /*
        on donne finalement tout les vecteurs normaux ainsi que l'indication de comment parcourir les triangles
        pour générer le mesh
        */
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

}
