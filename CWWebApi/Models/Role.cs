using Core.Classes;
using Core.Models;
using CWWebApi.Models;

namespace CWWebApi.Security
{

    public class Role : IEntity
    //�� �� ����� ��� ���� ����� ������ ����� 
    //�� �����-�������� ������-�� ����� ������ � ������� ef
    {
        public ApiUser User { get; set; }
        public int Id { get; set; }

        public UserRoles UserRole
        {
            get; set;
        }
    }
}