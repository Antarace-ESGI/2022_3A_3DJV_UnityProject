using UnityEngine;

public class ModelPreviewScript : MonoBehaviour
{
    // Activate only on preview

    // TODO : Generate the preview element

    [SerializeField] 
    private GameObject _model;
    
    private void OnEnable()
    {
          _model.SetActive(true);
    }

    private void OnDisable()
    {
        _model.SetActive(false);
    }
}
