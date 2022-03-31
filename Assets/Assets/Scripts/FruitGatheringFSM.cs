using System;
using UnityEngine;

public class FruitGatheringFSM : MonoBehaviour
{
    private FSM _fsm;
    private int _carryCapacity = 2;
    public GameObject[] hasTree;
    public GameObject TargetFruitTree;
    public GameObject targetVillage;
    public float pickingDistance = 5.0f;
    public float villageDistance = 10.0f;
    public int _inHand;
    public int Priority;
    public TreeCapacity tree;

    //use awake since Start() will raise issue in schedular
    private void Awake()
    {
        var anim = GetComponent<Animator>();
        _fsm = new FSM();
        hasTree = GameObject.FindGameObjectsWithTag("fruitTree");
        targetVillage = GameObject.FindGameObjectWithTag("village");

        //innitialize all states:
        var inVillage = new InVillage(this, anim);
        var findingTree = new FindingTree(this, anim);
        var walkingToTree = new WalkingToTree(this, anim);
        var pickingFruits = new PickingFruits(this, anim);
        var walkingToVillage = new WalkingToVillage(this, anim);
        var droppingOffFruits = new DroppingOffFruits(this, anim);

        //prevstate, nextstate, condition required
        addTrans(inVillage, findingTree, FruitsRequired());
        addTrans(findingTree, walkingToTree, NonEmptyTreeFound());
        addTrans(walkingToTree, findingTree, TargetTreeEmpty());
        addTrans(walkingToTree, pickingFruits, ArriveAtTree());
        addTrans(pickingFruits, findingTree, CanCarryMore());
        addTrans(findingTree, walkingToVillage, AllTreeEmptyNotInVillage());
        addTrans(findingTree, inVillage, AllTreeEmptyInVillage());
        addTrans(pickingFruits, walkingToVillage, InventoryFull());
        addTrans(walkingToVillage, droppingOffFruits, ArriveAtVillage());
        addTrans(droppingOffFruits, inVillage, InventoryEmpty());
        
        //set the initial state
        _fsm.setState(inVillage);

        //reference to addTrans in FSM
        void addTrans (FSMNode from, FSMNode to, Func<bool> condition) 
        {
         _fsm.addTransitions(from, to, condition);
        }

        //detail condition boolean checks, also check for animation
        Func<bool> FruitsRequired() => () => hasTree != null &&
                                             anim.GetCurrentAnimatorStateInfo(0).IsName("idle");
        Func<bool> NonEmptyTreeFound() => () => TargetFruitTree != null && 
                                                anim.GetCurrentAnimatorStateInfo(0).IsName("idle");
        Func<bool> TargetTreeEmpty() => () => tree.runOut()&&
                                              anim.GetCurrentAnimatorStateInfo(0).IsName("walking");
        Func<bool> ArriveAtTree() => () => TargetFruitTree != null &&
                                           Vector3.Distance(transform.position, TargetFruitTree.transform.position) <= pickingDistance &&
                                           anim.GetCurrentAnimatorStateInfo(0).IsName("walking");
        Func<bool> CanCarryMore() => () => TargetFruitTree != null &&
                                           tree.runOut() &&
                                           _inHand < _carryCapacity && 
                                           anim.GetCurrentAnimatorStateInfo(0).IsName("idle");
        Func<bool> AllTreeEmptyNotInVillage() => () => TargetFruitTree == null && Vector3.Distance(transform.position, targetVillage.transform.position) > villageDistance &&
                                           anim.GetCurrentAnimatorStateInfo(0).IsName("idle");
        Func<bool> InventoryFull() => () => _inHand == _carryCapacity &&
                                           anim.GetCurrentAnimatorStateInfo(0).IsName("idle");
        Func<bool> ArriveAtVillage() => () => Vector3.Distance(transform.position, targetVillage.transform.position) <= villageDistance &&
                                              anim.GetCurrentAnimatorStateInfo(0).IsName("walking");
        Func<bool> InventoryEmpty() => () => _inHand == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("idle");
        Func<bool>  AllTreeEmptyInVillage() => () => TargetFruitTree == null && Vector3.Distance(transform.position, targetVillage.transform.position) <= villageDistance &&
                                           anim.GetCurrentAnimatorStateInfo(0).IsName("idle");

    }

    //map update to Do()
     public void UpdateFSM() => _fsm.updateFSM();

    //inventory
     public void pickFruit()
    {
        if(_inHand < _carryCapacity && !tree.runOut()) { 
            _inHand++;
            tree._fruit--;
        }
        //Debug.Log("Tree: " + tree._fruit + " InHand: " + _inHand);

    }
    public void dropFruit()
    {
        if (_inHand > 0)
            _inHand--;  
        //Debug.Log("Drop, in Hand: "+_inHand);
    }

}
