using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IngotCollection : MonoBehaviour
{
    public List<IngotStack> collectedStacks;
    
    [SerializeField] private Vector2 collectionRange;
    [SerializeField] private LayerMask whatIsIngot;

    private void Awake()
    {
        // we'll get this from the save system later
        collectedStacks = new List<IngotStack>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Collect"))
        {
            Collider2D[] ingots = Physics2D.OverlapBoxAll(transform.position, collectionRange, 0, whatIsIngot);

            if (ingots.Any())
            {
                CollectIngot(ingots[0].GetComponent<Ingot>());
            }
        }
    }

    public void CollectIngot(Ingot ingot)
    {
        // check if we already have of a stack of this type, if so, increment the stack
        foreach (IngotStack stack in collectedStacks)
        {
            if (stack.type == ingot.type)
            {
                stack.AddIngot();
                ingot.Collect();
                return;
            }
        }

        // if we have collected this type before, make a new stack
        collectedStacks.Add(new IngotStack { type = ingot.type, amount = 1 });
        ingot.Collect();
    }
}

[System.Serializable]
public class IngotStack
{
    public string type;
    public int amount;

    public void AddIngot()
    {
        amount++;
    }
}
