using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(BoxCollider2D))]
public class Chunk : MonoBehaviour
{
    public Color markerColor = Color.red;
    
    [System.NonSerialized] public Chunk nextChunk = null;
    [System.NonSerialized] public bool destroyOnDisable = false;

    public delegate void OnTriggerDelegate(Chunk caller);
    public event OnTriggerDelegate onTriggerEnterCallback;
    public event OnTriggerDelegate onTriggerExitCallback;

    private BoxCollider2D coll = null;

    private float _gameObjectToTop = 0.0f;
    private float _gameObjectToBottom = 0.0f;

    public float gameObjectToTop
    {
        get
        {
            if (!coll)
            {
                coll = GetComponent<BoxCollider2D>();
                _gameObjectToTop = coll.size.y * 0.5f + coll.offset.y;
                _gameObjectToBottom = coll.size.y * 0.5f - coll.offset.y;
            }
            return _gameObjectToTop;
        }
    }

    public float gameObjectToBottom
    {
        get
        {
            if (!coll)
            {
                coll = GetComponent<BoxCollider2D>();
                _gameObjectToTop = coll.size.y * 0.5f + coll.offset.y;
                _gameObjectToBottom = coll.size.y * 0.5f - coll.offset.y;
            }
            return _gameObjectToBottom;
        }
    }

    /*
     * have a collider at the bottom of the chunk and a collider on the camera
     * when the camera collider hits the chunks collider,
     *     have a callback to the chunk manager,
     *     adding a new chunk at head and removing tail chunk
     */

    private void Awake()
    {
        if (!coll)
        {
            coll = GetComponent<BoxCollider2D>();
            _gameObjectToTop = coll.size.y * 0.5f + coll.offset.y;
            _gameObjectToBottom = coll.size.y * 0.5f - coll.offset.y;
        }
        coll.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            onTriggerEnterCallback?.Invoke(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera") )
        {
            onTriggerExitCallback?.Invoke(this);
        }
    }

    private void OnDisable()
    {
        onTriggerEnterCallback = null;
        onTriggerExitCallback = null; // prevent weird nullrefs when unloading scene
        if (destroyOnDisable)
        {
            Destroy(gameObject);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color oldColor = Handles.color;

        Handles.color = markerColor; 
        
        float lineThickness = 5.0f;
        float endLinesLength = 0.5f;
        
        Vector3 topPoint = transform.position;
        Vector3 bottomPoint = transform.position;
        
        Vector3 endLineModifier = new Vector3(endLinesLength * 0.5f, 0.0f, 0.0f);
        
        if (Application.isPlaying)
        {
            topPoint += new Vector3(0.0f, gameObjectToTop, 0.0f);
            bottomPoint -=  new Vector3(0.0f, gameObjectToBottom, 0.0f);
        }
        else
        {
            BoxCollider2D coll = GetComponent<BoxCollider2D>();

            topPoint += new Vector3(0.0f, coll.size.y * 0.5f + coll.offset.y, 0.0f);
            bottomPoint -=  new Vector3(0.0f, coll.size.y * 0.5f - coll.offset.y, 0.0f);
        }
        
        Handles.DrawAAPolyLine(lineThickness, topPoint, bottomPoint);
        Handles.DrawAAPolyLine(lineThickness, topPoint + endLineModifier, topPoint - endLineModifier);
        Handles.DrawAAPolyLine(lineThickness, bottomPoint + endLineModifier, bottomPoint - endLineModifier);

        Handles.color = oldColor;
    }
#endif

}
