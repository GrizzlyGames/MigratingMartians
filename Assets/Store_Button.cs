using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Button : MonoBehaviour {

    public Store_Manager store;

    public void Purchase(int index)    {        
        store.Purchase(index);
    }
}
