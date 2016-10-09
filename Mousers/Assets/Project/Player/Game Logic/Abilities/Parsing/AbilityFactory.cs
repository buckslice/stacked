using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class AbilityFactory : MonoBehaviour{

    private static Dictionary<Type, Action<object, FieldInfo, string>> switchDict = new Dictionary<Type, Action<object, FieldInfo, string>> {
        {typeof(int), (component, field, value) =>  field.SetValue(component, int.Parse(value))},
        {typeof(float), (component, field, value) =>  field.SetValue(component, float.Parse(value)) },
        {typeof(string), (component, field, value) =>  field.SetValue(component, value) },
        {typeof(MultiplierFloatStat),  (component, field, value) =>  field.SetValue(component, new MultiplierFloatStat(float.Parse(value))) }
    };

    public static GameObject createAbility(GameObject abilityObject, AbilityData data) {
        abilityObject.AddComponent<AbilityActivation>();
        AbstractAbilityAction[] abilityActions = new AbstractAbilityAction[data.behaviors.Length];
        int i = 0;
        abilityObject.name = data.name;
        foreach (BehaviorData behavior in data.behaviors) {
            Type type = Type.GetType(behavior.type);
            var component = abilityObject.AddComponent(type);
            foreach (AttributeData attribute in behavior.attributes) {
                FieldInfo field = type.GetField(attribute.name, BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public);
                switchDict[field.FieldType](component, field, attribute.value);
            }
            abilityActions[i] = (AbstractAbilityAction)component;
            i++;
        }
        typeof(AbilityActivation).GetField("abilityActions", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).SetValue(abilityObject.GetComponent<AbilityActivation>(), abilityActions);
        return abilityObject;
    }
}
