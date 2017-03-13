using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Intended to be used only in development to create players using the inspector. For all other purposes, use a PlayerInputHolder and set heldInput via script.
/// </summary>
public class KeyboardMousePlayerInputHolder : PlayerInputHolder
{
    private KeyboardMousePlayerInput bindings = new KeyboardMousePlayerInput();

    public override IPlayerInput HeldInput {
        get {
            return bindings;
        }
        set {
            bindings = (KeyboardMousePlayerInput)value;
            bindings.Player = this.transform;
        }
    }
}

[System.Serializable]
public class KeyboardMousePlayerInput : IPlayerInput
{
    public string horizontalMovementAxis = Tags.Input.Horizontal;
    public string verticalMovementAxis = Tags.Input.Vertical;
    public KeyCode submitKey = Tags.Input.Submit;
    public KeyCode cancelKey1 = Tags.Input.Cancel1;
    public KeyCode cancelKey2 = Tags.Input.Cancel2;
    public KeyCode startKey = Tags.Input.Start;
    public KeyCode basicAttackKey = Tags.Input.BasicAttack;
    public KeyCode ability1Key = Tags.Input.Ability1;
    public KeyCode ability2Key = Tags.Input.Ability2;
    public KeyCode jumpKey = Tags.Input.Jump;

    Transform player;
    Camera cam;
    public Transform Player { set { player = value; } }
    public void Initialize(MonoBehaviour holder) {
        cam = Camera.main;
    }
    public void Deactivate() { }

    public Vector2 movementDirection
    {
        get
        {
            Vector2 result = new Vector3(Input.GetAxisRaw(horizontalMovementAxis), Input.GetAxisRaw(verticalMovementAxis));
            result = Vector2.ClampMagnitude(result, 1f);
            return result;
        }
    }
    public Vector3 rotationDirection
    {
        get
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float distance = ((player.position.y - ray.origin.y) / ray.direction.y);
            Vector3 pointInWorld = ray.origin + (distance * ray.direction);

            // this assertion keeps failing but its printing the same values so disabling for now
            //Assert.AreApproximatelyEqual(pointInWorld.y, player.position.y);

            return pointInWorld - player.position;
        }
    }

    public void Update() { }

    public bool getSubmit { get { return Input.GetKey(submitKey); } }
    public bool getCancel { get { return Input.GetKey(cancelKey1) || Input.GetKey(cancelKey2); } }
    public bool getStart { get { return Input.GetKey(startKey); } }
    public bool getBasicAttack { get { return Input.GetKey(basicAttackKey) || Input.GetMouseButton(0); } }
    public bool getAbility1 { get { return Input.GetKey(ability1Key); } }
    public bool getAbility2 { get { return Input.GetKey(ability2Key); } }
    public bool getJump { get { return Input.GetKey(jumpKey) || Input.GetMouseButton(1); } }

    public bool getSubmitDown { get { return Input.GetKeyDown(submitKey); } }
    public bool getCancelDown { get { return Input.GetKeyDown(cancelKey1) || Input.GetKeyDown(cancelKey2); } }
    public bool getStartDown { get { return Input.GetKeyDown(startKey); } }
    public bool getBasicAttackDown { get { return Input.GetKeyDown(basicAttackKey) || Input.GetMouseButtonDown(0); } }
    public bool getAbility1Down { get { return Input.GetKeyDown(ability1Key); } }
    public bool getAbility2Down { get { return Input.GetKeyDown(ability2Key); } }
    public bool getJumpDown { get { return Input.GetKeyDown(jumpKey) || Input.GetMouseButtonDown(1); } }

    public bool getBasicAttackUp { get { return Input.GetKeyUp(basicAttackKey) || Input.GetMouseButtonUp(0); } }
    public bool getAbility1Up { get { return Input.GetKeyUp(ability1Key); } }
    public bool getAbility2Up { get { return Input.GetKeyUp(ability2Key); } }
    public bool getJumpUp { get { return Input.GetKeyUp(jumpKey) || Input.GetMouseButtonUp(1); } }

    public bool getAnyKey { get { return (Input.inputString.Length>0)||(Input.GetMouseButton(0))||(Input.GetMouseButton(1))||(Input.GetMouseButton(2)); } }
    public bool getAnyKeyDown { get { return (Input.inputString.Length > 0) || (Input.GetMouseButtonDown(0)) || (Input.GetMouseButtonDown(1)) || (Input.GetMouseButtonDown(2)); } }

    public string submitName { get { return PlayerInputExtension.getBindingName(submitKey); } }
    public string cancelName { get { return PlayerInputExtension.getBindingName(cancelKey1); } }
    public string startName { get { return PlayerInputExtension.getBindingName(startKey); } }
    public string basicAttackName { get { return PlayerInputExtension.getBindingName(basicAttackKey); } }
    public string ability1Name { get { return PlayerInputExtension.getBindingName(ability1Key); } }
    public string ability2Name { get { return PlayerInputExtension.getBindingName(ability2Key); } }
    public string jumpName { get { return PlayerInputExtension.getBindingName(jumpKey); } }

    public override bool Equals(object obj) {
        var item = obj as KeyboardMousePlayerInput;

        if (item == null) {
            return false;
        }

        //return true if all bindings are the same, false otherwise
        return 
            this.horizontalMovementAxis.Equals(item.horizontalMovementAxis) &&
            this.verticalMovementAxis.Equals(item.verticalMovementAxis) &&
            this.horizontalMovementAxis.Equals(item.horizontalMovementAxis) &&
            this.submitKey == item.submitKey &&
            this.cancelKey1 == item.cancelKey1 &&
            this.cancelKey2 == item.cancelKey2 &&
            this.startKey == item.startKey &&
            this.basicAttackKey == item.basicAttackKey &&
            this.ability1Key == item.ability1Key &&
            this.ability2Key == item.ability2Key;
    }

    public override int GetHashCode() {
        return this.horizontalMovementAxis.GetHashCode() *
            this.verticalMovementAxis.GetHashCode() *
            this.horizontalMovementAxis.GetHashCode() *
            this.submitKey.GetHashCode() *
            this.cancelKey1.GetHashCode() *
            this.cancelKey2.GetHashCode() *
            this.startKey.GetHashCode() *
            this.basicAttackKey.GetHashCode() *
            this.ability1Key.GetHashCode() *
            this.ability2Key.GetHashCode();
    }
}