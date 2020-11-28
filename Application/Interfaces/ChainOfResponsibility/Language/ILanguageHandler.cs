using System.Collections.Generic;
using Application.Models;

namespace Application.Interfaces.ChainOfResponsibility.Language
{
    public interface ILanguageHandler
    {
        ILanguageHandler SetNext(ILanguageHandler handler);
        T Handle<T>(ICollection<T> request)
        where T : ILanguageContent;
    }
}