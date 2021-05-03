using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTimeSystem : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    float timeSystem;
    void Start()
    {
        Time.timeScale = timeSystem;
    }
}
