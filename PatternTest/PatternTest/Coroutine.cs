using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternTest
{
    class Coroutine
    {
        private IEnumerable[] _routines;
        private AutoResetEvent[] _eventHandlers;
        private ManualResetEvent[] _exitHandlers;

        public Coroutine(IEnumerable[] routines)
        {
            _routines = routines;
            _eventHandlers = new AutoResetEvent[_routines.Length];
            _exitHandlers = new ManualResetEvent[_routines.Length];

            for (int i = 0; i < routines.Length; i++)
            {
                _eventHandlers[i] = new AutoResetEvent(false);

                // Thread 기반이므로 모든 작업을 완료 시킬때까지 대기를 하기 위해서
                // ManualResetEvent를 사용한다.
                _exitHandlers[i] = new ManualResetEvent(false);
            }
        }

        public void Do()
        {
            for (int i = 0; i < _routines.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DoRoutine), i);
            }
            // 현재 모든 스레드가 블럭상태이므로 최초 수행할 루틴(메소드)를 깨운다.
            _eventHandlers[0].Set();
            WaitHandle.WaitAll(_exitHandlers); // 모든 작업이 완료될 때까지 대기
        }

        private void DoRoutine(object id)
        {
            int currentId = (int)id;

            // 현재 스레드 블럭
            _eventHandlers[currentId].WaitOne();

            int callerId = currentId;

            foreach (int calleeId in _routines[currentId])
            {
                if (calleeId == -1)
                {
                    // 루틴을 빠져나가기 전에 다른 호출자 루틴을 깨운다.
                    _eventHandlers[callerId].Set();
                    // 종료 시그널 발생
                    _exitHandlers[currentId].Set();
                }
                else
                {
                    // yield 위치에 다다르면 실행될 루틴으로써 다른 루틴을 깨우고 자신은 블럭
                    _eventHandlers[calleeId].Set();
                    callerId = calleeId;
                    _eventHandlers[currentId].WaitOne();
                }
            }
        }
    }
}
