using UnityEngine;

public class ResetBalloon : MonoBehaviour
{
    public LayerMask PlayerLayer;
    private PolygonCollider2D _polygonCollider2D;
    private bool enableCollider = true;
    
    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        cheakBallon();
    }
  
    
    public void cheakBallon()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);

        if (!hit.collider)
        {
			return;
        }
        
        if (hit.collider.gameObject.layer != PlayerLayer)
        {
            enableCollider = false;
        }
        else
        {
            return;
        }

        if (enableCollider)
        {
            _polygonCollider2D.enabled = !_polygonCollider2D.enabled;
        }
        else
        {
            _polygonCollider2D.enabled = _polygonCollider2D.enabled;
        }
        
    }
}
