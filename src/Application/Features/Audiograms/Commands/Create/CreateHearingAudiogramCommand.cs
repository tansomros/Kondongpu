using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kondongpu.Application.Features.Audiograms.Commands.Create;
public class CreateHearingAudiogramCommand
{
    public required int Hertz { get; set; }
    public required double LeftHz { get; set; }
    public required double RightHz { get; set; }
}
