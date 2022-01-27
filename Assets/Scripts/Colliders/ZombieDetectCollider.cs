using UnityEngine;
using UnityEngine.Events;

public class ZombieDetectCollider : MonoBehaviour
{
    public UnityEvent<Zombie> OnZombieDetect;
    public UnityEvent OnZombieExit;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.TryGetComponent(out Zombie zombie))
            OnZombieDetect?.Invoke(zombie);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.TryGetComponent(out Zombie zombie))
            OnZombieExit?.Invoke();
    }
}
