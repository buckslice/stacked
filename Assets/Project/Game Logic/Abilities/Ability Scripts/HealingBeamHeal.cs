using UnityEngine;
using System.Collections.Generic;

public class HealingBeamHeal : MonoBehaviour {

    class HealTarget {
        public Collider c;
        public Health h;
        public HealTarget(Collider c, Health h) {
            this.c = c;
            this.h = h;
        }
    }

    public float healsPerSecond = 10.0f;

    private List<HealTarget> healTargets = new List<HealTarget>();
    private Collider c;
	// Use this for initialization
	void Start () {
        c = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!c.enabled) {
            healTargets.Clear();
            return;
        }

	    for(int i = 0; i < healTargets.Count; ++i) {
            healTargets[i].h.Heal(healsPerSecond * Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider c) {
        if(c.CompareTag(Tags.Player)) {
            Health h = c.transform.parent.GetComponent<Health>();
            Debug.Assert(h, "Healing Beam could not find Health from Player!");
            healTargets.Add(new HealTarget(c,h));
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.CompareTag(Tags.Player)) {
            for (int i = 0; i < healTargets.Count; ++i) {
                if(c == healTargets[i].c) {
                    healTargets.RemoveAt(i);
                }
            }
        }
    }
}
