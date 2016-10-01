using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class RegistrationUI : MonoBehaviour {

	void Start () {
        this.transform.SetParent(GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform.Find(Tags.UIPaths.PlayerRegistrationGroup));
	}
}
