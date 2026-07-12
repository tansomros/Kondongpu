using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Visions.Commands.Create
{
    public class CreateVisionCommand : IRequest<int>
    {

        public int CheckupId { get; set; }     
        public int CheckupItemId { get; set; }
        public string? VA_Right_Result { get; set; }
        public string? VA_Right_Value { get; set; }
        public string? VA_Left_Result { get; set; }
        public string? VA_Left_Value { get; set; }
        public string? PH_Right_Result { get; set; }
        public string? PH_Right_Value { get; set; }
        public string? PH_Left_Result { get; set; }
        public string? PH_Left_Value { get; set; }
#pragma warning disable CS8618
        public string VisitNumber { get; set; }
        public string VisionRightResult { get; set; }
        public string VisionLeftResult { get; set; }
        //public string ResultNote { get; set; }
#pragma warning restore CS8618
        public string? ColorBlind { get; set; }
        public string? PressureRight { get; set; }
        public string? PressureLeft { get; set; }
        public string? Vision3D { get; set; }
        public string? Squint { get; set; }
        public string? VisualField { get; set; }
        public string? RetinaRight { get; set; }
        public string? RetinaLeft { get; set; }    
        public bool IsActive { get; set; }
    }

    public class CreateVisionCommmandHandler : IRequestHandler<CreateVisionCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateVisionCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateVisionCommand request, CancellationToken cancellationToken)
        {
            var vision = new Vision(request.CheckupId,request.VisitNumber,request.CheckupItemId,request.VisionRightResult,request.VisionLeftResult,string.Empty)
            {
                //VA_Right_Result = request.VA_Right_Result,
                VA_Right_Value = request.VA_Right_Value,
                //VA_Left_Result = request.VA_Left_Result,
                VA_Left_Value = request.VA_Left_Value,

                //PH_Right_Result = request.PH_Right_Result,
                PH_Right_Value = request.PH_Right_Value,
                //PH_Left_Result = request.PH_Left_Result,
                PH_Left_Value = request.PH_Left_Value,

                ColorBlind = request.ColorBlind, 
                PressureRight = request.PressureRight,
                PressureLeft = request.PressureLeft,
                Vision3D = request.Vision3D,
                Squint = request.Squint,
                VisualField = request.VisualField,
                RetinaRight = request.RetinaRight,
                RetinaLeft = request.RetinaLeft,
                IsActive = request.IsActive,
                CreatedOn = DateTime.Now,
            };


            await _checkupDatabaseContext.Visions.AddAsync(vision, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return vision.Id;
        }
    }
}
