using System.Collections;
using UnityEngine;

public class FSMSchedular : MonoBehaviour
{
    private FruitGatheringFSM high;
    private FruitGatheringFSM low;
    private GameObject[] Villagers;
    void Start()
    {
        Villagers = GameObject.FindGameObjectsWithTag("FSM");
        if (Villagers[0].GetComponent<FruitGatheringFSM>().Priority < Villagers[1].GetComponent<FruitGatheringFSM>().Priority)
        {
            low = Villagers[0].GetComponent<FruitGatheringFSM>();
            high = Villagers[1].GetComponent<FruitGatheringFSM>();
        }
        else if (Villagers[1].GetComponent<FruitGatheringFSM>().Priority < Villagers[0].GetComponent<FruitGatheringFSM>().Priority)
        {
            low = Villagers[1].GetComponent<FruitGatheringFSM>();
            high = Villagers[0].GetComponent<FruitGatheringFSM>();
        }
        StartCoroutine(RunPerSec());
    }
    void Update()
    {
        high.UpdateFSM();
    }
    IEnumerator RunPerSec()
    {
        for (; ; )
        {
            low.UpdateFSM();
            yield return new WaitForSeconds(1f);
        }
    }


}
