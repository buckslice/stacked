using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Called to activate an ability. the stream allows you to read or write data for the network.
/// Returns false if an activation does not need to be sent over the network, true if it does.
/// </summary>
/// <param name="stream"></param>
/// 
public delegate bool ActivateAbility(PhotonStream stream);

/*
/// <summary>
/// An event that triggers based on player input.
/// </summary>
public interface IAbilityInput {}
 * */

public delegate void AbilityTrigger();

/// <summary>
/// Anything which sends out an event which can possibly activate the ability.
/// </summary>
public interface IAbilityTrigger
{
    event AbilityTrigger abilityTriggerEvent;
}

public enum AbilityKeybinding { ABILITY1, ABILITY2 };

/// <summary>
/// Interface indicating that this has a keybinding
/// </summary>
public interface IAbilityKeybound
{
    AbilityKeybinding Binding { get; set; }
}

/// <summary>
/// Interface with which ability activation can be restricted
/// </summary>
public interface IAbilityConstraint
{
    bool isAbilityActivatible();
}

/// <summary>
/// Abstract class to contain functionality that almost all ability constraints will use
/// </summary>
public abstract class AbstractAbilityConstraint : AbstractAbilityAction, IAbilityConstraint
{
    AbilityActivation activation = null;
    protected override void Start()
    {
        base.Start();
        activation = GetComponent<AbilityActivation>();
        activation.AddConstraint(this);
    }

    protected void OnDestroy()
    {
        if (activation != null)
        {
            activation.RemoveConstraint(this);
        }
    }

    public abstract bool isAbilityActivatible();

    public override bool Activate(PhotonStream stream) {
        Activate();
        return false;
    }

    public abstract void Activate();
}

/// <summary>
/// Filters triggers to activate abilities.
/// </summary>
public class AbilityActivation : MonoBehaviour
{
    /// <summary>
    /// List of abilityActions to activate with this ability, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected AbstractAbilityAction[] abilityActions;
    public AbstractAbilityAction[] AbilityActions { get { return abilityActions; } }

    protected List<IAbilityConstraint> constraints = new List<IAbilityConstraint>();

    public void AddConstraint(IAbilityConstraint toAdd) { constraints.Add(toAdd); }
    public bool RemoveConstraint(IAbilityConstraint toRemove) { return constraints.Remove(toRemove); }

    IActivationNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(IActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
    }

    public void Start()
    {
        foreach (IAbilityTrigger trigger in GetComponentsInParent<IAbilityTrigger>())
        {
            trigger.abilityTriggerEvent += trigger_abilityTriggerEvent;
        }
    }

    void trigger_abilityTriggerEvent()
    {
        foreach (IAbilityConstraint constraint in constraints)
        {
            if (!constraint.isAbilityActivatible())
            {
                //cannot activate, do nothing
                return;
            }
        }

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(true, null);

        bool send = false;
        foreach(AbstractAbilityAction abilityAction in abilityActions)
        {
            send |= abilityAction.Activate(stream);
            Assert.IsTrue(send || (stream.Count == 0), string.Format("Data written to stream but not flagged to be sent. {0}", abilityAction));
        }
        abilityNetwork.ActivateRemote(this, stream.ToArray());
    }

    public void Activate(object[] incomingData) {

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(false, incomingData);
        foreach (AbstractAbilityAction abilityAction in abilityActions) {
            abilityAction.Activate(stream);
        }
    }
}
