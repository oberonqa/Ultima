using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Stat mana;

    private float initMana = 50;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints;

    // Index to keep track of which exit point to use, default to 2 (down)
    private int exitIndex = 2;    

    // Use this for initialization
    protected override void Start ()
    {
        mana.Initialize(initMana, initMana);

        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        GetInput();

        base.Update();
    }

    private void GetInput()
    {

        /// THIS IS USED FOR DEBUGGING ONLY
        ///

        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;            
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;       
        }

        Direction = Vector2.zero;

        //if (Input.GetKeyDown(

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }

        if (IsMoving)
        {
            StopAttack();
        }

        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }
    }    

    private IEnumerator Attack(string spellName)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);

        IsAttacking = true;

        MyAnimator.SetBool("attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);  // Hardcoded cast time for debugging


        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }
        
        StopAttack();
    }

    public void CastSpell(string spellName)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellName));
        }        
    }

    private bool InLineOfSight()
    {

        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            // Create a raycast to check if player target is in line of sight by
            // using block layer sight blocks
            RaycastHit2D hit = Physics2D.Raycast(exitPoints[4].position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            // Check to see if raycast hits one of the sight blocks
            if (hit.collider == null)
            {
                // raycast did not hit one of the sight blocks
                return true;
            }        
        }

        // raycast did hit one of the sight blocks
        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

    public void StopAttack()
    {
        SpellBook.MyInstance.StopCasting();

        IsAttacking = false;

        MyAnimator.SetBool("attack", IsAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }
}
