using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Item Properties")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemText;

    public string ItemName {  get { return itemName; } }
    public Sprite ItemIcon { get {  return itemIcon; } }
    public string ItemText { get {  return itemText; } }
}
