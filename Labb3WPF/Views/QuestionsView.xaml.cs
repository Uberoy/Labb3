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

namespace Labb3WPF.Views
{
    /// <summary>
    /// Interaction logic for QuestionsView.xaml
    /// </summary>
    public partial class QuestionsView : UserControl
    {
        private readonly QuizRepository _repo;

        public ObservableCollection<QuestionModel> Questions { get; set; } = new();
        public ObservableCollection<AnswerModel> Answers { get; set; } = new();
        public ObservableCollection<CategoryModel> Categories { get; set; } = new();

        public QuestionModel? SelectedQuestion { get; set; } = new();
        public AnswerModel? SelectedAnswer { get; set; } = new();
        public CategoryModel? SelectedCategory { get; set; } = new();


        public QuestionsView()
        {
            InitializeComponent();

            DataContext = this;

            _repo = new QuizRepository();

            var allQuestions = _repo.GetAllQuestions();

            foreach (var question in allQuestions)
            {
                //Questions.Add(new QuestionModel() { Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description, Categories = question.Categories});
                Questions.Add(new QuestionModel() { Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description});
            }

            var allCategories = _repo.GetAllCategories();

            foreach (var category in allCategories)
            {
                Categories.Add(new CategoryModel(){ Id = category.Id, Name = category.Name});
            }
        }


        private void QuestionsLv_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedQuestion is null)
            {
                return;
            }

            Answers.Clear();

            var allAnswers = _repo.GetAllAnswersFromQuestion(SelectedQuestion.Id);
            var correctAnswer = _repo.GetCorrectAnswerFromQuestion(SelectedQuestion.Id);

            for (int i = 0; i < allAnswers.Count; i++)
            {
                if (i == correctAnswer)
                {
                    Answers.Add(new AnswerModel() { Text = allAnswers[i], IsCorrect = true });
                }
                else if (i != correctAnswer)
                {
                    Answers.Add(new AnswerModel() { Text = allAnswers[i], IsCorrect = false });
                }
            }
        }

        private void AddCategoryToQuestionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedQuestion is null || SelectedCategory is null)
            {
                return;
            }

            _repo.AddCategoryToQuestion(SelectedQuestion.Id, SelectedCategory.Id);
        }
    }
}