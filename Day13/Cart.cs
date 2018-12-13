using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class Cart
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Direction { get; set; }
        public CartStatus Status { get; set; }
        private int NumAtIntersection { get; set; }

        public const char DirectionUp    = '^';
        public const char DirectionDown  = 'v';
        public const char DirectionLeft  = '<';
        public const char DirectionRight = '>';

        public Cart(int x, int y, char direction)
        {
            PositionX = x;
            PositionY = y;
            Direction = direction;
            Status = CartStatus.Running;
        }

        public void Tick(char[,] map, List<Cart> carts)
        {
            if (Status == CartStatus.Crashed)
                return;

            var previousDirection = Direction;

            UpdatePosition();

            var amICrashed = CheckForCrashes(carts);
            if (amICrashed)
                return;

            UpdateDirection(map, previousDirection);
        }

        private void UpdateDirection(char[,] map, char previousDirection)
        {
            // otherwise check directions
            var mapElement = map[PositionX, PositionY];
            if (mapElement == '+')
            {
                // Intersection, turn now
                if (NumAtIntersection == 0)
                {
                    TurnLeft();
                }
                else if (NumAtIntersection == 2)
                {
                    TurnRight();
                }

                NumAtIntersection++;
                if (NumAtIntersection == 3)
                    NumAtIntersection = 0;
            }
            else if (mapElement == '/')
            {
                // Turn coming up, turn now
                switch (previousDirection)
                {
                    case DirectionLeft:
                    case DirectionRight:
                        TurnLeft();
                        break;
                    case DirectionUp:
                    case DirectionDown:
                        TurnRight();
                        break;
                }
            }
            else if (mapElement == '\\')
            {
                // Turn coming up, turn now
                switch (previousDirection)
                {
                    case DirectionLeft:
                    case DirectionRight:
                        TurnRight();
                        break;
                    case DirectionUp:
                    case DirectionDown:
                        TurnLeft();
                        break;
                }
            }
        }

        private bool CheckForCrashes(List<Cart> carts)
        {
            var crashingCarts = carts.Where(c => c != this && c.PositionX == PositionX && c.PositionY == PositionY).ToList();
            foreach (var cart in crashingCarts)
            {
                cart.Status = CartStatus.Crashed;
            }

            // if there are crashed carts on out position, we're gone as well
            if (crashingCarts.Count > 0)
            {
                Status = CartStatus.Crashed;
                return true;
            }

            return false;
        }

        private void UpdatePosition()
        {
            switch (Direction)
            {
                case DirectionUp:
                    PositionY -= 1;
                    break;
                case DirectionDown:
                    PositionY += 1;
                    break;
                case DirectionLeft:
                    PositionX -= 1;
                    break;
                case DirectionRight:
                    PositionX += 1;
                    break;
            }
        }

        private void TurnLeft()
        {
            switch (Direction)
            {
                case DirectionLeft:
                    Direction = DirectionDown;
                    break;
                case DirectionDown:
                    Direction = DirectionRight;
                    break;
                case DirectionRight:
                    Direction = DirectionUp;
                    break;
                case DirectionUp:
                    Direction = DirectionLeft;
                    break;
            }
        }

        private void TurnRight()
        {
            switch (Direction)
            {
                case DirectionLeft:
                    Direction = DirectionUp;
                    break;
                case DirectionUp:
                    Direction = DirectionRight;
                    break;
                case DirectionRight:
                    Direction = DirectionDown;
                    break;
                case DirectionDown:
                    Direction = DirectionLeft;
                    break;
            }
        }
    }
}