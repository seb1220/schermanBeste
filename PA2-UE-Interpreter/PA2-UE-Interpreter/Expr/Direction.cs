namespace PA2_UE_Interpreter.Expr
{
    internal class Direction
    {
        AbcRobotCore.RobotField.Direction direction;

        public Direction(Token direction)
        {
            if (Token.TokenType.DIRECTION == direction.Type)
            {
                if (direction.Value == "UP")
                {
                    this.direction = AbcRobotCore.RobotField.Direction.Up;
                }
                else if (direction.Value == "RIGHT")
                {
                    this.direction = AbcRobotCore.RobotField.Direction.Right;
                }
                else if (direction.Value == "LEFT")
                {
                    this.direction = AbcRobotCore.RobotField.Direction.Left;
                }
                else if (direction.Value == "DOWN")
                {
                    this.direction = AbcRobotCore.RobotField.Direction.Down;
                }
                else
                {
                    Interpreter.Errors.Add("Unexpected direction: " + direction.Value);
                }
            }
            else
            {
                Interpreter.Errors.Add("Unexpected token type: " + direction.Type);
            }
        }

        public AbcRobotCore.RobotField.Direction getRobotDirection()
        {
            return this.direction;
        }
    }
}
