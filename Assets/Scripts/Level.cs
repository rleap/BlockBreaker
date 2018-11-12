﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    // Configuration parameters
    [SerializeField] int breakableBlocks; // Serialized for debugging purposes

    // Cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
