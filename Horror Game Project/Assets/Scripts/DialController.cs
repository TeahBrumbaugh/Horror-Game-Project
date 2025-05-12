using UnityEngine;
using UnityEngine.UI;
public class DialController : MonoBehaviour
{
    public Transform outerRingTransform;
    public Transform innerRingTransform;

    [Tooltip("How many equal slices per ring")]
    public int sections = 10;
    
    private float _sectionAngle;
    private int[] _ringIndices = new int[2];

    // 0 = outer, 1 = inner
    private int _currentSelectedRing = 0;

    void Start()
    {
        // angle per section
        _sectionAngle = 360f / sections;
        // initialize both at section 0
        _ringIndices[0] = 0;
        _ringIndices[1] = 0;
        ApplyRotation(); 
    }

    public void RotateCW()
    {
        _ringIndices[_currentSelectedRing] = 
            (_ringIndices[_currentSelectedRing] + 1) % sections;
        ApplyRotation();
    }

    public void RotateCCW()
    {
        _ringIndices[_currentSelectedRing] = 
            (_ringIndices[_currentSelectedRing] - 1 + sections) % sections;
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        float angle = _ringIndices[_currentSelectedRing] * _sectionAngle;
        var ring = (_currentSelectedRing == 0) 
                   ? outerRingTransform 
                   : innerRingTransform;
        ring.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
    public void SelectOuterRing() => _currentSelectedRing = 0;
    public void SelectInnerRing() => _currentSelectedRing = 1;

    void LateUpdate()
    {
    var r0 = outerRingTransform.GetComponent<Image>();
    var r1 = innerRingTransform.GetComponent<Image>();

    if (r0 && r1)
    {
        r0.color = (_currentSelectedRing == 0) ? Color.gray : Color.white;
        r1.color = (_currentSelectedRing == 1) ? Color.gray : Color.white;
    }
    }
}
