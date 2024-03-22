using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingProgress : IProgress<float>
{
    public event Action<float> OnProgressed;
    public float ratio = 1f;
    public void Report(float value)
    {
        OnProgressed?.Invoke(value / ratio);
    }
}
