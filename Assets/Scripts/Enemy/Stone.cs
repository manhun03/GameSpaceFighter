using UnityEngine;

public class Stone : Enemy
{
    private Animator animator;
    private const string flashAnim = "StoneTakeDmg";
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (animator != null)
            animator.SetTrigger(flashAnim);
    }
}
