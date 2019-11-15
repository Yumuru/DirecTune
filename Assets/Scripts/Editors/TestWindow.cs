using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UniRx;

public class TestWindow : EditorWindow {
    [MenuItem("Custom/TestWindow")]
    static void ShowWindow() {
        EditorWindow.GetWindow<TestWindow>();
    }

    void OnGUI() {
        if (GUILayout.Button("Start")) {
            GameManager.Ins.m_onPlay.OnNext(Unit.Default);
        }

        if (GUILayout.Button("SpawnLeft")) {
            GameManager.Ins.m_enemyGhostManager.SpawnGhost(0);
        }
        if (GUILayout.Button("SpawnCenter")) {
            GameManager.Ins.m_enemyGhostManager.SpawnGhost(1);
        }
        if (GUILayout.Button("SpawnRight")) {
            GameManager.Ins.m_enemyGhostManager.SpawnGhost(2);
        }
    }
}