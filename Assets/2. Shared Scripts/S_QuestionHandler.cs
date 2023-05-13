using System;
using System.Collections;
using System.Collections.Generic;
using _2._Shared_Scripts;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_QuestionHandler : MonoBehaviour
{
    private List<S_Question> seventyQuestions;
    private List<S_Question> eightyQuestions;
    private List<S_Question> nintyQuestions;
    [SerializeField] private TMP_Text fieldText, a1, a2, a3, a4;
    private int currentIndex;
    [SerializeField] private S_InteractableObject door;
    [SerializeField] private FirstPersonMovement fpm;
    [SerializeField] private FirstPersonLook fpl;

    private void Start()
    {
        fpm.canMove = false;
        //fpl.enabled = false;
        fpl.KillMovement();
        seventyQuestions = new List<S_Question>();
        eightyQuestions = new List<S_Question>();
        nintyQuestions = new List<S_Question>();
        Init(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Init(string levelName)
    {
        var sq1 = new S_Question("What year did the Atari 2600 originally release?", "1977", "1981", "1986", "1984");
        var sq2 = new S_Question("How many revisions were released of the Atari 2600?", "2", "3", "4", "5");
        var sq3 = new S_Question("How much addressable memory did the Atari 2600 have?", "8kb", "6kb", "4kb", "2kb");
        var sq4 = new S_Question("What was the maximum size of cartridge when using bank switching techniques?", "16kb",
            "24kb", "32kb", "64kb");
        var sq5 = new S_Question("How many colours could a PAL Atari 2600 show?", "128", "104", "136", "100");
        var sq6 = new S_Question(
            "Atari 2600 games are iconic for their simple flickering sprites, this was due to the console only being able to display how many sprites at once?",
            "7", "8", "9", "10");
        
        seventyQuestions.Add(sq1);
        seventyQuestions.Add(sq2);
        seventyQuestions.Add(sq3);
        seventyQuestions.Add(sq4);
        seventyQuestions.Add(sq5);
        seventyQuestions.Add(sq6);

        var eq1 = new S_Question("What CPU did the NES use?", "2A03", "3B04", "1H02", "4E02");
        var eq2 = new S_Question("What was the best selling game for the NES?", "Super Mario Bros.", "Final Fantasy", "Kung Fu", "Duck Hunt");
        var eq3 = new S_Question("How much did the NES cost in North America at launch?", "$99.99", "$88.99", "$98.99", "$89.99");
        var eq4 = new S_Question("When was the Sega master system released?", "1985", "1986", "1987", "1988");
        var eq5 = new S_Question("The Master System was sega’s … console.", "First", "Second", "Third", "Fourth");
        var eq6 = new S_Question("What was the Sega Master System's biggest competitor in the 8-bit console market?", "Amiga 500", "Commodore 64", "NES", "Atari 2600");
        
        eightyQuestions.Add(eq1);
        eightyQuestions.Add(eq2);
        eightyQuestions.Add(eq3);
        eightyQuestions.Add(eq4);
        eightyQuestions.Add(eq5);
        eightyQuestions.Add(eq6);
        
        var nq1 = new S_Question("Which Enhancement Chip acted as graphics accelerator chip which drew polygons and advanced 2D effects?", "Super FX", "Cx4", "DSP", "OBC-1");
        var nq2 = new S_Question("Which Enhancement Chip was used to perform general trigonometric calculations for wireframe effects, sprite positioning, and rotation?",
            "Super FX", "Cx4", "DSP", "OBC-1");
        var nq3 = new S_Question("What was the standard space for a SNES cartridge?", "2MB", "4MB", "6MB", "8MB");
        var nq4 = new S_Question("The Sega Saturn was released in Japan in 1994, when was it released for the rest of the world?", "1992", "1995", "2000", "1998");
        var nq5 = new S_Question("How  many years did the Sega Saturn last before it was discontinued", "8", "3", "1", "4");
        var nq6 = new S_Question("What Texture Sizes was the PlayStation 1 limited to?", "256x256", "512x512", "768x768", "1024x1024");
        var nq7 = new S_Question("What Year was the PlayStation 1 first released in Europe and the US?", "1992", "1993", "1994", "1995");
        
        nintyQuestions.Add(nq1);
        nintyQuestions.Add(nq2);
        nintyQuestions.Add(nq3);
        nintyQuestions.Add(nq4);
        nintyQuestions.Add(nq5);
        nintyQuestions.Add(nq6);
        nintyQuestions.Add(nq7);

        switch (levelName)
        {
            case "L_70sFloor":
                currentIndex = 0;
                S_Question question2 = seventyQuestions[0];
                fieldText.text = question2.Question;
                a1.text = question2.Answer1;
                a2.text = question2.Answer2;
                a3.text = question2.Answer3;
                a4.text = question2.Answer4;
                break;
            
            case "L_80sFloor":
                currentIndex = 0;
                S_Question question = eightyQuestions[0];
                fieldText.text = question.Question;
                a1.text = question.Answer1;
                a2.text = question.Answer2;
                a3.text = question.Answer3;
                a4.text = question.Answer4;
                break;
            
            case "L_90sFloor":
                currentIndex = 0;
                S_Question question1 = nintyQuestions[0];
                fieldText.text = question1.Question;
                a1.text = question1.Answer1;
                a2.text = question1.Answer2;
                a3.text = question1.Answer3;
                a4.text = question1.Answer4;
                break;
        }
    }

    public void NextQuestion()
    {
        var levelName = SceneManager.GetActiveScene().name;
        S_Question q;
        currentIndex += 1;
        switch (levelName)
        {
            case "L_70sFloor":
                if (currentIndex < seventyQuestions.Count)
                {
                    q = seventyQuestions[currentIndex];
                    fieldText.text = q.Question;
                    a1.text = q.Answer1;
                    a2.text = q.Answer2;
                    a3.text = q.Answer3;
                    a4.text = q.Answer4;
                }
                if (currentIndex == seventyQuestions.Count)
                {
                    PassTest(levelName);
                }
                break;
            
            case "L_80sFloor":
                if (currentIndex < eightyQuestions.Count)
                {
                    q = eightyQuestions[currentIndex];
                    fieldText.text = q.Question;
                    a1.text = q.Answer1;
                    a2.text = q.Answer2;
                    a3.text = q.Answer3;
                    a4.text = q.Answer4;
                }
                if (currentIndex == eightyQuestions.Count)
                {
                    PassTest(levelName);
                }
                break;
            
            case "L_90sFloor":
                if (currentIndex < nintyQuestions.Count)
                {
                    q = nintyQuestions[currentIndex];
                    fieldText.text = q.Question;
                    a1.text = q.Answer1;
                    a2.text = q.Answer2;
                    a3.text = q.Answer3;
                    a4.text = q.Answer4;
                }
                if (currentIndex == nintyQuestions.Count)
                {
                    PassTest(levelName);
                }
                break;
        }
    }
    
    public void PassTest([CanBeNull] string levelName)
    {
        if (levelName == "L_90sFloor")
            SceneManager.LoadScene("L_MainMenu");
        
        fpm.canMove = true;
        fpl.enabled = true;
        door.questionAnswered = true;
    }
}
