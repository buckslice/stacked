using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TimestampedData<T>
{
    /// <summary>
    /// Time at which this data will be exactly displayed to the player.
    /// </summary>
    public readonly double outputTime;

    public readonly T data;

    public TimestampedData(double outputTime, T data)
    {
        this.outputTime = outputTime;
        this.data = data;
    }

    public static implicit operator T(TimestampedData<T> timestampedData)
    {
        return timestampedData.data;
    }

    public static Vector3 getVelocity(TimestampedData<Vector3> start, TimestampedData<Vector3> end)
    {
        Vector3 distanceDifference = end.data - start.data;
        if (distanceDifference.sqrMagnitude == 0)
        {
            return Vector3.zero;
        }
        else
        {
            float timeDifference = (float)(end.outputTime - start.outputTime);
            return distanceDifference / timeDifference;
        }
    }
}
