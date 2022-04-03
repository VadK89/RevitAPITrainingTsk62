using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;

namespace RevitAPITrainingTsk62
{
    /*Создайте приложение, которое позволяет расставлять элементы только из категории «Мебель».
     * В приложении должен быть выпадающий список для выбора уровня расположения
     * элемента. Расположение элемента в модели указывается с помощью запроса точки
     * вставки.*/
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        //public List<FamilyInstance> FurnitureTypes { get; } = new List<FamilyInstance>();
        //public FamilyInstance SelectedFurnitureType { get; set; }
        public List<FamilySymbol> FurnitureTypes { get; } = new List<FamilySymbol>();
        public FamilySymbol SelectedFurnitureType { get; set; }

        public List<Level> Levels { get; } = new List<Level>();
        public Level SelectedLevel { get; set; }

        public DelegateCommand SaveCommand { get; }


        List<XYZ> Points { get; set; } = new List<XYZ>();

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            //FurnitureTypes = FamilySymbolUtils.GetFamilySymbols(commandData).Where(x => x.Category.Name == "Мебель").ToList();

            FurnitureTypes = FurnitureUtils.GetFurniture(commandData);
            Levels = LevelsUtils.GetLevels(commandData); ;

            SaveCommand = new DelegateCommand(OnSaveCommand);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            RaiseCloseRequest();
            if (SelectedFurnitureType == null || SelectedLevel == null)
                return;
            Points = SelectionUtils.GetPoints(_commandData, "Укажите точку вставки мебели", ObjectSnapTypes.Endpoints);
            
            Points[0] = new XYZ(Points[0].X, Points[0].Y, SelectedLevel.ProjectElevation);

            FamilyInstanceUtils.CreateFamilyInstance(_commandData, SelectedFurnitureType, Points[0], SelectedLevel);

            //RaiseCloseRequest();
        }



        //для закрытия окна
        public event EventHandler CloseRequest;
        //метод для закрытия окна
        private void RaiseCloseRequest()
        {//Для запуска методов привзязанных к запросу закрытия
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
