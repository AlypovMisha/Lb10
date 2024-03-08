using LibraryForLabs;
using System;

namespace Lb10
{
    public class Program
    {
        static void Main(string[] args)
        {

            //Часть 1
            Console.WriteLine("\t\t|||1 часть|||");
            //Заполнение массива
            Random rand = new Random();
            Cars[] cars = new Cars[20];
            for (int i = 0; i < cars.Length; i++)
            {
                switch (rand.Next(1, 5))
                {
                    case 1:
                        cars[i] = new PassengerCar();
                        cars[i].RandomInit();
                        break;
                    case 2:
                        cars[i] = new TruckCar();
                        cars[i].RandomInit();
                        break;
                    case 3:
                        cars[i] = new OffRoadCar();
                        cars[i].RandomInit();
                        break;
                    case 4:
                        cars[i] = new Cars();
                        cars[i].RandomInit();
                        break;
                }
            }

            //Вывод элементов массива виртуальным методом          
            VirtualPrint(cars);

            //Вывод элементов массива не виртуальным методом
            NotVirtualPrint(cars);


            //Часть 2
            Console.WriteLine("\t\t|||2 часть|||");

            //Находим самый дорогой внедорожник
            MostExpensiveOffRoadCar(cars);

            //Для нахождения средней скорости легковых автомобилей
            FindAverageSpeed(cars);

            //Суммарная стоимость всех автомобилей
            SumCost(cars);

            //Цвета внедорожников с включенным полным приводом
            ColorsAWD(cars);


            //Часть 3
            Console.WriteLine("\t\t|||3 часть|||");

            DialClock dialClock = new DialClock();
            IInit[] initArray = new IInit[20];
            MakeIInitArray(initArray, dialClock, cars);
            PrintIInitArray(initArray);

            Cars cr = cars[5];
            //Сортировка по цене
            Console.WriteLine("\t\t|||Не отсортированный|||");
            Print(cars);
            Console.WriteLine("\t\t|||Отсортированный|||");
            Array.Sort(cars);
            Print(cars);
            //Бинпоиск для элемента в массиве который был отсортирован по цене
            Console.WriteLine("Индекс элемента в массиве который был отсортирован по цене: " + Array.BinarySearch(cars, cr));
            Console.WriteLine();

            //Сортировка по году
            Console.WriteLine("\t\t|||Не отсортированный|||");
            Print(cars);
            Console.WriteLine("\t\t|||Отсортированный|||");
            Array.Sort(cars, new CarComparer("Год"));
            Print(cars);
            //Бинпоиск для элемента в массиве который был отсортирован по году
            Console.WriteLine("Индекс элемента в массиве который был отсортирован по году: " + Array.BinarySearch(cars, cr, new CarComparer("Год")));
            Console.WriteLine();

            OffRoadCar offRoadCar2 = new OffRoadCar();
            offRoadCar2.RandomInit();
            offRoadCar2.Show();
            Console.WriteLine();
            OffRoadCar copy = (OffRoadCar)offRoadCar2.ShallowCopy();
            copy.Show();
            Console.WriteLine();
            OffRoadCar clone = (OffRoadCar)offRoadCar2.Clone();
            clone.Show();
            Console.WriteLine();
            Console.WriteLine("\t|||После изменения|||");
            copy.Cost = 300000;
            copy.id.number = 100;
            clone.Cost = 600000;
            clone.id.number = 200;

            offRoadCar2.Show();
            Console.WriteLine();
            copy.Show();
            Console.WriteLine();
            clone.Show();

            Console.ReadKey();
        }


        //Виртуальный метод вывода
        public static void VirtualPrint(Cars[] cars)
        {
            Console.WriteLine("\t|||Виртуальный метод|||");
            Console.WriteLine();
            foreach (Cars car in cars)
            {
                car.Show();
                Console.WriteLine();
            }
        }

        //Не виртуальный метод вывода
        public static void NotVirtualPrint(Cars[] cars)
        {
            Console.WriteLine("\t|||Не виртуальный метод|||");
            Console.WriteLine();
            foreach (Cars car in cars)
            {
                car.ShowNotVirtual();
                Console.WriteLine();
            }
        }

