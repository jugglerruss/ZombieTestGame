using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class ZombieDetectCollider : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public UnityEvent<Zombie> OnZombieDetect;
    public UnityEvent OnZombieExit;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
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
