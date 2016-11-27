using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class FlameSweepMK2 : AbstractAbilityAction, IBalanceStat {

    [SerializeField]
    protected Transform spawner;

    [SerializeField]
    protected MultiplierFloatStat duration = new MultiplierFloatStat(1);

    /// <summary>
    /// The radius of the area over which elements will be spawned.
    /// </summary>
    [SerializeField]
    protected float spawnAreaRadius = 10;

    /// <summary>
    /// The radius of each spawned object.
    /// </summary>
    [SerializeField]
    protected float spawnElementRadius = 2;

    IRemoteTrigger spawnTrigger;
    Queue<TimestampedData<Vector2>> toSpawn = new Queue<TimestampedData<Vector2>>();

    Coroutine durationCoroutine = null;

    protected override void Start() {
        base.Start();
        spawnTrigger = spawner.GetComponent<IRemoteTrigger>();
        Assert.IsNotNull(spawnTrigger);
    }

    public override bool Activate(PhotonStream stream) {
        //only have one active routine at a time.
        if (durationCoroutine != null) {
            StopCoroutine(durationCoroutine);
        }

        durationCoroutine = StartCoroutine(DurationRoutine());
        return false; //let the spawner handle the networking
    }

    protected IEnumerator DurationRoutine() {
        OnDurationBegin();
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            OnDurationTick(Mathf.InverseLerp(startTime, endTime, Time.time));
            yield return null;
        }

        durationCoroutine = null;
    }

    public virtual void setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.DURATION:
            default:
                duration = new MultiplierFloatStat(value);
                break;
        }
    }

    protected void OnDurationBegin() {
        toSpawn.Clear();
        PoissonDiscSampler poissonSampler = new PoissonDiscSampler(2 * (spawnAreaRadius + 1), 2 * (spawnAreaRadius + 1), 2 * spawnElementRadius);
        List<Vector2> points = new List<Vector2>();

        PoissonDiscSampler.PositionCheck check = (point) => {
            return Vector2.Distance(point, poissonSampler.center) < (spawnAreaRadius - spawnElementRadius);
        };

        foreach (Vector2 point in poissonSampler.Samples(check)) {
            Assert.IsTrue(Vector2.Distance(point, poissonSampler.center) < (spawnAreaRadius - spawnElementRadius));
            points.Add(point - poissonSampler.center);
        }

        points.Sort(delegate (Vector2 x, Vector2 y) {
            //lazy; possibly use the determinant method of angle comparison?
            float angle1 = Mathf.Atan2(x.y, -x.x);
            float angle2 = Mathf.Atan2(y.y, -y.x);
            return angle1.CompareTo(angle2);
        });

        foreach(Vector2 point in points) {
            float angle = Mathf.Atan2(point.y, -point.x);
            angle = (angle / (2 * Mathf.PI)) + 0.5f; //convert from radian range of [-pi..pi) to [0..1)
            toSpawn.Enqueue(new TimestampedData<Vector2>(angle, point));
        }
    }

    protected void OnDurationTick(float lerpProgress) {
        while(toSpawn.Count > 0 && toSpawn.Peek().outputTime <= lerpProgress) {
            Vector2 data = toSpawn.Dequeue().data;

            Vector3 data3D = new Vector3(data.x, 0, data.y);

            spawner.position = transform.position + data3D;
            spawner.rotation = Quaternion.LookRotation(data3D);
            spawnTrigger.Trigger();
        }
    }
}

/// Poisson-disc sampling using Bridson's algorithm.
/// Adapted from http://gregschlom.com/devlog/2014/06/29/Poisson-disc-sampling-Unity.html, 
/// which was adapted from Mike Bostock's Javascript source: http://bl.ocks.org/mbostock/19168c663618b7f07158
///
/// See here for more information about this algorithm:
///   http://devmag.org.za/2009/05/03/poisson-disk-sampling/
///   http://bl.ocks.org/mbostock/dbb02448b0f93e4c82c3
///
/// Usage:
///   PoissonDiscSampler sampler = new PoissonDiscSampler(10, 5, 0.3f);
///   foreach (Vector2 sample in sampler.Samples()) {
///       // ... do something, like instantiate an object at (sample.x, sample.y) for example:
///       Instantiate(someObject, new Vector3(sample.x, 0, sample.y), Quaternion.identity);
///   }
///
/// Adapted from Author: Gregory Schlomoff (gregory.schlomoff@gmail.com)
public class PoissonDiscSampler {
    private const int k = 30;  // Maximum number of attempts before marking a sample as inactive.

