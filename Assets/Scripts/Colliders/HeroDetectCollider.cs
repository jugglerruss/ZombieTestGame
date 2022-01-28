using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class HeroDetectCollider : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public UnityEvent<Hero> OnHeroDetect;
    public UnityEvent OnHeroExit;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
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
    public void DrawLines(Vector2[] points, Color color)
    {
        for (int i = 0; i < points.Length; i++)
        {
            _lineRenderer.SetPosition(i, points[i]);
        }
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }
    internal void DebugViewSetActive(bool isDebug)
    {
        _lineRenderer.enabled = isDebug;
    }
}
