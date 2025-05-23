using UnityEngine;
using System.Collections;

public class DialController3D : MonoBehaviour
{
    [Header("Rings")]
    public Transform innerRingTransform;
    public Transform outerRingTransform;

    [Header("Settings")]
    public int sectionsPerRing = 10;
    public float rotationSpeedDegreesPerSecond = 360f;
    public float buttonPressDepth = 0.02f;
    public float buttonPressDuration = 0.1f;

    private Transform _selectedRing;
    private float _sectionAngle;
    private int _innerIndex, _outerIndex;
    private bool isRotating = false;

    void Start()
    {
        _selectedRing = innerRingTransform;
        _sectionAngle = 360f / Mathf.Max(1, sectionsPerRing);
    }

    public void RotateCCW(Transform button)
    {
        if (isRotating) return;
        StartCoroutine(DoPressAndRotate(button, true));
    }

    public void RotateCW(Transform button)
    {
        if (isRotating) return;
        StartCoroutine(DoPressAndRotate(button, false));
    }

    public void SelectInner(Transform button)
    {
        if (isRotating) return;
        StartCoroutine(DoPressSelect(button, innerRingTransform));
    }

    public void SelectOuter(Transform button)
    {
        if (isRotating) return;
        StartCoroutine(DoPressSelect(button, outerRingTransform));
    }

    private IEnumerator DoPressSelect(Transform button, Transform ring)
    {
        isRotating = true;
        Debug.Log("[Dial] Select start");
        yield return PressButton(button);
        _selectedRing = ring;
        Debug.Log("[Dial] Select end");
        isRotating = false;
    }

    private IEnumerator DoPressAndRotate(Transform button, bool cw)
    {
        isRotating = true;
        Debug.Log("[Dial] Rotate start: " + (cw ? "CW" : "CCW"));
        yield return PressButton(button);

        // update index
        if (_selectedRing == innerRingTransform)
            _innerIndex = (_innerIndex + (cw ? 1 : sectionsPerRing - 1)) % sectionsPerRing;
        else
            _outerIndex = (_outerIndex + (cw ? 1 : sectionsPerRing - 1)) % sectionsPerRing;

        // animate exactly one section
        float targetAngle = cw ? -_sectionAngle : _sectionAngle;
        float rotated = 0f;
        while (Mathf.Abs(rotated) < Mathf.Abs(targetAngle) - 0.01f)
        {
            float step = rotationSpeedDegreesPerSecond * Time.deltaTime;

            float rem = targetAngle - rotated;
            float toRotate = Mathf.Sign(rem) * Mathf.Min(Mathf.Abs(step), Mathf.Abs(rem));
            _selectedRing.Rotate(Vector3.up, toRotate, Space.Self);
            rotated += toRotate;
            yield return null;
        }

        Debug.Log("[Dial] Rotate end. Final index - inner: " + _innerIndex + ", outer: " + _outerIndex);
        isRotating = false;
    }

    private IEnumerator PressButton(Transform button)
    {
        Vector3 orig = button.localPosition;
        button.localPosition += Vector3.down * buttonPressDepth;
        yield return new WaitForSeconds(buttonPressDuration);
        button.localPosition = orig;
    }

    public void ResetDial(Transform button)
    {
        if (isRotating) return;
        StartCoroutine(PressAndReset(button));
    }

    private IEnumerator PressAndReset(Transform button)
    {
        isRotating = true;
        Debug.Log("Dial Reset");
        yield return PressButton(button);

        _innerIndex = 0;
        _outerIndex = 0;

        _selectedRing = innerRingTransform;

        innerRingTransform.localRotation = Quaternion.identity;
        outerRingTransform.localRotation = Quaternion.identity;

        Debug.Log("Dial Reset successful");
        isRotating = false;
    }
}
