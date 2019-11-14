using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UniRx;

public class GhostSpownManager : MonoBehaviour
{
    public PlayableDirector m_playableDirector;
    private void Awake() {
		GetComponentInParent<GameManager>().m_ghostSpawnManager = this;
        m_playableDirector = GetComponent<PlayableDirector>();
    }
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
