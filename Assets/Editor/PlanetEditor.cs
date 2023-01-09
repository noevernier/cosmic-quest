using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/* 
class pour pouvoir modifier les paramètres dans la fenêtre Inspector
on ne peut pas utiliser directement SerializeField sur nos settings car ils n'ont
pas de type connu (style int ...)
*/

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;

    Editor shapeEditor;
    Editor colourEditor;

    public override void OnInspectorGUI() {

        using (var check = new EditorGUI.ChangeCheckScope()) {

            base.OnInspectorGUI();

            if (check.changed) {

                planet.GeneratePlanet();
            }
        }

        if (GUILayout.Button("Generate Planet")) {

            planet.GeneratePlanet();
        }

        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsUpdated, ref planet.colourSettingsFoldout, ref colourEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor) {

        if (settings != null) {

            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            using (var check = new EditorGUI.ChangeCheckScope()) {

                if (foldout) {

                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed) {
                        onSettingsUpdated();
                    }
                }
            }
        }
    }

    private void OnEnable() {

        planet = (Planet)target;
    }
    
}
