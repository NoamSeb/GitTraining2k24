using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject popUpSpeudo;

    string nomPlayer;

    public string timerString;
    public int timerInt;


    public TMP_InputField inputField;

    [SerializeField] TextMeshProUGUI affichageTimer;
    [SerializeField] GameObject panelLearderBoard;

    string url = "https://golias-api.noamsebahoun.fr/api.php";

    [SerializeField] List<TextMeshProUGUI> nomsPlayers = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> scorPlayers = new List<TextMeshProUGUI>();


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

        ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallback;
    }

    public void PostScorePopUp()
    {
        popUpSpeudo.SetActive(true);
    }


    public void AffichageLeaderBoard()
    {
        affichageTimer.text = timerString;
        panelLearderBoard.SetActive(true);
        GetDataFromAPI();
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
        StartCoroutine(PostRequest(postData));
    }




    // Coroutine pour envoyer la requête POST
    IEnumerator PostRequest(PostData postData)
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

















    bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        // Retourne toujours vrai, ignorera les erreurs de certificat
        return true;
    }












    // Appelle cette méthode pour démarrer la récupération des données
    public void GetDataFromAPI()
    {
        StartCoroutine(GetRequest());
    }











    // Coroutine pour effectuer la requête GET
    IEnumerator GetRequest()
    {
        // Crée la requête GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Envoie la requête et attend la réponse
            yield return request.SendWebRequest();

            // Vérifie les erreurs possibles
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erreur lors de la récupération des données : " + request.error);
            }
            else
            {
                // La réponse est dans request.downloadHandler.text
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Réponse API : " + jsonResponse);

                // Désérialise la réponse JSON en une liste d'objets
                PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>(WrapArray(jsonResponse));



                for (int i = 0; i < nomsPlayers.Count; i++)
                {
                    if (playerDataList.players.Count > i)
                    {
                        nomsPlayers[i].text = playerDataList.players[i].PlayerName;
                        scorPlayers[i].text = playerDataList.players[i].TimeString;
                    }
                }




                //// Vérifie que nous avons bien reçu des joueurs
                //if (playerDataList.players.Count > 0)
                //{
                //    // Récupérer les informations du premier joueur
                //    PlayerData firstPlayer = playerDataList.players[0];
                //
                //    Debug.Log("Premier joueur - Nom : " + firstPlayer.PlayerName + ", Score : " + firstPlayer.TimeString);
                //
                //    // Récupérer les informations du deuxième joueur s'il existe
                //    if (playerDataList.players.Count > 1)
                //    {
                //        PlayerData secondPlayer = playerDataList.players[1];
                //        Debug.Log("Deuxième joueur - Nom : " + secondPlayer.PlayerName + ", Score : " + secondPlayer.TimeString);
                //    }
                //}








            }
        }
    }

    // Classe pour modéliser les données de joueur
    [System.Serializable]
    public class PlayerData
    {
        public string id;
        public string PlayerName;
        public string TimeString;
        public int TimeInt;
    }

    // Classe contenant une liste de joueurs
    [System.Serializable]
    public class PlayerDataList
    {
        public List<PlayerData> players;
    }

    // Cette méthode est nécessaire pour traiter un tableau JSON avec JsonUtility
    private string WrapArray(string jsonArray)
    {
        return "{\"players\":" + jsonArray + "}";
    }



}
