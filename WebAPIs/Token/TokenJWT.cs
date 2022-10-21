using System.IdentityModel.Tokens.Jwt;

namespace WebAPIs.Token
{
    public class TokenJWT
    {
        private JwtSecurityToken token;


        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }


        //Propriedade que defini a data de criação do token
        public DateTime ValidTo => token.ValidTo;


        //Propriedade que defini o tempo de expiração do token
        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
