using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireson_Move : MonoBehaviour
{
    private AudioManager audio;
    // Start is called before the first frame update
    void Start()
    {
        //audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveZ -= 0.1f;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
         //   audio.Play("fireson_move");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveX -= 0.1f;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
            //audio.Play("fireson_move");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveX += 0.1f;
            //audio.Play("fireson_move");
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("fireson_move");
        }

        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    
    }
}
