using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 npc 입력값에 따른 움직임& 애니메이션& 사운드 구현
public class fireson_Move : MonoBehaviour
{
    private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        //audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveZ += 0.1f;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
            animation.Play("walks");
            animation.PlayQueued("idle");
           
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveZ -= 0.1f;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
            animation.Play("walks"); 
            animation.PlayQueued("idle");
            
            //   audio.Play("fireson_move");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveX -= 0.1f;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
            animation.Play("walks");
            animation.PlayQueued("idle");
           
            //audio.Play("fireson_move");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveX += 0.1f;
            //audio.Play("fireson_move");
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
            animation.Play("walks");
            animation.PlayQueued("idle");
           
        }

        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    
    }
}
