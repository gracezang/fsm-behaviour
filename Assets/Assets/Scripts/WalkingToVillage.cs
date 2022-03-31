using UnityEngine;

public class WalkingToVillage : FSMNode
{
    private FruitGatheringFSM _villager;
    private Animator _anim;
    public GameObject targetVillage;
    public float speed = 5.0f;
    bool atVillage = false;
    private int walkHash = Animator.StringToHash("Walk");

    public WalkingToVillage(FruitGatheringFSM villager, Animator anim)
    {
        _villager = villager;
        _anim = anim;
    }
    public void Entry()
    {
        targetVillage = GameObject.FindGameObjectWithTag("village");
        _villager.transform.LookAt(targetVillage.transform.position);
        _anim.SetTrigger(walkHash);
        //Debug.Log(_villager.name + " Enter WalkingToVillage");
    }
    public void Do()
    {
        if (atVillage == false)
        {
            if (_villager.name == "Fred")
            {
                _villager.transform.position += _villager.transform.forward * speed * 1f;
            }
            else
            {
                _villager.transform.position += _villager.transform.forward * speed * Time.deltaTime;
            }
            _villager.transform.position =
            new Vector3(
            _villager.transform.position.x,
            Terrain.activeTerrain.SampleHeight(_villager.transform.position),
            _villager.transform.position.z);
        }
        if ((_villager.transform.position - targetVillage.transform.position).magnitude <= _villager.villageDistance && !atVillage)
        {
            atVillage = true;
        }
    }
    public void Exit()
    {
        atVillage = false;
    }
}
