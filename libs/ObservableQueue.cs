using System;
using System.Collections.Generic;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ObservableQueue<T> : Queue<T>
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

  public class QueueEventArgs<T> : EventArgs
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
