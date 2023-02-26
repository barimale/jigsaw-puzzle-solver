using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_simple_board
    {
        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_no_unused_fields()
        {
            // given
            
            // when
            
            // then
        }

        // TODO: field type X allowed or O allowed, and block types X or O.
        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_two_types_of_fields()
        {
            // given
            var scaleFactor = 1;
            var fields = new List<BoardFieldDefinition>()
            {
                new BoardFieldDefinition(0,0, true, scaleFactor),
                new BoardFieldDefinition(0,1, true, scaleFactor),
                new BoardFieldDefinition(1,0, true, scaleFactor),
                new BoardFieldDefinition(1,1, true, scaleFactor)
            };
            BoardShapeBase boardDefinition = new BoardShapeBase(fields, 2, 2, 1);
            //board
            //blocks
            var blocks = [];
            //solver

            // when
            //execute solver

            // then
            // all suitable solutions are displayed in console
        }
    }
}