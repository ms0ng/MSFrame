using MSFrame.Xml;
using System.Collections;
using UnityEngine;


public class CharaterData : XmlObject<int>
{
    public int ID;
    public string Name;
    public float Attack;
    public float Defence;
    public float Speed;
    public override int GetID() => ID;
}
