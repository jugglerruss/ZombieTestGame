using UnityEngine;
using UnityEngine.Events;

public class HeroDetectCollider : MonoBehaviour
{
    public UnityEvent<Hero> OnHeroDetect;
    public UnityEvent OnHeroExit;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.TryGetComponent(out Hero hero))
            OnHeroDetect?.Invoke(hero);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.TryGetComponent(out Hero hero))
            OnHeroExit?.Invoke();
    }
}
