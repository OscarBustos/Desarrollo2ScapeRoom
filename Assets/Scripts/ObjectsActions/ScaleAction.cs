using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScaleAction : ObjectAction
{
    [SerializeField] private Transform attachPoint;
    private GameObject item;
    private string result;
    private Transform previousItemParent;

    public string Result { get { return result; } }
    public override void Execute()
    {
        WeighItem();
    }

    private void WeighItem()
    {
        result = item ? item.GetComponent<Rigidbody>().mass.ToString() : "0";        
    }

    public void AddItem(GameObject item)
    {
        previousItemParent = item.transform.parent;
        item.transform.parent = attachPoint;
        item.transform.position = attachPoint.position;
        this.item = item;
    }

    internal void RemoveItem(GameObject item)
    {
        item.transform.parent = previousItemParent;
        item.transform.position = previousItemParent.position;
        this.item = null;
    }
}
