   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ErsteErste  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       Erste.SluzbenikMainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Erste     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  
MainWindow��5   ?    � Title$ Erste��5
   	    � WindowStartupLocation$ CenterScreen=�6   $	��650��6<   $
��1000��6I   $	��540��6V   $��Stretch=�6f   $��Stretch=�6�   .��5      � 
�������$
��Auto=�6#    � RowDefinitions 5   
   �� 5      $
��361* �6    � ColumnDefinitions 5   
   �� 5      $
��185* �6   $	��160��6,   $	��200��6;   �� 5      $
��608* �6   .+�5   
   � $��0��6   ���	#FF6DCEF9 � UseLayoutRounding .  $��0,0,0,0q�6P    5      �� 5      $��60 �6    �� 5      $��31 �6    �� 5      $��35 �6    �� 5      $��35 �6    �� 5      $��45 �6    �� 5      $��31 �6    �� 5      $��35 �6    �� 5      $��35 �6    �� 5      $��31 �6    �� 5       $��35 �6    �� 5!      $��35 �6    �� 5"      $��* �6    $��31��6+   �� 5#      $��35 �6    �� 5$      $��35 �6    .+�5&      �� -   5         ��Name$ image��5&      $��0��6#   $��60��60   $��/Resources/erste_logo.png��6<   $
��Fill=�6_   $��0q�6n   �� 5'      -   5         ��Name$ label��5'      $��1��6#   $��31��60   $��0q�6<   $��	Kandidati��6G   $��14(�6[   $��Rockwell)�6i   ���	#FFEF3D4A$
��Bold"�6�   $��8,8,5,5q�6�   6�   �� 5(      -   5         ��Name$ 
upisButton��5(      $��2��6)      buttonStyle#���   66   $��Upiši kandidata��6\   $
��Left=�6v   $��35��6�   �� 5)      -   5        $ pregledButton��5)      $��3��6,   #���   69   $��Prikaži sve kandidate��6_   $
��Left=�6   6�   �� 5*      -   5        $ pregledCekanjeButton��5*      $��4��63   #���   6@   $
��Left=�6f   $��47��6�   $��0q�6�   .��5+      �� $$��Prikaži kandidate na čekanju��6   $
��Wrap=�6@   	 �� TextAlignment$	 Center=�6T   $	��132��6k   $��0q�6w   $��36��6�   5,      �� 5-      -   5        $ grupe��5-      $��5��6#   $��31��60   $��0q�6<   $��Grupe��6G   $��14(�6W   $��Rockwell)�6e   ���	#FFEF3D4A$
��Bold"�6�   $��8,8,5,5q�6�   6�   �� 5.      -   5        $ pregledGrupa��5.      $��6��6+   #���   68   $��Prikaži grupu��6^   $
��Left=�6v   6�   �� 5/      -	   5        $ kreiranjeGrupe��5/      $��7��6-   #���   6:   $��Dodaj novu grupu��6`   $
��Left=�6{   6�   �� 50      -
   5        $ label1��50      $��8��6$   $��31��61   $��0q�6=   $��Raspored nastave��6H   $��14(�6c   $��Rockwell)�6q   ���	#FFEF3D4A$
��Bold"�6�   $��8,8,5,5q�6�   6�   �� 51      -   5        $ rasporedButton��51      $��9��6-   #���   6:   $��Prikaži raspored��6`   $
��Left=�6{   6�   �� 52      -   5        $ noviTerminButton��52      $��10��6/   #���   6=   $��Dodaj novi termin��6c   $
��Left=�6   6�   �� 53      
 �� Orientation$
 Vertical=�6   $��11��60   .+�54      �� -   5        $ label2��54      $�� ��6(   $	��141��63   $��0q�6@   55      �� 56      -   5        $ odjavaButton��56      $��12��6+   #���   69   $��Odjava��6_   $
��Left=�6p   6�   �� 57      -   5        $ button4��57      $��13��6&   #���   64   $��Izlaz��6Z   $
��Left=�6j   $��Bottom=�6�   58      � 59   
   -   5         �Name$ GridZaPrikaz��59      $��1��6%   $��0,1,0,0q�65   5:      5;      