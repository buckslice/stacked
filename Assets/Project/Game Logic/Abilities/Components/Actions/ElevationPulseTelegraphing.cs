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

            Color resultColor;
            int elevation = stackable.elevationInStack();
            if (elevation == 0) {
                resultColor = Color.white;
            } else {
                HashSet<int> targetPlayers = Player.PlayersOnElevation(elevation);
                resultColor = targetPlayers.Count == 0 ? Color.clear : Player.playerColoring[targetPlayers.First()];
            }

            float progress = (Time.time - startTime) / pulseDuration;

            float animationValue = (1 - progress) * (1 - progress);
            resultColor.a *= animationValue;

            /*
            Debug.Log(attachment);
            Debug.Log(attachment.Material);
            Debug.Log(resultColor);
            */
             
            attachment.Material.SetColor(Tags.ShaderParams.color, resultColor);
            yield return null;
        }
    }
}
