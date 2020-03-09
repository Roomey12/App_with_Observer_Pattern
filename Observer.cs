using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lab3NET
{
    interface IObserver // наблюдатель
    {
        void Update(object ob);
    }

    interface IObservable // наблюдаемый
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }

    class Driver : IObservable //конкретная реализация интерфейса IObservable. Определяет коллекцию объектов наблюдателей.
    {
        public int CurrentSpeed { get; set; }
        public string Number { get;  }

        List<IObserver> observers;

        public Driver(string number, int speed)
        {
            observers = new List<IObserver>();
            Number = number;
            CurrentSpeed = speed;
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers.ToArray())
            {
                o.Update(this);
            }
        }

        public void Drive()
        {
            Console.WriteLine($"\nYou are driving a car with {Number} number at a speed of {CurrentSpeed} km/h.\n");
            NotifyObservers();
        }
    }

    class DAI : IObserver
    {
        public static List<DAI> dais = new List<DAI>();
        private int LimitSpeed = 60;
        public string Post { get; }
        public bool Catch { get; set; }

        IObservable driver;
        public DAI(string post, IObservable obs)
        {
            dais.Add(this);
            Post = post;
            driver = obs;
            driver.RegisterObserver(this);
        }

        public DAI(string post)
        {
            dais.Add(this);
            Post = post;
        }
        public void Update(object ob)
        {
            Driver driver = (Driver)ob;

            Random rnd = new Random();
            int x = rnd.Next(0, 3);
            if (driver.CurrentSpeed > LimitSpeed)
            {
                if(x == 0)
                {
                    Console.WriteLine($"Car with {driver.Number} number exceeded speed! {Post} catch him!");
                    Catch = true; 
                }
                else
                {
                    Console.WriteLine($"Car with {driver.Number} number exceeded speed! {Post} does not catch him! Another posts catch him!");
                    Catch = false;
                    driver.RemoveObserver(this);
                }
            }
        }

        public void StopObserve()
        {
            driver.RemoveObserver(this);
            driver = null;
        }

        public static void StartPatrol(Driver driver1)
        {
            for (int i = 0; i < dais.Count; i++)
            {
                driver1.Drive();
                if (i != dais.Count - 1 && dais[i].Catch == false)
                {
                    driver1.RegisterObserver(dais[i + 1]);
                    Console.WriteLine();
                }
                if (dais[i].Catch == true)
                {
                    break;
                }
            }
        }
    }

    class Camera : IObserver
    {
        public int LimitSpeed = 60;
        public string Name { get; set; }

        IObservable driver;
        public Camera(IObservable obs)
        {
            driver = obs;
            driver.RegisterObserver(this);
        }
        public void Update(object ob)
        {
            Driver driver = (Driver)ob;

            if (driver.CurrentSpeed > LimitSpeed)
            {
                Console.WriteLine($"Car with {driver.Number} number exceeded speed! Catch them!");
            }
        }
    }
}
