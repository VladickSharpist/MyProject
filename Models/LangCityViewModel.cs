using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.EfStuff.Models;

namespace WebApplication.Models
{
    public class LangCityViewModel
    {
        public List<SelectListItem> LangOptions { get; set; }
        public List<SelectListItem> CityOptions { get; set; }

        public LangCityViewModel()
        {
            var optionsLang = Enum.GetValues(typeof(Lang));
            LangOptions = new List<SelectListItem>();
            foreach (var option in optionsLang)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = option.ToString(),
                    Value = option.ToString()
                };
                LangOptions.Add(selectListItem);
            }

            var optionsCity = Enum.GetValues(typeof(City));
            CityOptions = new List<SelectListItem>();
            foreach (var option in optionsCity)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = option.ToString(),
                    Value = option.ToString()
                };
                CityOptions.Add(selectListItem);
            }
        }
    }
}