using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Svyaznoi
{
    enum Products { Cake, Biscuits, Wafers };
    class Programm
    {
         static void Main(string[] args)
        {
            int money = 150;
            CakeAutomat Automat1 = new CakeAutomat(money);
            Automat1.Menu();
        }
    }

    class CakeAutomat
    {
        private  int qiwi = 0;
        private  int iMoney;
        private short iChoose = 0;
        private short[] purchase = new short[3];
        private int[] KolOfProducts = new int[] { 4, 3, 10 };
        private int[] CostOfProducts = new int[] {50, 10 , 30 };
        private string[] NameOfProducts = new string[] {"Кекса(-ов)","Пачки(-ек) печенья", "Пачки(-ек) вафель" };
        
        public CakeAutomat(int money)
        {
            this.iMoney = money;
        }
        public void Menu()
        {

            while (true)
            {
                Console.WriteLine("\nНажмите                                                     qiwi: " + qiwi + " руб.\n1. для ввода денег\n2. для выбора товара                                        money: " + iMoney +" руб.\n3. для перехода в корзину\n4. для получения сдачи(снятия всех денег)\n0. для выхода\n");
                try
                {
                    iChoose = short.Parse(Console.ReadLine());
                    if (iChoose < 0 || iChoose > 4)
                        throw new Exception();
                }
                catch
                {
                    Error();
                }
                switch (iChoose)
                {
                    case 1:
                        {
                            Payment_of_money();
                            break;
                        }
                    case 2:
                        {
                            Purchase();
                            break;
                        }
                    case 3:
                        {
                            CardOnlineStory();
                            break;
                        }
                    case 4:
                        {
                            GettingSdacha();
                            break;
                        }
                    case 0:
                        {
                            return;
                        }
                }
            }

        }
        private void Error()
        {
            Console.WriteLine("\nТакого пункта меню не существует, попробуйте еще раз\n");
            return;
        }
        private void Payment_of_money()
        {
            bool fError = false;
            string[] money;
            string sInputMoney = "", sOutPutError = " ";
            short iTemp;
            int iInputMoney = 0;
            Console.WriteLine("Внесите деньги, принимаются моенты номиналом 1,2,5 и 10 рублей (через пробел) ");
            sInputMoney = Console.ReadLine();
            money = sInputMoney.Split(' ');
            iInputMoney = 0;
            bool bMoneyEnded = false;
            for (int i = 0; i < money.Count(); i++)
            {
                try
                {
                    iTemp = short.Parse(money[i]);

                    if (iTemp != 1 && iTemp != 2 && iTemp != 5 && iTemp != 10)
                        throw new Exception();
                    else
                    {
                        if (iMoney - iTemp >=0)
                        {
                            iInputMoney += iTemp;
                            iMoney -= iTemp;
                        }
                        else
                        {
                            bMoneyEnded = true;
                        }
                    }
                }
                catch
                {
                    if (money[i] != " ")
                    {
                        fError = true;
                        sOutPutError += money[i] + ", ";
                    }
                }

            }
            Console.WriteLine("Было внесено " + iInputMoney + " рублей");
            qiwi += iInputMoney;
            if(bMoneyEnded)
            {
                Console.WriteLine("У вас закончились наличные, сходите до банка");
            }
            if (fError)
            {

                Console.WriteLine("Следующие деньги не были обработаны: " + sOutPutError);
            }
            return;
        }
        private void Purchase()
        {
            while (true)
            {
                
                Console.WriteLine("1.Кекс - 50р (" + KolOfProducts[0] + "шт.)                                         qiwi: "+qiwi+" руб. \n2.Печенье - 10р (" + KolOfProducts[1] + "шт.)                                      money: " + iMoney + " руб.\n3.Вафли - 30р (" + KolOfProducts[2] + "шт.)\n4.Переход на предыдущйи уровень меню");
                Console.WriteLine("\nЧто бы вы хотели преобрести:\n");
                try
                {
                    iChoose = short.Parse(Console.ReadLine());
                    if (iChoose <= 0 || iChoose > 4)
                        throw new Exception();
                    else
                    {
                        iChoose--;
                        
                        switch (iChoose)
                        {

                            case (short)(Products.Cake):
                                {
                                    ChooseProduct((int)Products.Cake, purchase);
                                    break;
                                }
                            case (short)(Products.Biscuits):
                                {
                                    ChooseProduct((int)Products.Biscuits, purchase);
                                    break;
                                }
                            case (short)(Products.Wafers):
                                {
                                    ChooseProduct((int)Products.Wafers, purchase);
                                    break;
                                }
                            case 3:
                                return;
                        }

                    }
                }


                catch
                {
                    Error();
                }
            }
        }
        private void CardOnlineStory()
        {
            while (true)
            {
                bool flag = false;
                int iTotalCost = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (purchase[i] != 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    iTotalCost = 0;
                    Console.WriteLine("В вашей корзине имются товары: ");

                    for (int j = 0; j < 3; j++)
                    {
                        if (purchase[j] != 0)
                        {
                            Console.WriteLine(purchase[j] + " " + NameOfProducts[j]);
                            iTotalCost += purchase[j] * CostOfProducts[j];
                        }
                    }
                    Console.WriteLine("Общая стоимость: {0}  руб.", iTotalCost);
                    Console.WriteLine("\n                1. Купить товары\n                2. Удалить товар из корзины\n                3. Возврат на предыдущий уровень меню\n");
                    try
                    {
                        iChoose = short.Parse(Console.ReadLine());
                        if (iChoose < 1 || iChoose > 3)
                            throw new Exception();
                    }
                    catch
                    {
                        Error();
                    }
                    switch (iChoose)
                    {
                        case 1:
                            {
                                Buy();
                                break;
                            }
                        case 2:
                            {
                                DeleteFromCardOnline();
                                break;
                            }
                        case 3: { return; }

                    }
                }
                else
                {
                    Console.WriteLine("Ваши корзина пуста:(");
                    return;
                }
            }
        }
        private void Buy()
        {
            int iSdacha = 0;
            iSdacha = qiwi;
            for(int i=0;i<3;i++)
            {
                iSdacha -= purchase[i] * CostOfProducts[i];
            }
            if(iSdacha <0)
            {
                Console.WriteLine("Ваших средств недостаточно, внесите монеты");
                return;
            }
            Console.WriteLine("Товары куплены ");
            for (int i = 0; i < 3; i++)
                purchase[i] = 0;
            qiwi = iSdacha;
            return;

        }
        private void ChooseProduct(int choose,  short[] purchase)
        {
            string sTemp = "";
            short count = 0;
            do
            {
                Console.Write("\nВведите количество: ");
                sTemp = Console.ReadLine();
                try
                {
                    count = short.Parse(sTemp);
                    if (KolOfProducts[choose] == 0)
                    {
                        Console.WriteLine("\nДанный вид товара закончился:(\n");
                        return;
                    }
                        if (count <= 0 || count > KolOfProducts[choose])
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("\nПопробуйте еще раз\n");
                }
            }
            while (count <= 0 || count > KolOfProducts[choose]);
                KolOfProducts[choose] -= count;
                purchase[choose] = count;
                Console.WriteLine("\nТовар отправлен в корзину\n");
        }
        private void GettingSdacha()
        {
            int iSdacha, iInteger;
            if (qiwi <= 0)
            {
                Console.WriteLine("Вы не можете снять ничего");
                return;
            }
            
            else
            {
                short[,] masSdacha = new short[4, 2];
                masSdacha[0, 0] = 10;
                masSdacha[1, 0] = 5;
                masSdacha[2, 0] = 2;
                masSdacha[3, 0] = 1;
                iSdacha = qiwi;
                for(int j=0; j <4;j++)
                {
                    iInteger = iSdacha / masSdacha[j, 0];
                    iSdacha -= iInteger * masSdacha[j, 0];
                    masSdacha[j ,1] = (short)iInteger;

                }
                Console.WriteLine("Ваша сдача, монеты: ");
                for (int i = 0; i < 4; i++)
                {
                    if (masSdacha[i, 1] != 0)
                        Console.WriteLine(masSdacha[i, 0] + " рублевых- " + masSdacha[i, 1] + "шт.");
                }
                iMoney += qiwi;
                qiwi = 0;
            }
        }
        private void DeleteFromCardOnline()
        {
            short choose=0;
            Console.WriteLine("В вашей корзине имются товары: ");

            for (int j = 0; j < 3; j++)
            {
                if (purchase[j] != 0)
                {
                    Console.WriteLine(purchase[j] + " " + NameOfProducts[j]);
                }
            }
            Console.WriteLine("\nКакой вид товара хотите удалить(введите номер товара):\n4. Возврат на предыдущий уровень меню\n");
            try
            {
                choose = short.Parse(Console.ReadLine());
                if (choose <= 0 || choose > 4)
                    throw new Exception();
                else
                {
                    choose--;
                    if (choose == 3)
                        return;
                    else
                    {
                        KolOfProducts[choose] += purchase[choose];
                        purchase[choose] = 0;
                        return;
                    }

                }
            }


            catch
            {
                Error();
            }
        }
    }
}
