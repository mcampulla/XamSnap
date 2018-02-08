using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamSnap.Models
{
    public interface INotificationService
    {
        void Start(string userName);
        void SetToken(object deviceToken);
    }
}
