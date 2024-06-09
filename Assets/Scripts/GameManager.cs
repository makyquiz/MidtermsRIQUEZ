using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Multiple spawn points
    public Transform firstSpawnPoint;

    GameObject cloneChick;
    GameObject cloneEgg;
    GameObject cloneRooster;
    GameObject cloneHen;

    public GameObject[] chickenLife; // 0: Egg, 1: Chick, 2: Hen, 3: Rooster

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI eggCountText;
    public TextMeshProUGUI chickCountText;
    public TextMeshProUGUI henCountText;
    public TextMeshProUGUI roosterCountText; // Display the counts

    // Counters for Eggs, Chicks, Roosters, and Hens
    private int eggCount = 0;
    private int chickCount = 0;
    private int roosterCount = 0;
    private int henCount = 0;

    private bool isGameRunning = true;

    void Start()
    {
        eggCount = 1; // Initial egg
        UpdateCounters();
        Invoke("FirstEggHatch", 10f); // Start the lifecycle with the first egg hatching
    }

    void Update()
    {

        if (!isGameRunning)
        {
            return;
        }
    }

    void FirstEggHatch()
    {
        HatchEgg(firstSpawnPoint);
    }

    void HatchEgg(Transform spawnPoint)
    {
        cloneChick = Instantiate(chickenLife[1], spawnPoint.position, spawnPoint.rotation);
        eggCount--;
        chickCount++;
        UpdateCounters();
        Invoke("FirstChickMature", 10f); // Mature after 10 seconds
    }

    void FirstChickMature()
    {
        Transform spawnPoint = GetRandomSpawnPoint();

        // Always turn the first chick into a hen
        cloneHen = Instantiate(chickenLife[2], spawnPoint.position, spawnPoint.rotation);
        henCount++;
        UpdateCounters();
        Invoke("HenLayEggs", 30f); // Lay eggs after 30 seconds
        Invoke("DestroyHen", 70f); // 30s to lay eggs + 40s lifespan

        chickCount--;
        UpdateCounters();
        Destroy(cloneChick);
    }

    void HenLayEggs()
    {
        int eggsToLay = Random.Range(2, 11); // Lay between 2 to 10 eggs
        for (int i = 0; i < eggsToLay; i++)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            CreateEgg(spawnPoint);
        }
    }

    void CreateEgg(Transform spawnPoint)
    {
        cloneEgg = Instantiate(chickenLife[0], spawnPoint.position, spawnPoint.rotation);
        eggCount++;
        UpdateCounters();
        Invoke("EggHatch", 10f); // Hatch after 10 seconds
    }

    void EggHatch()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        cloneChick = Instantiate(chickenLife[1], spawnPoint.position, spawnPoint.rotation);
        eggCount--;
        chickCount++;
        UpdateCounters();
        Destroy(cloneEgg);
        Invoke("ChickMature", 10f); // Mature after 10 seconds
    }

    void ChickMature()
    {
        Transform spawnPoint = GetRandomSpawnPoint();

        if (chickCount == 1) // If this is the first chick after the initial one, allow random evolution
        {
            if (Random.value < 0.5f)
            {
                cloneHen = Instantiate(chickenLife[2], spawnPoint.position, spawnPoint.rotation);
                henCount++;
                UpdateCounters();
                Invoke("HenLayEggs", 30f);
                Invoke("DestroyHen", 70f);
            }
            else
            {
                cloneRooster = Instantiate(chickenLife[3], spawnPoint.position, spawnPoint.rotation);
                roosterCount++;
                UpdateCounters();
                Invoke("DestroyRooster", 40f);
            }
        }
        else // Subsequent chicks can evolve randomly
        {
            if (Random.value < 0.5f)
            {
                cloneHen = Instantiate(chickenLife[2], spawnPoint.position, spawnPoint.rotation);
                henCount++;
                UpdateCounters();
                Invoke("HenLayEggs", 30f);
                Invoke("DestroyHen", 70f);
            }
            else
            {
                cloneRooster = Instantiate(chickenLife[3], spawnPoint.position, spawnPoint.rotation);
                roosterCount++;
                UpdateCounters();
                Invoke("DestroyRooster", 40f);
            }
        }

        chickCount--;
        UpdateCounters();
        Destroy(cloneChick);
    }

    void DestroyHen()
    {
        henCount--;
        UpdateCounters();
        Destroy(cloneHen);
    }

    void DestroyRooster()
    {
        roosterCount--;
        UpdateCounters();
        Destroy(cloneRooster);
    }

    void UpdateCounters()
    {
        eggCountText.text = eggCount.ToString();
        chickCountText.text = chickCount.ToString();
        henCountText.text = henCount.ToString();
        roosterCountText.text = roosterCount.ToString();
    }

    Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}