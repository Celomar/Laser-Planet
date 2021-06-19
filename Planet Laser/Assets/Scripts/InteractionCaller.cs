using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractionCaller : MonoBehaviour
{
    public delegate bool WantsToInteract();
    public WantsToInteract wantsToInteract;
    
    private Dictionary<Collider2D,InteractibleObject> objectsInRange = new Dictionary<Collider2D,InteractibleObject>();

    void Update()
    {
        Debug.Assert(wantsToInteract != null, "wants to interact is null, sir");
        if(wantsToInteract())
        {
            foreach(InteractibleObject interactible in objectsInRange.Values)
            {
                interactible.Interact(this.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        InteractibleObject interactible = null;
        if(InteractibleObject.TryGetInteractible(other.gameObject, out interactible))
        {
            objectsInRange[other] = interactible;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(objectsInRange.ContainsKey(other))
        {
            objectsInRange.Remove(other);
        }
    }
}
