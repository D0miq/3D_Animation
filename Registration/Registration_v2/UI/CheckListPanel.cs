namespace Registration_v2.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// An instance of the <see cref="CheckListPanel"/> class represents an UI control with a check list box and provides access to data and events over the check list box.
    /// </summary>
    public partial class CheckListPanel : UserControl, IShowable
    {
        /// <summary>
        /// The <see cref="ICheckListPanelListener"/> interface has to be implemented by a class that wants to react to events from the <see cref="CheckListPanel"/>.
        /// </summary>
        public interface ICheckListPanelListener
        {
            /// <summary>
            /// Called when an item is saved.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">Event arguments.</param>
            void OnSaveClicked(object sender, EventArgs e);

            /// <summary>
            /// Called when an item is checked.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">Event arguments.</param>
            void OnItemCheckChanged(object sender, ItemCheckEventArgs e);
        }

        /// <summary>
        /// The listener that reacts on the events.
        /// </summary>
        private ICheckListPanelListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckListPanel"/> class.
        /// </summary>
        /// <param name="listener">The listener.</param>
        /// <param name="text">The caption of a check list panel.</param>
        public CheckListPanel(ICheckListPanelListener listener, string text)
        {
            this.InitializeComponent();
            this.listener = listener;
            this.groupBox.Text = text;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a panel is shown.
        /// </summary>
        public bool IsShown { get; set; }

        /// <summary>
        /// Adds an item to a check list panel.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item.</param>
        public void Add<T>(T item)
        {
            this.checkListBox.Items.Add(item);
        }

        /// <summary>
        /// Adds all items to a check list panel.
        /// </summary>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <param name="items">The list of items.</param>
        public void AddAll<T>(List<T> items)
        {
            this.checkListBox.Items.AddRange(items.Cast<object>().ToArray());
        }

        /// <summary>
        /// Gets a selected item from a check list box.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <returns>The selected item.</returns>
        public T GetSelectedItem<T>()
        {
            return (T)this.checkListBox.SelectedItem;
        }

        /// <summary>
        /// Gets an index of a selected item from a check list box.
        /// </summary>
        /// <returns>The index of the selected item.</returns>
        public int GetSelectedIndex()
        {
            return this.checkListBox.SelectedIndex;
        }

        /// <summary>
        /// Gets all checked items from a check list box.
        /// </summary>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <returns>The list of checked items.</returns>
        public List<T> GetCheckedItems<T>()
        {
            return this.checkListBox.CheckedItems.Cast<T>().ToList();
        }

        /// <summary>
        /// Gets indices of all checked items from a check list box.
        /// </summary>
        /// <returns>The list of indices.</returns>
        public List<int> GetCheckedIndices()
        {
            return this.checkListBox.CheckedIndices.Cast<int>().ToList();
        }

        /// <summary>
        /// Passes save event to a listener.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listener.OnSaveClicked(this, e);
        }

        /// <summary>
        /// Passes item check event to a listener.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        private void checkListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.listener.OnItemCheckChanged(this, e);
        }
    }
}
