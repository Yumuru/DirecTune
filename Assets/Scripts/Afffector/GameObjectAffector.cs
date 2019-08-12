using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityAffector {
    public static Affector<T, Transform> SetPosition<T>(this Affector<T, Transform> affector, Func<Transform, Vector3> vector) =>
        affector.Append(t =>  { t.position = vector(t); return t; });
    public static Affector<T, Transform> SetLocalPosition<T>(this Affector<T, Transform> affector, Func<Transform, Vector3> vector) =>
        affector.Append(t =>  { t.localPosition = vector(t); return t; });
    public static Affector<T, Transform> SetRotation<T>(this Affector<T, Transform> affector, Func<Transform, Quaternion> quat) =>
        affector.Append(t =>  { t.rotation = quat(t); return t; });
    public static Affector<T, Transform> SetLocalRotation<T>(this Affector<T, Transform> affector, Func<Transform, Quaternion> quat) =>
        affector.Append(t =>  { t.localRotation = quat(t); return t; });
    public static Affector<T, Transform> SetScale<T>(this Affector<T, Transform> affector, Func<Transform, Vector3> vector) =>
        affector.Append(t =>  { t.localScale = vector(t); return t; });
    public static Affector<T, GameObject> SetMaterial<T>(this Affector<T, GameObject> affector, Material material) =>
        affector.Append(go => { go.GetComponent<Renderer>().material = material; return go; });
}
