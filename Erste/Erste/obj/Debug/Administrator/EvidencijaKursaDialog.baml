   M S B A M L   `   `   `  ����@  <Erste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Erste.AdministratorErste.Administrator  5   	   /    )Erste.Administrator.EvidencijaKursaDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -local!clr-namespace:Erste.Administrator  5   	     � Title$  Erste - podaci o kursu��5	   	    � Icon$! ../Resources/erste_logo.png��6(   $	��350��6K   $	��379��6X    � 
ResizeMode$ NoResize=�6e    � WindowStartupLocation$ CenterScreen=�6{   .��5
      � -   5         �Name$
 grid��5
       � RowDefinitions 5   
   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $	��Top=�6    $��
68,105,0,0q�68   $��26��6L   $
��Left=�6X   $��56��6s   .��6~   Jezik:6�   �� 5   
   -   5        $��comboBox_Jezik��5      $	��Top=�6)   $��
0,107,61,0q�6A    �� 
IsReadOnly .  �� 
IsEditable . 	 �� Text$	 Izaberite...��6y   
 �� DisplayMemberPath$
 Naziv��6�   $��22��6�   $��Right=�6�   $	��159��6�   �� 5   
   $��0��6   $	��Top=�6    $��
68,153,0,0q�68   $��29��6L   $
��Left=�6X   $��50��6s   .��6~   Nivo:6�   �� 5   
   -   5        $��textBox_Nivo��5      $	��Top=�6&   $��
0,155,61,0q�6>   $��22��6R   $	��159��6^   $��Right=�6j   �� 5   
   -   5         ��Name$ Button1��5      $��Uredu��6"   $��0��6H   $��
73,256,0,0q�6X      buttonStyle#���   6l   $��28��6�   $	��Top=�6�   $
��Left=�6�   $��95��6�   �� 5   
   -   5        $ Button2��5      $��Otkaži��6"   $��
0,256,78,0q�6O   #���   6c   $��28��6�   $	��Top=�6�   $��Right=�6�   $	��100��6�   �� 5   
   -   5         ��Name$ label��5      $��Podaci o kursu��6   $��32��68   $��107,7,120,0q�6D   $	��Top=�6Y   $��19(�6r   $
��Bold"�6�   
���  ��6�   5      5      