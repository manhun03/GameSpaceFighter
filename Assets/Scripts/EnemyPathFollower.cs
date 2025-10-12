using UnityEngine;
using UnityEngine.Splines;

public class EnemyPathFollower : MonoBehaviour
{
    public SplineContainer spline;
    public float duration = 5f;
    private float t = 0f;
    private float startOffset = 0f;

    void Update()
    {
        if (spline == null) return;

        t += Time.deltaTime / duration;
        float progress = Mathf.Clamp01(startOffset + t);

        Vector3 position = spline.EvaluatePosition(progress);
        transform.position = position;

        Vector3 tangent = spline.EvaluateTangent(progress);
        if (tangent != Vector3.zero)
        {
            float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    public void SetStartOffset(float offset)
    {
        startOffset = offset; // ví dụ 0.1 = 10% đường spline
    }
}
