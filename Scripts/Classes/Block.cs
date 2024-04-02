using UnityEngine;

namespace Assets.Scripts.Classes
{
    [System.Serializable]
    public class Block
    {
        /// <summary>
        /// Создаётся
        /// </summary>
        public bool Init;
        /// <summary>
        /// Родителя у которого он создаётся (плашка)
        /// </summary>
        public Transform Parent;
        /// <summary>
        /// Созданный блок на сцене
        /// </summary>
        public GameObject SceneBlock;
    }
}
