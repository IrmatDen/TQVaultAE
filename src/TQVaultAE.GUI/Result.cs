//-----------------------------------------------------------------------
// <copyright file="Result.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.GUI
{
	using TQVaultData;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Linq;

	/// <summary>
	/// Class for an individual result in the results list.
	/// </summary>
	public class Result
	{
		[Browsable(false)]
		public SackType SackType => sackType;
		[Browsable(false)]
		public Color Color => color;
		[Browsable(false)]
		public int SackNumber => sackNumber;
		[Browsable(false)]
		public string Container => container;
		[Browsable(false)]
		public Item Item => item;

		public string ItemName => itemName;
		public string ItemStyle => itemStyle;
		public string ContainerName => containerName;
		public string ContainerType => containerType;

		public int RequiredLevel => requiredLevel;
		public int RequiredStrength => requiredStrength;
		public int RequiredDexterity => requiredDexterity;
		public int RequiredIntelligence => requiredIntelligence;

		private readonly string container;
		private readonly string containerName;
		private readonly int sackNumber;
		private readonly SackType sackType;
		private readonly Item item;

		private readonly string itemName;
		private readonly string itemStyle;
		private readonly string containerType;
		private readonly Color color;

		private readonly int requiredLevel;
		private readonly int requiredStrength;
		private readonly int requiredDexterity;
		private readonly int requiredIntelligence;

		public Result(string container, string containerName, int sackNumber, SackType sackType, Item item)
		{
			this.container = container ?? throw new ArgumentNullException(nameof(container));
			this.containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
			this.sackNumber = sackNumber;
			this.sackType = sackType;
			this.item = item ?? throw new ArgumentNullException(nameof(item));

			var itemStyle = item.ItemStyle;
			this.itemName = Item.ClipColorTag(item.ToString());
			this.itemStyle = MainForm.GetItemStyleString(itemStyle);
			this.containerType = ResultsDialog.GetContainerTypeString(sackType);
			this.color = Item.GetColor(itemStyle);

			var requirementVariables = item.GetRequirementVariables();
			var requirementVariablesList = requirementVariables.Values;
			this.requiredLevel = GetRequirement(requirementVariablesList, "levelRequirement");
			this.requiredStrength = GetRequirement(requirementVariablesList, "strengthRequirement");
			this.requiredDexterity = GetRequirement(requirementVariablesList, "dexterityRequirement");
			this.requiredIntelligence = GetRequirement(requirementVariablesList, "intelligenceRequirement");
		}

		private int GetRequirement(IList<Variable> variables, string key)
		{
			return variables
				.Where(v => string.Equals(v.Name, key, StringComparison.InvariantCultureIgnoreCase) && v.DataType == VariableDataType.Integer && v.NumberOfValues > 0)
				.Select(v => v.GetInt32(0))
				.DefaultIfEmpty(0)
				.Max();
		}
	}
}