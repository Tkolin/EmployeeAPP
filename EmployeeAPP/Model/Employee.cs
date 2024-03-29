﻿using System;

namespace EmployeeAPP.Model;

public class Employee
{
   public int    ID  { get; set; }
    
   public  string  First_Name { get; set; }
   public  string Last_Name { get; set; }
   public  string Login { get; set; }
   public  string Password { get; set; }
   public  string Patronymic { get; set; }
   public DateTime  Birth_Date { get; set; }
   public  string Birth_Adress { get; set; }
   public  string Phone_Number { get; set; }
   public   string INN { get; set; }
   public   int  Passport_ID  { get; set; }
   public  int   Gender_ID { get; set; }
   public  int  Family_Location_ID { get; set; }
   public  string  FamilyNameData { get; set; }
   public  string  PassportAdress { get; set; }
   public  string  GenderNameData { get; set; }
   public  string  PassportNameData { get; set; }

   
   public Employee(){}
   
   public Employee(
      int id,
      string first_Name,
      string last_Name,
      string patronymic,
      DateTime birth_Date,
      string birth_Adress,
      string phone_Number,
      string inn,
      int passport_ID,
      int gender_ID,
      int family_Location_ID,
      
      string familyNameData,
      string passportAdress,
      string genderNameData,
      string passportNameData,
      
      string login,
      string password
   )
   {
      ID = id;
      First_Name = first_Name;
      Last_Name = last_Name;
      Patronymic = patronymic;
      Birth_Date = birth_Date;
      Birth_Adress = birth_Adress;
      Phone_Number = phone_Number;
      INN = inn;
      Passport_ID = passport_ID;
      Gender_ID = gender_ID;
      Family_Location_ID = family_Location_ID;
      FamilyNameData = familyNameData;
      PassportAdress = passportAdress;
      GenderNameData = genderNameData;
      PassportNameData = passportNameData;
      Login = login;
      Password = password;

   }
}