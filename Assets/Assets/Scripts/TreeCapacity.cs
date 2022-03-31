using UnityEngine;

//attach this script to tree
public class TreeCapacity : MonoBehaviour
{
    [SerializeField] 
    public int _fruit = 3;
    public bool avail = true;

    public bool runOut()
    {
        if(_fruit <= 0)
            return true;
        return false;
    }

    private void Start()
    {
    }

}
