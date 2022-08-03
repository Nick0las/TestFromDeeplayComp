/*****************************************************
 * управляющие подразделениями (ФИО, должность, отдел)
 *                    через JOIN к БД
 ****************************************************/

namespace TestFromDeeplayComp.Models
{
    internal class DepartamentManager
    {
        public int IdProfile { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NamePost { get; set; }
        public string NameDepartament { get; set; }
    }
}
