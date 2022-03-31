using UnityEngine;

public class FindingTree : FSMNode
{
    private FruitGatheringFSM _villager;
    private Animator _anim;
    public GameObject target = null;
    private int idleHash = Animator.StringToHash("Idle");
    private TreeCapacity fruitTree;

    public FindingTree(FruitGatheringFSM villager, Animator anim)
    {
        _villager = villager;
        _anim = anim;
    }
    public void Entry()
    {
        _anim.SetTrigger(idleHash);
        //Debug.Log(_villager.name + " Enter Searching");
    }
    public void Do()
    {
        _villager.TargetFruitTree = ChooseTree();
    }
    private GameObject ChooseTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("fruitTree");
        foreach (GameObject tree in trees)
        {
            fruitTree = tree.GetComponent<TreeCapacity>();
            if (fruitTree._fruit > 0)
            {
                _villager.tree = fruitTree;
                return tree;
            }
        }
        return null;
    }
    public void Exit()
    {

    }

}
