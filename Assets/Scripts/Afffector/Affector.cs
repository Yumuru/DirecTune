using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public delegate T2 Affector<T1, T2>(T1 value);
public delegate T Getter<T>();

public static class Affector {
    public static Affector<T1, T2> New<T1, T2>(Affector<T1, T2> affector) => affector;
    public static Affector<T1, T3> Append<T1, T2, T3>(this Affector<T1, T2> affector, Affector<T2, T3> next) =>
        v => next(affector(v));
    public static Affector<T1, T2> Append<T1, T2>(this Affector<T1, T2> affector, Action<T2> action) =>
        v => { 
            var ret = affector(v);
            action(ret);
            return ret;
        };

    public static Affector<T, T> New<T>() => v => v;

    public static Affector<T, T> Append<T, T2>(this Affector<T, T> affector, Func<T, T2> select, Func<Getter<T>, Affector<T2, T2>, Affector<T2, T2>> genAffect) {
        T value = default(T);
        var affect = genAffect(() => value, Affector.New<T2>());
        return v => {
            value = affector(v);
            affect(select(value));
            return value;
        };
    }

    public static void Test() {
        Affector.New<GameObject>()
            .Append(go => go.transform.position, (go, vAffector) => vAffector
            .Append(v => 3f * v)
            .Append(v => go().transform.position = v));
    }
}
