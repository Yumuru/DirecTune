using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class EnemyGhostStep {
    public static Subject<Unit> OnGhostStep = new Subject<Unit>();
    public static SetStepAction(EnemyGhost ghost) {
    }
}
