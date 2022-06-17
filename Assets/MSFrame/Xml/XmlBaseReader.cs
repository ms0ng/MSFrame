using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace MSFrame.Xml
{
    public abstract class XmlBaseReader<TKey, TValue> : SingletonR<XmlBaseReader<TKey, TValue>> where TValue : XmlObject<TKey>
    {
        public const string ROOT_NODE_NAME = "RECORDS";
        public abstract string xmlText { get; }
        public Dictionary<TKey, TValue> Dict { get { return mDataDict; } }

        public int Count { get { return mDataDict.Count; } }

        private Dictionary<TKey, TValue> mDataDict = new Dictionary<TKey, TValue>();
        public void ReadXml(string xmlText = null)
        {
            XmlDocument doc = new XmlDocument();
            if (xmlText == null)
            {
                xmlText = this.xmlText;
            }

            try
            {
                doc.LoadXml(xmlText);
            }
            catch
            {
                throw;
            }

            XmlNode root = doc.SelectSingleNode(ROOT_NODE_NAME);
            if (root == null)
                return;
            XmlNodeList nodes = root.ChildNodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode pNode = nodes[i];
                if (!(pNode is XmlElement element))
                {
                    continue;
                }
                TValue value = CreateDataInstance(element);
                mDataDict[value.GetID()] = value;
            }
        }

        public TValue Get(TKey key)
        {
            mDataDict.TryGetValue(key, out TValue v);
            return v;
        }

        public bool ContainsKey(TKey key)
        {
            return mDataDict.ContainsKey(key);
        }
        public abstract TValue CreateDataInstance(XmlElement data);
    }
}