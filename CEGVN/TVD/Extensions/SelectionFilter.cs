using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace CEGVN.TVD.Extensions
{
	public class SelectionFilter : ISelectionFilter
	{
		public Type FilteredType
		{
			get
			{
				return this.filteredType;
			}
			set
			{
				this.filteredType = value;
			}
		}

		public Category FilteredCategory
		{
			get
			{
				return this.filteredCategory;
			}
			set
			{
				this.filteredCategory = value;
			}
		}

		public SelectionFilter(Type type)
		{
			this.filteredType = type;
			this.filteredCategory = null;
		}

		public SelectionFilter(Category category)
		{
			this.filteredType = null;
			this.filteredCategory = category;
		}

		public SelectionFilter(Type type, Category category)
		{
			this.filteredType = type;
			this.filteredCategory = category;
		}

		public bool AllowElement(Element elem)
		{
			bool flag = this.filteredType == null && this.FilteredCategory == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.filteredType == null;
				if (flag2)
				{
					result = (elem.Category.Name == this.filteredCategory.Name);
				}
				else
				{
					result = (this.filteredCategory == null && elem.GetType() == this.filteredType);
				}
			}
			return result;
		}

		public bool AllowReference(Reference refer, XYZ pos)
		{
			return true;
		}

		private Type filteredType;

		private Category filteredCategory;
	}
	public class DimensionSelectionFilter : ISelectionFilter
	{
		public bool AllowElement(Element element)
		{
			return element is Dimension;
		}

		public bool AllowReference(Reference refer, XYZ point)
		{
			return false;
		}
	}
}
