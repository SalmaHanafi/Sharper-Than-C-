using System;

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
        }
    }
}
