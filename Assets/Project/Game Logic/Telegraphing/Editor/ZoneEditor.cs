using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Zone))]
public class ZoneEditor : Editor {

    GUIContent teleMsg = new GUIContent("Is Telegraphed", "Should this zone have a warning visual before it activates?");
    GUIContent flightMsg = new GUIContent("Flies to Destination", "Should this zone fly from where it is spawned to its target destination using a separate particle system?");
    public override void OnInspectorGUI() {
        Zone targ = (Zone)target;

        targ.shape = (ZoneShape)EditorGUILayout.EnumPopup("Zone Shape", targ.shape);
        targ.type = (ZoneType)EditorGUILayout.EnumPopup("Zone Type", targ.type);

        switch (targ.type) {
            case ZoneType.DAMAGE_OVER_TIME:
                targ.healthChange = EditorGUILayout.FloatField("Damage Per Second", targ.healthChange);
                targ.mainDuration = EditorGUILayout.FloatField("Duration", targ.mainDuration);
                break;
            case ZoneType.HEALING_OVER_TIME:
                targ.healthChange = EditorGUILayout.FloatField("Healing Per Second", targ.healthChange);
                targ.mainDuration = EditorGUILayout.FloatField("Duration", targ.mainDuration);
                break;
            case ZoneType.EXPLODE_AFTER_TIMER:
                targ.healthChange = EditorGUILayout.FloatField("Explosion Damage", targ.healthChange);
                targ.mainDuration = EditorGUILayout.FloatField("Timer", targ.mainDuration);
                break;
            case ZoneType.EXPLODE_ON_CONTACT:
                targ.healthChange = EditorGUILayout.FloatField("Explosion Damage", targ.healthChange);
                targ.mainDuration = EditorGUILayout.FloatField("Duration", targ.mainDuration);
                break;
            default:
                break;
        }

        targ.mainParticles = (ParticleSystem)EditorGUILayout.ObjectField("Main Particle System Prefab", targ.mainParticles, typeof(ParticleSystem), false, null);

        targ.isTelegraphed = EditorGUILayout.Toggle(teleMsg, targ.isTelegraphed);
        if (targ.isTelegraphed) {
            targ.telegraphParticles = (ParticleSystem)EditorGUILayout.ObjectField("Telegraph Particle System Prefab", targ.telegraphParticles, typeof(ParticleSystem), false, null);
            targ.telegraphDuration = EditorGUILayout.FloatField("Telegraph Duration", targ.telegraphDuration);
        }

        targ.fliesToDestination = EditorGUILayout.Toggle(flightMsg, targ.fliesToDestination);
        if (targ.fliesToDestination) {
            targ.flightParticles = (ParticleSystem)EditorGUILayout.ObjectField("Flight Particle System Prefab", targ.flightParticles, typeof(ParticleSystem), false, null);
            targ.flightType = (FlightType)EditorGUILayout.EnumPopup("Flight Type", targ.flightType);
            if (targ.flightType == FlightType.ARC) {
                targ.initialYVel = EditorGUILayout.FloatField("Initial Y Velocity", targ.initialYVel);
                string s = targ.speedBasedOnDuration ? "Based on Duration" : "Constant";
                targ.speedBasedOnDuration = EditorGUILayout.ToggleLeft("Horizontal Flight Speed: " + s, targ.speedBasedOnDuration);
                if (targ.speedBasedOnDuration) {
                    targ.flightDuration = EditorGUILayout.FloatField("Flight Duration", targ.flightDuration);
                } else {
                    targ.flightSpeedHorizontal = EditorGUILayout.FloatField("Horizontal Flight Speed", targ.flightSpeedHorizontal);
                }
            } else {
                targ.flightDuration = EditorGUILayout.FloatField("Flight Duration", targ.flightDuration);
            }
        }

        //DrawDefaultInspector();

    }

}

