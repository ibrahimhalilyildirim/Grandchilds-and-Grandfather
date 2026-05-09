using System.Collections;
using UnityEngine;

public class BabeCollectible : MonoBehaviour
{   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CollectAndDestroy(gameObject));
        }
    }
    private IEnumerator CollectAndDestroy(GameObject obj)
    {
        Renderer renderer = obj.GetComponentInChildren<Renderer>();
        Animator anim = obj.GetComponentInChildren<Animator>();
        if(anim != null)
        {
            anim.Play("Celebrates");
        }
        if(obj.TryGetComponent<Collider>(out Collider col))
        {
            col.enabled = false;
        }
        yield return new WaitForSeconds(1f);
        if(renderer != null)
        {
            Material mat = renderer.material;
            Color initialColor = mat.color;
            float fadeDuration = 0.8f;
            float elapsedTime = 0f;
            while(elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / fadeDuration);
                mat.color = new Color(initialColor.r, initialColor.g, initialColor.b, newAlpha);
                yield return null;
            }
        }
        Destroy(obj);
    }
}
