using System.Collections;
using UnityEngine;

namespace Assets.MSFrame.Xml.Example
{
    public class Program : MonoBehaviour
    {

        private void Awake()
        {
            AllCharactersData.Instance.ReadXml();
            Debug.Log("Start Listing All Chatater...");
            foreach (var kv in AllCharactersData.Instance.Dict)
            {
                int key = kv.Key;
                CharaterData charater = kv.Value;
                Debug.Log($"ID:{charater.ID} Name:{charater.Name} ATK:{charater.Attack} DEF:{charater.Defence}");
            }
            Debug.Log("Find ID 100001:" + AllCharactersData.Instance.Get(100001) != null);
        }
    }
}