    private readonly Rect rect;
    private readonly float radius2;  // radius squared
    private readonly float cellSize;
    private Vector2[,] grid;
    public Vector2 center { get { return new Vector2(rect.width, rect.height) / 2; } }
    private List<Vector2> activeSamples = new List<Vector2>();

    public delegate bool PositionCheck(Vector2 position);

    /// Create a sampler with the following parameters:
    ///
    /// width:  each sample's x coordinate will be between [0, width]
    /// height: each sample's y coordinate will be between [0, height]
    /// radius: each sample will be at least `radius` units away from any other sample, and at most 2 * `radius`.
    public PoissonDiscSampler(float width, float height, float radius) {
        rect = new Rect(0, 0, width, height);
        radius2 = radius * radius;
        cellSize = radius / Mathf.Sqrt(2);
        grid = new Vector2[Mathf.CeilToInt(width / cellSize),
                           Mathf.CeilToInt(height / cellSize)];
    }

    /// Return a lazy sequence of samples. You typically want to call this in a foreach loop, like so:
    ///   foreach (Vector2 sample in sampler.Samples()) { ... }
    public IEnumerable<Vector2> Samples(PositionCheck checkParameter = null) {
        PositionCheck check = checkParameter;

        //delegates aren't compile-time constant; this allows me to set checkParameter as an optional parameter
        if (check == null) {
            check = (vec) => { return rect.Contains(vec); };
        }

        // First sample is choosen randomly

        Vector2 firstResult = new Vector2(Random.value * rect.width, Random.value * rect.height);
        while(!check(firstResult)) {
            firstResult = new Vector2(Random.value * rect.width, Random.value * rect.height);
        }
        yield return AddSample(firstResult);

        while (activeSamples.Count > 0) {

            // Pick a random active sample
            int i = (int)Random.value * activeSamples.Count;
            Vector2 sample = activeSamples[i];

            // Try `k` random candidates between [radius, 2 * radius] from that sample.
            bool found = false;
            for (int j = 0; j < k; ++j) {

                float angle = 2 * Mathf.PI * Random.value;
                float r = Mathf.Sqrt(Random.value * 3 * radius2 + radius2); // See: http://stackoverflow.com/questions/9048095/create-random-number-within-an-annulus/9048443#9048443
                Vector2 candidate = sample + r * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                // Accept candidates if it's inside the rect and farther than 2 * radius to any existing sample.
                if (check(candidate) && IsFarEnough(candidate)) {
                    Assert.IsTrue(rect.Contains(candidate));
                    
                    found = true;
                    yield return AddSample(candidate);
                    break;
                }
            }

            // If we couldn't find a valid candidate after k attempts, remove this sample from the active samples queue
            if (!found) {
                activeSamples[i] = activeSamples[activeSamples.Count - 1];
                activeSamples.RemoveAt(activeSamples.Count - 1);
            }
        }
    }

    private bool IsFarEnough(Vector2 sample) {

        GridPos pos = new GridPos(sample, cellSize);

        int xmin = Mathf.Max(pos.x - 2, 0);
        int ymin = Mathf.Max(pos.y - 2, 0);
        int xmax = Mathf.Min(pos.x + 2, grid.GetLength(0) - 1);
        int ymax = Mathf.Min(pos.y + 2, grid.GetLength(1) - 1);

        for (int y = ymin; y <= ymax; y++) {
            for (int x = xmin; x <= xmax; x++) {
                Vector2 s = grid[x, y];
                if (s != Vector2.zero) {
                    Vector2 d = s - sample;
                    if (d.x * d.x + d.y * d.y < radius2) return false;
                }
            }
        }

        return true;

        // Note: we use the zero vector to denote an unfilled cell in the grid. This means that if we were
        // to randomly pick (0, 0) as a sample, it would be ignored for the purposes of proximity-testing
        // and we might end up with another sample too close from (0, 0). This is a very minor issue.
    }

    /// Adds the sample to the active samples queue and the grid before returning it
    private Vector2 AddSample(Vector2 sample) {
        activeSamples.Add(sample);
        GridPos pos = new GridPos(sample, cellSize);
        grid[pos.x, pos.y] = sample;
        return sample;
    }

    /// Helper struct to calculate the x and y indices of a sample in the grid
    private struct GridPos {
        public int x;
        public int y;

        public GridPos(Vector2 sample, float cellSize) {
            x = (int)(sample.x / cellSize);
            y = (int)(sample.y / cellSize);
        }
    }
}