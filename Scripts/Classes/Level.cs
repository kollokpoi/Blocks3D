using Assets.Scripts.Classes;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Настройки")]
    public List<Biom> Bioms;

    [Header("Префабы")]
    public GameObject PrefBlock;
    public GameObject PrefMoney;

    public static Level init;
    public Level()
    {
        init = this;
    }

    [ContextMenu("Генерация уровня")]
    public void CreateLevel()
    {
        foreach (Biom Biom in Bioms)
        {
            foreach (Block Block in Biom.Blocks)
            {
                if (Block.Init)
                {
                    if (Biom.Vector == Vector.Center)
                    {
                        GameObject _object = Instantiate(PrefMoney, Block.Parent.position, Quaternion.identity);
                        _object.transform.parent = transform;
                        break;
                    }
                    else
                    {
                        Block.SceneBlock = Instantiate(PrefBlock, Block.Parent.position, Quaternion.identity);
                        Block.SceneBlock.transform.parent = transform;
                        Block.SceneBlock.GetComponent<BlockTransform>().VectorBlock = Biom.Vector;
                    }
                }
            }
        }
    }

    public bool CheckEndLevel()
    {
        // Мы прошли уровень
        bool EndLevel = true;
        // Проверяем биомы
        foreach (Biom Biom in Bioms)
        {
            // Проверяем блоки
            foreach (Block Block in Biom.Blocks)
            {
                // Блок создан
                if (Block.Init)
                {
                    // Не ценральный бок
                    if (Biom.Vector != Vector.Center)
                    {
                        // Если блок не занял своё место
                        if (Block.SceneBlock.GetComponent<BlockTransform>().EndPosition == false)
                        {
                            // Уровень не пройден
                            EndLevel = false;
                        }
                    }
                }
            }
        }
        return EndLevel;
    }
}
