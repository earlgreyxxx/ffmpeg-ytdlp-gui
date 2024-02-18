using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpeg_command_builder
{
  internal class MyProcess : Process
  {
    public string InputFileName { get; set; }
  }
}
