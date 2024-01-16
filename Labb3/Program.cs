using Common.DTO;
using Labb3Console.Models;

var _repo = new QuizRepository();

AddQuiz();




void AskAllQuestions()
{
    var allQuestions = _repo.GetAllQuestions();

    int score = 0;

    foreach (var question in allQuestions)
    {
        Console.WriteLine(question.Description);
        foreach (var answer in question.Answers)
        {
            Console.WriteLine(answer);
        }

        int userAnswer;
        Console.WriteLine("Svar: ");
        userAnswer = Convert.ToInt32(Console.ReadLine()) - 1;
        if (userAnswer == question.CorrectAnswer)
        {
            score++;
            Console.WriteLine("Rätt svar!");
        }
        else
        {
            Console.WriteLine("Fel svar!");
        }
    }

    Console.WriteLine($"Du fick {score}/{allQuestions.Capacity} poäng!");
}
void PrintAllQuestions()
{
    var allQuestions = _repo.GetAllQuestions();

    foreach (var question in allQuestions)
    {
        Console.WriteLine(question.Description);
        foreach (var answer in question.Answers)
        {
            Console.WriteLine(answer);
        }
    }
}

void AddQuestion()
{
    var newQuestion = new QuestionModel();
    Console.WriteLine("Fråga: ");
    newQuestion.Description = Console.ReadLine();

    string[] text = new string[4];
    for (int i = 0; i < text.Length; i++)
    {
        Console.WriteLine($"Alternativ {i+1}: ");
        text[i] = Console.ReadLine();
    }

    Console.WriteLine("Vilket alternativ är korrekt?");
    newQuestion.CorrectAnswer = Convert.ToInt32(Console.ReadLine()) - 1;

    var questionRecord = new QuestionRecord("", newQuestion.Description, new List<string>(text), newQuestion.CorrectAnswer);

    _repo.AddQuestion(questionRecord);
}

void AddQuestionDebug()
{
    var newQuestion = new QuestionModel();
    newQuestion.Description = "Vilken siffra är högst?";
    string[] text = { "10", "200", "3 000", "40 000" };
    newQuestion.Answers = new List<string>(text);
    newQuestion.CorrectAnswer = 3;

    var questionRecord = new QuestionRecord("", newQuestion.Description, new List<string>(text), newQuestion.CorrectAnswer);

    _repo.AddQuestion(questionRecord);
}

void AddQuiz()
{
    var newQuiz = new QuizModel();
    Console.WriteLine("Namn på quiz: ");
    newQuiz.Name = Console.ReadLine();
    Console.WriteLine("Beskrivning på quiz: ");
    newQuiz.Description = Console.ReadLine();

    var quizRecord = new QuizRecord("", newQuiz.Name, newQuiz.Description, new List<QuestionRecord>());

    _repo.AddQuiz(quizRecord);
}

void AddQuestionToQuiz()
{

}

void AddQuizDebug()
{
    var newQuiz = new QuizModel();
    newQuiz.Name = "Första quizen!";
    newQuiz.Description = "Frågor med tillhörande svar!";
    var allQuestions = _repo.GetAllQuestions();

    var quizRecord = new QuizRecord("", newQuiz.Name, newQuiz.Description, allQuestions);

    _repo.AddQuiz(quizRecord);
}