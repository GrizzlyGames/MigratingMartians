using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Button : MonoBehaviour {

    public int index;

    public GameObject closeGO;
    public Store_Button button;
    public Store_Manager store;

    public void OpenStore()
    {
        closeGO.SetActive(false);
        store.OpenStore();
    }

    public void SetStore(int i)
    {
        store.SetStore(i);
        button.index = index;
    }

    public void Purchase()    {        
        store.Purchase(index);
    }
}
