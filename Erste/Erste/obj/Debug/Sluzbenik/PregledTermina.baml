   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Erste.SluzbenikErste.Sluzbenik  5   	   $    Erste.Sluzbenik.PregledTermina   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Erste.Sluzbenik     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Erste - pregled termina��5	   	    � Icon$! ../Resources/erste_logo.png��6)   $	��376��6L   $��524.333��6Y    � WindowStartupLocation$ CenterScreen=�6i   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��150* �6   �� 5      $	��50* �6   �� 5      $
��250* �6   �� 5      $
��100* �6   �� 5      $
��310* �6   �� 5      $
��233* �6   �� 5      $
��310* �6    � RowDefinitions 5   
   �� 5      $	��70* �6   6+   �� 5      $	��50* �6   6+   �� 5      $	��70* �6   6+   �� 5      $	��50* �6   6+   �� 5      $	��70* �6   6+   �� 5      $	��50* �6   6+   �� 5      $	��70* �6   6+   �� 5      $	��50* �6   6+   �� 5      $	��70* �6   6+   �� 5      $	��50* �6   6+   �� 5      $	��70* �6   6+   �� 5       $	��50* �6   6+   �� 5!      $	��50* �6   6+   .+�5$   
   �� $��0��6(   $��Center=�68   $��Stretch=�6S   $��60��6r   $��7��6~   $��1��6�   ��6   Z� ._�5%      �� -   5         ��Name$ naslov��5%       �� TextAlignment$ Center=�6(   $��Center=�6?   $��Podaci o terminu��6Z   $��19(�6r   $
��Bold"�6�   
���  ��6�   5&      �� 5(   
   $��1��6(   $��2��68   $��Center=�6E   $��Stretch=�6`   $��60��6   $��2��6�   $��1��6�   ��6   Z� ._�5)      �� -   5        $ label��5)      $
��Dan:��6'   $ Center=�63   $��Center=�6J   5*      �� 5+   
   -   5         ��Name$ DanCombo��5+      $��4��6%   $��2��65    � ToolTip$/ )Unesite dan kad će se održavati termin.��6B   $��2��6t   $��1��6�   $��0,5,25,4q�6�   .@�5,      �� $��Ponedjeljak��6   �� 5-      $��Utorak��6   �� 5.      $��Srijeda��6   �� 5/      $��	Četvrtak��6   �� 50      $��Petak��6   �� 51      $��Subota��6   �� 52      $��Nedjelja��6   53      �� 55   
   $��1��6(   $��4��68   $��Center=�6E   $��Stretch=�6`   $��60��6   $��2��6�   $��1��6�   ��6   Z� ._�56      �� -   5        $ od��56      $	��Od:��6$   $ Center=�6/   $��Center=�6F   57      "  Xceed.Wpf.Toolkit.TimePicker  58   
   -   5        	  Name$	 TimePickerOd��58      $��4��60   $��4��6=   $��Center=�6M   $��2��6q   $��0,1,26,7q�6�   �� 5:   
   $��1��6(   $��6��68   $��Center=�6E   $��Stretch=�6`   $��60��6   $��2��6�   $��1��6�   ��6   Z� ._�5;      �� -   5        $ dp��5;      $	��Do:��6$   $ Center=�6/   $��Center=�6F   5<        5=   
   -   5        $	 TimePickerDo��5=      $��6��60   $��4��6=   $��2��6M   $��0,0,26,8q�6a   �� 5@   
   $��1��6(   $��8��68   $��Center=�6E   $��Stretch=�6`   $��60��6   $��2��6�   $��1��6�   ��6   Z� ._�5A      �� -   5        $ grupa��5A      $��Grupa:��6'   $ Center=�65   $��Center=�6L   5B      �� 5C   
   -	   5        $ 
GrupaCombo��5C      $��4��6'   $��8��67   $��2��6D   $��0,0,25,8q�6X   �� 5E   
   $��3,0,0,0q�6   $��8��6&   $��6��63   
 �� Orientation$
 
Horizontal=�6C   .+�5F      �� -
   5        $'��!../Resources/PregledKandidata.png��5F      $��4,0,0,2q�6?   6P   �� 5G      -   5        $��../Resources/add_icon.png��5G      $��0,0,0,2q�6Z   5H      �� 5J   
   -   5         ��Name$ Button1��5J      $��10��6"   $��2��60   $��U redu��6D   $��Center=�6U   $��Center=�6y      buttonStyle#���   6�   $��30��6�   $��0q�6�   $��Bottom=�6�   $��1,0,22.667,1.333q�6
  $��2��6$  �� 5K   
   -   5        $ Button2��5K      $��3��6"   $��9��62   $��2��6?   $��Obriši��6S   #���   6�   $��31��6�   $��0q�6�   $��Right=�6�   $��98��6�   $��Bottom=�6�   $��2��6   �� 5L   
   -   5        $ Button3��5L      $��5��6"   $��10��62   $��2��6@   $��Otkaži��6T   #���   6�   $��31��6�   $��0q�6�   $��Right=�6�   $��98��6�   $��Bottom=�6�   $��0,0,62.333,0.333q�6  5M      5N      