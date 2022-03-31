using UnityEngine;

public class InVillage : FSMNode
{
    private Animator _anim;
    private FruitGatheringFSM _villager;
    private int idleHash = Animator.StringToHash("Idle");

    public InVillage(FruitGatheringFSM villager, Animator animator)
    {
        _villager = villager;
        _anim= animator;
    }
    public void Entry()
    {
        _anim.SetTrigger(idleHash);
        //Debug.Log(_villager.name + " Enter InVillage");
    }
    public void Do()
    {
    }
    public void Exit()
    {
    }
}
