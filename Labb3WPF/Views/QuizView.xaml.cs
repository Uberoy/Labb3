using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb3WPF.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Labb3WPF.Views
{
    /// <summary>
    /// Interaction logic for QuizView.xaml
    /// </summary>
    public partial class QuizView : UserControl
    {
        private readonly QuizRepository _repo;

        public ObservableCollection<QuizModel> Quizes { get; set; } = new();
        public ObservableCollection<QuestionModel> QuestionsInQuiz { get; set; } = new();
        public ObservableCollection<QuestionModel> QuestionsAvailable { get; set; } = new();

        public ObservableCollection<AnswerModel> AnswersInQuestionQuiz { get; set; } = new();
        public ObservableCollection<AnswerModel> AnswersInQuestionAvailable { get; set; } = new();



        public QuizModel? SelectedQuiz { get; set; } = new();
        public QuestionModel? SelectedQuestionInQuiz { get; set; } = new();
        public QuestionModel? SelectedQuestionAvailable { get; set; } = new();


        public QuizView()
        {
            InitializeComponent();

            DataContext = this;

            _repo = new QuizRepository();

            var allQuizes = _repo.GetAllQuizzesWithQuestions();
            var allQuestions = _repo.GetAllQuestions();

            foreach (var question in allQuestions)
            {
                QuestionsAvailable.Add(new QuestionModel(){Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description});
            }

            //foreach (var quiz in allQuizes)
            //{
            //    Quizes.Add(new QuizModel() { Id = quiz.Id, Name = quiz.Name, Description = quiz.Description, Questions = quiz.Questions });
            //}

            //Den utkommenterade varianten fungerar bara om den utkommenterade propertyn i QuizModel läggs tillbaka. Frågan är om den propertyn äns behövs? Jag är inte 100 % säker på vad en
            //model bör innehålla, vad som är viktigt/koscher osv.
            foreach (var quiz in allQuizes)
            {
                Quizes.Add(new QuizModel() { Id = quiz.Id, Name = quiz.Name, Description = quiz.Description});
            }
        }

        private void QuizesLv_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedQuiz is null)
            {
                return;
            }

            QuestionsInQuiz.Clear();

            var questionsInQuiz = _repo.GetAllQuestionsFromQuiz(SelectedQuiz.Id);

            foreach (var question in questionsInQuiz)
            {
                QuestionsInQuiz.Add(new QuestionModel(){Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description});
            }
        }

        private void RemoveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedQuestionInQuiz is null || SelectedQuestionInQuiz is null)
            {
                return;
            }

            _repo.RemoveQuestionFromQuiz(SelectedQuiz.Id, SelectedQuestionInQuiz.Id);
            QuestionsInQuiz.Remove(SelectedQuestionInQuiz);
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedQuiz is null || SelectedQuestionAvailable is null)
            {
                return;
            }

            _repo.AddQuestionToQuiz(SelectedQuiz.Id, SelectedQuestionAvailable.Id);
            QuestionsInQuiz.Add(SelectedQuestionAvailable);
        }

        private void QuestionsInQuizLv_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedQuestionInQuiz is null)
            {
                return;
            }

            AnswersInQuestionQuiz.Clear();

            var allAnswers = _repo.GetAllAnswersFromQuestion(SelectedQuestionInQuiz.Id);
            var correctAnswer = _repo.GetCorrectAnswerFromQuestion(SelectedQuestionInQuiz.Id);

            for (int i = 0; i < allAnswers.Count; i++)
            {
                if (i == correctAnswer)
                {
                    AnswersInQuestionQuiz.Add(new AnswerModel() { Text = allAnswers[i], IsCorrect = true });
                }
                else if (i != correctAnswer)
                {
                    AnswersInQuestionQuiz.Add(new AnswerModel() { Text = allAnswers[i], IsCorrect = false });
                }
            }
        }

        private void QuestionsAvailableLv_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedQuestionAvailable is null)
            {
                return;
            }

            AnswersInQuestionAvailable.Clear();

            var allAnswers = _repo.GetAllAnswersFromQuestion(SelectedQuestionAvailable.Id);
            var correctAnswer = _repo.GetCorrectAnswerFromQuestion(SelectedQuestionAvailable.Id);

            for (int i = 0; i < allAnswers.Count; i++)
            {
                if (i == correctAnswer)
                {
                    AnswersInQuestionAvailable.Add(new AnswerModel() { Text = allAnswers[i], IsCorrect = true });
                }
                else if (i != correctAnswer)
                {
                    AnswersInQuestionAvailable.Add(new AnswerModel() { Text = allAnswers[i], IsCorrect = false });
                }
            }
        }
    }
}
