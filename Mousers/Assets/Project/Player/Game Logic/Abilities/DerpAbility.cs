using UnityEngine;
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
    CameraShakeScript cameraShakeScript;

    protected override void Start()
    {
        base.Start();
        cameraShakeScript = Camera.main.GetComponent<CameraShakeScript>();
    }

    public override void Activate()
    {
        cameraShakeScript.screenShake(.5f);
        DoDamage();
    }

    public override void ActivateWithData(object data)
    {
        Activate();
    }

    public override void ActivateRemote()
    {
        networkedActivation.ActivateRemote();
    }


    //temporary
    private void DoDamage()
    {
        GameObject[] listToHit = GameObject.FindGameObjectsWithTag(Tags.Boss);
        foreach(GameObject enemy in listToHit){
            enemy.GetComponent<Boss>().health = enemy.GetComponent<Boss>().health - 10;
        }
    }
}
