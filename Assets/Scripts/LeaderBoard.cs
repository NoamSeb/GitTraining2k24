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
        StartCoroutine(PostRequest(postData));
    }




    // Coroutine pour envoyer la requ�te POST
    IEnumerator PostRequest(PostData postData)
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

















    bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        // Retourne toujours vrai, ignorera les erreurs de certificat
        return true;
    }












    // Appelle cette m�thode pour d�marrer la r�cup�ration des donn�es
    public void GetDataFromAPI()
    {
        StartCoroutine(GetRequest());
    }











    // Coroutine pour effectuer la requ�te GET
    IEnumerator GetRequest()
    {
        // Cr�e la requ�te GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Envoie la requ�te et attend la r�ponse
            yield return request.SendWebRequest();

            // V�rifie les erreurs possibles
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erreur lors de la r�cup�ration des donn�es : " + request.error);
            }
            else
            {
                // La r�ponse est dans request.downloadHandler.text
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("R�ponse API : " + jsonResponse);

                // D�s�rialise la r�ponse JSON en une liste d'objets
                PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>(WrapArray(jsonResponse));



                for (int i = 0; i < nomsPlayers.Count; i++)
                {
                    if (playerDataList.players.Count > i)
                    {
                        nomsPlayers[i].text = playerDataList.players[i].PlayerName;
                        scorPlayers[i].text = playerDataList.players[i].TimeString;
                    }
                }




                //// V�rifie que nous avons bien re�u des joueurs
                //if (playerDataList.players.Count > 0)
                //{
                //    // R�cup�rer les informations du premier joueur
                //    PlayerData firstPlayer = playerDataList.players[0];
                //
                //    Debug.Log("Premier joueur - Nom : " + firstPlayer.PlayerName + ", Score : " + firstPlayer.TimeString);
                //
                //    // R�cup�rer les informations du deuxi�me joueur s'il existe
                //    if (playerDataList.players.Count > 1)
                //    {
                //        PlayerData secondPlayer = playerDataList.players[1];
                //        Debug.Log("Deuxi�me joueur - Nom : " + secondPlayer.PlayerName + ", Score : " + secondPlayer.TimeString);
                //    }
                //}








            }
        }
    }

    // Classe pour mod�liser les donn�es de joueur
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

    // Cette m�thode est n�cessaire pour traiter un tableau JSON avec JsonUtility
    private string WrapArray(string jsonArray)
    {
        return "{\"players\":" + jsonArray + "}";
    }



}
