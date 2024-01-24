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
using WPF.Models;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for QuestionsView.xaml
    /// </summary>
    public partial class QuestionsView : UserControl
    {
        private readonly QuizRepository _repo;

        public ObservableCollection<QuestionModel> Questions { get; set; } = new();

        public QuestionModel? SelectedQuestion { get; set; } = new();

        public QuestionsView()
        {
            InitializeComponent();

            DataContext = this;

            _repo = new QuizRepository();

            var allQuestions = _repo.GetAllQuestions();

            foreach (var question in allQuestions)
            {
                Questions.Add(new QuestionModel(){Id = question.Id, Answers = question.Answers, CorrectAnswer = question.CorrectAnswer, Description = question.Description});
            }
        }
    }
}
