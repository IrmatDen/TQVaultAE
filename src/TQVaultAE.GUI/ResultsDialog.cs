//-----------------------------------------------------------------------
// <copyright file="ResultsDialog.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.GUI
{
	using Properties;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Globalization;
	using System.Windows.Forms;
	using Tooltip;
	using TQVaultData;
	using Equin.ApplicationFramework;

	/// <summary>
	/// Results dialog form class
	/// </summary>
	internal partial class ResultsDialog : VaultForm
	{
		/// <summary>
		/// List of all results
		/// </summary>
		private List<Result> resultsList;

		private BindingListView<Result> resultsBindingListView;

		/// <summary>
		/// User selected result from the list
		/// </summary>
		private Result selectedResult;

		/// <summary>
		/// Search string passed from user
		/// </summary>
		private string searchString;

		/// <summary>
		/// Instance of the popup tool tip.
		/// </summary>
		private TTLib tooltip;

		/// <summary>
		/// Text for Tooltip control
		/// </summary>
		private string tooltipText;

		/// <summary>
		/// Initializes a new instance of the ResultsDialog class.
		/// </summary>
		public ResultsDialog()
		{
			this.InitializeComponent();
			this.tooltip = new TTLib();
			this.resultsList = new List<Result>();
			this.resultsBindingListView = new BindingListView<Result>(resultsList);
			this.resultsDataGridView.DataSource = resultsBindingListView;

			this.item.HeaderText = Resources.ResultsItem;
			this.containerName.HeaderText = Resources.ResultsContainer;
			this.containerType.HeaderText = Resources.ResultsContainerType;
			this.quality.HeaderText = Resources.ResultsQuality;
			this.requiredLevel.HeaderText = Resources.ResultsLevel;
			this.requiredStrength.HeaderText = Resources.ResultsStrength;
			this.requiredDexterity.HeaderText = Resources.ResultsDexterity;
			this.requiredIntelligence.HeaderText = Resources.ResultsIntelligence;

			this.DrawCustomBorder = true;

			this.FormDesignRatio = 0.0F;
			this.OriginalFormSize = this.Size;
			this.OriginalFormScale = 1.0F;
			this.LastFormSize = this.Size;
		}

		/// <summary>
		/// Event Handler for the ResultChanged event.
		/// </summary>
		/// <typeparam name="ResultChangedEventArgs">ResultChangedEventArgs type</typeparam>
		/// <param name="sender">sender object</param>
		/// <param name="e">ResultChangedEventArgs data</param>
		public delegate void EventHandler<ResultChangedEventArgs>(object sender, ResultChangedEventArgs e);

		/// <summary>
		/// Event for changing to a different highlighted result.
		/// </summary>
		public event EventHandler<ResultChangedEventArgs> ResultChanged;

		/// <summary>
		/// Gets the list of results collection
		/// </summary>
		public List<Result> ResultsList
		{
			get
			{
				return this.resultsList;
			}
		}

		/// <summary>
		/// Sets the user search string
		/// </summary>
		public string SearchString
		{
			set
			{
				this.searchString = value;
			}
		}

		/// <summary>
		/// Gets the string name for the corresponding SackType
		/// </summary>
		/// <param name="containterType">SackType which we are looking up</param>
		/// <returns>string containing the sack type</returns>
		public static string GetContainerTypeString(SackType containterType)
		{
			switch (containterType)
			{
				case SackType.Vault:
					return Resources.ResultsContainerVault;

				case SackType.Player:
					return Resources.ResultsContainerPlayer;

				case SackType.Equipment:
					return Resources.ResultsContainerEquip;

				case SackType.Stash:
					return Resources.ResultsContainerStash;

				case SackType.TransferStash:
					return Resources.GlobalTransferStash;

				default:
					return "Unknown";
			}
		}

		/// <summary>
		/// Handler for showing the results dialog.
		/// Populates the data grid view.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">EventArgs data</param>
		private void ResultsDialogShown(object sender, EventArgs e)
		{
			this.PopulateDataGridView();

			int totalWidth = 0;
			foreach (DataGridViewColumn column in this.resultsDataGridView.Columns)
			{
				totalWidth += column.Width;
			}

			totalWidth += this.resultsDataGridView.Margin.Horizontal;

			int totalHeight = 0;
			foreach (DataGridViewRow row in this.resultsDataGridView.Rows)
			{
				totalHeight += row.Height;
			}

			totalHeight += this.Padding.Vertical;

			if (totalHeight > this.Height)
			{
				totalWidth += SystemInformation.VerticalScrollBarWidth;
			}

			this.Width = Math.Min(Screen.PrimaryScreen.WorkingArea.Width, totalWidth + this.Padding.Horizontal);
			this.Height = Math.Max(this.Height, Math.Min(Screen.PrimaryScreen.WorkingArea.Height - SystemInformation.HorizontalScrollBarHeight, totalHeight - SystemInformation.HorizontalScrollBarHeight));
			this.Location = new Point(this.Location.X, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

			this.selectedResult = null;

			// the tooltip must be initialized after the main form is shown and active.
			this.tooltip.Initialize(this);
			this.tooltip.ActivateCallback = new TTLibToolTipActivate(this.ToolTipCallback);
		}

		/// <summary>
		/// Tooltip callback
		/// </summary>
		/// <param name="windowHandle">handle of this window form</param>
		/// <returns>tooltip string</returns>
		private string ToolTipCallback(int windowHandle)
		{
			// see if this is us
			if (this.resultsDataGridView.Handle.ToInt32() == windowHandle)
			{
				// yep.
				return this.GetToolTip(this.selectedResult);
			}

			return null;
		}

		/// <summary>
		/// Returns a string containing the tool tip text for the highlighted result.
		/// </summary>
		/// <param name="selectedResult">Currently selected Result</param>
		/// <returns>String containing the tool tip for the Result.</returns>
		private string GetToolTip(Result selectedResult)
		{
			if (selectedResult == null)
			{
				// hide the tooltip
				this.tooltipText = null;
				////this.tooltip.ChangeText(this.tooltipText);
			}
			else
			{
				string attributes = selectedResult.Item.GetAttributes(true); // true means hide uninteresting attributes
				string setitems = selectedResult.Item.GetItemSetString();
				string reqs = selectedResult.Item.GetRequirements();

				// combine the 2
				if (reqs.Length < 1)
				{
					this.tooltipText = attributes;
				}
				else if (setitems.Length < 1)
				{
					string reqTitle = Database.MakeSafeForHtml("?Requirements?");
					reqTitle = string.Format(CultureInfo.InvariantCulture, "<font size=+2 color={0}>{1}</font><br>", Database.HtmlColor(Item.GetColor(ItemStyle.Potion)), reqTitle);
					string separator = string.Format(CultureInfo.InvariantCulture, "<hr color={0}><br>", Database.HtmlColor(Item.GetColor(ItemStyle.Broken)));
					this.tooltipText = string.Concat(attributes, separator, reqs);
				}
				else
				{
					string reqTitle = Database.MakeSafeForHtml("?Requirements?");
					reqTitle = string.Format(CultureInfo.InvariantCulture, "<font size=+2 color={0}>{1}</font><br>", Database.HtmlColor(Item.GetColor(ItemStyle.Potion)), reqTitle);
					string separator1 = string.Format(CultureInfo.InvariantCulture, "<hr color={0}>", Database.HtmlColor(Item.GetColor(ItemStyle.Broken)));
					string separator2 = string.Format(CultureInfo.InvariantCulture, "<hr color={0}><br>", Database.HtmlColor(Item.GetColor(ItemStyle.Broken)));
					this.tooltipText = string.Concat(attributes, separator1, setitems, separator2, reqs);
				}

				// show tooltip
				this.tooltipText = string.Concat(Database.DB.TooltipBodyTag, this.tooltipText);
				////this.tooltip.ChangeText(this.tooltipText);
			}

			return this.tooltipText;
		}

		/// <summary>
		/// Populates the data grid view from the results list.
		/// </summary>
		private void PopulateDataGridView()
		{
			if (this.resultsList == null || this.resultsList.Count < 1)
			{
				return;
			}

			// Update the dialog text.
			this.Text = string.Format(CultureInfo.CurrentCulture, Resources.ResultsText, this.resultsList.Count, this.searchString);

			this.resultsBindingListView.Refresh();
		}

		/// <summary>
		/// Handler for user clicking on one of the result cells
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">DataGridViewCellEventArgs data</param>
		private void ResultsDataGridViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			// Ignore double click on the header.
			if (e.RowIndex < 0)
			{
				return;
			}

			this.Close();
		}

		/// <summary>
		/// Handler for pressing a key
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">KeyPressEventArgs data</param>
		private void ResultsDataGridViewKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 27)
			{
				// Escape key
				this.Close();
			}
		}

		/// <summary>
		/// Handler for the focus changing to a different row.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">DataGridViewCellEventArgs data</param>
		private void ResultsDataGridViewRowEnter(object sender, DataGridViewCellEventArgs e)
		{
			// Ignore click on the header.
			if (e.RowIndex < 0)
			{
				return;
			}

			this.selectedResult = ((ObjectView<Result>)this.resultsDataGridView.Rows[e.RowIndex].DataBoundItem).Object;

			this.ResultChanged?.Invoke(this, new ResultChangedEventArgs(this.selectedResult));

			////if (!string.IsNullOrEmpty(this.tooltipText))
			////{
			////this.tooltip.ChangeText(this.GetToolTip());
			////}
		}

		/// <summary>
		/// Handler for the mouse leaving the DataGridView.  Used to clear the tooltip.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">EventArgs data</param>
		private void ResultsDataGridViewMouseLeave(object sender, EventArgs e)
		{
			this.tooltipText = null;
			this.tooltip.ChangeText(this.tooltipText);
		}

		/// <summary>
		/// Handler for entering a cell in the DataGridView.
		/// Used to highlight the row under the mouse and updating the tool tip text.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">DataGridViewCellEventArgs data</param>
		private void ResultsDataGridViewCellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			// Ignore click on the header.
			if (e.RowIndex < 0)
			{
				return;
			}

			this.resultsDataGridView.CurrentCell = this.resultsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
			this.selectedResult = ((ObjectView<Result>)this.resultsDataGridView.Rows[e.RowIndex].DataBoundItem).Object;
			this.tooltip.ChangeText(this.GetToolTip(this.selectedResult));
		}

		/// <summary>
		/// Handler for the mouse leaving a DataGridView cell.
		/// Used to clear the tool tip and the selected result.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">DataGridViewCellEventArgs data</param>
		private void ResultsDataGridViewCellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			this.selectedResult = null;
			this.tooltipText = null;
			this.tooltip.ChangeText(this.tooltipText);
		}

		/// <summary>
		/// Handler for leaving a DataGridView row.
		/// Used to clear the tool tip and the selected result.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">DataGridViewCellEventArgs data</param>
		private void ResultsDataGridViewRowLeave(object sender, DataGridViewCellEventArgs e)
		{
			this.selectedResult = null;
			this.tooltipText = null;
			this.tooltip.ChangeText(this.tooltipText);
		}

		/// <summary>
		/// Handler for Resizing the Results Dialog Form.  Resizes the DataGridView based on the Results Dialog size.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">EventArgs data</param>
		private void ResultsDialog_Resize(object sender, EventArgs e)
		{
			this.resultsDataGridView.Size = new Size(
				Math.Max(this.Padding.Horizontal + 1, this.Width - this.Padding.Horizontal),
				Math.Max(this.Padding.Vertical + 1, this.Height - this.Padding.Vertical));
		}

		private void ResultsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex < 2)
			{
				var value = (ObjectView<Result>)this.resultsDataGridView.Rows[e.RowIndex].DataBoundItem;
				e.CellStyle.ForeColor = value.Object.Color;
			}
		}
	}
}