using System;
using System.Windows.Controls;
using Tetris.Entities.Shapes;
using Tetris.Shapes;

namespace Tetris.Entities
{
    class ShapesFactory
    {
        private static Random _randomFactor;

        static ShapesFactory()
        {
            _randomFactor = new Random();
        }

        public static Shape CreateShape(Canvas canvas, Type type = null)
        {
            string[] shapeTypes = Enum.GetNames(typeof(ShapeType));
            string shapeTypeName = shapeTypes[_randomFactor.Next(shapeTypes.Length)];

            if (type != null) shapeTypeName = type.Name;

            return GetShapeByTypeName(shapeTypeName, canvas);
        }

        private static Shape GetShapeByTypeName(string shapeTypeName, Canvas canvas)
        {
            Shape shape = default(Shape);

            switch (shapeTypeName)
            {
                case nameof(ShapeType.Stick):
                    shape = new Stick(canvas);
                    break;
                case nameof(ShapeType.Square):
                    shape = new Square(canvas);
                    break;
                case nameof(ShapeType.ZShape):
                    shape = new ZShape(canvas);
                    break;
                case nameof(ShapeType.ZShapeMirror):
                    shape = new ZShapeMirror(canvas);
                    break;
                case nameof(ShapeType.LShape):
                    shape = new LShape(canvas);
                    break;
                case nameof(ShapeType.LShapeMirror):
                    shape = new LShapeMirror(canvas);
                    break;
                case nameof(ShapeType.TShape):
                    shape = new TShape(canvas);
                    break;
                case nameof(ShapeType.TShapeMirror):
                    shape = new TShapeMirror(canvas);
                    break;
            }

            return shape;
        }
    }
}