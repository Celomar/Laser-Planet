using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleObject : MonoBehaviour
{
    static Dictionary<GameObject,InteractibleObject> interactibles = new Dictionary<GameObject, InteractibleObject>();
    [SerializeField] private UnityEvent<GameObject> OnInteracted = null;

    void Awake()
    {
        interactibles.Add(this.gameObject,this);
    }

    public void Interact(GameObject caller)
    {
        OnInteracted?.Invoke(caller);
    }

    public bool TryGetInteractible(GameObject obj, out InteractibleObject outInteractible)
    {
        return interactibles.TryGetValue(obj, out outInteractible);
    }
}
