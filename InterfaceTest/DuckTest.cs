using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
{
    public interface IQuacking
    {
        public string Quack(int numberOfQuacks);

    }

    public class Duck: IQuacking
    {
        public string Quack(int numberOfQuacks)
        {
            string result = "";

            for (int i = 0; i < numberOfQuacks; i++)
            {
                result += "quack ";
            }

            return result;
        }
    }

    public class DogDuck: IQuacking
    {
        public string Quack(int numberOfQuacks)
        {
            string res = "";

            for(int i = 0;i < numberOfQuacks; i++)
            {
                res += "bark ";
            }

            return res;
        }
    }

    public class CompositionRoot
    {
        public IQuacking GetDuck()
        {
            return new DogDuck();
        }
    }
}
