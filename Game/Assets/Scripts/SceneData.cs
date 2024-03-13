using Eflatun.SceneReference;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData
{
    [SerializeField]
    private SceneReference _reference;
    public string Name => _reference.Name;
    public SceneType SceneType;

    public SceneReference Reference => _reference;
}
