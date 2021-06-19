using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractionCaller : MonoBehaviour
{
    public delegate bool WantsToInteract();
    public WantsToInteract wantsToInteract;

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Assert(wantsToInteract != null, "wants to interact is null, sir");
        if(wantsToInteract())
        {
            InteractibleObject interactible = null;
            if(InteractibleObject.TryGetInteractible(other.gameObject, out interactible))
            {
                interactible.Interact(this.gameObject);
            }
        }
    }
}
