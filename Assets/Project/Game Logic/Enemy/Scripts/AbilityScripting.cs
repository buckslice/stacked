using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface for all triggers which are activated remotely by another script.
/// </summary>
public interface IRemoteTrigger {
    void Trigger();
}

public class AbilityScripting : MonoBehaviour {

    [System.Serializable]
    public class TriggerEvent {
        [SerializeField]
        protected MonoBehaviour trigger;
        public IRemoteTrigger Trigger { get { return trigger as IRemoteTrigger; } }

        [SerializeField]
        public float RangeStartTime;

        [SerializeField]
        public float RangeEndTime;
    }

    [SerializeField]
    protected float cycleLength;

    [SerializeField]
    protected TriggerEvent[] events;

    float cycleStartTime;
    Queue<TimestampedData<int>> cycleEvents;

    void Start() {
        cycleStartTime = Time.time;
        initializeCycle();
    }

    void Update() {
        while(cycleEvents.Count > 0 && Time.time >= cycleEvents.Peek().outputTime) {
            events[cycleEvents.Dequeue().data].Trigger.Trigger();
        }

        if(Time.time >= cycleStartTime + cycleLength) {
            Assert.IsTrue(cycleEvents.Count == 0);
            cycleStartTime += cycleLength;
            initializeCycle();
        }
    }

    void initializeCycle() {
        List<TimestampedData<int>> unsortedCycleEvents = new List<TimestampedData<int>>(events.Length);
        for (int i = 0; i < events.Length; i++) {
            TriggerEvent evnt = events[i];
            Assert.IsTrue(evnt.RangeEndTime < cycleLength);
            float outputTime = cycleStartTime + Mathf.Lerp(evnt.RangeStartTime, evnt.RangeEndTime, Random.value);
            unsortedCycleEvents.Add(new TimestampedData<int>(outputTime, i));
        }

        unsortedCycleEvents.Sort();
        cycleEvents = new Queue<TimestampedData<int>>(unsortedCycleEvents);
    }
}
