using Common.DTO;
using Labb3.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

public class QuizRepository
{
    private readonly IMongoCollection<Question> _questions;
    private readonly IMongoCollection<Quiz> _quizes;

    public QuizRepository()
    {
        var hostName = "localhost";
        var port = "27017";
        var databaseName = "QuizDb";
        var client = new MongoClient($"mongodb://{hostName}:{port}");
        var database = client.GetDatabase(databaseName);

        _questions = database.GetCollection<Question>("Questions", new MongoCollectionSettings() { AssignIdOnInsert = true });
        _quizes = database.GetCollection<Quiz>("Quizes", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public void AddQuestion(QuestionRecord questionRecord)
    {
        var newQuestion = new Question()
        {
            Description = questionRecord.Description,
            Answers = questionRecord.Answers,
            CorrectAnswer = questionRecord.CorrectAnswer
        };

        _questions.InsertOne(newQuestion);
    }

    public List<QuestionRecord> GetAllQuestions()
    {
        var filter = Builders<Question>.Filter.Empty;
        var allQuestions = _questions.Find(filter).ToList().Select(q =>
            new QuestionRecord(q.Id.ToString(), q.Description, q.Answers, q.CorrectAnswer));

        return allQuestions.ToList();
    }

    public List<QuizRecord> GetAllQuizes()
    {
        var filter = Builders<Quiz>.Filter.Empty;

        //Finns det inget bättre sätt att göra det här på?

        List<QuestionRecord> questionList = new List<QuestionRecord>();
        var allQuizes = _quizes.Find(filter).ToList().Select(q =>
            new Quiz());

        List<Question> quizQuestions = new List<Question>();

        foreach (var quiz in allQuizes as List<Quiz>)
        {
            quizQuestions = quiz.Questions;
        }

        var allQuizRecords = _quizes.Find(filter).ToList().Select(q =>
            new QuizRecord(q.Id.ToString(), q.Name,q.Description,q.Questions));

        return allQuizes.ToList();
    }

    public void AddQuiz(QuizRecord quizRecord)
    {
        List<Question> questionList = new List<Question>();
        foreach (var question in quizRecord.Questions)
        {
            questionList.Add(ConvertQuestionRecordToQuestion(question));
        }

        var newQuiz = new Quiz()
        {
            Name = quizRecord.Name,
            Description = quizRecord.Description,
            Questions = questionList
        };

        _quizes.InsertOne(newQuiz);
    }

    public Question ConvertQuestionRecordToQuestion(QuestionRecord questionRecord)
    {
        var newQuestion = new Question()
        {
            Id = ObjectId.Parse(questionRecord.Id),
            Description = questionRecord.Description,
            Answers = questionRecord.Answers,
            CorrectAnswer = questionRecord.CorrectAnswer
        };

        return newQuestion;
    }

    public QuestionRecord ConvertQuestionToQuestionRecord(Question question)
    {
        var newQuestionRecord = new QuestionRecord(question.Id.ToString(), question.Description, question.Answers, question.CorrectAnswer);

        return newQuestionRecord;
    }
}