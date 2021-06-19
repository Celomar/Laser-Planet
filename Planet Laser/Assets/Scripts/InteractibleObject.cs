using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractibleObject : MonoBehaviour
{
    static Dictionary<GameObject,InteractibleObject> interactibles = new Dictionary<GameObject, InteractibleObject>();

    [System.Serializable]
    public class InteractEvent : UnityEvent<GameObject> {}
    [SerializeField] private InteractEvent OnInteracted = null;

    void Awake()
    {
        interactibles.Add(this.gameObject,this);
    }

    public void Interact(GameObject caller)
    {
        OnInteracted?.Invoke(caller);
    }

    public static bool TryGetInteractible(GameObject obj, out InteractibleObject outInteractible)
    {
        return interactibles.TryGetValue(obj, out outInteractible);
    }
}
