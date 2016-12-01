using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ElevationUpdating : ProjectileLifetimeAction, IBalanceStat {

    [SerializeField]
    protected Transform topRing;
    [SerializeField]
    protected Transform ray;
    [SerializeField]
    protected Transform bottomRing;

    Material topRingMat;
    Material rayMat;
    Material bottomRingMat;

    protected override void Awake() {
        base.Awake();
        Player.playerElevationChanged += Player_playerElevationChanged;

        //instantiate materials
        topRingMat = topRing.GetComponent<Renderer>().material = topRing.GetComponent<Renderer>().material;
        rayMat = ray.GetComponent<Renderer>().material = ray.GetComponent<Renderer>().material;
        bottomRingMat = bottomRing.GetComponent<Renderer>().material = bottomRing.GetComponent<Renderer>().material;
    }

    protected override void Start() {
        base.Start();
        Player_playerElevationChanged();
    }

    protected override void OnProjectileDestroyed() {
        base.OnProjectileDestroyed();
        Player.playerElevationChanged -= Player_playerElevationChanged;
    }

    private void Player_playerElevationChanged() {
        int elevation = Stackable.heightToStackElevation(transform.position.y);

        Color resultColor1;
        Color resultColor2;
        
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
            }
        }

        topRingMat.SetColor("_Color1", resultColor1);
        rayMat.SetColor("_Color1", resultColor1);
        bottomRingMat.SetColor("_Color1", resultColor1);

        topRingMat.SetColor("_Color2", resultColor2);
        rayMat.SetColor("_Color2", resultColor2);
        bottomRingMat.SetColor("_Color2", resultColor2);
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch(type) {
            case BalanceStat.StatType.RADIUS:
            default:
                bottomRing.localScale = topRing.localScale = Vector3.one * value * 2;
                break;
        }
    }

    protected override void OnProjectileCreated() {
        Vector3 flooredPosition = bottomRing.transform.position;
        flooredPosition.y = 0.01f;

        bottomRing.transform.position = flooredPosition;
        ray.transform.position = (flooredPosition + topRing.position) / 2;

        Vector3 rayScale = ray.localScale;
        rayScale.y = topRing.position.y - 0.01f;
        ray.localScale = rayScale;

        Player_playerElevationChanged();
    }
}
