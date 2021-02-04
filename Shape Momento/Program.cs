//Name: Carl Peralta
//18315304
//Date: 4th December 2020
//OS: Windows 10
//VSCode
//Momento Software Design Pattern


using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace assignment_18415304
{
    class Program
    {
        public class Rectangle
        {
            public int X { get; private set; }  //top left x-coordinate
            public int Y { get; private set; }  //top left y-coordinate
            public int H { get; private set; }  //height
            public int W { get; private set; }  //width

            public Rectangle(int x, int y, int h, int w) { X = x; Y = y; H = h; W = w; }

            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for rectangle
                string dispSVG = String.Format(@"   <rect x=""{0}"" y=""{1}"" height=""{2}"" width=""{3}"" stroke=""black"" stroke-width=""2"" fill=""yellow""/>", X, Y, H, W);
                return dispSVG;
            }
        }
        public class Circle
        {
            public int X { get; private set; }  //centre x-coordinate
            public int Y { get; private set; }  //centre y-coordinate
            public int R { get; private set; }  //radius

            public Circle(int x, int y, int r) { X = x; Y = y; R = r; }

            public Circle()
            {
            }

            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for circle
                string dispSVG = String.Format(@"   <circle cx=""{0}"" cy=""{1}"" r=""{2}"" stroke=""black"" stroke-width=""2"" fill=""red""/>", X, Y, R);
                return dispSVG;
            }
        }

        public class Ellipse
        {
            public int X { get; private set; }  //centre x-coordinate
            public int Y { get; private set; }  //centre y-coordinate
            public int RX { get; private set; } //x radius
            public int RY { get; private set; } //y radius

            public Ellipse(int x, int y, int rx, int ry) { X = x; Y = y; RX = rx; RY = ry; }
            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for ellipse
                string dispSVG = String.Format(@"   <ellipse cx=""{0}"" cy=""{1}"" rx=""{2}"" ry=""{3}"" stroke=""black"" stroke-width=""2"" fill=""red""/>", X, Y, RX, RY);
                return dispSVG;
            }
        }

        public class Line
        {
            public int X1 { get; private set; }  //first point x-coordinate
            public int Y1 { get; private set; }  //first point y-coordinate
            public int X2 { get; private set; }  //second point x-coordinate
            public int Y2 { get; private set; }  //second point y-coordinate

            public Line(int x1, int y1, int x2, int y2) { X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; }

            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for line
                string dispSVG = String.Format(@"   <line x1=""{0}"" y1=""{1}"" x2=""{2}"" y2=""{3}"" stroke=""black"" stroke-width=""2"" fill=""green""/>", X1, Y1, X2, Y2);
                return dispSVG;
            }
        }

        public class Polyline
        {
            public string Points { get; set; }
            public Polyline() { Points = "0,40 40,40 40,80 80,80 80,120 120,120 120,160"; } //default constructer
            //https://www.w3schools.com/graphics/svg_polyline.asp
            public Polyline(string points) { Points = points; }

            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for polyline
                string dispSVG = String.Format(@"   <polyline points=""{0}"" stroke=""black"" stroke-width=""2"" fill=""yellow""/>", Points);
                return dispSVG;
            }
        }

        public class Polygon
        {
            public string Points { get; set; }
            public Polygon() { Points = "20,20 40,25 60,40 80,120 120,140 200,180"; } //default constructer
            //https://www.w3schools.com/graphics/svg_polygon.asp
            public Polygon(string points) { Points = points; }

            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for polygon
                string dispSVG = String.Format(@"   <polygon points=""{0}"" stroke=""black"" stroke-width=""2"" fill=""green""/>", Points);
                return dispSVG;
            }
        }

        public class Path
        {
            public string Points { get; set; }
            public Path() { Points = "M150 150 L75 350 L225 350 Z"; } //default constructer
            //https://www.w3schools.com/graphics/svg_path.asp
            public Path(string points) { Points = points; }

            public override string ToString()
            {
                // convert the object to an SVG element descriptor string for path
                string dispSVG = String.Format(@"   <path d=""{0}"" stroke=""black"" stroke-width=""2"" fill=""blue""/>", Points);
                return dispSVG;
            }
        }




        class Originator
        {
            private ArrayList _shapeList = new ArrayList();
            //I use this to add shapes
            public void Add(string c)
            {
                ArrayList s = ShapeList;
                s.Add(c);
                this.ShapeList = s;
            }
            //I use this to undo shape adds
            public void RemoveAt()
            {
                ArrayList s = ShapeList;
                int len = s.Count - 1;
                s.RemoveAt(len);
                this.ShapeList = s;
            }
            public int Count()
            {
                ArrayList s = ShapeList;
                int len = s.Count - 2;
                return len;
            }
            public ArrayList ShapeList
            {
                get { return _shapeList; }
                set
                {
                    _shapeList = value;
                }
            }

            // Produces memento snapshot of state
            //undo
            public Memento SaveMemento()
            {
                Console.WriteLine("\nSaving state --\n");
                Console.WriteLine("Added!");
                ArrayList _copyList = (ArrayList)_shapeList.Clone();
                return new Memento(_copyList);
            }
            // Restores state from memento snapshot
            //redo 
            public void RestoreMemento(Memento memento)
            {
                Console.WriteLine("\nRestoring state --\n");
                this.ShapeList = memento.ShapeList;
            }

            public List<string> ListShapes()
            {
                List<string> r = new List<string>();
                foreach(var item in ShapeList){
                    r.Add(item.ToString());
                }
                return r;
            }

            public void getShapes(){
                foreach(var item in ShapeList){
                    Console.WriteLine(item);
                }
            }
        }

        //snapshot of orginator state
        class Memento
        {
            private ArrayList _copyList = new ArrayList();
            // Stores a clone of originators shapeList
            public Memento(ArrayList shapeList)
            {
                this._copyList = (ArrayList)shapeList.Clone();
            }
            //constructor to get and set snapshot of ShapeList
            public ArrayList ShapeList
            {
                get
                {
                    return _copyList;
                }
                set
                {
                    _copyList = value;
                }
            }
        }

        //This is the Caretaker class, this controls memento 
        //it holds list of each saved memento state
        class Caretaker
        {
            private Memento _memento;

            private List<Memento> memoList = new List<Memento>();

            public Memento Memento
            {
                set { _memento = value; }
                get { return _memento; }
            }
            //adds memento to memoList
            public void AddMemento(Memento m)
            {
                memoList.Add(m);
            }
            //resets memento list
            public void ClearMemento()
            {
                memoList.Clear();
            }
            //gets and removes and last memento in memoList
            public Memento GetMemento()
            {
                var temp = memoList[memoList.Count - 1];
                memoList.RemoveAt(memoList.Count - 1);
                return temp;
            }
        }
    





        static void Main()
        {
            // this will creates the originator canvas and the other lists neccessary to run the application
            Originator canvas = new Originator();
            // This will add shapes to the array list
            ArrayList mem = new ArrayList(); 
            Memento m = new Memento(mem);
            List<Memento> holder = new List<Memento>();

            //this random number generator
            Random rnd = new Random(); 

            //This will prompt for user to use application adn help the user
            Console.WriteLine("Welcome to the canvas!");
            Console.WriteLine("Please enter a command: (H for Help)" + Environment.NewLine);

            while (true)
            {   
                //gets the size of the current canvas for referencing
                int x = canvas.Count();
                string sc = Console.ReadLine();
                //closing sequence for the application if the user chooses too.
                if (sc.Equals("q") || sc.Equals("Q"))
                {
                    Console.WriteLine("Closing application....");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Succusfully closed application");
                    break;
                }
                else
                {
                    switch (sc)
                    {
                        //If user chooses this command it will show this drop down menu
                         case "H":
                            Console.WriteLine("Commands: ");
                            Console.WriteLine("  A <shape>   Add <shape> to canvas");
                            Console.WriteLine("  U           Undo last operation");
                            Console.WriteLine("  R           Redo last operation");
                            Console.WriteLine("  D           Display Canvas");
                            Console.WriteLine("  S           Save canvas to SVG file");
                            Console.WriteLine("  C           Clear canvas");
                            Console.WriteLine("  Q           Close application");
                            break;
                            //all these cases will add whatever shape the user chooses onto the canvas
                        case "A Rectangle":
                            Rectangle rect = new Rectangle(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
                            string Rect = rect.ToString();
                            canvas.Add(Rect);
                            m = canvas.SaveMemento();
                            break;
                        case "A Circle":
                            Circle circle = new Circle();
                            string Circle = circle.ToString();
                            canvas.Add(Circle);
                            m = canvas.SaveMemento();
                            break;
                        case "A Ellipse":
                            Ellipse ellipse = new Ellipse(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
                            string Ellipse = ellipse.ToString();
                            canvas.Add(Ellipse);
                            m = canvas.SaveMemento();
                            break;
                        case "A Line":
                            Line line = new Line(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
                            string Line = line.ToString();
                            canvas.Add(Line);
                            m = canvas.SaveMemento();
                            break;
                        case "A Polyline":
                            Polyline polyline = new Polyline();
                            string Polyline = polyline.ToString();
                            canvas.Add(Polyline);
                            m = canvas.SaveMemento();
                            break;
                        case "A Polygon":
                            Polygon polygon = new Polygon();
                            string Polygon = polygon.ToString();
                            canvas.Add(Polygon);
                            m = canvas.SaveMemento();
                            break;
                        case "A Path":
                            Path path = new Path();
                            string Path = path.ToString();
                            canvas.Add(Path);
                            m = canvas.SaveMemento();
                            break;
                        case "U":
                        // if statement to check if the canvas is not empty if it is not then it will do the undo operation
                        if(x == 0)
                        {
                            Console.WriteLine("Canvas is empty! No undo is possible.");
                        }
                        else if(x > 0){
                            m = canvas.SaveMemento();
                            canvas.RemoveAt();
                        }
                            break;
                        case "R":
                        //this will restore the canvas if an undo is completed
                            canvas.SaveMemento();
                            canvas.RestoreMemento(m);
                            break;
                        case "C":
                        //if statment to check if the canvas is empty. If it is it will not perform the clear operation as there is nothing to clear.
                        if(x == 0)
                        {
                            Console.WriteLine("Canvas is empty! Nothing to clear.");
                        }
                        //if it isnt empty then obviously clear the canvas
                        else if(x > 0)
                        {
                            canvas = new Originator();
                            Console.WriteLine("Successfully cleared canvas.");
                        }
                            break;
                            //This will also check if the canvas is empty and if it is it will inform the user there is nothing to display 
                        case "D":
                        if(x == 0)
                        {
                            Console.WriteLine("Canvas is empty! Nothing to display.");
                        }
                        else if(x > 0){
                            canvas.getShapes();
                        }
                            break;
                        case "S":
                            String PathToSVG = @"./Shapes.svg";

                            List<string> Shapes = canvas.ListShapes();

                            //https://docs.microsoft.com/en-us/dotnet/api/system.io.file.create?view=net-5.0
                            using (FileStream fs = File.Create(PathToSVG))
                            {
                                // insert open svg tag into first line
                                byte[] info = new UTF8Encoding(true).GetBytes(@"<svg height=""2000"" width=""2000"" xmlns=""http://www.w3.org/2000/svg"">" + Environment.NewLine);
                                // Add some information to the file.
                                fs.Write(info, 0, info.Length);
                            }
                            //add close tag to the end
                            using (StreamWriter sw = File.AppendText(PathToSVG))
                            {
                                foreach(string item in Shapes){
                                    sw.WriteLine(item);
                                }
                                sw.WriteLine("</svg>");
                            }
                            Console.WriteLine("Successfully saved your canvas!");
                            break;
                        default:
                        //informs the user his/her input is invalid
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
            }
        }
    }
}