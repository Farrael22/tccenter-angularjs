using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace balcao.offline.api.Helpers.Business
{
    /// <summary>
    /// Classe de Fonema adapatada do objeto que era utilizado pelo balcão.
    /// </summary>
    public class FonemaHelper
    {
        /// <summary>
        /// Fonetiza o string recebido como parametro e devolve um outro string (que e o primeiro fonetizado)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getFonema(string str)
        {
            str = str.ToUpper(); //todas as letras maiusculas
            str = removePreposicao(str); //remove as preposições
            str = removeAcentuacao(str); //remove os acentos
            str = removeEstranhos(str); //remove caracteres diferentes de
            str = aplicarFonema(str); //fonetiza o texto
            return str;
        }

        /// <summary>
        /// Função que faz efetivamente a substituição de letras, fonetizando o texto matrizes de caracteres utilizadas para manipular o texto
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string aplicarFonema(string str)
        {
            //matrizes de caracteres utilizadas para manipular o texto
            //char[] foncmp = new char[256];
            char[] foncmp = new char[2000];

            //char[] fonwrk = new char[256];
            char[] fonwrk = new char[2000];

            //char[] fonaux = new char[256];
            char[] fonaux = new char[2000];

            //char[] fonfon = new char[256];
            char[] fonfon = new char[2000];

            int i, j, x, k, //contadores

            desloc, //posicao atual no vetor

            endfon, //indica se eh ultimo fonema

            copfon, //indica se o fonema deve ser copiado

            copmud, newmud; //indica se o fonema eh mudo

            //Vetor utilizado para armazenar o texto:

            //cada palavra do texto e armazenada em uma posicao do vetor

            List<string> component = new List<string>();

            i = 0;

            j = 0;//zera os contadores

            str = removeRepetidos(str);

            //todos os caracteres duplicados sao eliminados

            //exemplo: SS -> S, RR -> R

            component = convertestringEmVetor(str);

            //o texto eh armazenado no vetor:

            //cada palavra ocupa uma posicao do vetor

            for (desloc = 0; desloc < component.Count; desloc++)
            {

                //percorre o vetor, palavra a palavra

                //for (i = 0; i < 256; i++) {
                for (i = 0; i < 2000; i++)
                {

                    fonwrk[i] = ' ';

                    fonfon[i] = ' ';//branqueia as matrizes

                }//for

                foncmp = component[desloc].ToCharArray();

                fonaux = foncmp;

                //matrizes recebem os caracteres da palavra atual

                j = 0;

                if (component[desloc].Length == 1)
                {

                    fonwrk[0] = foncmp[0];

                    //se a palavra possuir apenas 1 caracter, nao altera a palavra

                    if (foncmp[0] == '_')
                    {

                        fonwrk[0] = ' ';

                        //se o caracter for "_", troca por espaco em branco

                    }//if

                    else

                        if ((foncmp[0] == 'E') ||

                                (foncmp[0] == '&') ||

                                (foncmp[0] == 'I'))
                    {

                        fonwrk[0] = 'i';

                        //se o caracter for "E", "&" ou "I", troca por "i"

                    }//if

                }//if

                else
                {

                    for (i = 0; i < component[desloc].Length; i++)

                        //percorre a palavra corrente, caracter a caracter

                        if (foncmp[i] == '_')

                            fonfon[i] = 'Y';     // _ -> Y

                        else

                            if (foncmp[i] == '&')

                            fonfon[i] = 'i';     //& -> i

                        else

                                if ((foncmp[i] == 'E') ||

                                        (foncmp[i] == 'Y') ||

                                        (foncmp[i] == 'I'))

                            fonfon[i] = 'i';     // E, Y, I -> i

                        else

                                    if ((foncmp[i] == 'O') ||

                                            (foncmp[i] == 'U'))

                            fonfon[i] = 'o';     // O, U -> u

                        else

                                        if (foncmp[i] == 'A')

                            fonfon[i] = 'a';      // A -> a

                        else

                                            if (foncmp[i] == 'S')

                            fonfon[i] = 's';     // S -> s

                        else

                            fonfon[i] = foncmp[i];

                    //caracter nao eh modificado
                    endfon = 0;

                    fonaux = fonfon;

                    //palavras formadas por apenas 3 consoantes 

                    //sao dispensadas do processo de fonetizacao

                    if (fonaux[3] == ' ')

                        if ((fonaux[0] == 'a') ||

                                (fonaux[0] == 'i') ||

                                (fonaux[0] == 'o'))

                            endfon = 0;

                        else

                            if ((fonaux[1] == 'a') ||

                                    (fonaux[1] == 'i') ||

                                    (fonaux[1] == 'o'))

                            endfon = 0;

                        else

                                if ((fonaux[2] == 'a') ||

                                        (fonaux[2] == 'i') ||

                                        (fonaux[2] == 'o'))

                            endfon = 0;

                        else
                        {

                            endfon = 1;

                            fonwrk[0] = fonaux[0];

                            fonwrk[1] = fonaux[1];

                            fonwrk[2] = fonaux[2];

                        }//else

                    if (endfon != 1)
                    { //se a palavra nao for formada por apenas 3 consoantes...

                        for (i = 0; i < component[desloc].Length; i++)
                        {

                            //percorre a palavra corrente, letra a letra

                            copfon = 0;

                            copmud = 0;

                            newmud = 0;

                            //zera variaveis de controle

                            switch (fonaux[i])
                            {

                                case 'a':  //se o caracter for a

                                    //se a palavra termina com As, AZ, AM, ou AN,

                                    //elimina a consoante do final da palavra

                                    if ((fonaux[i + 1] == 's') ||

                                            (fonaux[i + 1] == 'Z') ||

                                            (fonaux[i + 1] == 'M') ||

                                            (fonaux[i + 1] == 'N'))

                                        if (fonaux[i + 2] != ' ')

                                            copfon = 1;

                                        else
                                        {

                                            fonwrk[j] = 'a';

                                            fonwrk[j + 1] = ' ';

                                            j++;

                                            i++;

                                        }//else

                                    else copfon = 1;

                                    break;

                                case 'B':  //se o caracter for B

                                    // B nao eh modificado

                                    copmud = 1;

                                    break;

                                case 'C':  //se o caracter for C

                                    x = 0;

                                    // C + consoante != de C,H,R,K -> Ki
                                    if ((fonaux[i + 1] != 'a') &&
                                            (fonaux[i + 1] != 'e') &&
                                            (fonaux[i + 1] != 'i') &&
                                            (fonaux[i + 1] != 'o') &&
                                            (fonaux[i + 1] != 'u') &&
                                            (fonaux[i + 1] != 'C') &&
                                            (fonaux[i + 1] != 'H') &&
                                            (fonaux[i + 1] != 'R') &&
                                            (fonaux[i + 1] != 'K'))
                                    {

                                        fonwrk[j] = 'K';

                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        break;
                                    }

                                    if (fonaux[i + 1] == 'i')
                                    {

                                        //ci vira si

                                        fonwrk[j] = 's';

                                        j++;

                                        break;

                                    }//if

                                    //coes final vira cao

                                    if ((fonaux[i + 1] == 'o') &&

                                            (fonaux[i + 2] == 'i') &&

                                            (fonaux[i + 3] == 's') &&

                                            (fonaux[i + 4] == ' '))
                                    {

                                        fonwrk[j] = 'K';

                                        fonwrk[j + 1] = 'a';

                                        fonwrk[j + 2] = 'o';

                                        i = i + 4;

                                        break;

                                    }//if

                                    //   c vira k
                                    if (fonaux[i + 1] != 'H')
                                    {

                                        fonwrk[j] = 'K';

                                        newmud = 1;

                                        //   ck vira k

                                        if (fonaux[i + 1] == 'K')
                                        {

                                            i++;

                                            break;

                                        }//if

                                        else break;

                                    }//if

                                    //ch vira k para chi final, chi vogal, chini final e

                                    //chiti final

                                    //chi final ou chi vogal

                                    if (fonaux[i + 1] == 'H')

                                        if (fonaux[i + 2] == 'i')

                                            if ((fonaux[i + 3] == 'a') ||

                                                    (fonaux[i + 3] == 'i') ||

                                                    (fonaux[i + 3] == 'o'))

                                                x = 1;

                                            // chini final

                                            else

                                                if (fonaux[i + 3] == 'N')

                                                if (fonaux[i + 4] == 'i')

                                                    if (fonaux[i + 5] == ' ')

                                                        x = 1;

                                                    else;

                                                else;

                                            else

                                                    // chiti final

                                                    if (fonaux[i + 3] == 'T')

                                                if (fonaux[i + 4] == 'i')

                                                    if (fonaux[i + 5] == ' ')

                                                        x = 1;

                                    if (x == 1)
                                    {

                                        fonwrk[j] = 'K';

                                        j++;

                                        i++;

                                        break;

                                    }//if

                                    //chi, nao chi final, chi vogal, chini final ou chiti final

                                    //ch nao seguido de i

                                    //se anterior nao e s, ch = x

                                    if (j > 0)

                                        //sch: fonema recua uma posicao

                                        if (fonwrk[j - 1] == 's')
                                        {

                                            j--;

                                        }//if

                                    fonwrk[j] = 'X';

                                    newmud = 1;

                                    i++;

                                    break;

                                case 'D':  //se o caracter for D

                                    x = 0;

                                    //procura por dor

                                    if (fonaux[i + 1] != 'o')
                                    {

                                        copmud = 1;

                                        break;

                                    }//if

                                    else

                                        if (fonaux[i + 2] == 'R')

                                        if (i != 0)

                                            x = 1; // dor nao inicial

                                        else copfon = 1; // dor inicial

                                    else copfon = 1;  // nao e dor

                                    if (x == 1)

                                        if (fonaux[i + 3] == 'i')

                                            if (fonaux[i + 4] == 's') // dores

                                                if (fonaux[i + 5] != ' ')

                                                    x = 0;  // nao e dores

                                                else;

                                            else x = 0;

                                        else

                                            if (fonaux[i + 3] == 'a')

                                            if (fonaux[i + 4] != ' ')

                                                if (fonaux[i + 4] != 's')

                                                    x = 0;

                                                else

                                                    if (fonaux[i + 5] != ' ')

                                                    x = 0;

                                                else;

                                            else;

                                        else x = 0;

                                    else x = 0;

                                    if (x == 1)
                                    {

                                        fonwrk[j] = 'D';

                                        fonwrk[j + 1] = 'o';

                                        fonwrk[j + 2] = 'R';

                                        i = i + 5;

                                    }//if

                                    else copfon = 1;

                                    break;

                                case 'F':  //se o caracter for F

                                    //F nao eh modificado

                                    copmud = 1;

                                    break;

                                case 'G':  //se o caracter for G

                                    //gui -> gi

                                    if (fonaux[i + 1] == 'o')

                                        if (fonaux[i + 2] == 'i')
                                        {

                                            fonwrk[j] = 'G';

                                            fonwrk[j + 1] = 'i';

                                            j += 2;

                                            i += 2;

                                        }//if

                                        //diferente de gui copia como consoante muda

                                        else copmud = 1;

                                    else

                                        //gl

                                        if (fonaux[i + 1] == 'L')

                                        if (fonaux[i + 2] == 'i')

                                            //gli + vogal -> li + vogal

                                            if ((fonaux[i + 3] == 'a') ||

                                                    (fonaux[i + 3] == 'i') ||

                                                    (fonaux[i + 3] == 'o'))
                                            {

                                                fonwrk[j] = fonaux[i + 1];

                                                fonwrk[j + 1] = fonaux[i + 2];

                                                j += 2;

                                                i += 2;

                                            }//if

                                            else

                                                //glin -> lin

                                                if (fonaux[i + 3] == 'N')
                                            {

                                                fonwrk[j] = fonaux[i + 1];

                                                fonwrk[j + 1] = fonaux[i + 2];

                                                j += 2;

                                                i += 2;

                                            }/*if*/

                                            else copmud = 1;

                                        else copmud = 1;

                                    else

                                            //gn + vogal -> ni + vogal

                                            if (fonaux[i + 1] == 'N')

                                        if ((fonaux[i + 2] != 'a') &&

                                                (fonaux[i + 2] != 'i') &&

                                                (fonaux[i + 2] != 'o'))

                                            copmud = 1;

                                        else
                                        {

                                            fonwrk[j] = 'N';

                                            fonwrk[j + 1] = 'i';

                                            j += 2;

                                            i++;

                                        }//else

                                    else

                                                //   ghi -> gi

                                                if (fonaux[i + 1] == 'H')

                                        if (fonaux[i + 2] == 'i')
                                        {

                                            fonwrk[j] = 'G';

                                            fonwrk[j + 1] = 'i';

                                            j += 2;

                                            i += 2;

                                        }//if

                                        else copmud = 1;

                                    else copmud = 1;

                                    break;

                                case 'H':  //se o caracter for H

                                    //H eh desconsiderado

                                    break;

                                case 'i':  //se o caracter for i

                                    if (fonaux[i + 2] == ' ')

                                        //is ou iz final perde a consoante

                                        if (fonaux[i + 1] == 's')
                                        {

                                            fonwrk[j] = 'i';

                                            break;

                                        }//if

                                        else

                                            if (fonaux[i + 1] == 'Z')
                                        {

                                            fonwrk[j] = 'i';

                                            break;

                                        }//if

                                    //ix

                                    if (fonaux[i + 1] != 'X')

                                        copfon = 1;

                                    else

                                        if (i != 0)

                                        copfon = 1;

                                    else

                                            //ix vogal no inicio torna-se iz

                                            if ((fonaux[i + 2] == 'a') ||

                                                    (fonaux[i + 2] == 'i') ||

                                                    (fonaux[i + 2] == 'o'))
                                    {

                                        fonwrk[j] = 'i';

                                        fonwrk[j + 1] = 'Z';

                                        j += 2;

                                        i++;

                                        break;

                                    }//if

                                    else

                                                //ix consoante no inicio torna-se is

                                                if (fonaux[i + 2] == 'C' || fonaux[i + 2] == 's')
                                    {

                                        fonwrk[j] = 'i';

                                        j++;

                                        i++;

                                        break;

                                    }//if

                                    else
                                    {

                                        fonwrk[j] = 'i';

                                        fonwrk[j + 1] = 's';

                                        j += 2;

                                        i++;

                                        break;

                                    }//else

                                    break;

                                case 'J':  //se o caracter for J

                                    //J -> Gi

                                    fonwrk[j] = 'G';

                                    fonwrk[j + 1] = 'i';

                                    j += 2;

                                    break;

                                case 'K':  //se o caracter for K

                                    // Kisa -> Xa
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 'a'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'a';

                                        j = j + 2;

                                        i = i + 3;

                                        break;
                                    }

                                    // Kissa -> Xa
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'a'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'a';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Kisi, Kici -> Xi
                                    if ((fonaux[i + 1] == 'i') && ((fonaux[i + 2] == 's') || (fonaux[i + 2] == 'C')) && (fonaux[i + 3] == 'i'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        i = i + 3;

                                        break;
                                    }

                                    // Kissi -> Xi
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'i'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Kiso, Kico -> Xo
                                    if ((fonaux[i + 1] == 'i') && ((fonaux[i + 2] == 's') || (fonaux[i + 2] == 'C')) && (fonaux[i + 3] == 'o'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'o';

                                        j = j + 2;

                                        i = i + 3;

                                        break;
                                    }

                                    // Kisso -> Xo
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'o'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'o';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    //Ka,Ke,Ki,Ko,Ku -> Ka, Ki, Ku
                                    if ((fonaux[i + 1] == 'a') ||
                                            (fonaux[i + 1] == 'e') ||
                                            (fonaux[i + 1] == 'i') ||
                                            (fonaux[i + 1] == 'o') ||
                                            (fonaux[i + 1] == 'u'))
                                    {

                                        fonwrk[j] = 'K';

                                        j++;

                                        break;
                                    }


                                    fonwrk[j] = 'K';
                                    fonwrk[j + 1] = 'i';

                                    j = j + 2;

                                    break;


                                case 'L':  //se o caracter for L

                                    // L depois de consoante -> consoante + R
                                    if ((i - 1) >= 0)
                                    {
                                        if ((fonaux[i - 1] != 'a') && (fonaux[i - 1] != 'i') && (fonaux[i - 1] != 'u'))
                                        {

                                            fonwrk[j] = 'R';

                                            j = j + 1;

                                            break;

                                        }//if
                                    }//if	

                                    // Lai -> Li 
                                    if ((fonaux[i + 1] == 'a') && (fonaux[i + 2] == 'i'))
                                    {

                                        fonwrk[j] = 'L';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;
                                        i = i + 2;

                                        break;

                                    }//if

                                    //L + vogal nao eh modificado

                                    else if ((fonaux[i + 1] == 'a') ||

                                            (fonaux[i + 1] == 'i') ||

                                            (fonaux[i + 1] == 'o'))

                                        copfon = 1;

                                    else

                                        //L + consoante -> U + consoante

                                        if (fonaux[i + 1] != 'H')
                                    {

                                        fonwrk[j] = 'o';

                                        j++;

                                        break;

                                    }//if

                                    //LH + consoante nao eh modificado

                                    else

                                            if (fonaux[i + 2] != 'a' &&

                                                    fonaux[i + 2] != 'i' &&

                                                    fonaux[i + 2] != 'o')

                                        copfon = 1;

                                    else
                                    {

                                        //LH + vogal -> LI + vogal

                                        fonwrk[j] = 'L';

                                        fonwrk[j + 1] = 'i';

                                        j += 2;

                                        i++;

                                        break;

                                    }

                                    break;

                                case 'M':  //se o caracter for M

                                    //M + consoante -> N + consoante

                                    if ((fonaux[i + 1] != 'a' &&

                                            fonaux[i + 1] != 'e' &&

                                            fonaux[i + 1] != 'i' &&

                                            fonaux[i + 1] != 'o' &&

                                            fonaux[i + 1] != 'u' &&

                                            fonaux[i + 1] != ' '))
                                    {

                                        fonwrk[j] = 'N';

                                        j++;

                                    }//if

                                    //M final -> NI
                                    else if (fonaux[i + 1] == ' ')
                                    {

                                        fonwrk[j] = 'N';

                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                    }//if

                                    //ME e MI final -> NI
                                    else if ((fonaux[i + 1] == 'e' ||

                                            fonaux[i + 1] == 'i') &&

                                            fonaux[i + 2] == ' ')
                                    {

                                        fonwrk[j] = 'N';

                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                    }//if

                                    //M nao eh alterado

                                    else copfon = 1;

                                    break;

                                case 'N':  //se o caracter for N

                                    //NGT -> NT

                                    if ((fonaux[i + 1] == 'G') &&

                                            (fonaux[i + 2] == 'T'))
                                    {

                                        fonaux[i + 1] = 'N';

                                        copfon = 1;

                                    }//if

                                    else

                                        //NH + consoante nao eh modificado

                                        if (fonaux[i + 1] == 'H')

                                        if ((fonaux[i + 2] != 'a') &&

                                                (fonaux[i + 2] != 'i') &&

                                                (fonaux[i + 2] != 'o'))

                                            copfon = 1;

                                        //NH + vogal -> Ni + vogal

                                        else
                                        {

                                            fonwrk[j] = 'N';

                                            fonwrk[j + 1] = 'i';

                                            j += 2;

                                            i++;

                                        }

                                    // N final -> NI
                                    else if (fonaux[i + 1] == ' ')
                                    {

                                        fonwrk[j] = 'N';

                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                    }//if

                                    else copfon = 1;

                                    break;

                                case 'o':  //se o caracter for o

                                    //oS final -> o

                                    //oZ final -> o

                                    if ((fonaux[i + 1] == 's') ||

                                            (fonaux[i + 1] == 'Z'))

                                        if (fonaux[i + 2] == ' ')
                                        {

                                            fonwrk[j] = 'o';

                                            break;

                                        }//if

                                        else copfon = 1;

                                    else copfon = 1;

                                    break;

                                case 'P':  //se o caracter for P

                                    //PH -> F

                                    if (fonaux[i + 1] == 'H')
                                    {

                                        fonwrk[j] = 'F';

                                        i++;

                                        newmud = 1;

                                    }//if

                                    else

                                        copmud = 1;

                                    break;

                                case 'Q':  //se o caracter for Q

                                    // Qisa -> Xa
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 'a'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'a';

                                        j = j + 2;

                                        i = i + 3;

                                        break;
                                    }

                                    // Qissa -> Xa
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'a'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'a';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Qoisa -> Xa
                                    if ((fonaux[i + 1] == 'o') && (fonaux[i + 2] == 'i') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'a'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'a';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Qoissa -> Xa
                                    if ((fonaux[i + 1] == 'o') && (fonaux[i + 2] == 'i') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 's') && (fonaux[i + 5] == 'a'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'a';

                                        j = j + 2;

                                        i = i + 5;

                                        break;
                                    }

                                    // Qisi, Qici -> Xi
                                    if ((fonaux[i + 1] == 'i') && ((fonaux[i + 2] == 's') || (fonaux[i + 2] == 'C')) && (fonaux[i + 3] == 'i'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        i = i + 3;

                                        break;
                                    }

                                    // Qissi -> Xi
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'i'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Qoisi, Qoici -> Xi
                                    if ((fonaux[i + 1] == 'o') && (fonaux[i + 2] == 'i') && ((fonaux[i + 3] == 's') || (fonaux[i + 3] == 'C')) && (fonaux[i + 4] == 'i'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Qoissi -> Xi
                                    if ((fonaux[i + 1] == 'o') && (fonaux[i + 2] == 'i') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 's') && (fonaux[i + 5] == 'i'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'i';

                                        j = j + 2;

                                        i = i + 5;

                                        break;
                                    }

                                    // Qiso -> Xo
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 'o'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'o';

                                        j = j + 2;

                                        i = i + 3;

                                        break;
                                    }

                                    // Qisso -> Xo
                                    if ((fonaux[i + 1] == 'i') && (fonaux[i + 2] == 's') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'o'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'o';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Qoiso -> Xo
                                    if ((fonaux[i + 1] == 'o') && (fonaux[i + 2] == 'i') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 'o'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'o';

                                        j = j + 2;

                                        i = i + 4;

                                        break;
                                    }

                                    // Qoisso -> Xo
                                    if ((fonaux[i + 1] == 'o') && (fonaux[i + 2] == 'i') && (fonaux[i + 3] == 's') && (fonaux[i + 4] == 's') && (fonaux[i + 5] == 'o'))
                                    {
                                        fonwrk[j] = 'X';
                                        fonwrk[j + 1] = 'o';

                                        j = j + 2;

                                        i = i + 5;

                                        break;
                                    }

                                    //Koi -> Ki (QUE, QUI -> KE, KI)

                                    if (fonaux[i + 1] == 'o')

                                        if (fonaux[i + 2] == 'i')
                                        {

                                            fonwrk[j] = 'K';

                                            j++;

                                            i++;

                                            break;

                                        }//if

                                    //Qa,Qe,Qi,Qo,Qu -> Ka,Ke,Ki,Ko,Ku
                                    if ((fonaux[i + 1] == 'a') ||
                                            (fonaux[i + 1] == 'e') ||
                                            (fonaux[i + 1] == 'i') ||
                                            (fonaux[i + 1] == 'o') ||
                                            (fonaux[i + 1] == 'u'))
                                    {

                                        fonwrk[j] = 'K';

                                        j++;

                                        break;
                                    }

                                    //QoA -> KoA (QUA -> KUA)

                                    fonwrk[j] = 'K';
                                    fonwrk[j + 1] = 'i';

                                    j = j + 2;

                                    break;

                                case 'R':  //se o caracter for R

                                    // K, C, Q + R -> KiR
                                    if ((i - 1) >= 0)
                                    {
                                        if ((fonaux[i - 1] == 'K') || (fonaux[i - 1] == 'C') || (fonaux[i - 1] == 'Q'))
                                        {

                                            fonwrk[j] = 'i';
                                            fonwrk[j + 1] = 'R';

                                            j = j + 2;

                                            break;

                                        }//if
                                    }//if

                                    //R nao eh modificado

                                    copfon = 1;

                                    break;

                                case 's':  //se o caracter for s

                                    //s final eh ignorado

                                    if (fonaux[i + 1] == ' ')

                                        break;

                                    //s inicial + vogal nao eh modificado        						
                                    if ((fonaux[i + 1] == 'a') ||

                                            (fonaux[i + 1] == 'i') ||

                                            (fonaux[i + 1] == 'o'))

                                        if (i == 0)
                                        {

                                            copfon = 1;

                                            break;

                                        }//if

                                    //ss -> s

                                    if (fonaux[i + 1] == 's')

                                        if (fonaux[i + 2] != ' ')
                                        {

                                            copfon = 1;

                                            i++;

                                            break;

                                        }//if

                                        else
                                        {

                                            fonaux[i + 1] = ' ';

                                            break;

                                        }//else

                                    //s inicial seguido de consoante fica precedido de i

                                    //se nao for sci, sh ou sch nao seguido de vogal

                                    if (i == 0)

                                        if (!((fonaux[i + 1] == 'C') &&

                                                (fonaux[i + 2] == 'i')))

                                            if (fonaux[i + 1] != 'H')

                                                if (!((fonaux[i + 1] == 'C') &&

                                                        (fonaux[i + 2] == 'H') &&

                                                        ((fonaux[i + 3] != 'a') &&

                                                        (fonaux[i + 3] != 'i') &&

                                                        (fonaux[i + 3] != 'o'))))
                                                {

                                                    fonwrk[j] = 'i';

                                                    j++;

                                                    copfon = 1;

                                                    break;

                                                }//if

                                    //sH -> X;

                                    if (fonaux[i + 1] == 'H')
                                    {

                                        fonwrk[j] = 'X';

                                        i++;

                                        newmud = 1;

                                        break;

                                    }//if

                                    if (fonaux[i + 1] != 'C')
                                    {

                                        copfon = 1;

                                        break;

                                    }//if

                                    //   sCh nao seguido de i torna-se X

                                    if (fonaux[i + 2] == 'H')
                                    {

                                        fonwrk[j] = 'X';

                                        i += 2;

                                        newmud = 1;

                                        break;

                                    }//if

                                    if (fonaux[i + 2] != 'i')
                                    {

                                        copfon = 1;

                                        break;

                                    }//if

                                    //sCi final -> Xi

                                    if (fonaux[i + 3] == ' ')
                                    {

                                        fonwrk[j] = 'X';

                                        fonwrk[j + 1] = 'i';

                                        i = i + 3;

                                        break;

                                    }//if

                                    //sCi vogal -> X

                                    if ((fonaux[i + 3] == 'a') ||

                                            (fonaux[i + 3] == 'i') ||

                                            (fonaux[i + 3] == 'o'))
                                    {

                                        fonwrk[j] = 'X';

                                        j++;

                                        i += 2;

                                        break;

                                    }//if

                                    //sCi consoante -> si

                                    fonwrk[j] = 's';

                                    fonwrk[j + 1] = 'i';

                                    j += 2;

                                    i += 2;

                                    break;

                                case 'T':  //se o caracter for T

                                    //T + consoante != H, L, R, Y -> Ti 
                                    if ((fonaux[i + 1] != 'H') && (fonaux[i + 1] != 'L') && (fonaux[i + 1] != 'R') && (fonaux[i + 1] != 'Y')
                                            && (fonaux[i + 1] != 'a') && (fonaux[i + 1] != 'e') && (fonaux[i + 1] != 'i') && (fonaux[i + 1] != 'o') && (fonaux[i + 1] != 'u'))
                                    {

                                        fonwrk[j] = 'T';

                                        fonwrk[j + 1] = 'i';

                                        j += 2;

                                        break;

                                    }

                                    else
                                    {

                                        copmud = 1;

                                        break;

                                    }

                                case 'V':  //se o caracter for V

                                case 'W':  //ou se o caracter for W

                                    // WHa,WHe,WHi,WHo,WHu,VHa,VHe,VHi,VHo,VHu -> U        						
                                    if (((fonaux[i] == 'W') || (fonaux[i] == 'V')) &&
                                            (fonaux[i + 1] == 'H') &&
                                            ((fonaux[i + 2] == 'a') || (fonaux[i + 2] == 'i') || (fonaux[i + 2] == 'u')))
                                    {

                                        fonwrk[j] = 'u';

                                        j++;
                                        i = i + 2;

                                        break;
                                    }

                                    // Wa,We,Wi,Wo,Wu,Va,Ve,Vi,Vo,Vu -> U
                                    if (((fonaux[i] == 'W') || (fonaux[i] == 'V')) &&
                                            ((fonaux[i + 1] == 'a') || (fonaux[i + 1] == 'i') || (fonaux[i + 1] == 'u')))
                                    {

                                        fonwrk[j] = 'u';

                                        j++;
                                        i++;

                                        break;
                                    }

                                    break;

                                case 'X':  //se o caracter for X

                                    //caracter nao eh modificado

                                    copmud = 1;

                                    break;

                                case 'Y':  //se o caracter for Y

                                    //Y jah foi tratado acima

                                    break;

                                case 'Z':  //se o caracter for Z

                                    //Z final eh eliminado

                                    if (fonaux[i + 1] == ' ')

                                        break;

                                    //Z -> s

                                    else
                                    {

                                        fonwrk[j] = 's';

                                        j++;

                                        break;
                                    }

                                default: //se o caracter nao for um dos jah relacionados

                                    //o caracter nao eh modificado

                                    fonwrk[j] = fonaux[i];

                                    j++;

                                    break;

                            }//switch

                            //copia caracter corrente

                            if (copfon == 1)
                            {

                                fonwrk[j] = fonaux[i];

                                j++;

                            }//if

                            //insercao de i apos consoante muda

                            if (copmud == 1)

                                fonwrk[j] = fonaux[i];

                            if (copmud == 1 || newmud == 1)
                            {

                                j++;

                                k = 0;

                                while (k == 0)

                                    if (fonaux[i + 1] == ' ')
                                    {

                                        //e final mudo

                                        fonwrk[j] = 'i';

                                        k = 1;

                                    }//if

                                    else

                                        if ((fonaux[i + 1] == 'a') ||

                                                (fonaux[i + 1] == 'i') ||

                                                (fonaux[i + 1] == 'o'))

                                        k = 1;

                                    else

                                            if (fonwrk[j - 1] == 'X')
                                    {

                                        fonwrk[j] = 'i';

                                        j++;

                                        k = 1;

                                    }//if

                                    else

                                                if (fonaux[i + 1] == 'R')

                                        k = 1;

                                    else

                                                    if (fonaux[i + 1] == 'L')

                                        k = 1;

                                    else

                                                        if (fonaux[i + 1] != 'H')
                                    {

                                        fonwrk[j] = 'i';

                                        j++;

                                        k = 1;

                                    }//if

                                    else i++;

                            }



                        }//for

                    }//if

                }//else



                //for (i = 0; i < component[desloc].Tostring().Length ; i++)
                for (i = 0; i < component[desloc].Length + 3; i++)

                    //percorre toda a palavra, letra a letra

                    //i -> I

                    if (fonwrk[i] == 'i')

                        fonwrk[i] = 'I';

                    else

                        //a -> A

                        if (fonwrk[i] == 'a')

                        fonwrk[i] = 'A';

                    else

                            //o -> U

                            if (fonwrk[i] == 'o')

                        fonwrk[i] = 'U';

                    else

                                //s -> S

                                if (fonwrk[i] == 's')

                        fonwrk[i] = 'S';

                    else

                                    //E -> b

                                    if (fonwrk[i] == 'E')

                        fonwrk[i] = ' ';

                    else

                                        //Y -> _

                                        if (fonwrk[i] == 'Y')

                        fonwrk[i] = '_';

                //retorna a palavra, modificada, ao vetor que contem o texto

                component[desloc] = new string(fonwrk);

                j = 0; //zera o contador

            }//for

            str = converteVetorEmstring(component);

            //remonta as palavras armazenadas no vetor em um unico string

            str = removeRepetidos(str);

            //remove os caracteres duplicados

            return str.ToUpper().Trim();


        }

        public static string removePreposicao(string str)
        {

            int i, j;

            List<string> palavra = new List<string>();

            palavra = convertestringEmVetor(str);

            string[] prep = { "DEL", "DA", "DE", "DI", "DO", "DU", "DAS", "DOS", "DEU", "DER", "E", "LA", "LE", "LES", "LOS", "VAN", "VON", "EL" };

            for (i = 0; i < palavra.Count; i++)
            {

                for (j = 0; j < prep.Length; j++)
                {
                    if (i != 0)
                    {

                        if (palavra[i].CompareTo(prep[j]) == 0)
                        {

                            palavra.RemoveAt(i);

                            i--;

                        }

                    }
                    else
                    {
                        if (palavra[i].CompareTo(prep[j]) == 0)
                        {

                            palavra.RemoveAt(i);

                        }
                    }

                }

            }

            return converteVetorEmstring(palavra);

        }

        public static string removeRepetidos(string str)
        {

            //Retira do texto carateres que estao multiplicados:

            // ss -> s, sss -> s, rr -> r

            //char[] foncmp = new char[256];
            char[] foncmp = new char[2000];

            //matriz de caracteres que armazena o texto sem duplicatas

            //char[] fonaux = new char[256];
            char[] fonaux = new char[2000];

            //matriz de caracteres que armazena o texto original

            char[] tip = new char[1]; //armazena o caracter anterior

            int i, j;  //contadores

            j = 0;

            tip[0] = ' ';

            fonaux = str.ToCharArray();

            //a matriz de caracteres recebe o string original

            for (i = 0; i < str.Length; i++)
            {

                //percorre o texto, caracter a caracter

                //elimina o caracter se ele for duplicata e

                //nao for numero, espaco ou S

                if ((fonaux[i] != tip[0]) || (fonaux[i] == ' ')

                        || ((fonaux[i] >= '0') && (fonaux[i] <= '9'))

                        || ((fonaux[i] == 'S') && (fonaux[i - 1] == 'S') &&

                        ((i > 1) && (fonaux[i - 2] != 'S'))))
                {

                    foncmp[j] = fonaux[i];

                    j++;

                }

                tip[0] = fonaux[i];

                //reajusta o caracter de comparacao

            }

            //o string recebe o texto sem duplicatas

            str = new string(foncmp);

            str = str.Replace('\0', ' ');

            return str.Trim();

        }//removeRepetidos

        public static string removeAcentuacao(string text)
        {
            return System.Web.HttpUtility.UrlDecode(System.Web.HttpUtility.UrlEncode(text, Encoding.GetEncoding("iso-8859-7")));
        }

        #region AntigoRemoveAcentuacao
        /*
        public static string removeAcentuacao(string str)
        {

            //Substitui os caracteres acentuados por caracteres nao acentuados

            //char aux[] = new char[256];
            char[] aux = new char[2000];

            //matriz de caracteres onde o texto eh manipulado

            int i;  //contador

            aux = str.ToCharArray();

            //matriz recebe o texto

            for (i = 0; i < str.Length; i++)
            {

                //percorre o texto, caracter a caracter

                switch (aux[i])
                {

                    case 'É':

                        aux[i] = 'E';  //É -> E

                        break;

                    case 'Ê':

                        aux[i] = 'E';  //Ê -> E

                        break;

                    case 'Ë':

                        aux[i] = 'E';  //Ë -> E

                        break;

                    case 'Á':

                        aux[i] = 'A';  //Á -> A

                        break;

                    case 'À':

                        aux[i] = 'A';  //À -> A

                        break;

                    case 'Â':

                        aux[i] = 'A';  //Â -> A

                        break;

                    case 'Ã':

                        aux[i] = 'A';  //Ã -> A

                        break;

                    case 'Ä':

                        aux[i] = 'A';  //Ä -> A

                        break;

                    case 'Ç':

                        aux[i] = 'S';  //Ç -> S

                        break;

                    case 'Í':

                        aux[i] = 'I';  //Í -> I

                        break;

                    case 'Ó':

                        aux[i] = 'O';  //Ó -> O

                        break;

                    case 'Õ':

                        aux[i] = 'O';  //Õ -> O

                        break;

                    case 'Ô':

                        aux[i] = 'O';  //Ô -> O

                        break;

                    case 'Ö':

                        aux[i] = 'O';  //Ö -> O

                        break;

                    case 'Ú':

                        aux[i] = 'U';  //Ú -> U

                        break;

                    case 'Ü':

                        aux[i] = 'U';  //Ü -> U

                        break;

                    case 'Ñ':

                        aux[i] = 'N';  //Ñ -> N

                        break;

                }

            }

            str = (new string(aux)).Trim();

            //o string recebe o texto sem acentuacao

            return str;

        }//removeAcentuacao
        */
        #endregion

        /// <summary>
        /// Elimina os caracteres que NAO sejam alfanumericos ou espacos
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string removeEstranhos(string str)
        {
            //char[] foncmp = new char[256];
            char[] foncmp = new char[2000];

            //matriz de caracteres que armazena o texto original

            //char[] fonaux = new char[256];
            char[] fonaux = new char[2000];

            //matriz de caracteres que armazena o texto modificado

            int i, j,  //contadores

            first;  //indica se exitem espacos em branco antes do primeiro

            //caracter: se 1 -> existem, se 0 -> nao existem

            j = 0;

            first = 1;

            fonaux = str.ToCharArray();

            //matriz de caracteres recebe o texto

            //for (i = 0; i < 256; i++)
            for (i = 0; i < 2000; i++)

                foncmp[i] = ' ';

            //branqueia a matriz de caracteres

            for (i = 0; i < str.Length; i++)
            {

                //percorre o texto, caracter a caracter

                //elimina os caracteres que nao forem alfanumericos ou espacos

                if (((fonaux[i] >= 'A') &&

                        (fonaux[i] <= 'Z')) ||

                        ((fonaux[i] >= 'a') &&

                        (fonaux[i] <= 'z')) ||

                        ((fonaux[i] >= '0') &&

                        (fonaux[i] <= '9')) ||

                        (fonaux[i] == '&') ||

                        (fonaux[i] == '_') ||

                        ((fonaux[i] == ' ') && first == 0))
                {

                    foncmp[j] = fonaux[i];

                    j++;

                    first = 0;

                }//if

            }//for

            str = new string(foncmp);

            //string recebe o texto da matriz de caracteres

            return str.Trim();

        }//removeEstranhos

        /// <summary>
        /// Armazena o texto de um string em um vetor onde cada palavra do texto ocupa uma posicao do vetor
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> convertestringEmVetor(string str)
        {
            str = str.Trim();

            //char[] fonaux = new char[256];
            char[] fonaux = new char[2000];

            //matriz de caracteres que armazena o texto completo

            //char[] foncmp = new char[256];
            char[] foncmp = new char[2000];

            //matriz de caracteres que armazena cada palavra

            List<string> component = new List<string>();

            int i, j,  //contadores

            pos,   //posicao da matriz

            rep,   //indica se eh espaco em branco repetido

            first; //indica se eh o primeiro caracter

            first = 1;

            pos = 0;

            rep = 0;

            fonaux = str.ToCharArray();

            //matriz de caracteres recebe o texto

            //for (j = 0; j < 256; j++)
            for (j = 0; j < 2000; j++)

                foncmp[j] = ' ';

            //branqueia matriz de caracteres

            for (i = 0; i < str.Length; i++)
            {

                //percorre o texto, caracter a caracter

                //se encontrar um espaco e nao for o primeiro caracter,

                //armazena a palavra no vetor

                if ((fonaux[i] == ' ') && (first != 1))
                {

                    if (rep == 0)
                    {

                        component.Add((new string(foncmp)).Trim());

                        pos = 0;

                        rep = 1;

                        //for (j = 0; j < 256; j++)
                        for (j = 0; j < 2000; j++)

                            foncmp[j] = ' ';

                    }//if

                }//if

                //forma a palavra, letra a letra, antes de envia-la a uma

                //posicao do vetor

                else
                {

                    foncmp[pos] = fonaux[i];

                    first = 0;

                    pos++;

                    rep = 0;

                }//else

            }//for

            if (foncmp[0] != ' ')

                component.Add((new string(foncmp)).Trim());

            return component;

        }//convertestringEmVetor

        /// <summary>
        /// Converte o texto armazenado em um vetor para um unico string
        /// </summary>
        /// <param name="vtr"></param>
        /// <returns></returns>
        public static string converteVetorEmstring(List<string> vtr)
        {
            char[] foncmp = new char[2000];

            //matriz de caracteres que armazena o texto completo
            char[] auxChar = new char[2000];

            //matriz de caracteres que armazena cada palavra
            string auxStr;

            string str;

            int i, j, desloc;

            desloc = 0;  //deslocamento dentro da matriz

            //for (i = 0; i < 256; i ++)
            for (i = 0; i < 2000; i++)

                foncmp[i] = ' ';

            //branqueia a matriz de caracteres

            for (j = 0; j < vtr.Count; j++)
            {

                //percorre o vetor, palavra a palavra

                auxStr = (vtr[j]).Replace('\0', ' ').Trim();

                //string recebe a palavra armazenada pelo vetor

                auxChar = auxStr.ToCharArray();

                //matriz de caracteres recebe a palavra armazenada no vetor

                for (i = 0; i < auxStr.Length; i++)

                    //percorre a matriz, caracter a caracter

                    foncmp[desloc + i] = auxChar[i];

                desloc = desloc + auxStr.Length + 1;

            }//for

            str = new string(foncmp);

            //string recebe o texto completo

            return str.Trim();

        }//converteVetorEmstring

        public static string ordenaFrase(string frase)
        {

            string fraseOrdenada = "";

            if (string.IsNullOrEmpty(frase))
            {
                return fraseOrdenada;
            }
            else
            {

                frase = frase.Trim();
                // bug corrido
                frase = removeAcentuacao(frase.ToUpper());
                string[] arrayFrase = frase.Split(' ');
                Array.Sort(arrayFrase);

                for (int i = 0; i < arrayFrase.Length; i++)
                {
                    if (i != 0)
                    {
                        fraseOrdenada = fraseOrdenada + " ";
                    }
                    fraseOrdenada = fraseOrdenada + arrayFrase[i];
                }

                return fraseOrdenada;
            }

        }

        public static string trataEspacosFrase(string frase)
        {

            string retorno;

            if (frase == null)
            {
                retorno = "";
            }
            else
            {
                retorno = frase.Trim();
                char[] arrayCharFrase = retorno.ToCharArray();
                char caracterAnterior = ' ';
                retorno = "";
                for (int i = 0; i < arrayCharFrase.Length; i++)
                {
                    if ((arrayCharFrase[i] != ' ') || ((arrayCharFrase[i] == ' ') && (caracterAnterior != ' ')))
                    {
                        retorno = retorno + arrayCharFrase[i];
                    }
                    caracterAnterior = arrayCharFrase[i];
                }
            }

            return retorno;
        }

        public static string montaFraseFiltroInteligente(string filtroInteligente)
        {

            string retorno;

            if (filtroInteligente == null)
            {
                retorno = "";
            }
            else
            {
                retorno = getFonema(ordenaFrase(trataEspacosFrase(filtroInteligente)));
                string[] arrayRetorno = retorno.Split(' ');
                retorno = "";
                for (int i = 0; i < arrayRetorno.Length; i++)
                {
                    if ((i == 0) && (!arrayRetorno[i].Equals("")))
                    {
                        retorno = retorno + "%";
                    }
                    if (!arrayRetorno[i].Equals(""))
                    {
                        retorno = retorno + arrayRetorno[i] + "%";
                    }
                }
            }

            return retorno;

        }

        public string montaFraseFiltroInteligente2(string filtroInteligente)
        {

            string retorno;

            if (filtroInteligente == null)
            {
                retorno = "";
            }
            else
            {
                retorno = getFonema(ordenaFrase(trataEspacosFrase(filtroInteligente)));
                string[] arrayRetorno = retorno.Split(' ');
                retorno = "";
                for (int i = 0; i < arrayRetorno.Length; i++)
                {
                    if (!arrayRetorno[i].Equals("") && (i != (arrayRetorno.Length - 1)))
                    {
                        retorno = retorno + arrayRetorno[i] + "%";
                    }
                    if (i == (arrayRetorno.Length - 1))
                    {
                        retorno = retorno + arrayRetorno[i];
                    }
                }
            }

            return retorno;

        }

        /// <summary>
        /// Calcula a distância entre 2 strings.
        /// </summary>
        public int CalculaDistancia(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}