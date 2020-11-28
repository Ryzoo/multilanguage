using System.Collections.Generic;
using System.Linq;

namespace Application.ChainOfResponsibility.Language
{
    public class PlLanguageHandler : LanguageHandler
    {
        public override T Handle<T>(ICollection<T> request)
        {
            var element = request.FirstOrDefault(x => x.LangCode == "pl");

            if (element != null)
                return element;
            
            return base.Handle(request);
        }
    }
}

