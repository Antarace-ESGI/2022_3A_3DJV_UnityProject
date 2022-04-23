using UnityEngine;

public class ModelPreviewScript : MonoBehaviour
{
    // Activate only on preview

    // TODO : Generate the preview element

    [SerializeField] private GameObject _model;

    private void OnEnable()
    {
        if (_model)
        {
            _model.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (_model)
        {
            _model.SetActive(false);
        }
    }
}