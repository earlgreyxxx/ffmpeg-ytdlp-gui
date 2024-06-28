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
    public event Action<object,QueueEventArgs> Enqueued;
    public event Action<object,QueueEventArgs> Dequeued;
    protected virtual void OnEnqueue(QueueEventArgs e)
    {
      Enqueued?.Invoke(this,e);
    }

    protected virtual void OnDequeue(QueueEventArgs e)
    {
      Dequeued?.Invoke(this,e);
    }

    public new void Enqueue(T item)
    {
      base.Enqueue(item);
      OnEnqueue(new QueueEventArgs("enqueued"));
    }

    public new T Dequeue()
    {
      T item = base.Dequeue();
      OnDequeue(new QueueEventArgs("dequeued"));
      return item;
    }
  }

  internal class QueueEventArgs : EventArgs
  {
    private string data;
    public QueueEventArgs(string occure)
    {
      data = occure;
    }

    public override string ToString()
    {
      return data;
    }
  }
}
