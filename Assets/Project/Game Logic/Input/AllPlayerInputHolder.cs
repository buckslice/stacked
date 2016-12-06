using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Takes input from every player at the same time
/// </summary>
public class AllPlayerInputHolder : MonoBehaviour, IPlayerInputHolder {
    private List<IPlayerInput> heldInputs = new List<IPlayerInput>();
    public virtual IPlayerInput HeldInput {
        get { return heldInputs[0]; }
        set { heldInputs.Add(value);}
    }

    void Start() {
        foreach(RegisteredPlayer player in GameObject.FindObjectsOfType<RegisteredPlayer>()) {
            heldInputs.Add(player.inputBindings);
        }
    }

    public Vector2 movementDirection {
        get {
            Vector3 result = Vector3.zero;
            int count = 0;
            foreach (IPlayerInput input in heldInputs) {
                Vector3 direction = input.movementDirection;
                if (direction.sqrMagnitude > 0) {
                    result += direction;
                    count++;
                }
            }

            return result / Mathf.Max(1, count);
        }
    }
    public Vector3 rotationDirection {
        get {
            Vector3 result = Vector3.zero;
            int count = 0;
            foreach (IPlayerInput input in heldInputs) {
                Vector3 direction = input.rotationDirection;
                if (direction.sqrMagnitude > 0) {
                    result += direction;
                    count++;
                }
            }

            return result / Mathf.Max(1, count);
        }
    }

    public bool getSubmit {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getSubmit) {
                    return true;
                }
            }
            return false;
        }
    }
    public bool getCancel {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getCancel) {
                    return true;
                }
            }
            return false;
        }
    }
    public bool getStart {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getStart) {
                    return true;
                }
            }
            return false;
        }
    }

    public bool getBasicAttack {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getBasicAttack) {
                    return true;
                }
            }
            return false;
        }
    }
    public bool getAbility1 {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getAbility1) {
                    return true;
                }
            }
            return false;
        }
    }
    public bool getAbility2 {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getAbility2) {
                    return true;
                }
            }
            return false;
        }
    }
    public bool getJump {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getJump) {
                    return true;
                }
            }
            return false;
        }
    }

    public bool getSubmitDown {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getSubmitDown) {
                    return true;
                }
            }
            return false;
    } }
    public bool getCancelDown {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getCancelDown) {
                    return true;
                }
            }
            return false;
    } }
    public bool getStartDown {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getStartDown) {
                    return true;
                }
            }
            return false;
    } }
    public bool getBasicAttackDown {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getBasicAttackDown) {
                    return true;
                }
            }
            return false;
    } }
    public bool getAbility1Down {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getAbility1Down) {
                    return true;
                }
            }
            return false;
    } }
    public bool getAbility2Down {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getAbility2Down) {
                    return true;
                }
            }
            return false;
    } }
    public bool getJumpDown {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getJumpDown) {
                    return true;
                }
            }
            return false;
        }
    }

    public bool getBasicAttackUp {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getBasicAttackUp) {
                    return true;
                }
            }
            return false;
    } }
    public bool getAbility1Up {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getAbility1Up) {
                    return true;
                }
            }
            return false;
    } }
    public bool getAbility2Up {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getAbility2Up) {
                    return true;
                }
            }
            return false;
    } }
    public bool getJumpUp {
        get {
            foreach (IPlayerInput input in heldInputs) {
                if (input.getJumpUp) {
                    return true;
                }
            }
            return false;
        }
    }

    public bool getAnyKey {
        get {
            foreach(IPlayerInput input in heldInputs) {
                if (input.getAnyKey) {
                    return true;
                }
            }
            return false;
        }
    }

    public string submitName { get { return HeldInput.submitName; } }
    public string cancelName { get { return HeldInput.cancelName; } }
    public string startName { get { return HeldInput.startName; } }
    public string basicAttackName { get { return HeldInput.basicAttackName; } }
    public string ability1Name { get { return HeldInput.ability1Name; } }
    public string ability2Name { get { return HeldInput.ability2Name; } }
    public string jumpName { get { return HeldInput.jumpName; } }
}
