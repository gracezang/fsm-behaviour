using UnityEngine;

public class WalkingToTree : FSMNode
{
    private FruitGatheringFSM _villager;
    private Animator _anim;
    public GameObject target;
    public float speed = 5.0f;
    bool atTree = false;
    private int walkHash = Animator.StringToHash("Walk");

    public WalkingToTree(FruitGatheringFSM villager, Animator anim)
    {
        _villager = villager;
        _anim = anim;

    }
    public void Entry() 
    {
        target = _villager.TargetFruitTree;
        _villager.transform.LookAt(target.transform.position);
        _anim.SetTrigger(walkHash);
        //Debug.Log(_villager.name + " Enter WalkingToTree");
    }

    //added level of detail
    public void Do()
    {
        if (atTree == false)
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
        if ((_villager.transform.position - target.transform.position).magnitude <= _villager.pickingDistance && !atTree)
        {
            atTree = true;
        }
    }
    //turn bool to false else stuck at tree
    public void Exit() {
        atTree = false;
        }

}
