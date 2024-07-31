using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  public class Codec : ICloneable
  {
    public string Name { get; set; }
    public string GpuSuffix { get; set; }
    public string FullName { get; set; }

    public override string ToString()
    {
      return FullName;
    }

    public object Clone()
    {
      return new Codec(this);
    }

    public Codec(string name, string gpuSuffix = "",string fullname = "")
    {
      Name = name;
      GpuSuffix = gpuSuffix;
      if(string.IsNullOrEmpty(fullname))
        FullName = string.IsNullOrEmpty(GpuSuffix) ? Name : $"{Name}_{GpuSuffix}";
      else
        FullName = fullname;
    }

    public Codec(Codec other) : this(other.Name, other.GpuSuffix, other.FullName)
    {
    }
  }
}
