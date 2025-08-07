using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ToastPlugin;

public class ToastMessageHandler : MonoBehaviour
{
    public static ToastMessageHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string msg)
    {
        //ToastHelper.ShowToast(msg, true);
    }
}
