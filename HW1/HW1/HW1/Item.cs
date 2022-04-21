using System.ComponentModel;

namespace HW1
{
    public class Item : INotifyPropertyChanged {
        private static int lastId = 0;

        public Item()
        {
            lastId++;
            item_id = lastId;
        }

        public string item_name
        {
            get; set;
        }

        private int ic;

        public int item_count
        {
            get {
                return ic;
            }
            set {
                if (ic != value) {
                    ic = value;
                    OnPropertyChanged("item_count");
                }
            }
        }

        public string item_image
        {
            get; set;
        }

        public int item_id
        {
            get;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
