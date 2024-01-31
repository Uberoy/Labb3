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
        public ObservableCollection<CategoryModel> CategoriesInQuestion { get; set; } = new();

        public QuestionModel? SelectedQuestion { get; set; } = new();
        public AnswerModel? SelectedAnswer { get; set; } = new();
        public CategoryModel? SelectedCategory { get; set; } = new();
        public CategoryModel? SelectedCategoriesInQuestion { get; set; } = new();
        public CategoryModel? SelectedCategoryInFilter { get; set; } = new();


        public QuestionsView()
        {
            InitializeComponent();

            DataContext = this;

            _repo = new QuizRepository();

            PopulateQuestionList();

            

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
            CategoriesInQuestion.Clear();

            var allAnswers = _repo.GetAllAnswersFromQuestion(SelectedQuestion.Id);
            var correctAnswer = _repo.GetCorrectAnswerFromQuestion(SelectedQuestion.Id);
            var categories = _repo.GetAllCategoriesFromQuestion(SelectedQuestion.Id);

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

            foreach (var category in categories)
            {
                CategoriesInQuestion.Add(new CategoryModel(){Id = category.Id.ToString(), Name = category.Name});
            }
        }

        private void AddCategoryToQuestionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedQuestion is null || SelectedCategory is null)
            {
                return;
            }

            _repo.AddCategoryToQuestion(SelectedQuestion.Id, SelectedCategory.Id);
            CategoriesInQuestion.Add(SelectedCategory);
        }

        private void RemoveCategoryFromQuestionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedCategoriesInQuestion is null)
            {
                return;
            }

            _repo.RemoveCategoryFromQuestion(SelectedQuestion.Id, SelectedCategoriesInQuestion.Id);
            CategoriesInQuestion.Remove(SelectedCategoriesInQuestion);
        }

        private void CategoriesBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var categoryId = SelectedCategoryInFilter.Id;


            Questions.Clear();

            var allQuestionsWithFilter = _repo.GetAllQuestionsWithFilter(categoryId);

            foreach (var question in allQuestionsWithFilter)
            {
                //Questions.Add(new QuestionModel() { Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description, Categories = question.Categories});
                Questions.Add(new QuestionModel() { Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description });
            }
        }

        private void PopulateQuestionList()
        {
            var allQuestions = _repo.GetAllQuestions();

            foreach (var question in allQuestions)
            {
                //Questions.Add(new QuestionModel() { Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description, Categories = question.Categories});
                Questions.Add(new QuestionModel() { Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description });
            }
        }

        private void ResetFilterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Questions.Clear();
            PopulateQuestionList();
        }
    }
}