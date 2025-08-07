using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SettingsHandler : MonoBehaviour
{

    [SerializeField] string rateuslink, moregameslink, ourwebsite, termsofservices, billingterms, privacypolicy;

    [SerializeField] string Email, Subject, Body;
    public void RateUs()
    {
        Application.OpenURL(rateuslink);
    }
    public void MoreGames()
    {
        Application.OpenURL(moregameslink);
    }
    public void OpenWebsite()
    {
        Application.OpenURL(ourwebsite);
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL(privacypolicy);
    }
    public void GameModeClicked()
    {
    }
    string MyEscapeURL(string url)
    {
        //return WWW.EscapeURL(url).Replace("+", "20%");
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
        //return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    public void SendFeedback()
    {
        //Debug.LogError("Here.....");


        string email = Email;
        string subject = MyEscapeURL(Subject);
        string body = MyEscapeURL(Body);


        string emaillink = "mailto:" + email + "?subject=" + subject + "&body=" + body;

        //Debug.LogError($"Email {Email} \nSubject {Subject} \nBody  {Body} \nEmailLink {emaillink}");

        Application.OpenURL(emaillink);

        //Application.OpenURL(privacypolicy);



        //string email = "contentarcadegames @gmail.com";
        //string subject = MyEscapeURL("Pixel Art Feedback");
        //string body = MyEscapeURL("\n\n\nSent From,\n" +SystemInfo.deviceName + "\n" +SystemInfo.deviceModel);
        //Application.OpenURL("mailto:" +email + "?subject =" +subject + "&body =" +body);
    }

}
