   M S B A M L   `   `   `  ����G  CCisco_Script, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Cisco_Script.ViewModelCisco_Script.ViewModel  5   	   '    !Cisco_Script.ViewModel.Ajouter_NI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Cisco_Script.ViewModel     5   	     � Title$#  Ajouter un interface réseaux��5      $	��299��66   $	��367��6C   .��5	      � .+�5
   
   �� $��Nom :��6   $
��Left=�6    $	��Top=�6;   $��78��6S   $��	10,10,0,0q�6^   �� 5   
   -   5        $��
combo_name��5   %    �� SelectedIndex$ 0��6   $
��Left=�67   $	��Top=�6R   $	��140��6j   $��	93,10,0,0q�6v   $��26��6�   �� 5   
   -   5        $��	Confirmer��5      $
��Left=�6%   $	��Top=�6@   $��87��6X   $��
56,235,0,0q�6c   $��23��6w   6�   �� 5   
   -   5        $��Réinitialiser��5      $
��Left=�6)   $	��Top=�6D   $��87��6\   $��148,235,0,0q�6g   $��23��6|   6�   �� 5   
   -   5        $��Annuler��5      $
��Left=�6#   $	��Top=�6>   $��87��6V   $��240,235,0,0q�6a   $��23��6v   6�   �� 5   
   $��Numero :��6   $
��Left=�6#   $	��Top=�6>   $��78��6V   $��	10,41,0,0q�6a   �� 5   
   $��IPV4/IPV6 :��6   $
��Left=�6&   $	��Top=�6A   $��78��6Y   $��9,67,0,0q�6d   �� 5   
   $��	netmask :��6   $
��Left=�6$   $	��Top=�6?   $��78��6W   $��9,98,0,0q�6b   �� 5   
   -   5         ��Name$ textbox_number��5      $
��Left=�6*   $��26��6E   $��	93,41,0,0q�6Q    �� TextWrapping$
 Wrap=�6d   $	��0/0��6x   $	��Top=�6�   $	��249��6�   6�   �� 5   
   -   5        $ 
Textbox_ip��5      $
��Left=�6&   $��26��6A   $��	93,72,0,0q�6M   $
 Wrap=�6`   $��192.168.1.1��6t   $	��Top=�6�   $	��249��6�   6�   �� 5   
   -   5        $ Textbox_Netmask��5      $
��Left=�6+   $��26��6F   $��
92,103,0,0q�6R   $
 Wrap=�6f   $��255.255.255.0��6z   $	��Top=�6�   $	��250��6�   6�   �� 5   
   $��Description :��6   $
��Left=�6(   $	��Top=�6C   $��78��6[   $��
10,134,0,0q�6f   �� 5   
   -   5        $ TextboxDescription��5      $ �� HorizontalScrollBarVisibility$
 Auto=�6.   " �� VerticalScrollBarVisibility$
 Auto=�6S   $
��Left=�6v   $��55��6�   $��
93,134,0,0q�6�   $
 Wrap=�6�   $��Liens Vers Switch3��6�   $	��Top=�6�   $	��249��6�   �� 5   
   -	   5         ��Name$ active_interface��5   $    d� 	IsChecked$
 trueY�6   $��Interface active��6>   $
��Left=�6Y   $��
92,194,0,0q�6t   $	��Top=�6�   $	��107��6�   �� 5   
   -
   5        $ ipv6_check_box��5      $
��IPV6��6+   $
��Left=�6:   $��
92,214,0,0q�6U   $	��Top=�6i   $	��100��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   5      5      