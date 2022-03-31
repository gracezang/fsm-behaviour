using UnityEngine;

public class PickingFruits : FSMNode
{
    private FruitGatheringFSM _villager;
    private Animator _anim;
    public float speed = 5.0f;
    private int pickHash = Animator.StringToHash("Pickup");
    public PickingFruits(FruitGatheringFSM villager, Animator anim)
    {
        _villager = villager;
        _anim = anim;

    }
    public void Entry() 
    {
        _anim.SetTrigger(pickHash);
        //Debug.Log(_villager.name + " Enter Picking ");
    }
    public void Do(){
        if(_villager.TargetFruitTree != null)
        {
            _villager.pickFruit();
        } 
    }
    public void Exit()
    {
       
    }
    
}
