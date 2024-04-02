using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Ссылку на GameController
    /// </summary>
    public static GameController init;

    /// <summary>
    /// Коллекция уровней
    /// </summary>
    public List<GameObject> Levels;
    /// <summary>
    /// Создаваемый уровень
    /// </summary>
    private int iLevel = 0;

    private GameObject InitLEvel;
    public bool Game = false;

    public GameController()
    {
        init = this;
    }

    void Update()
    {
        if (Game)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                RaycastHit hit; // содержит информацию о попадении
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Выпускаемый луч
                                                                             // Проверка на то попали ли мы?
                if (Physics.Raycast(ray, out hit))
                {
                    // Получаем центральный биом уровня
                    Biom BiomCenter = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Center);
                    // Создаём динамичный биом, который равен в зависимости
                    // от того куда нажали
                    Biom BiomSelected = new Biom();
                    // Если нажали вверх
                    if (hit.transform.name == "Top")
                    {
                        // Получаем нижний биом
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Bottom);
                    }
                    // Если нажали вниз
                    else if (hit.transform.name == "Bottom")
                    {
                        // Получаем верхний бирм
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Top);
                    }
                    // Влево
                    else if (hit.transform.name == "Left")
                    {
                        // Правый
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Right);
                    }
                    // Вправо
                    else if (hit.transform.name == "Right")
                    {
                        // Левый
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Left);
                    }
                    // Перебираем блоки выбранного биома
                    for (int iBlock = 0; iBlock < BiomSelected.Blocks.Count; iBlock++)
                    {
                        // Упрощение плохого кода
                        Block BlockBiom = BiomSelected.Blocks[iBlock];
                        // Если у плашки, создан блок
                        if (BlockBiom.Init)
                        {
                            // Тогда в позицию на которую нужно переехать блоку, 
                            //указываем родителя центрального биома
                            BlockBiom.SceneBlock.GetComponent<BlockTransform>().target =
                                BiomCenter.Blocks[iBlock].Parent;
                        }
                    }
                }
            }

            
        }
    }
    [ContextMenu("Создание")]
    public void CreateLevel()
    {
        if (InitLEvel != null)
            Destroy(InitLEvel);

        if (iLevel == Levels.Count)
            return;

        if (Game)
        {
            UIController.init.AddTimer(5);
        }

        InitLEvel = Instantiate(Levels[iLevel], Vector3.zero, Quaternion.identity);
        Level.init.CreateLevel();
        iLevel++;
    }
    public void EndGame()
    {
        UIController.init.EndGame();
        Game = false;
    }
    public int GetILevel => iLevel - 1;
}
