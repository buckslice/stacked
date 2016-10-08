using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IActivationNetworking {
    void ActivateRemote(AbilityActivation ability, object[] data);
}

/// <summary>
/// TODO: refactor rename?
/// Photon requires that the target of an RPC be on the same gameObject as the photonview. This class fills that role.
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class AbilityNetworking : MonoBehaviour, IActivationNetworking {

    const string networkedActivationRPCName = "NetworkedActivationRPC";

    /// <summary>
    /// A collection of abilities to add as networkedAbilities. If an ability is in this collection, do not add it again via scripting. Intended for drag-and-drop inspector initialization.
    /// </summary>
    [SerializeField]
    protected GameObject[] abilities;

    /// <summary>
    /// An ability's networkedAbilityId is its index in this list. Elements can be removed, but indices are not shifted down.
    /// </summary>
    List<AbilityActivation> abilityActivations = new List<AbilityActivation>();

    /// <summary>
    /// Secondary data structure to look up the index (and thus the networkedAbilityId) of an element in abilityActivations
    /// </summary>
    Dictionary<AbilityActivation, int> abilityActivationIndices = new Dictionary<AbilityActivation, int>();

    PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    void Start()
    {
        foreach (GameObject ability in abilities)
        {
            AddNetworkedAbility(ability.GetComponent<AbilityActivation>());
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
    public void AddNetworkedAbility(AbilityActivation ability)
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

    /// <summary>
    /// Removes an ability as a networked ability. ORDER MATTERS! Does not modify the networkeAbilityIds of other abilities. If the ability is re-added, it can have a different index than it currently does.
    /// </summary>
    /// <param name="ability"></param>
    public void RemoveNetworkedAbility(AbilityActivation ability)
    {
        int index = getNetworkedAbilityId(ability);
        abilityActivations[index] = null;
        abilityActivationIndices.Remove(ability);
    }

    public byte getNetworkedAbilityId(AbilityActivation ability)
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
        AbilityActivation networkedAbilityActivation = ability.GetComponent<AbilityActivation>();
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
        if (!view.isMine) {
            Debug.LogError("We do not own this object. All activations should originate from the owner. Discarding activation.");
            return;
        }
        view.RPC(networkedActivationRPCName, PhotonTargets.Others, networkedAbilityId, data);
    }

    public void ActivateRemote(AbilityActivation ability, object[] data)
    {
        ActivateRemote(getNetworkedAbilityId(ability), data);
    }

    [PunRPC]
    public void NetworkedActivationRPC(byte networkedAbilityId, object[] incomingData, PhotonMessageInfo info)
    {
        if (view.isMine)
        {
            Debug.LogError("We own this object. All activations should originate from us. Discarding activation.");
            return;
        }

        abilityActivations[networkedAbilityId].Activate(incomingData);
    }
}
