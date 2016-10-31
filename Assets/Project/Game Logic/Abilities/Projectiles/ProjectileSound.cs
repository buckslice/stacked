using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ProjectileSound : MonoBehaviour
{

    AudioSource target;
    void Awake()
    {
        target = GetComponent<AudioSource>();
        target.Play();
    }

}
