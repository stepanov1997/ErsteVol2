   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Erste.SluzbenikErste.Sluzbenik  5   	   &     Erste.Sluzbenik.KreiranjeTermina   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   )localclr-namespace:Erste.Sluzbenik  5   	     � Title$  Erste - novi termin��5	   	    � Icon$! ../Resources/erste_logo.png��6%   $	��320��6H   $��383.333��6U    � 
ResizeMode$ NoResize=�6e    � WindowStartupLocation$ CenterScreen=�6{   .��5
      � .+�5   
   �� $��Bottom=�6   $��
81,0,0,180q�6,   $��26��6@   $
��Dan:��6L   $
��Left=�6[   $��43��6v   �� 5   
   -   5         ��Name$ DanCombo��5      $��	0,89,69,0q�6%   $��22��68    � ToolTip$/ )Unesite dan kad će se održavati termin.��6D   $	��Top=�6v   $��Right=�6�   $	��159��6�   @�5      �� 5      $��Ponedjeljak��6   67   �� 5      $��Utorak��6   62   �� 5      $��Srijeda��6   63   �� 5      $��	Četvrtak��6   64   �� 5      $��Petak��6   61   �� 5      $��Subota��6   62   �� 5      $��Nedjelja��6   64   5      �� 5   
   $��Bottom=�6   $��25��6+   $��
84,0,0,142q�67   $	��Od:��6K   $
��Left=�6Y   $��29��6t   �� 5   
   -   5         ��Name$ Button1��5      $��Uredu��6"   $��Center=�62   $��Center=�6V   $��	63,0,0,32q�6�      buttonStyle#���   6�   $��30��6�   $��0q�6�   $��Bottom=�6�   $
��Left=�6�   $	��102��6  �� 5   
   -   5        $ Button2��5      $��Otkaži��6"   $��	0,0,50,32q�6O   #���   6b   $��30��6�   $��0q�6�   $��Right=�6�   $	��108��6�   $��Bottom=�6�   �� 5   
   $��Bottom=�6   $��29��6+   $��	84,0,0,93q�67   $	��Do:��6J    J� RenderTransformOrigin$ 1.357,0.138&�6X   $
��Left=�6|   $��29��6�   �� 5   
   -   5         ��Name$ label��5      $��Podaci o terminu��6   $��
0,0,-2,235q�6:   $��Center=�6N   $��Bottom=�6r   $��19(�6�   $
��Bold"�6�   
���  ��$��38��6�   "  Xceed.Wpf.Toolkit.TimePicker  5   
   -   5        	  Name$	 TimePickerOd��5      $��
0,0,68,142q�6R   $��20��6f   $��Bottom=�6r   $��Right=�6�   $	��160��6�   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Value
 60   �� *CurrentDateTime+6�     5   
   -   5        $	 TimePickerDo��5      $��
0,0,68,102q�6R   $��20��6f   $��Bottom=�6r   $��Right=�6�   $	��160��6�   
 60   �� *CurrentDateTime+6�   5       5!      