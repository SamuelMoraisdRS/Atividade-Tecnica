using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esig.DAL
{
    public class DAO
    {
        // Tamanho das paginas em requisicoes para o banco de dados deve ser maior que o tamanho de pagina no GridView, para diminuir
        // o numero de requisicoes
        protected static readonly Int32 TAMANHO_PAGINA_DB = 50;

        protected static readonly string STRING_CONEXAO = "USER ID=SYS;PASSWORD=samuel;DATA SOURCE=192.168.1.15:1521/xepdb1;TNS_ADMIN=\"C:\\Users\\Samuel Morais\\Oracle\\network\\admin\";DBA PRIVILEGE=SYSDBA";
    }
}