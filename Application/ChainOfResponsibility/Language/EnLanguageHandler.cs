using System.Collections.Generic;
using System.Linq;

namespace Application.ChainOfResponsibility.Language
{
    public class EnLanguageHandler : LanguageHandler
    {
        public override T Handle<T>(ICollection<T> request)
        {
            var element = request.FirstOrDefault(x => x.LangCode == "en");

            if (element != null)
                return element;
            
            return base.Handle(request);
        }
    }
}

