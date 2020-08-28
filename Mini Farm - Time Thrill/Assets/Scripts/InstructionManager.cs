using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InstructionManager : MonoBehaviour
{
    //public Image image;
    public TextMeshProUGUI text;

    private Queue<Image> images;
    private Queue<string> sentences;

    void Start()
    {
        images = new Queue<Image>();
        sentences = new Queue<string>();
    }

    public void StartInstruction(Instraction instraction)
    {
        images.Clear();
        sentences.Clear();

        foreach (string sentence in instraction.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if (sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeInstruction(sentence));
    }

    IEnumerator TypeInstruction (string sentence)
    {
        text.text = "";
        foreach ( char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }
}
