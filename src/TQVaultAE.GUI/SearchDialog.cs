//-----------------------------------------------------------------------
// <copyright file="SearchDialog.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.GUI
{
	using Properties;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;
	using TQVaultData;

	/// <summary>
	/// Class for the Search Dialog box.
	/// </summary>
	public partial class SearchDialog : VaultForm
	{
		List<UiItemType> uiItemTypes;

		/// <summary>
		/// Initializes a new instance of the SearchDialog class.
		/// </summary>
		public SearchDialog()
		{
			this.InitializeComponent();

			// Load localized strings
			this.Text = Resources.SearchDialogCaption;
			this.searchLabel.Text = Resources.SearchDialogText;
			this.findButton.Text = Resources.MainFormSearchButtonText;
			this.cancelButton.Text = Resources.GlobalCancel;

			this.InitializeTypeComboBox();

			this.searchTextBox.Focus();
		}

		public IItemPredicate GetFilters()
		{
			var predicates = new List<IItemPredicate>();
			string searchString = searchTextBox.Text.Trim();

			if (!string.IsNullOrEmpty(searchString))
			{
				var TOKENS = "@#$".ToCharArray();
				int fromIndex = 0;
				int toIndex;
				do
				{
					string term;

					toIndex = searchString.IndexOfAny(TOKENS, fromIndex + 1);
					if (toIndex < 0)
					{
						term = searchString.Substring(fromIndex);
					}
					else
					{
						term = searchString.Substring(fromIndex, toIndex - fromIndex);
						fromIndex = toIndex;
					}

					switch (term[0])
					{
						case '@':
							predicates.Add(GetPredicateFrom(term.Substring(1), it => new ItemTypePredicate(it)));
							break;
						case '#':
							predicates.Add(GetPredicateFrom(term.Substring(1), it => new ItemAttributePredicate(it)));
							break;
						case '$':
							predicates.Add(GetPredicateFrom(term.Substring(1), it => new ItemQualityPredicate(it)));
							break;
						default:
							foreach (var name in term.Split('&'))
							{
								predicates.Add(GetPredicateFrom(name, it => new ItemNamePredicate(it)));
							}
							break;
					}
				} while (toIndex >= 0);
			}
			else
			{
				predicates.Add(GetRequiredLevelPredicate(predicates));
				predicates.Add(new ItemTypeProxyPredicate(((UiItemType)itemTypeComboBox.SelectedItem).predicate));
			}

			return new ItemAndPredicate(predicates);
		}

		#region Private methods

		private void InitializeTypeComboBox()
		{
			Func<string, UiItemType> uiItemFromRealType = type => {
				string label = Resources.ResourceManager.GetString("ItemType" + type);
				if (string.IsNullOrEmpty(label))
				{
					label = type;
				}
				return new UiItemType("-- " + label, i => i.ItemClass.ToUpperInvariant().Contains(type.ToUpperInvariant()));
			};

			uiItemTypes = new List<UiItemType>();
			uiItemTypes.Add(new UiItemType(Resources.SearchAllItems, _ => true ));
			uiItemTypes.Add(new UiItemType(Resources.SearchAllWeapons, i => i.IsWeapon));
			uiItemTypes.Add(new UiItemType("- " + Resources.SearchAll1HWeapon, i => i.IsWeapon && !i.Is2HWeapon));
			Item.Get1HWeaponTypes().ForEach(type => uiItemTypes.Add(uiItemFromRealType(type)));

			uiItemTypes.Add(new UiItemType("- " + Resources.SearchAll2HWeapon, i =>  i.Is2HWeapon));
			Item.Get2HWeaponTypes().ForEach(type => uiItemTypes.Add(uiItemFromRealType(type)));

			uiItemTypes.Add(new UiItemType(Resources.SearchAllArmors, i => i.IsArmor));
			Item.GetArmorTypes().ForEach(type => uiItemTypes.Add(uiItemFromRealType(type)));

			uiItemTypes.Add(new UiItemType(Resources.SearchAllJewelry, i => i.IsAmulet || i.IsRing));
			Item.GetJewelryTypes().ForEach(type => uiItemTypes.Add(uiItemFromRealType(type)));

			uiItemTypes.Add(new UiItemType(Resources.ItemStyleArtifact, i => i.IsArtifact));
			
			uiItemTypes.Add(new UiItemType(Resources.SearchMisc, i => i.IsFormulae || i.IsParchment || i.IsQuestItem || i.IsRelicComplete || i.IsScroll || i.IsPotion));
			uiItemTypes.Add(new UiItemType("- " + Resources.ItemStyleFormulae, i => i.IsFormulae));
			uiItemTypes.Add(new UiItemType("- " + Resources.ItemStyleParchment, i => i.IsParchment));
			uiItemTypes.Add(new UiItemType("- " + Resources.ItemStyleQuest, i => i.IsQuestItem));
			uiItemTypes.Add(new UiItemType("- " + Resources.ItemStyleRelic, i => i.IsRelicComplete));
			uiItemTypes.Add(new UiItemType("- " + Resources.ItemStyleScroll, i => i.IsScroll));
			uiItemTypes.Add(new UiItemType("- " + Resources.ItemStylePotion, i => i.IsPotion));

			itemTypeBindingSource.DataSource = uiItemTypes;
			itemTypeComboBox.DataSource = itemTypeBindingSource;
			itemTypeComboBox.DisplayMember = "HumanName";

			itemTypeComboBox.SelectedItem = uiItemTypes[0];
		}

		private IItemPredicate GetRequiredLevelPredicate(List<IItemPredicate> predicates)
		{
			int minLevel, maxLevel;

			if (!Int32.TryParse(minLevelTextBox.Text, out minLevel))
			{
				minLevel = 0;
			}
			if (!Int32.TryParse(maxLevelTextBox.Text, out maxLevel))
			{
				maxLevel = 85;
			}

			return new ItemLevelRangePredicate(minLevel, maxLevel);
		}

		private static IItemPredicate GetPredicateFrom(string term, Func<string, IItemPredicate> newPredicate)
		{
			var predicates = term.Split('|')
				.Select(it => it.Trim())
				.Where(it => it.Count() > 0)
				.Select(it => newPredicate(it));

			switch (predicates.Count())
			{
				case 0:
					return new ItemTruePredicate();
				case 1:
					return predicates.First();
				default:
					return new ItemOrPredicate(predicates);
			}
		}

		/// <summary>
		/// Handler for clicking the search button on the form.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">EventArgs data</param>
		private void FindButtonClicked(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// Handler for clicking the cancel button on the form.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">EventArgs data</param>
		private void CancelButtonClicked(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Handler for showing the search dialog.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">EventArgs data</param>
		private void SearchDialogShown(object sender, EventArgs e)
		{
			this.searchTextBox.Focus();
		}

		private void levelTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
			{
				e.Handled = true;
			}
		}

		#endregion

		#region Private types

		#region Item predicates

		private class ItemTruePredicate : IItemPredicate
		{
			public bool Apply(Item item)
			{
				return true;
			}

			public override string ToString()
			{
				return "true";
			}
		}

		private class ItemFalsePredicate : IItemPredicate
		{
			public bool Apply(Item item)
			{
				return false;
			}

			public override string ToString()
			{
				return "false";
			}
		}

		private class ItemAndPredicate : IItemPredicate
		{
			private readonly List<IItemPredicate> predicates;

			public ItemAndPredicate(params IItemPredicate[] predicates)
			{
				this.predicates = predicates.ToList();
			}

			public ItemAndPredicate(IEnumerable<IItemPredicate> predicates)
			{
				this.predicates = predicates.ToList();
			}

			public bool Apply(Item item)
			{
				return predicates.TrueForAll(predicate => predicate.Apply(item));
			}

			public override string ToString()
			{
				return "(" + String.Join(" && ", predicates.ConvertAll(p => p.ToString()).ToArray()) + ")";
			}
		}

		private class ItemOrPredicate : IItemPredicate
		{
			private readonly List<IItemPredicate> predicates;

			public ItemOrPredicate(params IItemPredicate[] predicates)
			{
				this.predicates = predicates.ToList();
			}

			public ItemOrPredicate(IEnumerable<IItemPredicate> predicates)
			{
				this.predicates = predicates.ToList();
			}

			public bool Apply(Item item)
			{
				return predicates.Exists(predicate => predicate.Apply(item));
			}

			public override string ToString()
			{
				return "(" + String.Join(" || ", predicates.ConvertAll(p => p.ToString()).ToArray()) + ")";
			}
		}

		private class ItemNamePredicate : IItemPredicate
		{
			private readonly string name;

			public ItemNamePredicate(string type)
			{
				this.name = type;
			}

			public bool Apply(Item item)
			{
				return item.ToString().ToUpperInvariant().Contains(name.ToUpperInvariant());
			}

			public override string ToString()
			{
				return $"Name({name})";
			}
		}

		private class ItemTypePredicate : IItemPredicate
		{
			private readonly string type;

			public ItemTypePredicate(string type)
			{
				this.type = type;
			}

			public bool Apply(Item item)
			{
				return item.ItemClass.ToUpperInvariant().Contains(type.ToUpperInvariant());
			}

			public override string ToString()
			{
				return $"Type({type})";
			}
		}

		private class ItemTypeProxyPredicate : IItemPredicate
		{
			private readonly Func<Item, bool> p;
			public ItemTypeProxyPredicate(Func<Item, bool> realPredicate) { p = realPredicate; }
			public bool Apply(Item item) => p(item);
		}

		private class ItemQualityPredicate : IItemPredicate
		{
			private readonly string quality;

			public ItemQualityPredicate(string quality)
			{
				this.quality = quality;
			}

			public bool Apply(Item item)
			{
				return MainForm.GetItemStyleString(item.ItemStyle).ToUpperInvariant().Contains(quality.ToUpperInvariant());
			}

			public override string ToString()
			{
				return $"Quality({quality})";
			}
		}

		private class ItemAttributePredicate : IItemPredicate
		{
			private readonly string attribute;

			public ItemAttributePredicate(string attribute)
			{
				this.attribute = attribute;
			}

			public bool Apply(Item item)
			{
				return item.GetAttributes(true).ToUpperInvariant().Contains(attribute.ToUpperInvariant());
			}

			public override string ToString()
			{
				return $"Attribute({attribute})";
			}
		}

		private class ItemLevelRangePredicate : IItemPredicate
		{
			private readonly int minLevel, maxLevel;

			public ItemLevelRangePredicate(int minLevel, int maxLevel)
			{
				this.minLevel = minLevel;
				this.maxLevel = maxLevel;
			}

			public bool Apply(Item item)
			{
				return minLevel <= item.RequiredLevel && item.RequiredLevel <= maxLevel;
			}

			public override string ToString()
			{
				return $"[{minLevel}-{maxLevel}]";
			}
		}

		#endregion

		#endregion
	}

	public class UiItemType
	{
		public UiItemType(string name, Func<Item, bool> pred) { HumanName = name; predicate = pred; }

		public string HumanName { get; set; }
		public Func<Item, bool> predicate;
	}
}