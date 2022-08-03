/*                                  МОДЕЛЬ
*           рабочий персонал (ФИО,адрес, дата рождения, телефон, должность, id_руководителя,id_Профиля_Руководителя, ФИО_Руководителя, отдел)
*/

namespace TestFromDeeplayComp.Models
{
    internal class WorkPersonal_Info
    {
        public int IdProfileWorkPersonal { get; set; }
        public int IdWorkPersonal { get; set; }
        public int IdPostWorkPersonal { get; set; }
        public string LastNameWorkPersonal { get; set; }
        public string FirstNameWorkPersonal { get; set; }
        public string MiddleNameWorkPersonal { get; set; }
        public string BirthdayWorkPersonal { get; set; }
        public string AdressWorkPersonal { get; set; }
        public string PhoneNumberWorkPersonal { get; set; }
        public string PostWorkPersonal { get; set; }
        public int PersonalManagerIdProfile { get; set; }
        public int PersonalManagerIdManager { get; set; }
        public string PersonalManagerLastName { get; set; }
        public string PersonalManagerFirstName { get; set; }
        public string PersonalManagerMiddleName { get; set; }
        public string PersonalManagerDepartament { get; set; }
        public string WorkPersonalDateAdmission { get; set; }
        public string WorkPersonalDateFired { get; set; }
    }
}
