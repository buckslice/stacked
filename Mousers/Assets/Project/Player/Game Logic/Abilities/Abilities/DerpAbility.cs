﻿using UnityEngine;
using System.Collections;

public class DerpAbility : AbstractAbilityAction {
    /*
    public string buttonName = "Fire1";
    private CameraShakeScript cameraShakeScript;

	// Use this for initialization
	void Start () {
        cameraShakeScript = Camera.main.GetComponent<CameraShakeScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(buttonName)){
            cameraShakeScript.screenShake(.5f);
        }
	}
     * */
    Player playerReference;
    CameraShakeScript cameraShakeScript;

    protected override void Start()
    {
        base.Start();
        playerReference = GetComponentInParent<Player>();
        cameraShakeScript = Camera.main.GetComponent<CameraShakeScript>();
    }

    public override bool Activate(PhotonStream stream)
    {
        cameraShakeScript.screenShake(.5f);
        DoDamage();
        return true;
    }

    //temporary
    private void DoDamage()
    {
        GameObject[] listToHit = GameObject.FindGameObjectsWithTag(Tags.Boss);
        foreach (GameObject enemy in listToHit)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth)
            {
                enemyHealth.Damage(10, playerReference);
            }
        }
    }
}
