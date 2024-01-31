using Common.DTO;
using Labb3Console.Models;

var _repo = new QuizRepository();

AddCategory();

//Gamla funktioner pre-categories
#region
void AddQuestionToQuiz()
{
    PrintAllQuizes();

    Console.WriteLine("Vilken Quiz vill du lägga till en fråga till?");

    string selectedQuiz;
    Console.WriteLine("Quiz: ");
    selectedQuiz = Console.ReadLine();

    Console.WriteLine("Vilken fråga vill du lägga till?");

    string selectedQuestion;
    Console.WriteLine("Fråga: ");
    selectedQuestion = Console.ReadLine();

    _repo.AddQuestionToQuiz(selectedQuiz, selectedQuestion);
}

void RemoveQuestionFromQuiz()
{
    PrintAllQuizes();

    Console.WriteLine("Vilken Quiz vill du ta bort en fråga från?");

    string selectedQuiz;
    Console.WriteLine("Quiz: ");
    selectedQuiz = Console.ReadLine();

    Console.WriteLine("Vilken fråga vill du ta bort?");

    string selectedQuestion;
    Console.WriteLine("Fråga: ");
    selectedQuestion = Console.ReadLine();

    _repo.RemoveQuestionFromQuiz(selectedQuiz, selectedQuestion);
}
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
        Console.WriteLine($"Id: {question.Id}");
        Console.WriteLine(question.Description);
        foreach (var answer in question.Answers)
        {
            Console.WriteLine(answer);
        }
    }
}

void PrintAllQuizes()
{
    var allQuizes = _repo.GetAllQuizzesWithQuestions();

    foreach (var quiz in allQuizes)
    {
        Console.WriteLine($"Id(Quiz): {quiz.Id}");
        Console.WriteLine($"Namn på Quiz: {quiz.Name}");
        Console.WriteLine($"Beskrivning av Quiz: {quiz.Description}");

        foreach (var question in quiz.Questions)
        {
            Console.WriteLine($"Id(Fråga): {question.Id}");
            Console.WriteLine($"Beskrivning av fråga: {question.Description}");
            foreach (var answer in question.Answers)
            {
                Console.WriteLine($"Svarsalternativ på fråga: {answer}");
            }
        }
    }
}

//void AddQuestion()
//{
//    var newQuestion = new QuestionModel();
//    Console.WriteLine("Fråga: ");
//    newQuestion.Description = Console.ReadLine();

//    string[] text = new string[4];
//    for (int i = 0; i < text.Length; i++)
//    {
//        Console.WriteLine($"Alternativ {i+1}: ");
//        text[i] = Console.ReadLine();
//    }

//    Console.WriteLine("Vilket alternativ är korrekt?");
//    newQuestion.CorrectAnswer = Convert.ToInt32(Console.ReadLine()) - 1;

//    var questionRecord = new QuestionRecord("", newQuestion.Description, new List<string>(text), newQuestion.CorrectAnswer);

//    _repo.AddQuestion(questionRecord);
//}



//void AddQuestionDebug()
//{
//    var newQuestion = new QuestionModel();
//    newQuestion.Description = "Vilken siffra är högst?";
//    string[] text = { "10", "200", "3 000", "40 000" };
//    newQuestion.Answers = new List<string>(text);
//    newQuestion.CorrectAnswer = 3;

//    var questionRecord = new QuestionRecord("", newQuestion.Description, new List<string>(text), newQuestion.CorrectAnswer);

//    _repo.AddQuestion(questionRecord);
//}

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

void AddQuizDebug()
{
    var newQuiz = new QuizModel();
    newQuiz.Name = "Första quizen!";
    newQuiz.Description = "Frågor med tillhörande svar!";
    var allQuestions = _repo.GetAllQuestions();

    var quizRecord = new QuizRecord("", newQuiz.Name, newQuiz.Description, allQuestions);

    _repo.AddQuiz(quizRecord);
}

#endregion


void AddQuestionWithCategories()
{
    var newQuestion = new QuestionModel();
    Console.WriteLine("Fråga: ");
    newQuestion.Description = Console.ReadLine();

    string[] text = new string[4];
    for (int i = 0; i < text.Length; i++)
    {
        Console.WriteLine($"Alternativ {i + 1}: ");
        text[i] = Console.ReadLine();
    }

    Console.WriteLine("Vilket alternativ är korrekt?");
    newQuestion.CorrectAnswer = Convert.ToInt32(Console.ReadLine()) - 1;

    var questionRecord = new QuestionRecord("", newQuestion.Description, new List<string>(text), newQuestion.CorrectAnswer, new List<CategoryRecord>());

    _repo.AddQuestionWithCategories(questionRecord);
}

void AddCategory()
{
    var newCategory = new CategoryModel();
    Console.WriteLine("Kategori: ");
    newCategory.Name = Console.ReadLine();

    var categoryRecord = new CategoryRecord("", newCategory.Name);

    _repo.AddCategory(categoryRecord);
}

void AddCategoryToQuestion()
{
    PrintAllQuizes();

    Console.WriteLine("Vilken fråga vill du lägga till en kategori på?");

    string selectedQuestion;
    Console.WriteLine("QuestionId: ");
    selectedQuestion = Console.ReadLine();

    Console.WriteLine("Vilken kategori vill du lägga till på frågan?");

    string selectedCategory;
    Console.WriteLine("KategoriId: ");
    selectedCategory = Console.ReadLine();

    _repo.AddCategoryToQuestion(selectedQuestion, selectedCategory);
}

void GetAllCategories()
{
    var allCategories = _repo.GetAllCategories();

    foreach (var category in allCategories)
    {
        Console.WriteLine($"Id: {category.Id}");
        Console.WriteLine(category.Name);
    }
}

void PrintAllQuestionsWithCategories()
{
    var allQuestions = _repo.GetAllQuestions();

    foreach (var question in allQuestions)
    {
        Console.WriteLine($"Id: {question.Id}");
        Console.WriteLine(question.Description);
        foreach (var answer in question.Answers)
        {
            Console.WriteLine(answer);
        }

        foreach (var category in question.Categories)
        {
            Console.WriteLine($"Kategori-ID: {category.Id}");
            Console.WriteLine(category.Name);
        }
    }
}

void PrintAllQuizesWithCategories()
{
    var allQuizes = _repo.GetAllQuizzesWithQuestions();

    foreach (var quiz in allQuizes)
    {
        Console.WriteLine($"Id(Quiz): {quiz.Id}");
        Console.WriteLine($"Namn på Quiz: {quiz.Name}");
        Console.WriteLine($"Beskrivning av Quiz: {quiz.Description}");

        foreach (var question in quiz.Questions)
        {
            Console.WriteLine($"Id(Fråga): {question.Id}");
            foreach (var category in question.Categories)
            {
                Console.WriteLine($"Kategorier: {category.Name}");
            }
            Console.WriteLine($"Beskrivning av fråga: {question.Description}");
            foreach (var answer in question.Answers)
            {
                Console.WriteLine($"Svarsalternativ på fråga: {answer}");
            }
        }
    }
}