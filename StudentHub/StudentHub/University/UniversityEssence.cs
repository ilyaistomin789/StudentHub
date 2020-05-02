using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.University
{
    public class UniversityEssence
    {
        private static UniversityEssence instance;
        public string[] faculties = new[] { "undefined", "ХТиТ", "ЛХФ", "ИЭФ", "ТТЛП", "ТОВ", "ИТ", "ИДиП" };
        public string[] specializations = new[] { "undefined", "ИСиТ", "ПОИТ", "ПОИБМС", "ДЭиВИ", "ИД", "ПОиСОИ", "КиПИИКМ", "МиАХПИПСМ", "МиАХПиПСМ", "ИЛХ", "ЭиУНП", "БУаИА" };
        public int[] courses = new int[5];
        public int[] groups = new int[10];

        public string[] subjects = new[]
            {"СУБД", "ООТПиСП", "СТПвI", "КГиГ", "БД", "ИНФ", "ОАиП", "ПЗ", "ПСП", "МСОИ", "ПИС", "ПМАПЛ"};

        public int[] notes = new int[10];
        public int[] countOfGaps = new int[30];

        private UniversityEssence()
        {
            for (int i = 0; i < 5; i++)
            {
                courses[i] = i + 1;
            }

            for (int i = 0; i < 10; i++)
            {
                groups[i] = i + 1;
            }

            for (int i = 0; i < 10; i++)
            {
                notes[i] = i + 1;
            }

            for (int i = 0; i < 30; i++)
            {
                countOfGaps[i] = i + 1;
            }
        }

        public static UniversityEssence GetInstance()
        {
            return instance ?? (instance = new UniversityEssence());
        }

    }
}
