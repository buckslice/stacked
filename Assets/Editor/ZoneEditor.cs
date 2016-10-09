using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Zone))]
public class ZoneEditor : Editor {

    public override void OnInspectorGUI() {
        Zone targ = (Zone)target;

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

    }

}

