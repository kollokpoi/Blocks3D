using System.Collections.Generic;

namespace Assets.Scripts.Classes
{
    [System.Serializable]
    public class Biom
    {
        /// <summary>
        /// Вектор
        /// </summary>
        public Vector Vector;
        /// <summary>
        /// Список блоков в векторе (9, 4)
        /// </summary>
        public List<Block> Blocks;
    }

    /// <summary>
    /// Вектора направления
    /// </summary>
    public enum Vector
    {
        Top, Right, Bottom, Left, Center
    }
}
