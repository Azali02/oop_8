using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace oop4_1.Observers
{
    internal class Observable
    {
        public List<Observer> _observers = new List<Observer>();
        public string _name = "";

        virtual public void AddObserver(Observer o)
        {
            _observers.Add(o);
        }
        virtual public void RemoveObserver()
        {
            _observers.Clear();
        }

        virtual public void NotifyEveryone()
        {
            foreach (Observer o in _observers)
            {
                o.OnSubjectChanged(this);
            }
        }

        virtual public void NotifyEveryoneSelect()
        {
            foreach (Observer o in _observers)
            {
                o.OnSubjectSelect(this);
            }
        }

        virtual public void NotifyEveryoneMove(int x, int y, int widht, int height)
        {
            foreach (Observer o in _observers)
            {
                o.OnSubjectMove(x, y, widht, height);
            }
        }
        virtual public void NotifyEveryoneSizeUp(int a, int widht, int height)
        {
            foreach (Observer o in _observers)
            {
                o.OnSubjectSizeUp(a, widht, height);
            }
        }
        virtual public String Who()
        {
            return _name;
        }

    }
}
