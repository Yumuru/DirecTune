using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectAffector {
    public static Affector<T, GameObject> SetMaterial<T>(this Affector<T, GameObject> affector, Material material) =>
        affector.Append(go => { go.GetComponent<Renderer>().material = material; return go; });
}
