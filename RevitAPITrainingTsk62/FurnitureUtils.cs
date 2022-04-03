using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAPITrainingTsk62
{
    public class FurnitureUtils
    {
        public static List<FamilySymbol> GetFurniture(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //List<FamilyInstance> furniture = new FilteredElementCollector(doc)
            //    .OfCategory(BuiltInCategory.OST_Furniture)
            //    .WhereElementIsNotElementType()
            //    .Cast<FamilyInstance>()
            //    .ToList();
            List<FamilySymbol> furniture = new FilteredElementCollector(doc)
              .OfCategory(BuiltInCategory.OST_Furniture)
              .WhereElementIsElementType()
              .Cast<FamilySymbol>()
              .ToList();

            return furniture;
        }
    }
}
