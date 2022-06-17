using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;

namespace MSFrame.UI
{
    /// <summary>
    /// TODO:请根据项目情况手动改写<see cref="GetAtlas(string)"/>方法以获取图集
    /// <para></para>
    /// 图集管理类基类。针对<see cref="SpriteAtlas.GetSprite(string)"/>复制Clone的Sprite到内存这一点进行缓存。
    /// <para></para>
    /// 继承这个类并重写<see cref="AtlasName"/>，该值为图集在Addressable中的名称
    /// <para></para>
    /// 使用方法<see cref="GetSprite(string)"/>获取Sprite图片
    /// </summary>
    /// <typeparam name="T">单例类型</typeparam>
    public abstract class BaseSpriteAtlasManager<T> : Singleton<T> where T : new()
    {
        /// <summary>
        /// 图集名称，用于Log
        /// </summary>
        public abstract string AtlasName { get; set; }
        /// <summary>
        /// 图集，请勿直接访问。若要获取Sprite，请使用<see cref="GetSprite(string)"/>
        /// </summary>
        protected SpriteAtlas BaseAtlas;
        /// <summary>
        /// 若需要清除引用计数并释放内存，请调用方法<see cref="Clear"/>
        /// </summary>
        protected Dictionary<string, Sprite> SpriteCache;

        /// <summary>
        /// 初始化图集
        /// </summary>
        public override void Init()
        {
            if (BaseAtlas != null) return;
            Debug.Log($"加载图集{AtlasName}");
            SpriteCache = new Dictionary<string, Sprite>();
            BaseAtlas = GetAtlas(AtlasName);
        }

        /// <summary>
        /// 获取对应的图集，TODO:请根据项目实际情况改写此方法。
        /// </summary>
        /// <returns></returns>
        public SpriteAtlas GetAtlas(string addressablePath)
        {
            Debug.LogError($"请根据项目情况手动重写{GetType().Name}的{nameof(GetAtlas)}方法");
            SpriteAtlas spriteAtlas = null;
            //若使用Addressable作为资源加载方案，可以参考：
            //spriteAtlas = Addressables.LoadAssetAsync<SpriteAtlas>(addressablePath).WaitForCompletion();
            return spriteAtlas;
        }

        /// <summary>
        /// 获取Sprite Atlas中指定的Sprite
        /// </summary>
        /// <param name="name">Sprite Name</param>
        /// <returns>Sprite</returns>
        public Sprite GetSprite(string name)
        {
            if (SpriteCache == null)
            {
                Init();
                Debug.LogError($"{AtlasName}图集:尚未初始化");
            }

            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError($"{AtlasName}图集:传入名称为空");
                return null;
            }

            if (SpriteCache.ContainsKey(name))
            {
                return SpriteCache[name];
            }

            if (BaseAtlas == null)
            {
                Debug.LogError($"{AtlasName}图集加载异常:不存在该图集");
                return null;
            }

            if (BaseAtlas.spriteCount == 0)
            {
                Debug.LogError($"{AtlasName}图集加载异常:图集内图片数量为0");
                return null;
            }

            Sprite sprite = BaseAtlas.GetSprite(name);

            if (sprite == null)
            {
                Debug.LogWarning($"{AtlasName}图集:图片{name}获取失败,尝试返回“Unknown”图片");
                sprite = BaseAtlas.GetSprite("Unknown");
                if (sprite == null)
                {
                    Debug.LogError($"{AtlasName}图集:该图集没有“Unknown”图片，返回null");
                    return null;
                }
            }

            SpriteCache.Add(name, sprite);
            return sprite;
        }

        /// <summary>
        /// 清除缓存字典，释放资源。
        /// <para></para>
        /// 执行垃圾回收可能会暂时消耗一部分CPU性能，也可能会清理部分业务需要但引用计数为0的资源
        /// </summary>
        public void Clear()
        {
            SpriteCache.Clear();
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
    }

}