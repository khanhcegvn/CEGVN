using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CEGVN.TVD.Controller
{
	public class RvtController : IController
	{
		public UIApplication UiApp { get; set; }
		public UIDocument UiDoc { get; set; }
		public Document Doc { get; set; }
		public View ActiveView { get; set; }
		public XYZ RightDirection { get; set; }
		public XYZ Updirection { get; set; }
		public Selection RSelection { get; set; }

		public List<Workset> Worksets { get; set; }
		public bool IsValid { get; set; }

		public RvtController(UIApplication uiapp)
		{
			this.UiApp = uiapp;
			this.UiDoc = this.UiApp.ActiveUIDocument;
			this.RSelection = this.UiApp.ActiveUIDocument.Selection;
			this.Doc = this.UiDoc.Document;
			this.ActiveView = this.Doc.ActiveView;
			this.IsValid = true;
			this.RightDirection = this.ActiveView.RightDirection;
			this.Updirection = this.ActiveView.UpDirection;
		}

		public RvtController(Document doc)
		{
			this.UiApp = new UIApplication(doc.Application);
			this.UiDoc = new UIDocument(doc);
			this.RSelection = this.UiDoc.Selection;
			this.Doc = doc;
			this.ActiveView = this.Doc.ActiveView;
			this.RightDirection = this.ActiveView.RightDirection;
			this.Updirection = this.ActiveView.UpDirection;
		}

		public virtual void LoadSettings()
		{
		}

		public virtual void SaveSettings()
		{
		}

		public virtual void Execute()
		{
		}

		public virtual void ReadData()
		{
		}

		public virtual void SaveData()
		{
		}

		public virtual void FilterData(string filter)
		{
		}

		public virtual void CheckedToSelected(int i)
		{
		}
	}
}
