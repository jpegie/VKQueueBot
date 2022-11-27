using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_QueueBot.Enums;

public enum MessageValidationStatus
{
    ValidCommand= 0,
    InvalidCommand = 1, 
    NotCommand = 2
}