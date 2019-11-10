using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KontrabasGhost : MonoBehaviour {
    public PerformerGhost m_performerGhost;

    private void Start() {
        m_performerGhost.Initialize(gameObject);
        m_performerGhost.Play();
    }
}
