using System.Collections.Generic;

namespace ServiceLocator.Entities
{
    public static class ServicesCategory
    {
        public static List<string> ServicesCategoryList = new List<string>
        {
            IT,
            Household,
            Beauty,
            Business,
            Art,
            Courier,
            Hour,
            Nurses,
            Training,
            Transport,
            Security,
            Power,
            Holidays,
            Advertising,
            Repair,
            Construction,
            Garden,
            Transport,
            Cleaning,
            Installation,
            Animal,
            Photo,
            Other
        };

        public static string IT => "IT, интернет, телеком";
        public static string Household => "Бытовые услуги";
        public static string Business => "Деловые услуги";
        public static string Art => "Искусство";
        public static string Beauty => "Красота, здоровье";
        public static string Courier => "Курьерские поручения";
        public static string Hour => "Мастер на час";
        public static string Nurses => "Няни, сиделки";
        public static string Training => "Обучение, курсы";
        public static string Security => "Охрана, безопасность";
        public static string Power => "Питание";
        public static string Holidays => "Праздники, мероприятия";
        public static string Advertising => "Реклама, полиграфия";
        public static string Repair => "Ремонт и обслуживание техники";
        public static string Construction => "Ремонт, строительство";
        public static string Garden => "Сад, благоустройство";
        public static string Transport => "Транспорт, перевозки";
        public static string Cleaning => "Уборка";
        public static string Installation => "Установка техники";
        public static string Animal => "Уход за животными";
        public static string Photo => "Фото- и видеосъёмка";
        public static string Other => "Другое";
    }
    public enum ScheduleType
    {
        Free,
        Busy
    }
}