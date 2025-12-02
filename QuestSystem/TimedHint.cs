using UnityEngine;
using System;

[System.Serializable]
public struct TimedHint
{
    public string text;
    public float time;

    public TimedHint(string t, float ti)
    {
        text = t;
        time = ti;
    }
}
