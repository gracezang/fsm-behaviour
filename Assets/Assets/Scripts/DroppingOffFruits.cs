using UnityEngine;

public class DroppingOffFruits : FSMNode
{
    private FruitGatheringFSM _villager;
    private Animator _anim;
    private int dropHash = Animator.StringToHash("Drop");
    private int idleHash = Animator.StringToHash("Idle");
    public DroppingOffFruits(FruitGatheringFSM villager, Animator anim)
    {
        _villager = villager;
        _anim = anim;

    }
    //if no fruit to dropoff, don't paly animation
    public void Entry()
    {
        if (_villager._inHand > 0) 
        { 
            _anim.SetTrigger(dropHash);
        }
        if (_villager._inHand <= 0)
        {
            _anim.SetTrigger(idleHash);
        }
        //Debug.Log(_villager.name + " Enter Dropping");
    }
    public void Do()
    {
      _villager.dropFruit();
    }

    public void Exit()
    {

    }
}
