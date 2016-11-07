using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectIcon : MonoBehaviour, ISelectable {
    //[SerializeField]
    //protected GameObject playerSetup;
    //public GameObject getPlayerSetup()
    //{
    //    return playerSetup;
    //}

    public PlayerSetupNetworkedData.AbilityId ability;

    public Sprite abilityIcon { get { return GetComponent<Image>().sprite; } }

    public GameObject visualsIcon;
    public GameObject tooltip;
    public UIMeshLine tooltipLine;
    public AnimationCurve tooltipAnimation; // should probably store this somewhere else

    public Color color = Color.white;

    bool hover = false;
    Coroutine animRoutine = null;
    public void SetHover() {
        hover = true;
    }

    // if nobody has hovered over this icon this frame then disable tooltip
    void LateUpdate() {
        if(!tooltip || !tooltipLine) {
            return;
        }
        if (!hover) {
            if (animRoutine != null) {
                StopCoroutine(animRoutine);
                animRoutine = null;
            }
            tooltip.SetActive(false);
            tooltipLine.gameObject.SetActive(false);
            return;
        }

        if (animRoutine == null) {
            animRoutine = StartCoroutine(AnimRoutine());
        }

        hover = false;
    }

    IEnumerator AnimRoutine() {
        tooltipLine.gameObject.SetActive(true);

        float t = 0.0f;
        float drawTime = 0.25f;
        while(t < drawTime) {
            tooltipLine.lengthRatio = t / drawTime;
            t += Time.deltaTime;
            yield return null;
        }
        tooltipLine.lengthRatio = 1.0f;

        tooltip.SetActive(true);
        t = 0.0f;
        while(t < drawTime) {
            tooltip.transform.localScale = Vector3.one * tooltipAnimation.Evaluate(t / drawTime);
            t += Time.deltaTime;
            yield return null;
        }
        tooltip.transform.localScale = Vector3.one;

    }

}
