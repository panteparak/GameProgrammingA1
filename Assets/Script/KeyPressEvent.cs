using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class KeyPressEvent : MonoBehaviour
{
    [SerializeField] private List<KeyMap> KeyMapper = new List<KeyMap>();
    private Dictionary<string, Text> map = new Dictionary<string, Text>();
    private int index = 0;

    private void Start()
    {
        if (KeyMapper.Count <= 0)
        {
            throw new Exception("Invalid Key Size");
        }

        foreach (var key in KeyMapper)
        {
            Text t = key.label;
            t.color = key.color;
            map.Add(key.character, t);
        }
        
        StartCoroutine("Countdown", 3);
    }
    
    private void Update()
    {
        if (!Storage.Instance.IsAllowKeyPressed()) return;
        
        
        foreach (String k in map.Keys)
        {
            if (!Input.GetKeyDown(k)) continue;
            
//            Debug.Log("Checking");
            check(k);
            return;
        }
    }

    private IEnumerator waitAndExecute()
    {
        yield return new WaitForSeconds(0.9f);
        StartCoroutine("GameLoop");
    }

    private void check(String k)
    {
        Trigger(k);
        var match = CheckSequence(k);
            
        Debug.Log(String.Format("Seq-Length: '{0}' - index: '{1}' match: '{2}'", Storage.Instance.GetOriginalSequence().Count, index, match));

        if (!match)
        {
            Debug.Log("Next Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        
        if (index == Storage.Instance.GetOriginalSequence().Count - 1 && match)
        {
            index = 0;
            StartCoroutine("waitAndExecute");
            Debug.Log("Next Game Iteration");
        }
        else
        {
            index++;
            Debug.Log("Next Character");
        }
    }

    private bool CheckSequence(String seq)
    {
        Debug.Log(String.Format("Checking seq: '{0}'", seq));
        if (Storage.Instance.GetOriginalSequence()[index] != seq)
        {
            Debug.Log("Game Over");
            return false;
        }
        else
        {
            Debug.Log(String.Format("Seq '{0}' matched", seq));
            return true;
        }
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
       
        while (count > 0) {
           
            // display something...
            yield return new WaitForSeconds(1);
            Debug.Log("Starting in..." + count);
            count --;
        }
        
        Debug.Log("Start");
        yield return GameLoop();
    }

    private void NextSequence()
    {
        int randomIndex = Random.Range(0, KeyMapper.Count);
        String randomCandidate = KeyMapper[randomIndex].character;
        Storage.Instance.GetOriginalSequence().Add(randomCandidate);
        Debug.Log(String.Format("Next Sequence: '{0}'", string.Join(",", Storage.Instance.GetOriginalSequence().ToArray())));
    }

    private IEnumerator PlaySequence()
    {
        foreach (String each in Storage.Instance.GetOriginalSequence())
        {
            Trigger(each);
            yield return new WaitForSeconds(1f);
        }
        
        yield return null;
    }
    

    private IEnumerator GameLoop()
    {
        Storage.Instance.LockKeyPressed();
        NextSequence();
        yield return PlaySequence();
        Storage.Instance.UnlockKeyPressed();
    }

    public void Trigger(String pressedKey)
    {
        String k = pressedKey.ToLower();
        if (map.ContainsKey(k))
        {
            StartCoroutine("ColorChanger", map[k]);
        }
    }
    
    private IEnumerator ColorChanger([NotNull] Text text) 
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        
//        Color temp = text.color;
//        text.color = Color.red;
//        yield return new WaitForSeconds(0.2f);
//        text.color = temp;

        
        Color temp = text.color;
        Debug.Log("Changing");
        text.color = new Color(temp.r, temp.g, temp.b, 1);
        yield return new WaitForSeconds(.15f);
        Debug.Log("Change Back");
        text.color = new Color(temp.r, temp.g, temp.b, 0.2f);
//        yield return null;
    }    
}
    