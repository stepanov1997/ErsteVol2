   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Erste.AdministratorErste.Administrator  5   	   3    -Erste.Administrator.EvidencijaProfesoraDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Erste.Administrator     5   	     � Title$  Erste - izmjena profesora��5   	    � Icon$! ../Resources/erste_logo.png��6+    � SizeToContent$ WidthAndHeight=�6N   $��487.553��6m   $	��361��6~   $	��405��6�    � WindowStartupLocation$ CenterScreen=�6�    � 
ResizeMode$ NoResize=�6�   .��5	      � -   5         �Name$
 grid��5	       � RowDefinitions 5
   
   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $	��Top=�6   $��	92,71,0,0q�6)   $��26��6<   $
��Left=�6H   $��41��6c   .��6o   Ime:6u   �� 5   
   -   5        $��textBox_Ime��5      $	��Top=�6%   $��	0,76,81,0q�6=   $��20��6P   $��Right=�6\   $	��154��6x   �� 5   
   $	��Top=�6   $��25��6(   $��
72,116,0,0q�64   $
��Left=�6H   $��61��6c   .��6n   
Prezime:6x   �� 5   
   -   5        $��textBox_Prezime��5      $	��Top=�6)   $��
0,119,81,0q�6A   $��20��6U   $��Right=�6a   $	��154��6}   �� 5   
   -   5        $��textBox_Email��5      $	��Top=�6'   $��
0,157,81,0q�6?   $��20��6S   $��Right=�6_   $	��154��6{   �� 5   
   $	��Top=�6   $��28��6(   $��
44,194,0,0q�64   $
��Left=�6H   $��84��6c   .��6n   Broj telefona:6~   �� 5   
   -   5        $��textBox_BrojTelefona��5      $	��Top=�6.   $��
0,198,81,0q�6F   $��20��6Z   $��Right=�6f   $	��154��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridKurs��5      $��81,240,81,0q�6)   $	��237��6>   $	��100��6J   $	��Top=�6W   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6%      Header$ Jezik��6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   ��  �� Path$ jezik.Naziv�6e     5      $
 * 6%   $
 Nivo��6/    6=   �� $
 Nivo�6]   5       �� 5!   
   -   5         ��Name$ dodavanjeKursa��5!      
���  �� �� 
IsReadOnly .  �� 
IsEditable .  �� Text$ Dodjeli kurs��6`   $��Center=�6t   $��Center=�6�   $��Center=�6�   $��20��6�   $��0q�6�   $��-7,340,-9,0q�6�   $	��237��6  $	��Top=�6  ��5"      �� 5#      .R�5$      �� ��5%      `� 5&       �� StringFormat$ {0} {1}��6+   .1�5'   "   �� $ jezik.Naziv�6*   �� 5(   "   $
 Nivo�6*   5)      5+      5,      5.      �� 50   
   -   5        $��Button1��50      $��U redu��6    $��Center=�61   $��Center=�6U   $��72,369,0,37q�6�      buttonStyle#���   6�   $��30��6�   $��0q�6�   $��Bottom=�6�   $
��Left=�6�   $	��102��6  �� 51   
   -	   5        $��Button2��51      $��Otkaži��6    $��0,264,81,37q�6M   #���   6b   $��30��6�   $��0q�6�   $��Right=�6�   $	��108��6�   $��Bottom=�6�   �� 52   
   $	��Top=�6   $��29��6(   $��
82,156,0,0q�64   $��Email:��6H   $
��Left=�6Y   $��51��6t   �� 53   
   -
   5         ��Name$ label��53      $��Podaci o profesoru��6   $��106,18,112,0q�6<   $	��Top=�6R   $��19(�6j   $
��Bold"�6x   
���  ��$��38��6�   54      55      