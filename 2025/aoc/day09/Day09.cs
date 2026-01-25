using System.Globalization;
using aoc._shared;

namespace aoc.day09;

public class Day09
{
    private const string Folder = "day09";
    private const string Impala = "impala";

    public static string Execute()
    {
        List<string> lines = [];

        // lines.AddRange("7,1", "11,1", "11,7", "9,7", "9,5", "2,5", "2,3", "7,3");
        Folder.Read(line => lines.Add(line));

        var points = TransformInput(lines);

        Console.WriteLine(Impala);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(points),
            "2" => B(),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa)
        };
    }

    private static List<Point> TransformInput(List<string> lines)
    {
        List<Point> points = [];
        foreach (var line in lines)
        {
            var numbers = line.Split(",").Select(double.Parse).ToArray();
            points.Add(new Point(numbers[0], numbers[1]));
        }

        return points;
    }

    private static string A(List<Point> points)
    {
        var rectangles = TransformPoints(points);
        var largestRectangle = rectangles.OrderByDescending(r => r.Size).First();
        return largestRectangle.Size.ToString(CultureInfo.InvariantCulture);
    }

    private static List<Rectangle> TransformPoints(List<Point> points)
    {
        List<Rectangle> rectangles = [];
        for (var i = 0; i < points.Count; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                var rectangle = new Rectangle(points[i], points[j]);
                rectangles.Add(rectangle);
            }
        }

        return rectangles;
    }

    private static string B()
    {
        return "bar";
    }

    private readonly record struct Point(double X, double Y);

    private readonly struct Rectangle(Point a, Point b)
    {
        public double Size { get; } = (Math.Abs(b.X - a.X) + 1) * (Math.Abs(b.Y - a.Y) + 1);

        public Point A { get; } = a;
        public Point B { get; } = b;
    }
}