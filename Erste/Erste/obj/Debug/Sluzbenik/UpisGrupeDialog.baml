   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Erste.SluzbenikErste.Sluzbenik  5   	   &     Erste.Sluzbenik.UpisTerminaGrupe   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   )localclr-namespace:Erste.Sluzbenik  5   	     � Title$  Erste - upis grupe��5	   	    � Icon$! ../Resources/erste_logo.png��6$   $��519.152��6G   $��400.333��6X    � 
ResizeMode$ NoResize=�6h    � WindowStartupLocation$ CenterScreen=�6~   .��5
      � $��0,0,0,-4q�6   .+�5   
   �� $��Bottom=�6   $��87,0,0,189.333q�6,   $��26��6D   $
��Dan:��6P   $
��Left=�6_   $��43��6z   �� 5   
   -   5         ��Name$ DanCombo��5      $��161,0,0,193.333q�6%   $��22��6>    � ToolTip$/ )Unesite dan kad će se održavati termin.��6J   $
��Left=�6|   $	��159��6�   $��Bottom=�6�   @�5      �� 5      $��Ponedjeljak��6   67   �� 5      $��Utorak��6   62   �� 5      $��Srijeda��6   63   �� 5      $��	Četvrtak��6   64   �� 5      $��Petak��6   61   �� 5      $��Subota��6   62   �� 5      $��Nedjelja��6   64   5      �� 5   
   $��Bottom=�6   $��25��6+   $��91,0,0,145.333q�67   $	��Od:��6O   $
��Left=�6]   $��29��6x    J� RenderTransformOrigin$ 	0.517,0.4&�6�   �� 5   
   -   5         ��Name$ Button1��5      $��Uredu��6"   $��Center=�62   $��Center=�6V   $��81,0,0,40.667q�6�      buttonStyle#���   6�   $��30��6�   $��0q�6�   $��Bottom=�6�   $
��Left=�6�   $	��102��6  �� 5   
   -   5        $ Button2��5      $��Otkaži��6"   $��0,0,53.667,40.667q�6O   #���   6j   $��30��6�   $��0q�6�   $��Right=�6�   $	��108��6�   $��Bottom=�6�   �� 5   
   $��Bottom=�6   $��29��6+   $��91,0,0,105.333q�67   $	��Do:��6O   $ 1.357,0.138&�6]   $
��Left=�6�   $��29��6�   �� 5   
   -   5         ��Name$ label��5      $��Podaci o terminu��6   $��0,0,-0.333,241.333q�6:   $��Bottom=�6V   $��19(�6q   $��Center=�6   $
��Bold"�6�   
���  ��$��38��6�   "  Xceed.Wpf.Toolkit.TimePicker  5   
   -   5        	  Name$	 TimePickerOd��5      $��160,0,0,150.333q�6R   $
��Left=�6k   $	��160��6�   $��20��6�   $��Bottom=�6�   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Value
 60   �� *CurrentDateTime+6�     5   
   -   5        $	 TimePickerDo��5      $��160,0,0,110.333q�6R   $��20��6k   $��Bottom=�6w   $
��Left=�6�   $	��160��6�   
 60   �� *CurrentDateTime+6�   �� 5    
   -   5        $ label1��5       $��Podaci o grupi��6    $��0,0,-0.333,446.333q�69   $��Bottom=�6U   $��19(�6p   $
��Bold"�6~   $��Center=�6�   
���  ��$��38��6�   �� 5!   
   $��Bottom=�6   $��
61,0,0,397q�6,   $��26��6@   $��Naziv grupe:��6L   $
��Left=�6c   $��86��6~   �� 5"   
   -   5         ��Name$ textBox_NazivGrupe��5"      $
��Left=�6.   $��23��6I   $��
160,74,0,0q�6U    �� TextWrapping$
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��160��6�   $
��Left=�6�   $��Center=�6�   (  "System.Windows.Controls.DatePicker  5#   
   -	   5          Name$ DatePickerOd��5#      $��0,123,74.667,0q�6+   $	��160��6C   $��22��6O   $	��Top=�6[   $��Right=�6s      FirstDayOfWeek$ Monday=�6�    � 	Resources 5$      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%_   '        �� 5%      "  6   .�5&      ��  � ��$ Izaberite datum��6-   5'      5(      5)      �� 5*   
   $��Bottom=�6   $��
61,0,0,349q�6,   $��26��6@   $��DatumOd��6L   $
��Left=�6^   $��86��6y     5+   
   -
   5        $ DatePickerDo��5+      $��0,170,74.667,0q�6+   $	��160��6C   $��22��6O   $	��Top=�6[   $��Right=�6s   $ Monday=�6�    5,      ��%_   '        �� 5-      "  6   .�5.      ��  � ��$ Izaberite datum��6-   5/      50      51      �� 52   
   $��Bottom=�6   $��
61,0,0,305q�6,   $��26��6@   $��DatumDo��6L   $
��Left=�6^   $��86��6y   53      54      