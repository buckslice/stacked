using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IActivationNetworking {
    void Initialize(IAbilityRelay relay, PhotonView view);
    void ActivateRemote(IAbilityActivation ability, object[] data);
    void NetworkedActivationRPC(object[] incomingData, PhotonMessageInfo info);
}

/// <summary>
/// Interface used to enable and disable the target's ability to use abilities
/// </summary>
public interface IAbilities {
    AllBoolStat ActivationEnabled { get; }
}

public abstract class AbstractActivationNetworking : MonoBehaviour, IActivationNetworking, IAbilities {

    AllBoolStat activationEnabled = new AllBoolStat(true);
    public AllBoolStat ActivationEnabled { get { return activationEnabled; } }

    protected IAbilityRelay relay;
    protected PhotonView view;

    public void Initialize(IAbilityRelay relay, PhotonView view) {
        this.relay = relay;
        this.view = view;
    }

    public abstract void ActivateRemote(IAbilityActivation ability, object[] data);

    public abstract void NetworkedActivationRPC(object[] incomingData, PhotonMessageInfo info);
}

/// <summary>
/// TODO: refactor rename?
/// Photon requires that the target of an RPC be on the same gameObject as the photonview. This class fills that role.
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class AbilityNetworking : AbstractActivationNetworking {

    const string networkedActivationRPCName = "NetworkedActivationRPC";

    /// <summary>
    /// A collection of abilities to add as networkedAbilities. If an ability is in this collection, do not add it again via scripting. Intended for drag-and-drop inspector initialization.
    /// </summary>
    [SerializeField]
    protected GameObject[] abilities;

    /// <summary>
    /// An ability's networkedAbilityId is its index in this list. Elements can be removed, but indices are not shifted down.
    /// </summary>
    List<IAbilityActivation> abilityActivations = new List<IAbilityActivation>();

    /// <summary>
    /// Secondary data structure to look up the index (and thus the networkedAbilityId) of an element in abilityActivations
    /// </summary>
    Dictionary<IAbilityActivation, int> abilityActivationIndices = new Dictionary<IAbilityActivation, int>();

    void Start()
    {
        foreach (GameObject ability in abilities)
        {
            AddNetworkedAbility(ability);
        }

        if (!view.isMine)
        {
            //we need to disable all input-related ability activation scripts
            foreach (IUntargetedAbilityTrigger inputActivation in GetComponentsInChildren<IUntargetedAbilityTrigger>())
            {
                ((MonoBehaviour)inputActivation).enabled = false;
            }
        }
    }

    /// <summary>
    /// Adds an ability as a networked ability. ORDER MATTERS! I don't think this is reliable after initialization, but I haven't tested it.
    /// </summary>
    /// <param name="ability">The ability to add.</param>
    public void AddNetworkedAbility(IAbilityActivation ability)
    {
        //default value
        int index = abilityActivations.Count;

        for (int i = 0; i < abilityActivations.Count; i++)
        {
            //find first open spot, if any
            if (abilityActivations[i] == null)
            {
                //index value is the first open spot
                index = i;
                break;
            }
        }

        abilityActivations.Insert(index, ability);
        abilityActivationIndices[ability] = index;
        ability.Initialize(this);
    }

    public void AddNetworkedAbility(GameObject ability) {

        MultipleAbilityActivationHolder multipleAbilities = ability.GetComponentInChildren<MultipleAbilityActivationHolder>();

        if (multipleAbilities != null) {
            Assert.IsTrue(ability.GetComponentsInChildren<MultipleAbilityActivationHolder>().Length == 1);

            foreach (AbilityActivation abilityActivation in multipleAbilities.abilityActivations) {
                AddNetworkedAbility(abilityActivation);
            }
            foreach (TargetedAbilityActivation targetedAbilityActivation in multipleAbilities.targetedAbilityActivation) {
                AddNetworkedAbility(targetedAbilityActivation);
            }
        } else {

            Assert.IsTrue(ability.GetComponentsInChildren<AbilityActivation>().Length <= 1);
            Assert.IsTrue(ability.GetComponentsInChildren<TargetedAbilityActivation>().Length <= 1);
            Assert.IsTrue(ability.GetComponentsInChildren<IAbilityActivation>().Length > 0);

            AbilityActivation untargetedActivation = ability.GetComponentInChildren<AbilityActivation>();
            if (untargetedActivation != null) {
                AddNetworkedAbility(untargetedActivation);
            }

            TargetedAbilityActivation targetedActivation = ability.GetComponentInChildren<TargetedAbilityActivation>();
            if (targetedActivation != null) {
                AddNetworkedAbility(targetedActivation);
            }
        }
    }

    /// <summary>
    /// Removes an ability as a networked ability. ORDER MATTERS! Does not modify the networkeAbilityIds of other abilities. If the ability is re-added, it can have a different index than it currently does.
    /// </summary>
    /// <param name="ability"></param>
    public void RemoveNetworkedAbility(IAbilityActivation ability)
    {
        int index = getNetworkedAbilityId(ability);
        abilityActivations[index] = null;
        abilityActivationIndices.Remove(ability);
    }

    public byte getNetworkedAbilityId(IAbilityActivation ability)
    {
        if (!abilityActivationIndices.ContainsKey(ability))
        {
            Debug.LogErrorFormat(this, "{0} is not present as a networked ability on {1}", ability.ToString(), this.ToString());
            return byte.MaxValue;
        }
        else
        {
            int result = abilityActivationIndices[ability];
            return (byte)result;
        }
    }

    public byte getNetworkedAbilityId(GameObject ability)
    {
        IAbilityActivation networkedAbilityActivation = ability.GetComponent<IAbilityActivation>();
        if (networkedAbilityActivation == null)
        {
            Debug.LogErrorFormat(this, "{0} does not have a NetworkedAbilityActivation component", ability.ToString());
            return byte.MaxValue;
        }
        else
        {
            return getNetworkedAbilityId(networkedAbilityActivation);
        }
    }

    /// <summary>
    /// Tells other clients to activate the ability on their end.
    /// </summary>
    public void ActivateRemote(byte networkedAbilityId, object[] data)
    {
#if UNITY_ENGINE
        if (!view.isMine) {
            RequireLocal requirement = abilityActivations[networkedAbilityID].GetComponentInChildren<RequireLocal>(); //one of the exceptions to this assumption
            if(requirement == null) {
            Debug.LogError("We do not own this object. All activations should originate from the owner. Discarding activation.", this);
            return;
            }
        }
#endif

        object[] joinedData = new object[data.Length + 1];
        joinedData[0] = networkedAbilityId;
        data.CopyTo(joinedData, 1);

        relay.ActivateRemote(this, joinedData);
    }

    public override void ActivateRemote(IAbilityActivation ability, object[] data)
    {
        ActivateRemote(getNetworkedAbilityId(ability), data);
    }

    
    public override void NetworkedActivationRPC(object[] incomingData, PhotonMessageInfo info)
    {
        byte networkedAbilityID = (byte)incomingData[0];
        object[] unjoinedData = new object[incomingData.Length - 1];
        Array.Copy(incomingData, 1, unjoinedData, 0, unjoinedData.Length);

#if UNITY_ENGINE
        if (view.isMine)
        {
            RequireLocal requirement = abilityActivations[networkedAbilityID].GetComponentInChildren<RequireLocal>(); //one of the exceptions to this assumption
            if(requirement == null) {
                Debug.LogError("We own this object. All activations should originate from us. Discarding activation.");
                return;
            }
        }
#endif

        abilityActivations[networkedAbilityID].Activate(unjoinedData, info);
    }
}
