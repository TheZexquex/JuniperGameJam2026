using UnityEngine;

namespace Menu.MenuSystem
{
    public class UIPrefab
    {
        private const string _prefabPath = "Prefabs/UI/";
        protected string PrefabName;
        protected GameObject Instance;

        public UIPrefab(string prefabName)
        {
            PrefabName = prefabName;
        }
        
        public string PrefabPath => $"{_prefabPath}{PrefabName}";
        
        public GameObject GetInstance => Instance;
        
        public virtual void InstantiatePrefab(Transform parent)
        {
            var prefab = UnityEngine.Resources.Load<GameObject>(PrefabPath);
            Instance = Object.Instantiate(prefab, parent);
        }
    }
}