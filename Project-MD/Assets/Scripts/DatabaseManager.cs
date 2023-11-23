using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;


public class DatabaseManager : MonoBehaviour
{
    public Text detailsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SingIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        // 구글 플레이 계정접속 성공시
        if(status == SignInStatus.Success)
        {
            // continue with Play Games services
            Debug.Log(" SingInStatus. Success : 접속 성공");

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            detailsText.text = " Success to Login + \n" + name;
        }
    
        else
        {
            //Disable your integration with Play Games Services or show a login button
            //to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).

            detailsText.text = " Sign in Failed";

        }

    }

}
