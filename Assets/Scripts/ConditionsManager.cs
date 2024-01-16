using System;
using System.Collections.Generic;
using System.Linq;
using ArticleAndIndicatorEnums;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ConditionsManager : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject articleLeftAbortion;
    public GameObject articleLeftGun;
    public GameObject articleRightAbortion;
    public GameObject articleRightGun;
    public GameObject forewarningMessage;
    public GameObject politicalClassificationLeft;
    public GameObject politicalClassificationRight;
    public GameObject introduction;
    public GameObject breaker;
    public GameObject annotationTutorial;

    private Participant _participant = new Participant();
    private CsvManager _csvManager = new CsvManager(@".\Assets\Csvs\results.csv");
    private int iteration;
    private Article article;
    private Indicator indicator;
    private GameObject articleGameObject;

    void Start()
    {
        PlayModeListener();

        Participant partialParticipant = this._csvManager.GetPartialParticipant();

        List<Article> possibleArticles = new List<Article>();
        List<Indicator> possibleIndicators = new List<Indicator>();

        foreach (Article article in Enum.GetValues(typeof(Article)))
        {
            if (!ArticleIsAlreadyTaken(article, partialParticipant))
            {
                possibleArticles.Add(article);
            }
        }

        foreach (Indicator indicator in Enum.GetValues(typeof(Indicator)))
        {
            if (!IndicatorIsAlreadyTaken(indicator, partialParticipant))
            {
                possibleIndicators.Add(indicator);
            }
        }

        if (possibleArticles.Count() == 0 || possibleIndicators.Count() == 0)
        {
            Debug.Log("Either possibleArticles or possibleIndicators list are empty. Aborting!");
            return;
        }

        System.Random Random = new System.Random();

        int possibleArticlesRandomIndex = Random.Next(0, possibleArticles.Count());
        int possibleIndicatorsRandomIndex = Random.Next(0, possibleIndicators.Count());

        this.article = possibleArticles.ElementAt(possibleArticlesRandomIndex);
        this.indicator = possibleIndicators.ElementAt(possibleIndicatorsRandomIndex);

        if (partialParticipant == null)
        {
            this._participant.Id = this._csvManager.GetNewParticipantId();
            this.iteration = 0;
        }
        else
        {
            this._participant = partialParticipant;
            this.iteration = GetCurrentIteration(partialParticipant);
        }

        assignArticleGameObject(this.article);

        if (this.indicator == Indicator.Annotations)
        {
            annotationTutorial.SetActive(true);
            activateAnnotations(this.articleGameObject);
        }

        Debug.Log(
            "Iteration " + this.iteration + " - " +
            this.article + "(" + (int)this.article + ")" + " - " +
            this.indicator + "(" + (int)this.indicator + ")"
        );
    }

    private void assignArticleGameObject(Article article)
    {
        if (article == Article.LeftAbortion)
        {
            articleGameObject = articleLeftAbortion;
        }
        else if (article == Article.LeftGun)
        {
            articleGameObject = articleLeftGun;
        }
        else if (article == Article.RightAbortion)
        {
            articleGameObject = articleRightAbortion;
        }
        else if (article == Article.RightGun)
        {
            articleGameObject = articleRightGun;
        }
    }

    private void activateAnnotations(GameObject articleGameObject)
    {
        Transform articleGameObjectHighlights = articleGameObject.transform.GetChild(0);

        for (int i = 0; i < articleGameObjectHighlights.transform.childCount; i++)
        {
            articleGameObjectHighlights.transform.GetChild(i).gameObject.SetActive(true);
        }   
    }

    public void OnContinue()
    {
        if (introduction.activeSelf)
        {
            introduction.SetActive(false);
            annotationTutorial.SetActive(false);

            if (this.indicator == Indicator.ForewarningMessage)
            {
                forewarningMessage.SetActive(true);
            }
            else
            {
                articleGameObject.SetActive(true);
            }
        }
        else if (forewarningMessage.activeSelf)
        {
            forewarningMessage.SetActive(false);
            articleGameObject.SetActive(true);
        }
        else if (articleGameObject.activeSelf)
        {
            articleGameObject.SetActive(false);

            if (this.indicator == Indicator.PoliticalClassification)
            {
                if (this.article == Article.LeftAbortion || this.article == Article.LeftGun)
                {
                    politicalClassificationLeft.SetActive(true);
                }
                else
                {
                    politicalClassificationRight.SetActive(true);
                }
            }
            else
            {
                breaker.SetActive(true);
                continueButton.SetActive(false);
            }
        }
        else if (politicalClassificationLeft.activeSelf || politicalClassificationRight.activeSelf)
        {
            politicalClassificationLeft.SetActive(false);
            politicalClassificationRight.SetActive(false);

            breaker.SetActive(true);
            continueButton.SetActive(false);
        }
    }

    private int GetCurrentIteration(Participant participant)
    {
        if (participant != null)
        {
            if (participant.Article0 == "")
            {
                return 0;
            }
            else if (participant.Article1 == "")
            {
                return 1;
            }
            else if (participant.Article2 == "")
            {
                return 2;
            }
            else if (participant.Article3 == "")
            {
                return 3;
            }
        }

        Debug.Log("Current iteration is unexpectedly 0, despite the participant not being null");
        return 0;
    }

    private bool ArticleIsAlreadyTaken(Article article, Participant participant)
    {
        if (participant == null)
        {
            return false;
        }
        else
        {
            return
            (
                participant.Article0 != null && participant.Article0 != "" && (Article)int.Parse(participant.Article0) == article ||
                participant.Article1 != null && participant.Article1 != "" && (Article)int.Parse(participant.Article1) == article ||
                participant.Article2 != null && participant.Article2 != "" && (Article)int.Parse(participant.Article2) == article ||
                participant.Article3 != null && participant.Article3 != "" && (Article)int.Parse(participant.Article3) == article
            );
        }
    }

    private bool IndicatorIsAlreadyTaken(Indicator indicator, Participant participant)
    {
        if (participant == null)
        {
            return false;
        }
        else
        {
            return
            (
                participant.Indicator0 != null && participant.Indicator0 != "" && (Indicator)int.Parse(participant.Indicator0) == indicator ||
                participant.Indicator1 != null && participant.Indicator1 != "" && (Indicator)int.Parse(participant.Indicator1) == indicator ||
                participant.Indicator2 != null && participant.Indicator2 != "" && (Indicator)int.Parse(participant.Indicator2) == indicator ||
                participant.Indicator3 != null && participant.Indicator3 != "" && (Indicator)int.Parse(participant.Indicator3) == indicator
            );
        }
    }

    public void Save()
    {
        this._participant.SaveData(this.article, this.indicator, this.iteration);
        this._csvManager.SaveParticipant(this._participant);
        Debug.Log("Participant data saved!");
    }

    private void PlayModeListener()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            Save();
        }
    }
}
