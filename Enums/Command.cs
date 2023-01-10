using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_QueueBot.Enums;
public enum Command
{
    SetClosingQueueTime,
    Help,
    CreateQueue,
    Shuffle,
    EraseQueue,
    AddMember,
    RemoveMember,
    MoveToEnd,
    PrintQueue,
    RemoveQueue,
    GetClosingTime,
    NotDefined,
    InvalidCommand,
    QueueIsNotStarted
}
