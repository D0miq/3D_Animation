namespace Registration_v2.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class CheckListPanel : UserControl, IShowable
    {
        public interface ICheckListPanelListener
        {
            void OnSaveClicked(object sender, EventArgs e);

            void OnDeleteClicked(object sender, EventArgs e);

            void OnItemCheckChanged(object sender, ItemCheckEventArgs e);
        }

        private ICheckListPanelListener listener;

        public CheckListPanel(ICheckListPanelListener listener, string text)
        {
            this.InitializeComponent();
            this.listener = listener;
            this.groupBox.Text = text;
        }

        public bool IsShown { get; set; }

        public void Add<T>(T item)
        {
            this.checkListBox.Items.Add(item);
        }

        public void AddAll<T>(List<T> items)
        {
            this.checkListBox.Items.AddRange(items.Cast<object>().ToArray());
        }

        public T GetSelectedItem<T>()
        {
            return (T)this.checkListBox.SelectedItem;
        }

        public int GetSelectedIndex()
        {
            return this.checkListBox.SelectedIndex;
        }

        public List<T> GetCheckedItems<T>()
        {
            return this.checkListBox.CheckedItems.Cast<T>().ToList();
        }

        public List<int> GetCheckedIndices()
        {
            return this.checkListBox.CheckedIndices.Cast<int>().ToList();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listener.OnSaveClicked(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listener.OnDeleteClicked(sender, e);
        }

        private void checkListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.listener.OnItemCheckChanged(sender, e);
        }
    }
}
