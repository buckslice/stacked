using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Zone))]
public class ZoneEditor : Editor {

    public override void OnInspectorGUI() {
        Zone targ = (Zone)target;

        targ.shape = (ZoneShape)EditorGUILayout.EnumPopup("Zone Shape", targ.shape);
        targ.type = (ZoneType)EditorGUILayout.EnumPopup("Zone Type", targ.type);

        switch (targ.type) {
            case ZoneType.DAMAGE_OVER_TIME:
                targ.healthChange = EditorGUILayout.FloatField("Damage Per Second", targ.healthChange);
                targ.duration = EditorGUILayout.FloatField("Duration", targ.duration);
                break;
            case ZoneType.HEALING_OVER_TIME:
                targ.healthChange = EditorGUILayout.FloatField("Healing Per Second", targ.healthChange);
                targ.duration = EditorGUILayout.FloatField("Duration", targ.duration);
                break;
            case ZoneType.EXPLODE_AFTER_TIMER:
                targ.healthChange = EditorGUILayout.FloatField("Explosion Damage", targ.healthChange);
                targ.duration = EditorGUILayout.FloatField("Timer", targ.duration);
                break;
            case ZoneType.EXPLODE_ON_CONTACT:
                targ.healthChange = EditorGUILayout.FloatField("Explosion Damage", targ.healthChange);
                targ.duration = EditorGUILayout.FloatField("Duration", targ.duration);
                break;
            default:
                break;
        }

        targ.mainParticles = (ParticleSystem)EditorGUILayout.ObjectField("Particle System Prefab", targ.mainParticles, typeof(ParticleSystem), false, null);

        GUIContent teleMsg = new GUIContent("Is Telegraphed", "Should this zone have a warning visual before it activates?");
        targ.isTelegraphed = EditorGUILayout.Toggle(teleMsg, targ.isTelegraphed);

        if (targ.isTelegraphed) {
            targ.telegraphParticles = (ParticleSystem)EditorGUILayout.ObjectField("Telegraph Particle System Prefab", targ.telegraphParticles, typeof(ParticleSystem), false, null);
            
            targ.telegraphDuration = EditorGUILayout.FloatField("Telegraph Duration", targ.telegraphDuration);
        }

        //DrawDefaultInspector();

    }

}

