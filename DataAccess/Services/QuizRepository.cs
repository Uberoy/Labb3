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

    public void AddQuestionToQuiz(string quizId, string questionId)
    {
        var quizObjectId = ObjectId.Parse(quizId);
        var questionObjectId = ObjectId.Parse(questionId);

        var quizFilter = Builders<Quiz>.Filter.Eq(q => q.Id, quizObjectId);
        var questionFilter = Builders<Question>.Filter.Eq(q => q.Id, questionObjectId);

        var foundQuestion = _questions.Find(questionFilter).FirstOrDefault();

        var update = Builders<Quiz>.Update.Push(q => q.Questions, foundQuestion);

        _quizes.UpdateOne(quizFilter, update);
    }

    public void RemoveQuestionFromQuiz(string quizId, string questionId)
    {
        var quizObjectId = ObjectId.Parse(quizId);
        var questionObjectId = ObjectId.Parse(questionId);

        var quizFilter = Builders<Quiz>.Filter.Eq(q => q.Id, quizObjectId);
        var questionFilter = Builders<Question>.Filter.Eq(q => q.Id, questionObjectId);

        var foundQuestion = _questions.Find(questionFilter).FirstOrDefault();

        var update = Builders<Quiz>.Update.Pull(q => q.Questions, foundQuestion);

        _quizes.UpdateOne(quizFilter, update);
    }

    public List<QuestionRecord> GetAllQuestions()
    {
        var filter = Builders<Question>.Filter.Empty;
        var allQuestions = _questions.Find(filter).ToList().Select(q =>
            new QuestionRecord(q.Id.ToString(), q.Description, q.Answers, q.CorrectAnswer));

        return allQuestions.ToList();
    }

    public List<QuestionRecord> GetAllQuestionsFromQuiz(string quizId)
    {
        var quizObjectId = ObjectId.Parse(quizId);

        var filter = Builders<Quiz>.Filter.Eq(q => q.Id, quizObjectId);
        var quiz = _quizes.Find(filter).FirstOrDefault();

        var questions = quiz.Questions.Select(q =>
            new QuestionRecord(q.Id.ToString(), q.Description, q.Answers, q.CorrectAnswer)).ToList();

        return questions;
    }

    public List<string> GetAllAnswersFromQuestion(string questionId)
    {
        var questionObjectId = ObjectId.Parse(questionId);

        var filter = Builders<Question>.Filter.Eq(q => q.Id, questionObjectId);
        var allAnswers = _questions.Find(filter).ToList().SelectMany(a => a.Answers);

        return allAnswers.ToList();
    }

    public int GetCorrectAnswerFromQuestion(string questionId)
    {
        var questionObjectId = ObjectId.Parse(questionId);
        var filter = Builders<Question>.Filter.Eq(q => q.Id, questionObjectId);

        var correctAnswer = _questions.Find(filter).FirstOrDefault();

        return correctAnswer.CorrectAnswer;
    }

    public List<QuizRecord> GetAllQuizzesWithQuestions()
    {
        var allQuizzes = _quizes.Find(Builders<Quiz>.Filter.Empty).ToList();
        var quizDTOs = new List<QuizRecord>();

        foreach (var quiz in allQuizzes)
        {
            var questionIds = quiz.Questions.Select(q => q.Id).ToList();
            var questionFilter = Builders<Question>.Filter.In(q => q.Id, questionIds);
            var questions = _questions.Find(questionFilter).ToList();

            var quizDTO = new QuizRecord
            ( 
                quiz.Id.ToString(), 
                quiz.Name, 
                quiz.Description, 
                questions.Select(q =>
                    new QuestionRecord(q.Id.ToString(), q.Description, q.Answers, q.CorrectAnswer)).ToList()
            );

            quizDTOs.Add(quizDTO);
        }

        return quizDTOs;
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
}