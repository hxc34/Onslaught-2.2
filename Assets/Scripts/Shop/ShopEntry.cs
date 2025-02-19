using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour
{
    public string id = "ShopEntry";
    public string name = "Shop Item";
    public string description = "I am a shop item, replace me";
    public (int, int) icon = (0, 0);
    public string category = "None";
    public string gives = "MyItemID";
}
