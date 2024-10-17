using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject popUpSpeudo;

    string nomPlayer;

    public string timerString;
    public int timerInt;


    public TMP_InputField inputField;

    [SerializeField] TextMeshProUGUI affichageTimer;
    [SerializeField] GameObject panelLearderBoard;


    [System.Serializable]
    public class PostData
    {
        public string playerName;
        public string timeString;
        public int timeInt;

        public PostData(string playerName, string timeString, int timeInt)
        {
            this.playerName = playerName;
            this.timeString = timeString;
            this.timeInt = timeInt;
        }
    }






    private void Start()
    {
        popUpSpeudo.SetActive(false);
        panelLearderBoard.SetActive(false);
    }

    public void PostScorePopUp()
    {
        popUpSpeudo.SetActive(true);
    }


    public void AffichageLeaderBoard()
    {
        affichageTimer.text = timerString;
        panelLearderBoard.SetActive(true);
    }





    // M�thode pour valider l'InputField
    public void ValidateInput()
    {
        // R�cup�rer le texte saisi dans l'InputField
        nomPlayer = inputField.text;

        // Valider l'entr�e (par exemple, ici on v�rifie si l'entr�e est vide)
        if (string.IsNullOrEmpty(nomPlayer))
        {
            Debug.Log("L'entr�e est invalide. Le champ est vide !");
        }
        else
        {
            Debug.Log("L'entr�e est valide : " + nomPlayer);
            // Tu peux ajouter ici d'autres actions, comme passer � la sc�ne suivante ou autre
            if (timerString != null)
            {
                PostDataToAPI(nomPlayer, timerString, timerInt);
            }
        }
    }










    














    // Fonction pour envoyer une requ�te POST � une API
    public void PostDataToAPI(string playerName, string TimeString, int TimeInt)
    {
        // Cr�er une instance des donn�es � envoyer
        PostData postData = new PostData(playerName, TimeString, TimeInt);

        // D�marrer la coroutine pour faire l'appel API
        StartCoroutine(PostRequest("https://golias-api.noamsebahoun.fr/api.php", postData));
    }




    // Coroutine pour envoyer la requ�te POST
    IEnumerator PostRequest(string url, PostData postData)
    {
        WWWForm form = new WWWForm();

        // Ajoute les param�tres requis
        form.AddField("playerName", postData.playerName);      // Nom du joueur
        form.AddField("TimeString", postData.timeString);      // Temps en format string
        form.AddField("TimeInt", postData.timeInt);            // Temps en format int

        // Cr�e la requ�te
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            // Envoie la requ�te et attend la r�ponse
            yield return request.SendWebRequest();

            // V�rifie les erreurs
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Erreur : " + request.error);
            }
            else
            {
                Debug.Log("R�ponse API : " + request.downloadHandler.text);
            }
        }
    }


}
