using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpeg_command_builder
{
  internal class ObservableQueue<T> : Queue<T>
  {
    public event Action<object,QueueEventArgs<T>> Enqueued;
    public event Action<object,QueueEventArgs<T>> Dequeued;
    protected virtual void OnEnqueue(QueueEventArgs<T> e)
    {
      Enqueued?.Invoke(this,e);
    }

    protected virtual void OnDequeue(QueueEventArgs<T> e)
    {
      Dequeued?.Invoke(this,e);
    }

    public new void Enqueue(T item)
    {
      base.Enqueue(item);
      OnEnqueue(new QueueEventArgs<T>("enqueued",item));
    }

    public new T Dequeue()
    {
      T item = base.Dequeue();
      OnDequeue(new QueueEventArgs<T>("dequeued", item));
      return item;
    }
  }

  internal class QueueEventArgs<T> : EventArgs
  {
    private string EventKind;
    public T data { get; private set; }
    public QueueEventArgs(string kind,T item)
    {
      data = item;
      EventKind = kind;
    }

    public override string ToString()
    {
      return EventKind;
    }
  }
}
