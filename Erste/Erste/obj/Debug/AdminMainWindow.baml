   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ErsteErste  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       Erste.AdminMainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Erste     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  
MainWindow��5   ?    � Title$ Erste��5
   	   $	��511��6   $	��833��6$   $	��500��60   $��Stretch=�6@   $��Stretch=�6e    � WindowStartupLocation$ CenterScreen=�6�   .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��185* �6   $	��160��6,   $	��200��6;   �� 5      $
��608* �6   .+�5   
   � ���	#FF6DCEF9 � UseLayoutRounding .   � RowDefinitions 5      �� 5      $��60 �6    �� 5      $��31 �6    �� 5      $��35 �6    �� 5      $��35 �6    �� 5      $��31 �6    �� 5      $��35 �6    �� 5      $��35 �6    �� 5      $��31 �6    �� 5      $��35 �6    �� 5      $��35 �6    �� 5      $��* �6    $��31��6+   �� 5      $��35 �6    �� 5       $��35 �6    .+�5"      �� -   5         ��Name$ image��5"      $��0��6#   $��60��60   $��0q�6<   $��Resources/erste_logo.png��6G   $	��Top=�6i   $
��Fill=�6�   �� 5#      -   5         ��Name$ label��5#      $��1��6#   $��31��60   $��0q�6<   $��Službenici��6G   $��14(�6\   $��Rockwell)�6j   ���	#FFEF3D4A$
��Bold"�6�   $��8,8,5,5q�6�   6�   �� 5$      -   5         ��Name$ prikaziSluzbenikeButton��5$      $��2��66      buttonStyle#���   6c   $��Prikaži službenike��6�   $
��Left=�6�   �� 5%      -   5        $ dodajSluzbenikeButton��5%      $��3��64   #���   6`   $��Dodaj službenika��6�   $
��Left=�6�   �� 5&      -   5        $ label1��5&      $��4��6$   $��31��61   $��0q�6=   $��	Profesori��6H   $��14(�6\   $��Rockwell)�6j   ���	#FFEF3D4A$
��Bold"�6�   $��8,8,5,5q�6�   6�   �� 5'      -   5        $ prikaziProfesoreButton��5'      $��5��65   #���   6e   $��Prikaži profesore��6�   $
��Left=�6�   �� 5(      -   5        $ dodajProfesoreButton��5(      $��6��63   #���   6^   $��Dodaj profesora��6�   $
��Left=�6�   �� 5)      -	   5        $ label2��5)      $��7��6$   $��31��61   $��0q�6=   $��Kursevi��6H   $��14(�6Z   $��Rockwell)�6h   ���	#FFEF3D4A$
��Bold"�6�   $��8,8,5,5q�6�   6�   �� 5*      -
   5        $ prikaziKurseveButton��5*      $��8��63   #���   6a   $��Prikaži kurseve��6�   $
��Left=�6�   �� 5+      -   5        $ dodajKursButton��5+      $��9��6.   #���   6T   $��
Dodaj kurs��6z   $
��Left=�6�   �� 5,      $��40��6   $��10��6    60   �� 5-      -   5        $ odjavaButton��5-      $��11��6+   #���   69   $��Odjava��6_   $
��Left=�6p   $��Bottom=�6�   �� 5.      -   5        $ button6��5.      $��12��6&   #���   64   $��Izlaz��6Z   $
��Left=�6j   $��Bottom=�6�   5/      � 50   
   -   5        	 �Name$	 GridZaPrikaz��50      $��1��6%   51      52      53      