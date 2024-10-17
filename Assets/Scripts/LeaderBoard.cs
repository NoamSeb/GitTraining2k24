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





    // Méthode pour valider l'InputField
    public void ValidateInput()
    {
        // Récupérer le texte saisi dans l'InputField
        nomPlayer = inputField.text;

        // Valider l'entrée (par exemple, ici on vérifie si l'entrée est vide)
        if (string.IsNullOrEmpty(nomPlayer))
        {
            Debug.Log("L'entrée est invalide. Le champ est vide !");
        }
        else
        {
            Debug.Log("L'entrée est valide : " + nomPlayer);
            // Tu peux ajouter ici d'autres actions, comme passer à la scène suivante ou autre
            if (timerString != null)
            {
                PostDataToAPI(nomPlayer, timerString, timerInt);
            }
        }
    }










    














    // Fonction pour envoyer une requête POST à une API
    public void PostDataToAPI(string playerName, string TimeString, int TimeInt)
    {
        // Créer une instance des données à envoyer
        PostData postData = new PostData(playerName, TimeString, TimeInt);

        // Démarrer la coroutine pour faire l'appel API
        StartCoroutine(PostRequest("https://golias-api.noamsebahoun.fr/api.php", postData));
    }




    // Coroutine pour envoyer la requête POST
    IEnumerator PostRequest(string url, PostData postData)
    {
        WWWForm form = new WWWForm();

        // Ajoute les paramètres requis
        form.AddField("playerName", postData.playerName);      // Nom du joueur
        form.AddField("TimeString", postData.timeString);      // Temps en format string
        form.AddField("TimeInt", postData.timeInt);            // Temps en format int

        // Crée la requête
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            // Envoie la requête et attend la réponse
            yield return request.SendWebRequest();

            // Vérifie les erreurs
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Erreur : " + request.error);
            }
            else
            {
                Debug.Log("Réponse API : " + request.downloadHandler.text);
            }
        }
    }


}
