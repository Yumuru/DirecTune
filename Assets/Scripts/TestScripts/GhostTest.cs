using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GhostTest : MonoBehaviour
{
    [SerializeField]
    private EnemyGhost m_prefabEnemyGhost;
    private EnemyGhost m_enemyGhost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            var parameter = new GhostNoteParameter(new Timing(0, 0, 0), 0, 0);
            m_enemyGhost = Instantiate(m_prefabEnemyGhost);
            m_enemyGhost.Initialize(parameter);
        }
        if (Input.GetKeyDown(KeyCode.U)) {
            m_enemyGhost.m_onStep.OnNext(Unit.Default);
        }
    }
}
