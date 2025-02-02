﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HW1 {

    public partial class MainPage : ContentPage {
        public static ObservableCollection<Item> items = new ObservableCollection<Item>();

        public MainPage() {
            InitializeComponent();
            
            BindableLayout.SetItemsSource(items_container, items);
        }

        private void Button_Clicked(object sender, EventArgs e) {
            var itemPage = new AddItemPage();
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

        private void IncreaseAmount(object sender, EventArgs e) {
            var countLabel = ((sender as Button).Parent as Grid).Children[3] as Label;
            countLabel.Text = (int.Parse(countLabel.Text) + 1).ToString();
        }

        private void DecreaseAmount(object sender, EventArgs e) {
            var countLabel = ((sender as Button).Parent as Grid).Children[3] as Label;
            countLabel.Text = (int.Parse(countLabel.Text) - 1).ToString();
        }
    }
}
