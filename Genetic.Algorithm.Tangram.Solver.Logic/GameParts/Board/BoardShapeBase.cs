namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board
{
    public class BoardShapeBase
    {
        public IList<BoardFieldDefinition> BoardFieldsDefinition { private set; get; }
        public int WidthUnit { private set; get; }
        public int HeightUnit { private set; get; }
        public int ScaleFactor { private set; get; } = 1;

        public BoardShapeBase(

            IList<BoardFieldDefinition> boardFieldsDefinition,
            int widthUnit,
            int heightUnit,
            int scaleFactor)
        {
            BoardFieldsDefinition = boardFieldsDefinition;
            WidthUnit = widthUnit;
            HeightUnit = heightUnit;
            ScaleFactor = scaleFactor;
        }

        public BoardShapeBase Clone()
        {
            return new BoardShapeBase(
                this.BoardFieldsDefinition,
                this.WidthUnit,
                this.HeightUnit,
                this.ScaleFactor);
        }
    }
}
