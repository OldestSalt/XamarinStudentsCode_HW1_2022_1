using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HW1 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage {
        private const int max_limit = 100;
        private const int min_limit = 1;

        class ItemDescription {
            public string Image { get; set; }
            public string Description { get; set; }
        }

        private SortedDictionary<string, ItemDescription> drinksDependences = new SortedDictionary<string, ItemDescription>() {
            { "Coca-cola", new ItemDescription() { Image = "Cola.jpg", Description = "Пендоская кола"} },
            { "Baikal", new ItemDescription() { Image = "Baikal.jpg", Description = "Русская кола"} }
        };

        public AddItemPage() {
            InitializeComponent();
            drinks.ItemsSource = drinksDependences.Keys.ToList();
            drinks.SelectedIndex = 0;
        }

        private void Button_Order(object sender, EventArgs e) {
            Item selectedItem = new Item() {
                item_name = drinks.SelectedItem.ToString(),
                item_count = int.Parse(count.Text),
                item_image = drinksDependences[drinks.SelectedItem.ToString()].Image
            };

            var found = MainPage.items.FirstOrDefault(x => x.item_name == selectedItem.item_name);
            if (found != null) {
                found.item_count += selectedItem.item_count;
            }
            else {
                MainPage.items.Add(selectedItem);
            }

            Navigation.PopAsync();
        }

        private void Button_Minus(object sender, EventArgs e) {
            count.Text = int.Parse(count.Text) > min_limit ? 
                (int.Parse(count.Text) - 1).ToString() : 
                min_limit.ToString();
        }

        private void Button_Plus(object sender, EventArgs e) {
            count.Text = int.Parse(count.Text) < max_limit ? 
                (int.Parse(count.Text) + 1).ToString() : 
                max_limit.ToString();
        }

        private void count_Completed(object sender, EventArgs e) {
            if (uint.TryParse((sender as Entry).Text, out var number)) {
                if (number > max_limit) {
                    count.Text = max_limit.ToString();
                }
                else if (number < min_limit) {
                    count.Text = min_limit.ToString();
                }
                else {
                    count.Text = number.ToString();
                }

            }
            else {
                count.Text = "1";
            }

        }

        private void drinks_SelectedIndexChanged(object sender, EventArgs e) {
            img.Source = drinksDependences[drinks.SelectedItem.ToString()].Image;
            item_description.Text = drinksDependences[drinks.SelectedItem.ToString()].Description;
        }
    }
}