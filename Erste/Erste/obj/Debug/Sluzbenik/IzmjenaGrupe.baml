   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Erste.SluzbenikErste.Sluzbenik  5   	   "    Erste.Sluzbenik.IzmjenaGrupe   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Erste.Sluzbenik     5   	     � Title$  Erste - izmjena grupe��5       � Icon$! ../Resources/erste_logo.png��6)   $��390.333��6L   $��387.667��6]    � 
ResizeMode$ NoResize=�6m    � WindowStartupLocation$ CenterScreen=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��245* �6   �� 5      $
��117* �6    � ColumnDefinitions 5   
   �� 5      $
��235* �6   �� 5      $
��143* �6   .+�5   
   �� $��67,78,83,110q�6   $��Naziv grupe:��6'   �� 5   
   -   5         ��Name$ NazivBox��5      $��155,84,63.667,135q�6$    � ToolTip$ Unesite naziv grupe.��6?   $��2��6^   $
��Left=�6s   $��Center=�6�   �� 5   
   $��67,120,93,75.333q�6   $��Jezik kursa:
��6*   �� 5   
   -   5         ��Name$ Button1��5      $��U redu��6"   $��Center=�63   $��Center=�6W   $��
82,45,51,0q�6�      buttonStyle#���   6�   $��30��6�   $��0q�6�   $	��Top=�6�   $��1��6�   �� 5   
   -   5        $ Button2��5      $��Otkaži��6"   $��
217,45,0,0q�6O   #���   6c   $��30��6�   $��0q�6�   $
��Left=�6�   $	��108��6�   $��2��6�   $	��Top=�6�   $��1��6�   �� 5   
   $��70,161,95,40q�6   $��Nivo kursa:��6&   	 J� RenderTransformOrigin$	 1.357,0.138&�6<   �� 5   
   -   5        
 ��Name$
 label��5      $��Center=�6   $��Kreiranje nove grupe��6C   $��0,19,2.667,0q�6b   $��19(�6x   $
��Bold"�6�   
���  ��$��2��6�   $��38��6�   $	��Top=�6�   �� 5   
   -   5         ��Name$ 
JezikCombo��5      $��155,125,62.667,0q�6'   $��2��6A   $��20��6U   $	��Top=�6a   �� 5   
   -   5        $ NivoKursaCombo��5      $��155,165,62.667,0q�6+   $��2��6E   $��20��6Y   $	��Top=�6e   �� 5   
   $��0��6   $	��Top=�6    $��
70,200,0,0q�68   $��50��6L   $
��Left=�6X   $��70��6s   $��	Datum od:��6~   $��2��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ TimePickerOd��5      $��0,202,62.667,0q�6+   $	��165��6C   $��22��6O   $	��Top=�6[   $��Right=�6s   $��2��6�      FirstDayOfWeek$ Monday=�6�    � 	Resources 5      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%_   '        �� 5       "  6   .�5!      ��  � ��$ Izaberite datum��6-   5"      5#      5$      �� 5%   
   $��0��6   $��Center=�6    $��71,235,0,97q�6;   $��30��6P   $
��Left=�6\   $��69��6w   $��	Datum do:��6�   $��2��6�     5&   
   -   5        $ TimePickerDo��5&      $��0,0,62.667,100q�6+   $	��165��6C   $��22��6O   $��Bottom=�6[   $��Right=�6v   $��2��6�   $��2��6�    5'      ��%_   '        �� 5(      "  6   .�5)      ��  � ��$ Izaberite datum��6-   5*      5+      5,      5-      5.      