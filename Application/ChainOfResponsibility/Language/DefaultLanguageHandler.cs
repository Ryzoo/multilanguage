using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Database.Models;

namespace Application.ChainOfResponsibility.Language
{
    public class DefaultLanguageHandler : LanguageHandler
    {
        public T Handle<T>(ICollection<T> request, string langCode)
            where T : ILanguageContent
        {
            var element = request.FirstOrDefault(x => x.LangCode == langCode);

            if (element != null)
                return element;
            
            return base.Handle(request);
        }
    }
}

