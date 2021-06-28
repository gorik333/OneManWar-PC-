using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private GameObject _textTip;

    private Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Equals("PlayerBody") && _textTip != null)
        {
            _textTip.SetActive(true);
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name.Equals("PlayerBody") && Input.GetKey(KeyCode.E) && Time.timeScale != 0)
        {
            if (_textTip != null)
            {
                Destroy(_textTip);
                _animator.SetTrigger("Open");
                AudioController.Instance.PlayOpenChestSound();
                DefaultLevelController.EarnMoneyFromChest();
            }
            Destroy(gameObject, 2f); 
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name.Equals("PlayerBody") && _textTip != null)
        {
            _textTip.SetActive(false);
        }
    }
}
