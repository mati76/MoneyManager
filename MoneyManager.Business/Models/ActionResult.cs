using System.Collections.Generic;

namespace MoneyManager.Business.Models
{
    public class ActionResult
    {
        public IEnumerable<string> Errors { get; set; }
        public bool Succeeded { get; set; }
    }
}
