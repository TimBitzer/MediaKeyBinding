   M S B A M L   `   `   `  ����J  FMediaKeyBinding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MediaKeyBindingMediaKeyBinding  5   	        MediaKeyBinding.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MediaKeyBinding     5   	   -   5          �Name$  Window��5   	    � Title$ MKB - MediaKeyBinding��5   	   $	��280��6'   $	��265��64    � Icon$ AppIcon.ico��6�    � 
ResizeMode$ NoResize=�6�   .��5	      � .+�5
   
   �� -   5         ��Name$ TB_PlayPause��5
      $��23��6(   $��	0,65,10,0q�64    �� TextWrapping$
 Wrap=�6G   $	��Top=�6[   $��Right=�6s   $	��120��6�    �� 
IsReadOnly . $��F7��6�   �� 5   
   -   5         ��Name$ LBL_HeadLine��5      $!��Configure your key bindings��6&   $��
10,10,10,0q�6L   $	��Top=�6`   $��16(�6x   $
��Bold"�6�   �� 5   
   -   5        $ LBL_PlayPause��5      $��Play / Pause��6'   $��	37,62,0,0q�6>   $	��Top=�6Q   $
��Left=�6i   $��80��6�   �� 5   
   -   5        $ TB_Stop��5      $��23��6#   $��	0,96,10,0q�6/   $
 Wrap=�6B   $	��Top=�6V   $��Right=�6n   $	��120��6�   $��F9��6�    . 6�   �� 5   
   -   5        $ LBL_Stop��5      $
��Stop��6"   $��	37,93,0,0q�61   $	��Top=�6D   $
��Left=�6\   $��80��6w   �� 5   
   -   5        $ TB_Next��5      $��23��6#   $��
0,127,10,0q�6/   $
 Wrap=�6C   $	��Top=�6W   $��Right=�6o   $	��120��6�   $��F8��6�    . 6�   �� 5   
   -   5        $ LBL_Next��5      $
��Next��6"   $��
37,124,0,0q�61   $	��Top=�6E   $
��Left=�6]   $��80��6x   �� 5   
   -	   5        $ TB_Previous��5      $��23��6'   $��
0,158,10,0q�63   $
 Wrap=�6G   $	��Top=�6[   $��Right=�6s   $	��120��6�   $��F6��6�    . 6�   �� 5   
   -
   5        $ LBL_Previous��5      $��Previous��6&   $��
37,155,0,0q�69   $	��Top=�6M   $
��Left=�6e   $��80��6�   �� 5   
   -   5         ��Name$ BTN_Save��5      $��Save Configuration��6#   $��	0,0,10,20q�6@   $��Right=�6j   $	��120��6�   $��27��6�   $��Bottom=�6�   �� 5   
   -   5        	 ��Name$	 CHB_PlayPause��5      $�� ��6*   $
��Left=�65   $��	10,69,0,0q�6P   $	��Top=�6c   
 d� 	IsChecked$

 TrueY�6{   6�   �� 5   
   -   5        $	 CHB_Stop��5      $�� ��6%   $
��Left=�60   $��	10,99,0,0q�6K   $	��Top=�6^   $

 TrueY�6v   6�   �� 5   
   -   5        $	 CHB_Next��5      $�� ��6%   $
��Left=�60   $��
10,131,0,0q�6K   $	��Top=�6_   $

 TrueY�6w   6�   �� 5   
   -   5        $	 CHB_Previous��5      $�� ��6)   $
��Left=�64   $��
10,162,0,0q�6O   $	��Top=�6c   $

 TrueY�6{   6�   �� 5   
   -   5        $ 
LBL_Status��5      $�� ��6$   $
��Left=�6/   $��	10,36,0,0q�6J   $	��Top=�6]   $	��239��6u   $��26��6�   $��10(�6�   
���  ��$y�Hidden=�6�   5      5      