using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// TODO: refactor rename?
/// Photon requires that the target of an RPC be on the same gameObject as the photonview. This class fills that role.
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class AbilityNetworking : MonoBehaviour {

    const string networkedActivationRPCName = "NetworkedActivationRPC";

    /// <summary>
    /// A collection of abilities to add as networkedAbilities. If an ability is in this collection, do not add it again via scripting. Intended for drag-and-drop inspector initialization.
    /// </summary>
    [SerializeField]
    protected GameObject[] abilities;

    /// <summary>
    /// An ability's networkedAbilityId is its index in this list.
    /// </summary>
    List<NetworkedAbilityActivation> abilityActivations = new List<NetworkedAbilityActivation>();

    PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    void Start()
    {
        foreach (GameObject ability in abilities)
        {
            AddNetworkedAbility(ability.GetComponent<NetworkedAbilityActivation>());
        }

        if (!view.isMine)
        {
            //we need to disable all input-related ability activation scripts
            foreach (IAbilityInputActivation inputActivation in GetComponentsInChildren<IAbilityInputActivation>())
            {
                ((MonoBehaviour)inputActivation).enabled = false;
            }
        }
    }

    /// <summary>
    /// Adds an ability as a networked ability. ORDER MATTERS! I don't think this is reliable after initialization, but I haven't tested it.
    /// </summary>
    /// <param name="ability">The ability to add.</param>
    public void AddNetworkedAbility(NetworkedAbilityActivation ability)
    {
        abilityActivations.Add(ability);
        ability.Initialize(this);
    }

    //todo: add ability removal

    public byte getNetworkedAbilityId(NetworkedAbilityActivation ability)
    {
        int result = abilityActivations.IndexOf(ability);
        if (result < 0)
        {
            Debug.LogErrorFormat(this, "{0} is not a networked ability of {1}", ability.ToString(), this.ToString());
            return byte.MaxValue;
        }
        else
        {
            return (byte)result;
        }
    }

    public byte getNetworkedAbilityId(GameObject ability)
    {
        NetworkedAbilityActivation networkedAbilityActivation = ability.GetComponent<NetworkedAbilityActivation>();
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
    public void ActivateRemote(byte networkedAbilityId)
    {
        view.RPC(networkedActivationRPCName, PhotonTargets.Others, networkedAbilityId);
    }

    public void ActivateRemote(NetworkedAbilityActivation ability)
    {
        ActivateRemote(getNetworkedAbilityId(ability));
    }

    [PunRPC]
    public void NetworkedActivationRPC(byte networkedAbilityId, PhotonMessageInfo info)
    {
        abilityActivations[networkedAbilityId].ActivateLocal();
    }
}
