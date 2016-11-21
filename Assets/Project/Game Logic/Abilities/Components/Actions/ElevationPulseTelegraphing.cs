using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ElevationPulseTelegraphing : DurationAbilityAction {

    [SerializeField]
    protected GameObject overlayAttachmentPrefab;

    [SerializeField]
    protected float pulseDuration;

    Stackable stackable;
    Collider target;
    OverlayAttachment attachment;
    Coroutine elevationPulseCoroutine;

	protected override void Start () {
        base.Start();
        target = transform.root.GetComponentInChildren<Damageable>().GetComponent<Collider>();
        stackable = transform.root.GetComponentInChildren<Stackable>();
        attachment = SimplePool.Spawn(overlayAttachmentPrefab).GetComponent<OverlayAttachment>();
        Assert.IsNotNull(target);
        Assert.IsNotNull(stackable);
	}

    protected override void OnDurationBegin() {

        attachment.Initialize(target);

        Assert.IsNull(elevationPulseCoroutine);
        elevationPulseCoroutine = StartCoroutine(elevationPulse());
    }

    protected override void OnDurationEnd() {
        StopCoroutine(elevationPulseCoroutine);
        attachment.Destroy();
        elevationPulseCoroutine = null;
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }

    IEnumerator elevationPulse() {
        float startTime = Time.time;

        while (true) {
            while(Time.time - pulseDuration > startTime) {
                startTime += pulseDuration;
            }

            Color resultColor1;
            Color resultColor2;
            int elevation = stackable.elevationInStack();
            if (elevation == 0) {
                resultColor1 = resultColor2 = Color.white;
            } else {

                HashSet<int> targetPlayers = Player.PlayersOnElevation(elevation);
                if (targetPlayers.Count == 0) {
                    resultColor1 = resultColor2 = Color.clear;
                } else {
                    resultColor1 = Player.playerColoring[targetPlayers.First()];
                    if (targetPlayers.Count > 1) {
                        resultColor2 = Player.playerColoring[targetPlayers.Last()];
                    } else {
                        resultColor2 = resultColor1;
                    }
                    Debug.Log(resultColor1);
                }
            }

            float progress = (Time.time - startTime) / pulseDuration;

            float animationValue = (1 - progress) * (1 - progress);
            resultColor1.a *= animationValue;
            resultColor2.a *= animationValue;
             
            attachment.Material.SetColor("_Color1", resultColor1);
            attachment.Material.SetColor("_Color2", resultColor2);
            yield return null;
        }
    }
}
