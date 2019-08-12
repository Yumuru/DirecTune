using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_SwitchBlockLighting : MonoBehaviour
{

    private MeshRenderer cubeLight; //床の光る部分
    private ParticleSystem effLight; //子のパーティクルシステム

    private bool lightTrigger = false; //コルーチン"PlayBlockLight"の呼び出し時に使用

    // Start is called before the first frame update
    void Start()
    {
        cubeLight = this.GetComponent<MeshRenderer>();
        cubeLight.enabled = false;

        effLight = transform.Find("sircle1_add").gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) //仮で条件をマウス左クリックに設定、ここをお化けがブロックの上に来た時にしてもらえるといい感じになるはず
        {
            if(lightTrigger == false)
            {
                lightTrigger = true;
                StartCoroutine("PlayBlockLight");
            }
        }
    }

    private IEnumerator PlayBlockLight()
    {
        cubeLight.enabled = true;
        effLight.Play();

        yield return new WaitForSeconds(0.5f); //床の光る継続時間をここで調整できる

        lightTrigger = false;
        cubeLight.enabled = false;
        effLight.Stop();
    }
}
