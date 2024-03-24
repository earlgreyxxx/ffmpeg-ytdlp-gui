using System;
using System.Collections.Generic;
using System.Linq;

namespace ffmpeg_command_builder
{
  internal class ListItem<T>(T value, string label = null,object data = null)
  {
    public static List<ListItem<T>> GetList(IEnumerable<KeyValuePair<string,T>> enumerable)
    {
      return enumerable.Select(pair => new ListItem<T>(pair.Value,pair.Key)).ToList();
    }

    public static List<ListItem<T>> GetList(T[] values,string[] labels = null)
    {
      int len = values.Length;
      if(values == null || len <= 0 || len != labels.Length)
        throw new Exception("values and lables are empty or there length not same");

      return (labels == null)
        ? values.Select(v => new ListItem<T>(v)).ToList()
        : Enumerable.Range(0,len).Select(i => new ListItem<T>(values[i], labels[i])).ToList();
    }

    public string Label { get; set; } = label ?? value.ToString();
    public T Value { get; set; } = value;
    public object Data { get; set; } = data;
  }
}
