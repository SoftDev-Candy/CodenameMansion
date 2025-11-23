using UnityEngine;
using System.Collections;
using TMPro;

public class Stove : ItemBase
{
    [SerializeField] int BakingTimeInSec;
    public TextMeshProUGUI Timer;
    public Transform CakeSpawnPos;
    public GameObject cakePrefab;
    public GameObject EnemySpawn;

    private void Start()
    {
        Timer.text = BakingTimeInSec.ToString();
        Timer.raycastTarget = false;
    }

    public override void DragAndDrop(ItemData item)
    {
        if ((int)droppableItem == item.Item.ID)
        {
            Inventory.Singleton.RemoveItem(item);
            GameManager.instance.SpawnEnemy(EnemySpawn);
            StartCoroutine(StartBaking(BakingTimeInSec));
        }
    }

    public override void Interact()
    {
        
    }

    public override void PickUp()
    {
        
    }

    
    IEnumerator StartBaking(int TargetTime)
    {

        while (TargetTime >= 0)
        {
            Timer.text = TargetTime.ToString();
            TargetTime -= 1;
            yield return new WaitForSeconds(1);
        }
        SpawnCake();
        yield return null;
    }

    void SpawnCake()
    {
        Debug.Log("Spawned the Cake");
        Instantiate(cakePrefab, CakeSpawnPos.position, Quaternion.identity, CakeSpawnPos);

    }
}
