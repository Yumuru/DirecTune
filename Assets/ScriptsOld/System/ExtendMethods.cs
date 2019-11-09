using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class ExtendMethods {
    public static T Do<T>(this T obj, Action action) {
        action(); return obj;
    }
    public static T Do<T>(this T obj, Action<T> action) {
        action(obj); return obj;
    }

    public static IObservable<TimeParameter<T>> Anim<T>(this IObservable<T> observable, float actTime) {
        var sTime = Time.time;
        var stop = false;
        return observable
            .TakeWhile(_ => !stop)
            .Select(v => {
                var time = Time.time - sTime;
                if (time > actTime) {
                    stop = true;
                    return new TimeParameter<T>() { value = v, time = actTime, rate = 1f };
                }
                return new TimeParameter<T>() { value = v, time = time, rate = time / actTime };
            });
    }

    public static IObservable<TimeParameter<Unit>> Anim(this Component component, float actTime) {
        return Anim(component.UpdateAsObservable(), actTime)
            .TakeUntil(component.OnDestroyAsObservable());
    }

    public static IObservable<Unit> PlayDestroy (this ParticleSystem particle) {
        var observable =
        particle.UpdateAsObservable()
            .SkipWhile(_ => particle.isPlaying);
        observable
            .Subscribe(_ => GameObject.Destroy(particle.gameObject));
        return observable;
    }

    public static T RandomGet<T>(this T[] values) {
        return values[UnityEngine.Random.Range(0, values.Length - 1)];
    }
}

public struct TimeParameter<T> {
    public T value;
    public float time;
    public float rate;
}