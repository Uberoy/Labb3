using Labb3WPF.Models;
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
using Common.DTO;

namespace Labb3WPF.Views
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        private readonly QuizRepository _repo;
        public ObservableCollection<CategoryModel> CategoriesAvailable { get; set; } = new();

        public CategoryModel? SelectedCategory { get; set; } = new();

        public string EditCategory { get; set; } = string.Empty;

        public CategoryView()
        {
            InitializeComponent();

            DataContext = this;

            _repo = new QuizRepository();

            UpdateCategoryList();
        }

        private void CategoriesAvailableLv_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var newCategory = new CategoryModel();
            newCategory.Name = EditCategory;

            var categoryRecord = new CategoryRecord("", newCategory.Name);
            
            _repo.AddCategory(categoryRecord);

            UpdateCategoryList();
        }

        private void UpdateCategoryList()
        {

            CategoriesAvailable.Clear();
            var allCategories = _repo.GetAllCategories();

            foreach (var category in allCategories)
            {
                CategoriesAvailable.Add(new CategoryModel() { Id = category.Id, Name = category.Name });
            }
        }
    }
}
