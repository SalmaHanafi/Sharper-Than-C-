using System;
using System.Collections;
using System.Collections.Generic;

namespace CovarianceContravariance
{
    /*
     * In C#, Covariance and Contravariance play a role in 
     * Delegate types, Array types and Generic Type arguments
     */
    public class Animal
    {
        public void Eat() => Console.WriteLine("Eat");
    }
    public class Bird : Animal
    {
        public void Fly() => Console.WriteLine("Fly");
    }

    //used to show covariance 
    delegate Animal ReturnAnimalDelegate();
    delegate Bird ReturnBirdDelegate();

    //used to show contravariance
    delegate void TakeAnimalDelegate(Animal a);
    delegate void TakeBirdDelegate(Bird b);


    //generics
    interface IProcess<out T>
    {
        T Process();
    }

    public class AnimalProcess<T> : IProcess<T>
    {
        public T Process()
        {
            throw new NotImplementedException();
        }
    }
    
    interface IZoo<in T>
    {
        void Add(T t)
    }

    public class Zoo<T> : IZoo<T>
    {
        public void Add(T t)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        public static Bird GetBird() => new Bird();
        public static Animal GetAnimal() => new Animal();

        public static void Eat(Animal a) => a.Eat();
        public static void Fly(Bird b) => b.Fly();
        static void Main(string[] args)
        {
            /*
             * this works because ReturnAnimalDelegate can point to any 
             * method that returns an Animal 
             */
            ReturnAnimalDelegate d = GetAnimal;

            /*
             * this shows convariance as we can assign a bird to an animal delegate
             * since bird extends a Animal (derives from Animal). preserves assignment 
             * compatibility
             */
            ReturnAnimalDelegate d2 = GetBird;


            /*
             * this works normally because TakeBirdDelegate points to any 
             * method that takes a Bird
             */
            TakeBirdDelegate t = Fly;


            /*
             * this shows contravariance as a bird is an Animal, the method
             * expects an Animal and we passed a bird which is an Animal. Reverses
             * assignment compatibility
             */
            TakeBirdDelegate t2 = Eat;


            //generics
            IProcess<Animal> animalProcess = new AnimalProcess<Animal>();
            IProcess<Bird> birdProcess = new AnimalProcess<Bird>();

            //covariance in generic types
            animalProcess = birdProcess;

            IEnumerable<Animal> animalList = new List<Bird>();

            //contravariance in generic types

            IZoo<Animal> animalZoo = new Zoo<Animal>();
            IZoo<Bird> birdZoo = new Zoo<Bird>();
            birdZoo = animalZoo;

            IZoo<Bird> ContraBirdZoo = new Zoo<Animal>();
        }
    }
}
