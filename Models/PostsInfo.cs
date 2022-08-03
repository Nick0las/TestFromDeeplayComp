/********************************************************
 * информация о должностях (id, namePost, TypeControler)
 *                  запрос к БД через JOIN
 *******************************************************/

namespace TestFromDeeplayComp.Models
{
    internal class PostsInfo
    {
        public int IdPost { get; set; }
        public string NamePost { get; set; }
        public string TypeControler { get; set; }
    }
}
