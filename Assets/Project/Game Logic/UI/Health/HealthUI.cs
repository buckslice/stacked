using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(IEntityUIGroupHolder))]
public class HealthUI : MonoBehaviour {

    Health health;
    HealthBar bar;
    public HealthBarType barType;

    void Start() {
        health = GetComponent<Health>();
        health.onHealthChanged += UpdateBar;
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        CanvasHelper ch = canvasRoot.GetComponent<CanvasHelper>();

        GameObject barPrefab = null;
        switch (barType) {
            case HealthBarType.MINIMAL:
                barPrefab = ch.playerHealthBarPrefab;
                break;
            case HealthBarType.REGULAR:
                barPrefab = ch.regularHealthBarPrefab;
                break;
            case HealthBarType.BOSS:
                barPrefab = ch.bossHealthBarPrefab;
                break;

        }

        GameObject healthBar = Instantiate(barPrefab, GetComponent<IEntityUIGroupHolder>().EntityGroup.HealthBarHolder);

        RectTransform rt = healthBar.transform as RectTransform;
        // kinda weird but doing this so iceboss healthbar outline is visible
        if (barType == HealthBarType.BOSS) { 
            rt.localScale = Vector3.one;
            rt.localRotation = Quaternion.identity;
            rt.offsetMax = -Vector2.one * 2.0f;
            rt.offsetMin = Vector2.one * 2.0f;
        } else {
            rt.Reset();
        }
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
        bar.SetText(gameObject.name);
        Player player = GetComponent<DamageHolder>().GetRootDamageTracker() as Player;
        if (player != null) {
            bar.SetTextColor(Player.playerColoring[player.PlayerID]);
        }
    }

    void OnDestroy() {
        if (bar != null) {
            Destroy(bar.gameObject);
        }
    }

    /// <summary>
    /// Delegate method for the health_onHealthChanged event
    /// </summary>
    public void UpdateBar() {
        bar.SetPercent(health.healthPercent);
    }
}
