using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsHandler : MonoBehaviour
{
    [SerializeField]private List<GameObject> holderObjectsSelectionMark, holderObjectsLocks;
    [SerializeField]private string Object_Name;

    private void OnEnable()
    {//DANI
        foreach (GameObject locks in holderObjectsLocks)
        {
            if (PlayerPrefs.GetString(locks.transform.parent.gameObject.name) == "NO"
                && PlayerPrefs.GetString("SubscriptionPurchased") != "YES")
                locks.SetActive(true);
            else
                locks.SetActive(false);
        }
        SelectHolderObject(PlayerPrefs.GetInt(Object_Name));

    }
    public void SelectHolderObject(int index)
    {
        foreach (GameObject selectionMark in holderObjectsSelectionMark)
            selectionMark.SetActive(false);
        holderObjectsSelectionMark[index].SetActive(true);
    }
    public void SetSelectedObjectValue(int index)
    {
        PlayerPrefs.SetInt(Object_Name, index);

    }
    public void EnableLocks(bool status)
    {
        foreach (GameObject locks in holderObjectsLocks)
            locks.SetActive(status);

    }

}
