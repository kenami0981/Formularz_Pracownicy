   M S B A M L   `   `   `  ����O  KFormularz_Pracownicy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Formularz_PracownicyFormularz_Pracownicy  5   	   %    Formularz_Pracownicy.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Formularz_Pracownicy     5   	     � Title$  
MainWindow��5   	   $	��460��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��1* �6   �� 5      $��2* �6   .+�5   
   �� $��0��6   .+�5      �� $��Imię:��6   �� 5      -   5         ��Name$ tbx_imie��5      6(   �� 5      $��	Nazwisko:��6   �� 5      -   5        $ tbx_nazwisko��5      6,   �� 5      $��Data urodzenia:��6   (  "System.Windows.Controls.DatePicker  5      -   5        $��dp_data��5      �� 5      $��Stanowisko:��6   �� 5      -   5         ��Name$ cb_team��5      5      �� 5      $��Rodzaj umowy��6   	� 5      -   5         	�Name$ rb_contract1��5       	� 	GroupName$ rb_contract��60   $!��Umowa na czas nieokreślony��6H   	� 5       -   5        $ rb_contract2��5       $ rb_contract��60   $��Umowa na czas określony��6H   	� 5!      -   5        $ rb_contract3��5!      $ rb_contract��60   $��Umowa zlecenie��6H   �� 5"      $��Pensja:��6   �� 5#      -   5        $ 
tbx_salary��5#      6*   �� 5%      -	   5         ��Name$ 	btn_dodaj��5%      $��Dodaj��6(   
����  �
�������$	��0 5q�5'      6   �� 5(      -
   5        $ 
btn_edytuj��5(      $��Edytuj��6)   
���  ��
�������$	��0 5q�5*   	   6   �� 5+      -   5        $ btn_usun��5+      $��Usuń��6'   
�������
�������$	��0 5q�5-   	   6   � 5.       5/      �� 50      6)   �� 51      6)   .+�53      �� -   5        $ 
btn_zapisz��53   )   $��0��6   $��Zapisz��6=   
��� d �
�������$	��0 5q�55      6   �� 57      -   5        $ btn_wczytaj��57   )   $��1��6   $��Wczytaj��6>   
��� ���
�������$	��0 5q�59      6   5:      5<      �� 5>   
   -   5         ��Name$ lb_pracownicy��5>      $��1��6)   69   5?      5@      