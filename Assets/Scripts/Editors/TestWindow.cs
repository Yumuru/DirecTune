using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestWindow : EditorWindow {
    [MenuItem("Custom/TestWindow")]
    static void ShowWindow() {
        EditorWindow.GetWindow<TestWindow>();
    }

    void OnGUI() {
        if (GUILayout.Button("SpawnRight")) {
            GameManager.Ins.m_enemyGhostManager.SpawnGhost(2);
        }
    }
}