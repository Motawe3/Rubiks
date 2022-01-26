using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private CubePreviewImage cubePreviewImage;
    [SerializeField] private int cubeSize;

    private void Start()
    {
        newGameButton.onClick.AddListener(NewGame);
    }

    private void NewGame()
    {
        GameManager.Instance.StartNewGame(cubeSize);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cubePreviewImage.SetPreview(cubeSize);
    }
}
