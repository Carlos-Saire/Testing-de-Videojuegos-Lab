using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TextController : MonoBehaviour
{
    private TMP_Text text; 
    [SerializeField] private float typingSpeed;
    public event Action finishingWriting;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void Print(string content)
    {
        StartCoroutine(TypeText(content));
    }

    private IEnumerator TypeText(string content)
    {
        text.text = "";
        for (int i = 0; i < content.Length; i++)
        {
            text.text += content[i]; 
            yield return new WaitForSeconds(typingSpeed);
        }
        finishingWriting?.Invoke();
    }
}
