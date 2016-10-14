using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EntityUIGroupHolder))]
public class HealthUI : MonoBehaviour {

    [SerializeField]
    protected HealthBarType barType = HealthBarType.FLOATING;
    Health health;
    HealthBar bar;

    void Start() {
        health = GetComponent<Health>();
        health.onHealthChanged += UpdateBar;
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        CanvasHelper ch = canvasRoot.GetComponent<CanvasHelper>();

        GameObject healthBar = (GameObject)Instantiate(ch.playerHealthBarPrefab, GetComponent<EntityUIGroupHolder>().EntityGroup.HealthBarHolder);
        (healthBar.transform as RectTransform).Reset();
        bar = healthBar.GetComponent<HealthBar>();

        /*
        if (barType == HealthBarType.PLAYER) {
            healthBar = (GameObject)Instantiate(ch.playerHealthBarPrefab, ch.playerHealthBarGroup);
            healthBar.transform.localScale = Vector3.one;
            bar = healthBar.GetComponent<HealthBar>();
        } else {   // need to implement boss bars still so this is temp
            healthBar = (GameObject)Instantiate(ch.floatingHealthBarPrefab, ch.floatingHealthBarGroup);
            healthBar.transform.localScale = Vector3.one;
            bar = healthBar.GetComponent<HealthBar>();
            bar.followTransform = gameObject.transform;
            Debug.Assert(ch.scaler, "Need canvas scaler on canvas!");
            bar.scaler = ch.scaler;

            // try to find bounds for object to use as floating health bar offset
            Bounds bounds = new Bounds();
            Collider col = GetComponent<Collider>();
            if (col) {
                bounds = col.bounds;
            } else {
                Renderer rend = GetComponent<Renderer>();
                if (rend) {
                    bounds = rend.bounds;
                }
            }

            bar.followOffset = new Vector3(0.0f, bounds.size.y * 1.5f, 0.0f);
        }
        */
        bar.type = barType;
        bar.SetText(gameObject.name);
    }

    void OnDestroy() {
        Destroy(bar.gameObject);
    }

    /// <summary>
    /// Delegate method for the health_onHealthChanged event
    /// </summary>
    public void UpdateBar() {
        bar.SetPercent(health.healthPercent);
    }
}
