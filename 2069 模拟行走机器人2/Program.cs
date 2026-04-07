string[] cmds =
    ["Robot", "getPos", "getDir", "step", "step", "step", "step", "step", "step", "step", "step", "getDir", "getPos"];
int[][] parameters = [[97, 98], [], [], [66392], [83376], [71796], [57514], [36284], [69866], [31652], [32038], [], []];

Solution sol = new(cmds, parameters);
sol.Run();


public class Solution
{
    private Robot _robot;
    private string[] _commands;
    private int[][] _parameters;
    public Solution(string[] commands, int[][] parameters)
    {
        _commands = commands;
        _parameters = parameters;
    }

    public void Run()
    {
        if(_commands.Length != _parameters.Length)
            throw new ArgumentException("Number of commands and parameters must match.");
        for (int i = 0; i < _commands.Length; i++)
        {
            var cmd = _commands[i];
            var param = _parameters[i];
            if (cmd == nameof(Robot))
            {
                _robot = new Robot(param[0], param[1]);
            }else if (cmd == "step")
            {
                _robot.Step(param[0]);
            }else if (cmd == "getDir")
            {
                Console.Write($"{_robot.GetDir()} ");
            }else if (cmd == "getPos")
            {
                Console.Write($"[{_robot.GetPos()[0]},{_robot.GetPos()[1]}] ");
            }
            else
            {
                throw new NotImplementedException($"Command {cmd} not implemented.");
            }
        }
    }
}

public class Robot {
    private enum Direction
    {
        East,
        South,
        West,
        North
    }

    private Direction _dir;
    private readonly int _width;
    private readonly int _height;

    private int _curX;
    private int _curY;
    private int _rec;
    public Robot(int width, int height)
    {
        _width = width - 1;
        _height = height - 1;
        _rec = (_width + _height) * 2;
        _dir = Direction.East;
        _curX = 0;
        _curY = 0;
    }
    
    public void Step(int num) {
        num %= _rec;
        if (num == 0)
        {
            if(_curX == 0 && _curY == 0)
                _dir = Direction.South;
            else if(_curX == _width && _curY == 0)
                _dir = Direction.East;
            else if(_curX == _width && _curY == _height)
                _dir = Direction.North;
            else if(_curX == 0 && _curY == _height)
                _dir = Direction.West;
            return;
        }
        while (num > 0)
        {
            var maxStep = MaxStep();
            var step = Math.Min(maxStep, num);
            Move(step);
            num -= step;
            if (num > 0)
            {
                _dir = Next(_dir);
            }
        }
    }
    
    public int[] GetPos() {
        return [_curX, _curY];
    }
    
    public string GetDir() {
        return _dir switch
        {
            Direction.East => "East",
            Direction.South => "South",
            Direction.West => "West",
            Direction.North => "North",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Direction Next(Direction dir)
    {
        return dir switch
        {
            Direction.East => Direction.North,
            Direction.South => Direction.East,
            Direction.West => Direction.South,
            Direction.North => Direction.West,
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
    }
    
    private void Move(int step)
    {
        switch (_dir)
        {
            case Direction.East:
                _curX += step;
                break;
            case Direction.South:
                _curY -= step;
                break;
            case Direction.West:
                _curX -= step;
                break;
            case Direction.North:
                _curY += step;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private int MaxStep()
    {
        return _dir switch
        {
            Direction.East => _width - _curX,
            Direction.South => _curY,
            Direction.West => _curX,
            Direction.North => _height - _curY,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
