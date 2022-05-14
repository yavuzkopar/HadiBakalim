using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
   [SerializeField] TextMeshProUGUI AIText;
   [SerializeField] Button nextButton;
   [SerializeField] Transform choiseRoot;
   [SerializeField] GameObject choisePrefab;

    void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
      //  nextButton.onClick.AddListener(Next);
   //   playerConversant.onConversationUpdated += Next;
        playerConversant.onConversationUpdated += UpdateUI;
     
        UpdateUI();
    }
    private void UpdateUI() {
        gameObject.SetActive(playerConversant.IsActive());
        if (!playerConversant.IsActive())
        {
            return;
        }
        AIText.text = playerConversant.GetText();
    //    nextButton.gameObject.SetActive(playerConversant.HasNext());
        foreach (Transform item in choiseRoot)
        {
            Destroy(item.gameObject);
        }
        foreach (DialogueNode choisetext in playerConversant.GetChoises())
        {
            GameObject obj = Instantiate(choisePrefab,choiseRoot);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = choisetext.GetText();
            Button buton = obj.GetComponentInChildren<Button>();
            buton.onClick.AddListener(() =>{
                Debug.Log(choisetext.GetText());
                playerConversant.SelectChoise(choisetext);
                Next();
              //  UpdateUI();

            });
        }
    }
    void Next()
    {
        if(playerConversant.HasNext())
            playerConversant.Next();
        else
            playerConversant.Quit();
            
      //  UpdateUI();
    }
}