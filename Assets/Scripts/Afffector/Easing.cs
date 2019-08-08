using System;
using UnityEngine;

public static class Easing {
    public static Func<float, float> SetEase(this Func<float, float> func, Func<float, float> ease) {
        return t => ease(func(t));
    }

    public static Func<float, float> Linear =
        t => Mathf.Lerp(0f, 1f, t);
    public static Func<float, float> Spring =
        t => {
            t = Mathf.Clamp01(t);
            t = (Mathf.Sin(t * Mathf.PI * (0.2f + 2.5f * t * t * t)) * Mathf.Pow(1f - t, 2.2f) + t) * (1f + (1.2f * (1f - t)));
            return t;
        };
    public static Func<float, float> InQuad =
        t => t * t; 
    public static Func<float, float> OutQuad =
        t => -t * (t - 2);
    public static Func<float, float> InOutQuad =
        t => {
            t /= 0.5f;
            if (t < 1) return 0.5f * t * t;
            t--;
            return -0.5f * (t * (t - 2) - 1);
        };
    public static Func<float, float> InCubic =
        t => t * t * t;
    public static Func<float, float> OutCubic =
        t =>{
            t--;
            return (t * t * t + 1);
        };
    public static Func<float, float> InOutCubic = 
        t => {
            t /= 0.5f;
            if (t < 1) return 0.5f * t * t * t;
            t -= 2;
            return 0.5f * (t * t * t + 2);
        };
    public static Func<float, float> InQuart =
        t => t * t * t * t;
    public static Func<float, float> OutQuart =
        t => {
            t--;
            return -(t * t * t * t - 1);
        };
    public static Func<float, float> InOutQuart =
        t => {
            t /= 0.5f;
            if (t < 1) return 0.5f * t * t * t * t;
            t -= 2;
            return -0.5f * (t * t * t * t - 2);
        };
    public static Func<float, float> InQuint =
        t => t * t * t * t * t;
    public static Func<float, float> OutQuint =
        t => {
            t--;
            return (t * t * t * t * t + 1);
        };
    public static Func<float, float> InOutQuint =
        t => {
            t /= 0.5f;
            if (t < 1) return 0.5f * t * t * t * t * t;
            t -= 2;
            return 0.5f * (t * t * t * t * t + 2);
        };
    public static Func<float, float> InSine =
        t => -Mathf.Cos(t * (Mathf.PI * 0.5f)) + 1f;
    public static Func<float, float> OutSine =
        t => Mathf.Sin(t * (Mathf.PI * 0.5f));
    public static Func<float, float> InOutSine =
        t => -0.5f * (Mathf.Cos(Mathf.PI * t - 1));
}