        //Самый дорогой внедорожник
        public static void MostExpensiveOffRoadCar(Cars[] cars)
        {
            int maxCost = 0;
            OffRoadCar mostExpensive = new OffRoadCar();
            foreach (var item in cars)
            {
                if (typeof(OffRoadCar) == item.GetType() && maxCost < item.Cost)
                {
                    maxCost = item.Cost;
                    mostExpensive = (OffRoadCar)item;
                }
            }
            Console.WriteLine($"Самый дорогой внедорожник с ценой {maxCost}");
            mostExpensive.Show();
            Console.WriteLine();
        }

        //Нахождение средней скорости легковых автомобилей
        public static void FindAverageSpeed(Cars[] cars)
        {
            int count = 0;
            double totalSpeed = 0;
            foreach (var item in cars)
            {
                if (item is PassengerCar car)
                {
                    totalSpeed += car.TopSpeed;
                    count++;
                }
            }
            Console.WriteLine("Средняя скорость легковых автомобилей: " + (count > 0 ? totalSpeed / count : 0));
            Console.WriteLine();
        }

        //Суммарная стоимость всех автомобилей
        public static void SumCost(Cars[] cars)
        {
            double totalCost = 0;
            foreach (var car in cars)
            {
                totalCost += car.Cost;
            }
            Console.WriteLine("Суммарная стоимость всех автомобилей: " + totalCost);
            Console.WriteLine();
        }

        //Нахождение цветов полноприводных внедорожников
        public static void ColorsAWD(Cars[] cars)
        {
            int countAWD = 0;
            // Подсчитываем количество внедорожников с включенным полным приводом
            foreach (var item in cars)
            {
                OffRoadCar car = item as OffRoadCar;
                if (car != null && car.Awd)
                    countAWD++;
            }
            string[] colors = new string[countAWD];

            // Заполняем массив цветов
            int index = 0;
            foreach (var item in cars)
            {
                OffRoadCar car = item as OffRoadCar;
                if (car != null && car.Awd)
                {
                    colors[index] = car.Color;
                    index++;
                }
            }
            Console.WriteLine("Цвета внедорожников с включенным полным приводом:");
            foreach (var clr in colors)
                Console.WriteLine(clr);

            Console.WriteLine();
        }

        //Создание массива с помощью интерфейса
        public static IInit[] MakeIInitArray(IInit[] initArray, DialClock dialClock, Cars[] cars)
        {
            for (int a = 0; a < initArray.Length - 10; a++)
            {
                dialClock.RandomInit();
                initArray[a] = dialClock;
            }
            for (int i = 10; i < initArray.Length; i++)
            {
                initArray[i] = cars[i];
            }
            return initArray;
        }

        //Печать массива с помощью интерфейса
        public static void PrintIInitArray(IInit[] initArray)
        {
            int countDialClock = 0;
            int countPassengerCar = 0;
            int countOffRoadCar = 0;
            int countTruckCar = 0;
            if (initArray.Length > 0 && initArray != null)
            {
                foreach (var item in initArray)
                {
                    if (item.GetType() == typeof(PassengerCar))
                        countPassengerCar++;
                    if (item.GetType() == typeof(TruckCar))
                        countTruckCar++;
                    if (item.GetType() == typeof(OffRoadCar))
                        countOffRoadCar++;
                    if (item.GetType() == typeof(DialClock))
                        countDialClock++;
                    item.Show();
                }
                Console.WriteLine($"\nКоличество внедорожников {countOffRoadCar}");
                Console.WriteLine($"Количество грузовиков {countTruckCar}");
                Console.WriteLine($"Количество легковых автомобилей {countPassengerCar}");
                Console.WriteLine($"Количество часов {countDialClock}");
            }

        }


        public static void Print(Cars[] cars)
        {
            foreach (Cars car in cars)
            {
                car.Show();
                Console.WriteLine();
            }
        }
    }

}
