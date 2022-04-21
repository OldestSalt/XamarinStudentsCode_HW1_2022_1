using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HW1 {

    public partial class MainPage : ContentPage {
        public ObservableCollection<Item> items = new ObservableCollection<Item>();

        public MainPage() {
            InitializeComponent();
            
            BindableLayout.SetItemsSource(items_container, items);
        }

        private void Button_Clicked(object sender, EventArgs e) {
            var itemPage = new AddItemPage();
            itemPage.Disappearing += (_, __) => {
                if (itemPage.SelectedItem != null) {
                    var selectedItem = itemPage.SelectedItem;
                    var found = items.Where(x => x.item_name == selectedItem.item_name).FirstOrDefault();
                    if (found != null) {
                        found.item_count += selectedItem.item_count;
                    }
                    else {
                        items.Add(selectedItem);
                    }
                }
            };
            Navigation.PushAsync(itemPage);
        }

        private async void OrderButton_Clicked(object sender, EventArgs e) {
            if (items.Count == 0) {
                await DisplayAlert("Order is empty", "Add item to order", "Ok");
            }
            else {
                var result = await DisplayAlert("Confirm", "Order description", "Agree", "Disagree");
                if (result) {
                    items.Clear();
                    await DisplayAlert("Successeful", "Order description", "Ok");
                }
            }
        }

        private void DeleteItem(object sender, EventArgs e) {
            items.Remove(items.FirstOrDefault(i => i.item_id == int.Parse((sender as Button).ClassId)));
        }
    }
}
