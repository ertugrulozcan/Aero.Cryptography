   M S B A M L   `   `   `  ����Q  MAero.Cryptography.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Aero.Cryptography.TestAero.Cryptography.Test  5   	   '    !Aero.Cryptography.Test.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Aero.Cryptography.Test     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Aero.Cryptography Test��5	   	   $	��400��5
   	   $	��720��6   .��5      � .+�5   
   ��  �� Padding$ 10q�6   ._�5      �  � RowDefinitions 5      �� 5      $��* �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �  5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $��* �6(   �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5      � $��0��6    � ColumnDefinitions 5      �� 5       $��* �6/   �� 5!      $
��Auto �6/   .+�5$      �� -   5         ��Name$ InputTextBox��5$   "   $��Center=�68   $��0��6S   �� 5%      -   5         ��Name$ EncryptButton��5%   !   $��Encrypt��68    �� 	IsDefault . $	��100��6w   $��10,0,0,0q�6�   $��Center=�6�   $��1��6�   5&      � 5(      $��1��6   .+�5)      �� -   5         ��Name$ IsSignCheckBox��5)   #   $
��Sign��6;   $��0,7,0,0q�6J   5*      � 5,      $��2��6   .+�5-      � $��0,10,0,0q�6    5.      �� 5/   "   $��* �63   �� 50   "   $
��Auto �63   .+�53      �� -   5        $ OutputTextBox��53   &   	 �� TextWrapping$	 NoWrap=�6=   $��Consolas)�6S   $
��Auto=�6i   $��0��6�   � 55      $��5,0,0,0q�6#   $��1��64   .+�56   "   �� .+�57   &   �� �� 58   *   $�(��6.   �� 69   �6=   �� *Text.Length+
 �� ElementName$
 OutputTextBox�� �� Mode$ OneWay=��� 6�   $�)��6�   59   '   5:   #   5;      5<      5=      � 5?      $��3��6   $��0,10,0,0q�6(    5@      �� 5A      $
��Auto �6/   �� 5B      $��* �6/   �� 5C      $
��Auto �6/   .+�5F      �� -   5        $ DecryptButton��5F   !   $��Decrypt��68   $	��100��6f   $��Center=�6r   $��0��6�   �� 5G      -   5        $ DecryptionResultTextBox��5G   "   $��Center=�6C   $��1��6^   $��10,0,0,0q�6n   �� 5H      $��2��6$   $��5,0,0,0q�64   �� 5I      $�(��6"   �� 6-   �61   �� *Text.Length+$
 DecryptionResultTextBox��$ OneWay=��� 6�   $�)��6�   5J      5K      � 5M      $��4��6   .+�5N      �� -   5         ��Name$$ SignatureVerificationTextBlock��5N   $   $��0,5,0,0q�6L   5O      5P      �� 5R      ���	#77777777$��0,1,0,0q�61   $��1��6K   $
��0,10q�6X   � 5T      $��2��6   .+�5U      �  5V      �� 5W      $��* �6/   �� 5X      $
��Auto �6/   .+�5[      �� -   5        $ TestTextBox��5[   "   $��Center=�67   $��0��6R   �� 5\      -	   5        $ 
TestButton��5\   !   $
��Test��65   $	��100��6]   $��10,0,0,0q�6i   $��Center=�6{   $��1��6�   5]      5^      5_      5`      5a      5b      