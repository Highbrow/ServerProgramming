using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternTest
{
    class Program
    {
        static string _volatileString;

        static void Main(string[] args)
        {
            _volatileString = "";
            IEnumerable[] subroutines = new IEnumerable[3] {
                Callee00(), Callee01(), Callee02()
            };

            // Coroutine을 수행하는 클래스의 인스턴스 생성
            Coroutine co = new Coroutine(subroutines);
            co.Do();

            // 최종 결과값 출력
            Console.WriteLine("The volatile string value has {0}.", _volatileString);
            Console.ReadKey();
        }

        static IEnumerable Callee00()
        {
            _volatileString += "1";
            _volatileString += "1";
            yield return 1; // 호출 Callee01

            _volatileString += "1";
            yield return 2; // 호출 Callee02

            _volatileString += "1";
            yield return -1; // 종료
        }

        static IEnumerable Callee01()
        {
            _volatileString += "2";
            yield return 2; // 호출 Callee02

            _volatileString += "2";
            _volatileString += "2";
            yield return 0; // 호출 Callee00

            yield return -1; // 종료
        }

        static IEnumerable Callee02()
        {
            _volatileString += "3";
            yield return 1; // 호출 Callee01

            _volatileString += "3";
            _volatileString += "3";
            yield return -1; // 종료
        }
    }
}


