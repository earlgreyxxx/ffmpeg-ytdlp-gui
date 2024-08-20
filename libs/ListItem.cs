using System;
using System.Collections.Generic;
using System.Linq;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ListItem<T>(T value, string? label = null, object? data = null) : ICloneable
  {
    public static List<ListItem<T>> GetList(IEnumerable<KeyValuePair<string, T>> enumerable)
    {
      return enumerable.Select(pair => new ListItem<T>(pair.Value, pair.Key)).ToList();
    }

    public static List<ListItem<T>> GetList(T[] values, string[]? labels = null)
    {
      int len = values.Length;
      if (values == null || len <= 0 || len != labels?.Length)
        throw new Exception("values and lables are empty or there length not same");

      return (labels == null)
        ? values.Select(v => new ListItem<T>(v)).ToList()
        : Enumerable.Range(0, len).Select(i => new ListItem<T>(values[i], labels[i])).ToList();
    }

    public object Clone()
    {
      return new ListItem<T>(this);
    }

    public string? Label { get; set; } = (label ?? value?.ToString() ?? throw new NullReferenceException("Both label and value were null"));
    public T Value { get; set; } = value;
    public object? Data { get; set; } = data;

    public ListItem(T value,object data) : this(value, null, data)
    {
    }

    public ListItem(ListItem<T> item) : this(item.Value, item.Label, item.Data)
    {
    }

    public override string ToString()
    {
      return Label!;
    }
  }
}
