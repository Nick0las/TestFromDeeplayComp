/*                                      МОДЕЛЬ
 * управляющий персонал (ФИО, адрес, дата рождения, телефон, должность, отдел, тип контролера)
 */

using System;

namespace TestFromDeeplayComp.Models
{
    internal class ManagerPersonal_Info
    {
        public int IdManager { get; set; }
        public int IdProfileManager { get; set; }
        public string LastNameManager { get; set; }
        public string FirstNameManager { get; set; }
        public string MiddleNameManager { get; set; }
        public string BirthdayManager { get; set; }
        public string AdressManager { get; set; }
        public string PhoneNumberManager { get; set; }
        public int Id_post { get; set; }
        public string PostManager { get; set; }
        public string DepartamentManager { get; set; }
        public string TypeControler { get; set; }
        public string ManagerDateAdmission { get; set; }
        public string ManagerEndDateContract { get; set; }
        public string DateFired { get; set; }
    }
}
